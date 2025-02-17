using Bunifu.UI.WinForms;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Music
{
    public partial class List : UserControl
    {
        private readonly HttpClient httpClient;
        private int trackCounter = 0;
        private WaveOutEvent _waveOut;
        private AudioFileReader _audioFileReader;
        private string _currentTrackPath;
        private string _currentTrackImage;
        private string _currentTrackName;
        private string _currentTrackArtist;
        private string _currentTrackDuration;
        private bool _isPlaying = false;
        private bool _isPaused = false;
        private List<dynamic> _tracks;
        private int _currentTrackIndex = -1;
        private bool isRepeatOn = false;
        private bool isShuffleOn = false;
        public TimeSpan CurrentTime => _audioFileReader?.CurrentTime ?? TimeSpan.Zero;
        public TimeSpan TotalTime => _audioFileReader?.TotalTime ?? TimeSpan.Zero;
        public bool IsPlaying => _isPlaying;
        public Form1 ParentForm { get; set; }

        public List()
        {
            InitializeComponent();
            _waveOut = new WaveOutEvent();
            _waveOut.PlaybackStopped += (sender, e) =>
            {
                // Kiểm tra nếu đã phát hết nhạc
                if (_waveOut.PlaybackState == PlaybackState.Stopped)
                {
                    UpdateCardSongsPlayPauseState(_currentTrackPath, false);
                    UpdateUSCPlaysPlayPauseState(_currentTrackPath, false);
                    if (isRepeatOn)
                    {
                        PlayMusic(_currentTrackPath, _currentTrackImage, _currentTrackName, _currentTrackArtist, _currentTrackDuration);
                    }
                    else if (isShuffleOn)
                    {
                        PlayRandomMusic();
                    }
                    else
                    {
                        StopMusic();
                    }
                }
            };
            httpClient = new HttpClient();
            InitializeFlowLayoutPanel(SharedData.PlaylistID);

        }
        private void AddUSCPlayEventHandlers(USCPlay uscPlay)
        {
            uscPlay.RepeatToggled += USCPlay_RepeatToggled;
            uscPlay.ShuffleToggled += USCPlay_ShuffleToggled;
        }
        public void SetVolume(float volume)
        {
            if (_waveOut != null)
            {
                _waveOut.Volume = volume;
            }
        }
        public void Seek(TimeSpan newPosition)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = newPosition;
            }
        }
        private async Task InitializeFlowLayoutPanel(string playlistID)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        var responseTracks = await client.GetAsync($"http://localhost:9999/v1/playlist/tracks?playlistID={playlistID}");
                        if (responseTracks.IsSuccessStatusCode)
                        {
                            // Gọi API để lấy bài hát theo playlistID tương ứng
                            var tracksContent = await responseTracks.Content.ReadAsStringAsync();
                            _tracks = JsonConvert.DeserializeObject<List<dynamic>>(tracksContent);

                            if (_tracks == null || !_tracks.Any())
                            {
                                ShowInformation("No tracks found in the playlist.");
                                return;
                            }

                            foreach (var track in _tracks)
                            {
                                string imageURL = track.IMAGE.String;
                                string NameTrack = track.NAME;
                                string NameArtist = track.ARTIST_NAME;
                                string NameAlbum = track.ALBUM_NAME;
                                string Duration = track.DURATION.String;
                                trackCounter++;
                                string stt = trackCounter.ToString();
                                string fs_path = track.FS_PATH.String;
                                CardSong card = new CardSong(stt, imageURL, NameTrack, NameArtist, NameAlbum, Duration,fs_path, this);
                                AddRemoveTrackButton(card, playlistID, track.ID.ToString());
                                flowLayoutPanelTrack.Controls.Add(card);
                            }
                        }
                        else
                        {
                            ShowInformation("Failed to retrieve tracks data");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowInformation($"An error occurred: {ex.Message}");
            }
        }
        public async void PlayMusic(string trackUrl, string trackImage, string trackName, string trackArtist, string trackDuration)
        {
            try
            {
                if (ParentForm != null)
                {
                    ParentForm.SetPlayingControl(this);
                }
                if (_isPaused && _currentTrackPath == trackUrl)
                {
                    _waveOut.Play();
                    _isPlaying = true;
                    _isPaused = false;

                    UpdateCardSongsPlayPauseState(trackUrl, true);
                    UpdateUSCPlaysPlayPauseState(trackUrl, true);
                }
                else
                {
                    StopMusic(); // Dừng bất kỳ bài hát nào đang phát

                    _audioFileReader = new AudioFileReader(trackUrl);
                    _waveOut.Init(_audioFileReader);

                    _waveOut.Play();
                    _isPlaying = true;
                    _isPaused = false;
                    _currentTrackPath = trackUrl;
                    _currentTrackImage = trackImage;
                    _currentTrackName = trackName;
                    _currentTrackArtist = trackArtist;
                    _currentTrackDuration = trackDuration;
                    _currentTrackIndex = _tracks.FindIndex(t => t.FS_PATH.String == trackUrl);

                    UpdateCardSongsPlayPauseState(trackUrl, true);
                    UpdateUSCPlaysPlayPauseState(trackUrl, true);
                    bunifuPictureBox1.ImageLocation = trackImage;
                    label1.Text = trackName;
                    label2.Text = trackArtist;
                    // Thêm USCPlay vào panel
                    USCPlay uscPlay = new USCPlay(_currentTrackImage, _currentTrackName, _currentTrackArtist, _currentTrackDuration, _currentTrackPath, this);

                    AddUSCPlayEventHandlers(uscPlay);
                    if (ParentForm != null)
                    {
                        ParentForm.AddUSCPlay(uscPlay);
                    }
                    //flowLayoutPanel2.Controls.Clear();
                    //flowLayoutPanel2.Controls.Add(uscPlay);
                    uscPlay.UpdateRepeatShuffleState(isRepeatOn, isShuffleOn);
                    uscPlay.StartTimerList();
                    var endpoint = $"http://localhost:9999/v1/Track/increment/{Uri.EscapeDataString(trackName)}";
                    var content = new StringContent("", Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(endpoint, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Failed to increment listen count for track: " + trackName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi phát nhạc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateCardSongsPlayPauseState(string trackUrl, bool isPlaying)
        {
            foreach (CardSong card in flowLayoutPanelTrack.Controls)
            {
                card.SetPlayPauseButtonState(card.fs_path == trackUrl && isPlaying);
            }
        }

        private void UpdateUSCPlaysPlayPauseState(string trackUrl, bool isPlaying)
        {
            foreach (Control control in ParentForm.Panel3.Controls)
            {
                if (control is USCPlay uscPlay)
                {
                    uscPlay.SetPlayPauseButtonState(uscPlay.fs_path == trackUrl && isPlaying);
                    uscPlay.UpdateRepeatShuffleState(isRepeatOn, isShuffleOn);
                }
            }
        }
        public void PauseMusicList()
        {
            if (_waveOut != null && _waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Pause();
                _isPlaying = false;
                _isPaused = true;

                UpdateCardSongsPlayPauseState(_currentTrackPath, false);
                UpdateUSCPlaysPlayPauseState(_currentTrackPath, false);
            }
        }
        public void StopMusic()
        {
            if (_waveOut != null && (_waveOut.PlaybackState == PlaybackState.Playing || _waveOut.PlaybackState == PlaybackState.Paused))
            {
                _waveOut.Stop();
                _isPlaying = false;
                _isPaused = false;
            }

            //ParentForm?.RemoveUSCPlay();
        }
        private void PlayRandomMusic()
        {
            if (flowLayoutPanelTrack.Controls.Count > 0)
            {
                Random random = new Random();
                int randomIndex;
                do
                {
                    randomIndex = random.Next(0, flowLayoutPanelTrack.Controls.Count);
                }
                while (flowLayoutPanelTrack.Controls[randomIndex] is CardSong cardSong && cardSong.fs_path == _currentTrackPath);

                if (flowLayoutPanelTrack.Controls[randomIndex] is CardSong selectedCardSong)
                {
                    string trackUrl = selectedCardSong.fs_path;
                    string trackImage = selectedCardSong.TrackImage;
                    string trackName = selectedCardSong.namesong;
                    string trackArtist = selectedCardSong.nameartist;
                    string trackDuration = selectedCardSong.duration;

                    PlayMusic(trackUrl, trackImage, trackName, trackArtist, trackDuration);
                }
            }

        }
        public void USCPlay_RepeatToggled(object sender, EventArgs e)
        {
            var uscPlay = sender as USCPlay;
            if (uscPlay != null)
            {
                isRepeatOn = !isRepeatOn;

                if (isRepeatOn)
                {
                    isShuffleOn = false;
                }

                UpdateRepeatShuffleState(isRepeatOn, isShuffleOn);
            }
        }
        public void USCPlay_ShuffleToggled(object sender, EventArgs e)
        {
            var uscPlay = sender as USCPlay;
            if (uscPlay != null)
            {
                isShuffleOn = !isShuffleOn;

                if (isShuffleOn)
                {
                    isRepeatOn = false;
                }

                UpdateRepeatShuffleState(isRepeatOn, isShuffleOn);
            }
        }
        public void UpdateRepeatShuffleState(bool repeatOn, bool shuffleOn)
        {
            isRepeatOn = repeatOn;
            isShuffleOn = shuffleOn;

            foreach (Control control in ParentForm.Panel3.Controls)
            {
                if (control is USCPlay uscPlay)
                {
                    uscPlay.UpdateRepeatShuffleState(isRepeatOn, isShuffleOn);
                }
            }
        }
        public void SkipNext()
        {
            if (_currentTrackIndex < _tracks.Count - 1)
            {
                _currentTrackIndex++;
                PlayCurrentTrack();
            }
        }
        public void SkipPrevious()
        {
            if (_currentTrackIndex > 0)
            {
                _currentTrackIndex--;
                PlayCurrentTrack();
            }
        }
        private void PlayCurrentTrack()
        {
            var track = _tracks[_currentTrackIndex];
            string trackPath = track.FS_PATH.String;
            string trackName = track.NAME;
            string trackArtist = track.ARTIST_NAME;
            string trackDuration = track.DURATION.String;
            string imageUrl = track.IMAGE.String;

            string trackImage = track.IMAGE.String;

            PlayMusic(trackPath, trackImage, trackName, trackArtist, trackDuration);
        }
        private void AddRemoveTrackButton(CardSong card, string playlistID, string trackID)
        {
            Button removeButton = new Button
            {
                Text = "✖", // Dấu X đỏ
                AutoSize = true,
                BackColor = Color.Black,
                ForeColor = Color.Green,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 12, FontStyle.Bold) // Tùy chỉnh phông chữ và kích thước
            };

            // Đặt vị trí của nút thấp hơn một chút
            int cardWidth = card.Width;
            int paddingRight = 75; // Khoảng cách từ phải
            int paddingTop = -8; // Khoảng cách từ trên (tăng giá trị này để di chuyển nút xuống dưới)
            int buttonWidth = 10;
            int buttonHeight = 3;

            removeButton.Location = new Point(cardWidth - buttonWidth - paddingRight, paddingTop);

            removeButton.Click += async (sender, e) =>
            {
                await RemoveTrackFromPlaylist(playlistID, trackID);
            };

            card.Controls.Add(removeButton);
            removeButton.BringToFront(); // Đưa nút lên phía trước
        }

        private void ShowInformation(string message)
        {
            // Thay thế MessageBox.Show bằng một thông báo thông tin hoặc cách hiển thị khác.
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async Task RemoveTrackFromPlaylist(string playlistID, string trackID)
        {
            CardControl.isOpened = true;
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    var cookieContainer = new CookieContainer();
                    handler.CookieContainer = cookieContainer;

                    using (var client = new HttpClient(handler))
                    {
                        // Lấy token từ manageToken.AccessToken
                        string token = manageToken.AccessToken;

                        // Tạo cookie từ token và thêm vào cookieContainer
                        var cookie = new Cookie("token", token, "/", "localhost");
                        cookieContainer.Add(cookie);

                        // Tạo đối tượng request để xóa track khỏi playlist
                        var requestData = new
                        {
                            PlaylistID = playlistID,
                            TrackID = trackID
                        };

                        // Chuyển đổi requestData sang JSON
                        var jsonContent = JsonConvert.SerializeObject(requestData);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        // Gửi yêu cầu DELETE đến endpoint /playlist/removetrack
                        var response = await client.PostAsync("http://localhost:9999/v1/playlist/removetrack", content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Track removed from playlist successfully");
                            // Cập nhật giao diện sau khi xóa track
                            await InitializeFlowLayoutPanel(playlistID);
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove track from playlist");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}");
            }
        }

    }
}