using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Music;
using NAudio.Wave;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Music
{
    public partial class USCRankMusic : UserControl
    {
        private HttpClient httpClient;
        private WaveOutEvent _waveOut;
        private AudioFileReader _audioFileReader;
        private string _currentTrackPath;
        private string _currentTrackImage;
        private string _currentTrackName;
        private string _currentTrackArtist;
        private string _currentTrackDuration;
        private bool _isPlaying = false;
        private bool _isPaused = false;
        private int trackCounter = 0;
        private List<dynamic> _topTracks;
        private int _currentTrackIndex = -1;
        private bool isRepeatOn = false;
        private bool isShuffleOn = false;
        public TimeSpan CurrentTime => _audioFileReader?.CurrentTime ?? TimeSpan.Zero;
        public TimeSpan TotalTime => _audioFileReader?.TotalTime ?? TimeSpan.Zero;
        public bool IsPlaying => _isPlaying;
        public Form1 ParentForm { get; set; }

        public USCRankMusic()
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
            InitializePanel();
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

        private async void InitializePanel()
        {
            try
            {
                // Gọi API để lấy top 5 bài hát có lượt nghe nhiều nhất
                var responseTracks = await httpClient.GetAsync("http://localhost:9999/v1/Top5Tracks");
                if (responseTracks.IsSuccessStatusCode)
                {
                    var tracksContent = await responseTracks.Content.ReadAsStringAsync();
                    _topTracks = JsonConvert.DeserializeObject<List<dynamic>>(tracksContent);

                    // Thêm card cho mỗi bài hát vào panel
                    foreach (var track in _topTracks)
                    {
                        string imageURL = track.IMAGE.String;
                        string NameSong = track.NAME;
                        string NameArtist = track.ARTIST_NAME;
                        string NameAlbum = track.ALBUM_NAME;
                        string Duration = track.DURATION.String;
                        string Fs_path = track.FS_PATH.String;
                        trackCounter++;
                        string stt = trackCounter.ToString();

                        CardSong cardSong = new CardSong(stt, imageURL, NameSong, NameArtist, NameAlbum, Duration, Fs_path, this);
                        flowLayoutPanel1.Controls.Add(cardSong);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to retrieve top tracks data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve top tracks data: " + ex.Message);
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
                    _currentTrackIndex = _topTracks.FindIndex(t => t.FS_PATH.String == trackUrl);

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

                    uscPlay.StartTimerRank();

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
            foreach (CardSong card in flowLayoutPanel1.Controls)
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

        public void PauseMusicRank()
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
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                Random random = new Random();
                int randomIndex;
                do
                {
                    randomIndex = random.Next(0, flowLayoutPanel1.Controls.Count);
                }
                while (flowLayoutPanel1.Controls[randomIndex] is CardSong cardSong && cardSong.fs_path == _currentTrackPath);

                if (flowLayoutPanel1.Controls[randomIndex] is CardSong selectedCardSong)
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
            if (_currentTrackIndex < _topTracks.Count - 1)
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
            var track = _topTracks[_currentTrackIndex];
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