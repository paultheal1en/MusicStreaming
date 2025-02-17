using NAudio.Wave;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Music
{
    public partial class USCPlay : UserControl
    {
        public static class CurrentTrack
        {
            public static string NameSong { get; set; }
            public static string TrackImage { get; set; }
        }

        private USCRankMusic _rankMusicControl;
        private List _listControl;
        private Homepage _homepageControl;
        private Search _searchControl;
        private USCSinger _singerControl;
        private USCAlbum _albumControl;
        private string _trackUrl;
        public event EventHandler RepeatToggled;
        public event EventHandler ShuffleToggled;
        private bool isRepeatOn = false;
        private bool isShuffleOn = false;
        private Timer _timerRank;
        private Timer _timerList;
        private Timer _timerSearch;
        private Timer _timerSinger;
        private Timer _timerHomepage;
        private Timer _timerAlbum;
        private bool _isDragging = false;

        public USCPlay()
        {
            InitializeComponent();
            InitializeTimerRank();
            InitializeTimerList();
            InitializeTimerSinger();
            InitializeTimerAlbum();
            InitializeTimerHomepage();
            InitializeTimerSearch();
        }

        private void AddRemoveButton()
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
            int cardWidth = this.Width;
            int paddingRight = 75; // Khoảng cách từ phải
            int paddingTop = 10; // Khoảng cách từ trên (tăng giá trị này để di chuyển nút xuống dưới)
            int buttonWidth = 30;
            int buttonHeight = 30;

            removeButton.Size = new Size(buttonWidth, buttonHeight);
            removeButton.Location = new Point(cardWidth - buttonWidth - paddingRight, paddingTop);
            removeButton.Click += RemoveButton_Click;

            this.Controls.Add(removeButton);
            removeButton.BringToFront(); // Đưa nút lên phía trước
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem Form cha có phải là Form1 không
            if (this.ParentForm is Form1 parentForm)
            {
                parentForm.RemoveUSCPlay();
            }
        }

public USCPlay(string artistimage, string namesong, string nameartist, string duration, string fs_path, USCRankMusic rankMusicControl)
        {
            InitializeComponent();
            _rankMusicControl = rankMusicControl;
            _trackUrl = fs_path;

            picImage.ImageLocation = artistimage;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lTime2.Text = duration;

            HSlider1.Minimum = 0;
            HSlider1.Maximum = 100;
            HSlider1.Value = 0;

            HSlider2.Minimum = 0;
            HSlider2.Maximum = 100;
            HSlider2.Value = 50;


            bPause.Click += (sender, e) => PauseMusicRank();
            bPlay.Click += (sender, e) => PlayMusicRank();
            bRepeat.Click += (sender, e) => ToggleRepeatRank();
            bShuffle.Click += (sender, e) => ToggleShuffleRank();
            bSkipNext.Click += (sender, e) => SkipNextRank();
            bSkipPrevious.Click += (sender, e) => SkipPreviousRank();

            HSlider2.ValueChanged += hslider2Rank_ValueChanged;
            HSlider1.MouseDown += HSlider1Rank_MouseDown;
            HSlider1.MouseUp += HSlider1Rank_MouseUp;
            HSlider1.ValueChanged += HSlider1Rank_ValueChanged;

            InitializeTimerRank();
            AddRemoveButton();
        }
        public USCPlay(string artistimage, string namesong, string nameartist, string duration, string fs_path, List listControl)
        {
            InitializeComponent();
            _listControl = listControl;
            _trackUrl = fs_path;

            picImage.ImageLocation = artistimage;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lTime2.Text = duration;

            HSlider1.Minimum = 0;
            HSlider1.Maximum = 100;
            HSlider1.Value = 0;

            HSlider2.Minimum = 0;
            HSlider2.Maximum = 100;
            HSlider2.Value = 50;


            bPause.Click += (sender, e) => PauseMusicList();
            bPlay.Click += (sender, e) => PlayMusicList();
            bRepeat.Click += (sender, e) => ToggleRepeatList();
            bShuffle.Click += (sender, e) => ToggleShuffleList();
            bSkipNext.Click += (sender, e) => SkipNextList();
            bSkipPrevious.Click += (sender, e) => SkipPreviousList();

            HSlider2.ValueChanged += hslider2List_ValueChanged;
            HSlider1.MouseDown += HSlider1List_MouseDown;
            HSlider1.MouseUp += HSlider1List_MouseUp;
            HSlider1.ValueChanged += HSlider1List_ValueChanged;

            InitializeTimerList();
            AddRemoveButton();
        }
        public USCPlay(string artistimage, string namesong, string nameartist, string duration, string fs_path, Homepage homepageControl)
        {
            InitializeComponent();
            _homepageControl = homepageControl;
            _trackUrl = fs_path;

            picImage.ImageLocation = artistimage;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lTime2.Text = duration;

            HSlider1.Minimum = 0;
            HSlider1.Maximum = 100;
            HSlider1.Value = 0;

            HSlider2.Minimum = 0;
            HSlider2.Maximum = 100;
            HSlider2.Value = 50;


            bPause.Click += (sender, e) => PauseMusicHomepage();
            bPlay.Click += (sender, e) => PlayMusicHomepage();
            bRepeat.Click += (sender, e) => ToggleRepeatHomepage();
            bShuffle.Click += (sender, e) => ToggleShuffleHomepage();
            bSkipNext.Click += (sender, e) => SkipNextHomepage();
            bSkipPrevious.Click += (sender, e) => SkipPreviousHomepage();

            HSlider2.ValueChanged += hslider2Homepage_ValueChanged;
            HSlider1.MouseDown += HSlider1Homepage_MouseDown;
            HSlider1.MouseUp += HSlider1Homepage_MouseUp;
            HSlider1.ValueChanged += HSlider1Homepage_ValueChanged;

            InitializeTimerHomepage();
            AddRemoveButton();

        }
        public USCPlay(string artistimage, string namesong, string nameartist, string duration, string fs_path, Search searchControl)
        {
            InitializeComponent();

            _searchControl = searchControl;
            _trackUrl = fs_path;

            picImage.ImageLocation = artistimage;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lTime2.Text = duration;

            HSlider1.Minimum = 0;
            HSlider1.Maximum = 100;
            HSlider1.Value = 0;

            HSlider2.Minimum = 0;
            HSlider2.Maximum = 100;
            HSlider2.Value = 50;

            bPause.Click += (sender, e) => PauseMusicSearch();
            bPlay.Click += (sender, e) => PlayMusicSearch();
            bRepeat.Click += (sender, e) => ToggleRepeatSearch();
            bShuffle.Click += (sender, e) => ToggleShuffleSearch();
            bSkipNext.Click += (sender, e) => SkipNextSearch();
            bSkipPrevious.Click += (sender, e) => SkipPreviousSearch();

            HSlider2.ValueChanged += hslider2Search_ValueChanged;
            HSlider1.MouseDown += HSlider1Search_MouseDown;
            HSlider1.MouseUp += HSlider1Search_MouseUp;
            HSlider1.ValueChanged += HSlider1Search_ValueChanged;

            InitializeTimerSearch();
            AddRemoveButton();
        }
        public USCPlay(string artistimage, string namesong, string nameartist, string duration, string fs_path, USCSinger singerControl)
        {
            InitializeComponent();

            _singerControl = singerControl;
            _trackUrl = fs_path;

            picImage.ImageLocation = artistimage;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lTime2.Text = duration;

            HSlider1.Minimum = 0;
            HSlider1.Maximum = 100;
            HSlider1.Value = 0;

            HSlider2.Minimum = 0;
            HSlider2.Maximum = 100;
            HSlider2.Value = 50;

            bPause.Click += (sender, e) => PauseMusicSinger();
            bPlay.Click += (sender, e) => PlayMusicSinger();
            bRepeat.Click += (sender, e) => ToggleRepeatSinger();
            bShuffle.Click += (sender, e) => ToggleShuffleSinger();
            bSkipNext.Click += (sender, e) => SkipNextSinger();
            bSkipPrevious.Click += (sender, e) => SkipPreviousSinger();

            HSlider2.ValueChanged += hslider2Singer_ValueChanged;
            HSlider1.MouseDown += HSlider1Singer_MouseDown;
            HSlider1.MouseUp += HSlider1Singer_MouseUp;
            HSlider1.ValueChanged += HSlider1Singer_ValueChanged;

            InitializeTimerSinger();
            AddRemoveButton();

        }
        public USCPlay(string artistimage, string namesong, string nameartist, string duration, string fs_path, USCAlbum AlbumControl)
        {
            InitializeComponent();

            _albumControl = AlbumControl;
            _trackUrl = fs_path;

            picImage.ImageLocation = artistimage;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lTime2.Text = duration;

            HSlider1.Minimum = 0;
            HSlider1.Maximum = 100;
            HSlider1.Value = 0;

            HSlider2.Minimum = 0;
            HSlider2.Maximum = 100;
            HSlider2.Value = 50;

            bPause.Click += (sender, e) => PauseMusicAlbum();
            bPlay.Click += (sender, e) => PlayMusicAlbum();
            bRepeat.Click += (sender, e) => ToggleRepeatAlbum();
            bShuffle.Click += (sender, e) => ToggleShuffleAlbum();
            bSkipNext.Click += (sender, e) => SkipNextAlbum();
            bSkipPrevious.Click += (sender, e) => SkipPreviousAlbum();

            HSlider2.ValueChanged += hslider2Album_ValueChanged;
            HSlider1.MouseDown += HSlider1Album_MouseDown;
            HSlider1.MouseUp += HSlider1Album_MouseUp;
            HSlider1.ValueChanged += HSlider1Album_ValueChanged;

            InitializeTimerAlbum();
            AddRemoveButton();
        }

        public void StartTimerRank()
        {
            _timerRank.Start();
        }
        public void StartTimerList()
        {
            _timerList.Start();
        }
        public void StartTimerAlbum()
        {
            _timerAlbum.Start();
        }
        public void StartTimerSinger()
        {
            _timerSinger.Start();
        }
        public void StartTimerHomepage()
        {
            _timerHomepage.Start();
        }
        public void StartTimerSearch()
        {
            _timerSearch.Start();
        }
        private void InitializeTimerRank()
        {
            _timerRank = new Timer();
            _timerRank.Interval = 1000;
            _timerRank.Tick += TimerRank_Tick;
        }
        private void InitializeTimerList() {
            _timerList = new Timer();
            _timerList.Interval = 1000;
            _timerList.Tick += TimerList_Tick;
        }
        private void InitializeTimerSinger()
        {
            _timerSinger = new Timer();
            _timerSinger.Interval = 1000;
            _timerSinger.Tick += TimerSinger_Tick;
        }
        private void InitializeTimerAlbum()
        {
            _timerAlbum = new Timer();
            _timerAlbum.Interval = 1000;
            _timerAlbum.Tick += TimerAlbum_Tick;
        }
        private void InitializeTimerHomepage()
        {
            _timerHomepage = new Timer();
            _timerHomepage.Interval = 1000;
            _timerHomepage.Tick += TimerHomepage_Tick;
        }
        private void InitializeTimerSearch()
        {
            _timerSearch = new Timer();
            _timerSearch.Interval = 1000;
            _timerSearch.Tick += TimerSearch_Tick;
        }
        private void TimerRank_Tick(object sender, EventArgs e)
        {
            if (_rankMusicControl != null && _rankMusicControl.IsPlaying)
            {
                var currentTime = _rankMusicControl.CurrentTime;
                var totalTime = _rankMusicControl.TotalTime;

                // Cập nhật thời gian đã phát
                lTime1.Text = $"{currentTime.Minutes:D2}:{currentTime.Seconds:D2}";

                // Cập nhật HSlider1
                if (totalTime.TotalSeconds > 0)
                {
                    HSlider1.Value = (int)((currentTime.TotalSeconds / totalTime.TotalSeconds) * HSlider1.Maximum);
                }
            }
        }
        private void TimerList_Tick(object sender, EventArgs e)
        {
            if (_listControl != null && _listControl.IsPlaying)
            {
                var currentTime = _listControl.CurrentTime;
                var totalTime = _listControl.TotalTime;

                // Cập nhật thời gian đã phát
                lTime1.Text = $"{currentTime.Minutes:D2}:{currentTime.Seconds:D2}";

                // Cập nhật HSlider1
                if (totalTime.TotalSeconds > 0)
                {
                    HSlider1.Value = (int)((currentTime.TotalSeconds / totalTime.TotalSeconds) * HSlider1.Maximum);
                }
            }
        }
        private void TimerSinger_Tick(object sender, EventArgs e)
        {
            if (_singerControl != null && _singerControl.IsPlaying)
            {
                var currentTime = _singerControl.CurrentTime;
                var totalTime = _singerControl.TotalTime;

                // Cập nhật thời gian đã phát
                lTime1.Text = $"{currentTime.Minutes:D2}:{currentTime.Seconds:D2}";

                // Cập nhật HSlider1
                if (totalTime.TotalSeconds > 0)
                {
                    HSlider1.Value = (int)((currentTime.TotalSeconds / totalTime.TotalSeconds) * HSlider1.Maximum);
                }
            }
        }
        private void TimerAlbum_Tick(object sender, EventArgs e)
        {
            if (_albumControl != null && _albumControl.IsPlaying)
            {
                var currentTime = _albumControl.CurrentTime;
                var totalTime = _albumControl.TotalTime;

                // Cập nhật thời gian đã phát
                lTime1.Text = $"{currentTime.Minutes:D2}:{currentTime.Seconds:D2}";

                // Cập nhật HSlider1
                if (totalTime.TotalSeconds > 0)
                {
                    HSlider1.Value = (int)((currentTime.TotalSeconds / totalTime.TotalSeconds) * HSlider1.Maximum);
                }
            }
        }
        private void TimerHomepage_Tick(object sender, EventArgs e)
        {
            if (_homepageControl != null && _homepageControl.IsPlaying)
            {
                var currentTime = _homepageControl.CurrentTime;
                var totalTime = _homepageControl.TotalTime;

                // Cập nhật thời gian đã phát
                lTime1.Text = $"{currentTime.Minutes:D2}:{currentTime.Seconds:D2}";

                // Cập nhật HSlider1
                if (totalTime.TotalSeconds > 0)
                {
                    HSlider1.Value = (int)((currentTime.TotalSeconds / totalTime.TotalSeconds) * HSlider1.Maximum);
                }
            }
        }
        private void TimerSearch_Tick(object sender, EventArgs e)
        {
            if (_searchControl != null && _searchControl.IsPlaying)
            {
                var currentTime = _searchControl.CurrentTime;
                var totalTime = _searchControl.TotalTime;

                // Cập nhật thời gian đã phát
                lTime1.Text = $"{currentTime.Minutes:D2}:{currentTime.Seconds:D2}";

                // Cập nhật HSlider1
                if (totalTime.TotalSeconds > 0)
                {
                    HSlider1.Value = (int)((currentTime.TotalSeconds / totalTime.TotalSeconds) * HSlider1.Maximum);
                }
            }
        }
        private void HSlider1Rank_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            PauseMusicRank();
        }
        private void HSlider1Rank_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            var totalTime = _rankMusicControl.TotalTime;
            var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
            _rankMusicControl.Seek(newTime);
            PlayMusicRank();
        }

        private void HSlider1Rank_ValueChanged(object sender, EventArgs e)
        {
            if (_isDragging)
            {
                var totalTime = _rankMusicControl.TotalTime;
                var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
                lTime1.Text = $"{newTime.Minutes:D2}:{newTime.Seconds:D2}";
            }
        }
        private void hslider2Rank_ValueChanged(object sender, EventArgs e)
        {
            _rankMusicControl.SetVolume(HSlider2.Value / (float)HSlider2.Maximum);
        }
        private void ToggleRepeatList()
        {
            isRepeatOn = !isRepeatOn;
            RepeatToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }

        private void ToggleShuffleList()
        {
            isShuffleOn = !isShuffleOn;
            ShuffleToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }

        public void UpdateRepeatShuffleState(bool repeatOn, bool shuffleOn)
        {
            isRepeatOn = repeatOn;
            isShuffleOn = shuffleOn;

            UpdateButtonColors();
        }

        private void UpdateButtonColors()
        {
            bRepeat.BackColor = isRepeatOn ? Color.Green : Color.Transparent;
            bShuffle.BackColor = isShuffleOn ? Color.Green : Color.Transparent;
        }


        //RankMusic
        

        

        private void SkipPreviousRank()
        {
            _rankMusicControl.SkipPrevious();
        }

        private void SkipNextRank()
        {
            _rankMusicControl.SkipNext();
        }

        private void ToggleRepeatRank()
        {
            isRepeatOn = !isRepeatOn;
            RepeatToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }

        private void ToggleShuffleRank()
        {
            isShuffleOn = !isShuffleOn;
            ShuffleToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void PlayMusicRank()
        {
            if(_rankMusicControl != null)
            {
                _homepageControl?.StopMusic();
                _searchControl?.StopMusic();
                _albumControl?.StopMusic();
                _singerControl?.StopMusic();
                _listControl?.StopMusic();
                _rankMusicControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime2.Text);
                _timerRank.Start();
            }
        }

        public void PauseMusicRank()
        {
            _rankMusicControl.PauseMusicRank();
            _timerRank.Stop();
        }


        public void SetPlayPauseButtonState(bool isPlaying)
        {
            if (isPlaying)
            {
                ShowPauseButton();
            }
            else
            {
                ShowPlayButton();
            }
        }
        //List
        private void HSlider1List_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            PauseMusicList();
        }

        private void HSlider1List_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            var totalTime = _listControl.TotalTime;
            var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
            _listControl.Seek(newTime);
            PlayMusicList();
        }

        private void HSlider1List_ValueChanged(object sender, EventArgs e)
        {
            if (_isDragging)
            {
                var totalTime = _listControl.TotalTime;
                var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
                lTime1.Text = $"{newTime.Minutes:D2}:{newTime.Seconds:D2}";
            }
        }

        private void hslider2List_ValueChanged(object sender, EventArgs e)
        {
            _listControl.SetVolume(HSlider2.Value / (float)HSlider2.Maximum);
        }

        private void SkipPreviousList()
        {
            _listControl.SkipPrevious();
        }

        private void SkipNextList()
        {
            _listControl.SkipNext();
        }
        private void PlayMusicList()
        {
            if( _listControl != null)
            {
                _homepageControl?.StopMusic();
                _searchControl?.StopMusic();
                _albumControl?.StopMusic();
                _rankMusicControl?.StopMusic();
                _singerControl?.StopMusic();
                _listControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime2.Text);
                _timerList.Start();
            }
            
        }
        public void PauseMusicList()
        {
            _listControl.PauseMusicList();
            _timerList.Stop();
        }
        //Singer
        private void HSlider1Singer_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            PauseMusicSinger();
        }

        private void HSlider1Singer_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            var totalTime = _singerControl.TotalTime;
            var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
            _singerControl.Seek(newTime);
            PlayMusicSinger();
        }

        private void HSlider1Singer_ValueChanged(object sender, EventArgs e)
        {
            if (_isDragging)
            {
                var totalTime = _singerControl.TotalTime;
                var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
                lTime1.Text = $"{newTime.Minutes:D2}:{newTime.Seconds:D2}";
            }
        }

        private void hslider2Singer_ValueChanged(object sender, EventArgs e)
        {
            _singerControl.SetVolume(HSlider2.Value / (float)HSlider2.Maximum);
        }

        private void SkipPreviousSinger()
        {
            _singerControl.SkipPrevious();
        }

        private void SkipNextSinger()
        {
            _singerControl.SkipNext();
        }
        private void PlayMusicSinger()
        {
            if (_singerControl != null)
            {
                _homepageControl?.StopMusic();
                _searchControl?.StopMusic();
                _albumControl?.StopMusic();
                _listControl?.StopMusic();
                _rankMusicControl?.StopMusic();
                _singerControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime2.Text);
                _timerSinger.Start();
            }
        }
        public void PauseMusicSinger()
        {
            _singerControl.PauseMusicSinger();
            _timerSinger.Stop();
        }
        private void ToggleRepeatSinger()
        {
            isRepeatOn = !isRepeatOn;
            RepeatToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void ToggleShuffleSinger()
        {
            isShuffleOn = !isShuffleOn;
            ShuffleToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void HSlider1Album_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            PauseMusicAlbum();
        }

        private void HSlider1Album_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            var totalTime = _albumControl.TotalTime;
            var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
            _albumControl.Seek(newTime);
            PlayMusicAlbum();
        }

        private void HSlider1Album_ValueChanged(object sender, EventArgs e)
        {
            if (_isDragging)
            {
                var totalTime = _albumControl.TotalTime;
                var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
                lTime1.Text = $"{newTime.Minutes:D2}:{newTime.Seconds:D2}";
            }
        }

        private void hslider2Album_ValueChanged(object sender, EventArgs e)
        {
            _albumControl.SetVolume(HSlider2.Value / (float)HSlider2.Maximum);
        }

        private void SkipPreviousAlbum()
        {
            _albumControl.SkipPrevious();
        }

        private void SkipNextAlbum()
        {
            _albumControl.SkipNext();
        }
        private void PlayMusicAlbum()
        {
            if (_albumControl != null)
            {
                _homepageControl?.StopMusic();
                _searchControl?.StopMusic();
                _singerControl?.StopMusic();
                _listControl?.StopMusic();
                _rankMusicControl?.StopMusic();
                _albumControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime2.Text);
                _timerAlbum.Start();
            }
        }
        public void PauseMusicAlbum()
        {
            _albumControl.PauseMusicAlbum();
            _timerAlbum.Stop();
        }
        private void ToggleRepeatAlbum()
        {
            isRepeatOn = !isRepeatOn;
            RepeatToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void ToggleShuffleAlbum()
        {
            isShuffleOn = !isShuffleOn;
            ShuffleToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void HSlider1Homepage_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            PauseMusicHomepage();
        }

        private void HSlider1Homepage_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            var totalTime = _homepageControl.TotalTime;
            var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
            _homepageControl.Seek(newTime);
            PlayMusicHomepage();
        }

        private void HSlider1Homepage_ValueChanged(object sender, EventArgs e)
        {
            if (_isDragging)
            {
                var totalTime = _homepageControl.TotalTime;
                var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
                lTime1.Text = $"{newTime.Minutes:D2}:{newTime.Seconds:D2}";
            }
        }

        private void hslider2Homepage_ValueChanged(object sender, EventArgs e)
        {
            _homepageControl.SetVolume(HSlider2.Value / (float)HSlider2.Maximum);
        }

        private void SkipPreviousHomepage()
        {
            _homepageControl.SkipPrevious();
        }

        private void SkipNextHomepage()
        {
            _homepageControl.SkipNext();
        }
        private void PlayMusicHomepage()
        {
            if (_homepageControl != null)
            {
                _albumControl?.StopMusic();
                _singerControl?.StopMusic();
                _searchControl?.StopMusic();
                _listControl?.StopMusic();
                _rankMusicControl?.StopMusic();
                _homepageControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime2.Text);
                _timerHomepage.Start();
            }
        }
        public void PauseMusicHomepage()
        {
            _homepageControl.PauseMusicHomepage();
            _timerHomepage.Stop();
        }
        private void ToggleRepeatHomepage()
        {
            isRepeatOn = !isRepeatOn;
            RepeatToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void ToggleShuffleHomepage()
        {
            isShuffleOn = !isShuffleOn;
            ShuffleToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void HSlider1Search_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            PauseMusicSearch();
        }

        private void HSlider1Search_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            var totalTime = _searchControl.TotalTime;
            var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
            _searchControl.Seek(newTime);
            PlayMusicSearch();
        }

        private void HSlider1Search_ValueChanged(object sender, EventArgs e)
        {
            if (_isDragging)
            {
                var totalTime = _searchControl.TotalTime;
                var newTime = TimeSpan.FromSeconds((HSlider1.Value / (double)HSlider1.Maximum) * totalTime.TotalSeconds);
                lTime1.Text = $"{newTime.Minutes:D2}:{newTime.Seconds:D2}";
            }
        }

        private void hslider2Search_ValueChanged(object sender, EventArgs e)
        {
            _searchControl.SetVolume(HSlider2.Value / (float)HSlider2.Maximum);
        }

        private void SkipPreviousSearch()
        {
            _searchControl.SkipPrevious();
        }

        private void SkipNextSearch()
        {
            _searchControl.SkipNext();
        }
        private void PlayMusicSearch()
        {
            if (_searchControl != null)
            {
                _singerControl?.StopMusic();
                _albumControl?.StopMusic();
                _homepageControl?.StopMusic();
                _listControl?.StopMusic();
                _rankMusicControl?.StopMusic();
                _searchControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime2.Text);
                _timerSearch.Start();
            }
        }
        public void PauseMusicSearch()
        {
            _searchControl.PauseMusicSearch();
            _timerSearch.Stop();
        }
        private void ToggleRepeatSearch()
        {
            isRepeatOn = !isRepeatOn;
            RepeatToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        private void ToggleShuffleSearch()
        {
            isShuffleOn = !isShuffleOn;
            ShuffleToggled?.Invoke(this, EventArgs.Empty);
            UpdateButtonColors();
        }
        public void ShowPlayButton()
        {
            bPlay.Visible = true;
            bPause.Visible = false;
        }

        public void ShowPauseButton()
        {
            bPlay.Visible = false;
            bPause.Visible = true;
        }
        
        public string TrackImage
        {
            get { return picImage.ImageLocation; }
            set { picImage.ImageLocation = value; }
        }
        public string Namesong
        {
            get { return lNameSong.Text; }
            set { lNameSong.Text = value; }
        }
        public string Nameartist
        {
            get { return lNameSinger.Text; }
            set { lNameSinger.Text = value; }
        }

        public string Duration
        {
            get { return lTime2.Text; }
            set { lTime2.Text = value; }
        }
            
        public string fs_path
        {
            get { return _trackUrl; }
            set { _trackUrl = value; }
        }

        private void bAddPlaylist_Click(object sender, EventArgs e)
        {
            CurrentTrack.NameSong = Namesong;
            CurrentTrack.TrackImage = TrackImage;

            Form background = new Form();
            try
            {
                using (AddTrack ut = new AddTrack())
                {
                    background.StartPosition = FormStartPosition.Manual;
                    background.FormBorderStyle = FormBorderStyle.None;
                    background.Opacity = .50d;
                    background.BackColor = Color.Black;
                    background.WindowState = FormWindowState.Maximized;
                    background.Location = this.Location;
                    background.ShowInTaskbar = false;
                    background.Show();

                    ut.Owner = background;
                    ut.ShowDialog();
                    background.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { background.Dispose(); }
        }
    }
}