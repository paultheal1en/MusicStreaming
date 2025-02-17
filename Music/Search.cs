using CloudinaryDotNet.Actions;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Music
{
    public partial class Search : UserControl
    {
        private HttpClient httpClient;
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
        public Search(string keyword)
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
            InitializeFlowLayoutPanel(keyword);
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
        private async void InitializeFlowLayoutPanel(string keyword)
        {
            try
            {
                var resetpasswordRequest = new
                {
                    keyword
                };
                string json = JsonConvert.SerializeObject(resetpasswordRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Gọi API để lấy top 10 bài hát cùng thông tin về nghệ sĩ và album
                var responseTracks = await httpClient.PostAsync("http://localhost:9999/v1/Search/Tracks",content);
                if (responseTracks.IsSuccessStatusCode)
                {
                    var tracksContent = await responseTracks.Content.ReadAsStringAsync();
                    _tracks = JsonConvert.DeserializeObject<List<dynamic>>(tracksContent);
                    if (_tracks != null)
                    {
                        // Thêm card cho mỗi bài hát vào FlowLayoutPanelTrack
                        foreach (var track in _tracks)
                        {
                            string imageURL = track.IMAGE.String;
                            string NameTrack = track.NAME;
                            string NameArtist = track.ARTIST_NAME;
                            string NameAlbum = track.ALBUM_NAME;
                            string Duration = track.DURATION.String;
                            trackCounter++;
                            string stt = trackCounter.ToString();
                            string fspath = track.FS_PATH.String;
                            CardSong card = new CardSong(stt, imageURL, NameTrack, NameArtist, NameAlbum, Duration, fspath, this);
                            flowLayoutPanelTrack.Controls.Add(card);
                        }
                    }
                    else
                    {
                        lbTrack.Text = null;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to retrieve top tracks data");
                }

                // Gọi API để lấy top 10 album cùng thông tin nghệ sĩ
                string jsonAlbums = JsonConvert.SerializeObject(resetpasswordRequest);
                var contentAlbums = new StringContent(json, Encoding.UTF8, "application/json");
                var responseAlbums = await httpClient.PostAsync("http://localhost:9999/v1/Search/Albums", contentAlbums);
                if (responseAlbums.IsSuccessStatusCode)
                {
                    var albumsContent = await responseAlbums.Content.ReadAsStringAsync();
                    var albums = JsonConvert.DeserializeObject<List<dynamic>>(albumsContent);
                    if (albums != null)
                    {
                        // Thêm card cho mỗi album vào FlowLayoutPanelAlbum
                        foreach (var album in albums)
                        {
                            string imageURL = album.IMAGE.String;
                            string NameArtist = album.ARTIST_NAME;
                            string NameAlbum = album.NAME;
                            album_item card = new album_item(imageURL, NameArtist, NameAlbum);
                            flowLayoutPanelAlbum.Controls.Add(card);
                        }
                    }
                    else
                    {
                        lbAlbum.Text = null;
                        panel3.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to retrieve top albums data");
                }

                // Gọi API để lấy top 10 nghệ sĩ
                string jsonArtists = JsonConvert.SerializeObject(resetpasswordRequest);
                var contentArtists = new StringContent(json, Encoding.UTF8, "application/json");
                var responseArtists = await httpClient.PostAsync("http://localhost:9999/v1/Search/Artists", contentArtists);
                if (responseArtists.IsSuccessStatusCode)
                {
                    var artistsContent = await responseArtists.Content.ReadAsStringAsync();
                    var artists = JsonConvert.DeserializeObject<List<dynamic>>(artistsContent);
                    if (artists != null)
                    {
                        // Thêm card cho mỗi nghệ sĩ vào FlowLayoutPanelArtist
                        foreach (var artist in artists)
                        {
                            string imageURL = artist.IMAGE.String;
                            string NameArtist = artist.NAME;
                            Artist card = new Artist(imageURL, NameArtist);
                            flowLayoutPanelArtist.Controls.Add(card);
                        }
                    }
                    else
                    {
                        lbArtist.Text = null;
                        panel4.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to retrieve top artists data");
                }
                if (lbAlbum.Text == null &&lbArtist.Text == null &&lbTrack.Text==null)
                {
                    lbAlbum.Text = "Not Found!!!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
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
                    uscPlay.StartTimerSearch();
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
            if (ParentForm == null)
            {
                throw new InvalidOperationException("ParentForm is null");
            }

            if (ParentForm.Panel3 == null)
            {
                throw new InvalidOperationException("ParentForm.Panel3 is null");
            }

            if (ParentForm.Panel3.Controls == null)
            {
                throw new InvalidOperationException("ParentForm.Panel3.Controls is null");
            }

            foreach (Control control in ParentForm.Panel3.Controls)
            {
                if (control is USCPlay uscPlay)
                {
                    if (uscPlay.fs_path == null)
                    {
                        throw new InvalidOperationException("uscPlay.fs_path is null");
                    }

                    uscPlay.SetPlayPauseButtonState(uscPlay.fs_path == trackUrl && isPlaying);
                    uscPlay.UpdateRepeatShuffleState(isRepeatOn, isShuffleOn);
                }
            }
        }

        public void PauseMusicSearch()
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
    }
}
