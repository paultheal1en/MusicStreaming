using Music;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Music
{
    public partial class CardSong : Bunifu.UI.WinForms.BunifuUserControl
    {
        private string _trackUrl;
        private USCRankMusic _rankMusicControl;
        private List _listControl;
        private Homepage _homepageControl;
        private Search _searchControl;
        private USCSinger _singerControl;
        private USCAlbum _albumControl;
        public CardSong()
        {
            InitializeComponent();
        }

        public CardSong(string stt, string imageUrl, string namesong, string nameartist, string namealbum, string duration, string fs_path, USCRankMusic rankMusicControl)
        {
            InitializeComponent();
            _rankMusicControl = rankMusicControl;
            _trackUrl = fs_path;

            // Gán các giá trị cho các thuộc tính
            lNumber.Text = FormatTrackNumber(stt);
            picImage.ImageLocation = imageUrl;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lNameAlbum.Text = namealbum;
            lTime.Text = duration;

            bPause.Visible = false;

            bPause.Click += (sender, e) =>
            {
                PauseMusicRank();
            };

            bPlay.Click += (sender, e) =>
            {
                PlayMusicRank();
            };
        }
        public CardSong(string stt, string imageUrl, string namesong, string nameartist, string namealbum, string duration, string fs_path, List listControl)
        {
            InitializeComponent();
            _listControl = listControl;
            _trackUrl = fs_path;

            // Gán các giá trị cho các thuộc tính
            lNumber.Text = FormatTrackNumber(stt);
            picImage.ImageLocation = imageUrl;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lNameAlbum.Text = namealbum;
            lTime.Text = duration;

            bPause.Visible = false;

            bPause.Click += (sender, e) =>
            {
                PauseMusicList();
            };

            bPlay.Click += (sender, e) =>
            {
                PlayMusicList();
            };
        }
        public CardSong(string stt, string imageUrl, string namesong, string nameartist, string namealbum, string duration, string fs_path, USCSinger singerControl)
        {
            InitializeComponent();
            _singerControl = singerControl;
            _trackUrl = fs_path;

            // Gán các giá trị cho các thuộc tính
            lNumber.Text = FormatTrackNumber(stt);
            picImage.ImageLocation = imageUrl;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lNameAlbum.Text = namealbum;
            lTime.Text = duration;

            bPause.Visible = false;

            bPause.Click += (sender, e) =>
            {
                PauseMusicSinger();
            };

            bPlay.Click += (sender, e) =>
            {
                PlayMusicSinger();
            };
        }
        public CardSong(string stt, string imageUrl, string namesong, string nameartist, string namealbum, string duration, string fs_path, Homepage homepageControl)
        {
            InitializeComponent();
            _homepageControl = homepageControl;
            _trackUrl = fs_path;

            // Gán các giá trị cho các thuộc tính
            lNumber.Text = FormatTrackNumber(stt);
            picImage.ImageLocation = imageUrl;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lNameAlbum.Text = namealbum;
            lTime.Text = duration;

            bPause.Visible = false;

            bPause.Click += (sender, e) =>
            {
                PauseMusicHomepage();
            };

            bPlay.Click += (sender, e) =>
            {
                PlayMusicHomepage();
            };
        }
        public CardSong(string stt, string imageUrl, string namesong, string nameartist, string namealbum, string duration, string fs_path, Search searchControl)
        {
            InitializeComponent();
            _searchControl = searchControl;
            _trackUrl = fs_path;

            // Gán các giá trị cho các thuộc tính
            lNumber.Text = FormatTrackNumber(stt);
            picImage.ImageLocation = imageUrl;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lNameAlbum.Text = namealbum;
            lTime.Text = duration;

            bPause.Visible = false;

            bPause.Click += (sender, e) =>
            {
                PauseMusicSearch();
            };

            bPlay.Click += (sender, e) =>
            {
                PlayMusicSearch();
            };
        }
        public CardSong(string stt, string imageUrl, string namesong, string nameartist, string namealbum, string duration, string fs_path, USCAlbum albumControl)
        {
            InitializeComponent();
            _albumControl = albumControl;
            _trackUrl = fs_path;

            // Gán các giá trị cho các thuộc tính
            lNumber.Text = FormatTrackNumber(stt);
            picImage.ImageLocation = imageUrl;
            lNameSong.Text = namesong;
            lNameSinger.Text = nameartist;
            lNameAlbum.Text = namealbum;
            lTime.Text = duration;

            bPause.Visible = false;

            bPause.Click += (sender, e) =>
            {
                PauseMusicAlbum();
            };

            bPlay.Click += (sender, e) =>
            {
                PlayMusicAlbum();
            };
        }
        private string FormatTrackNumber(string stt)
        {
            if (int.TryParse(stt, out int number))
            {
                if (number >= 1 && number <= 9)
                {
                    return number.ToString("D2");
                }
            }
            return stt;
        }
        private void PlayMusicRank()
        {
            _rankMusicControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime.Text);
        }
        private void PlayMusicList()
        {
            _listControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime.Text);
        }
        private void PlayMusicSinger()
        {
            _singerControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime.Text);
        }
        private void PlayMusicHomepage()
        {
            _homepageControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime.Text);
        }
        private void PlayMusicSearch()
        {
            _searchControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime.Text);
        }
        private void PlayMusicAlbum()
        {
            _albumControl.PlayMusic(_trackUrl, picImage.ImageLocation, lNameSong.Text, lNameSinger.Text, lTime.Text);
        }
        private void PauseMusicRank()
        {
            _rankMusicControl.PauseMusicRank();
        }
        private void PauseMusicList()
        {
            _listControl.PauseMusicList();

        }
        private void PauseMusicSinger()
        {
            _singerControl.PauseMusicSinger();
        }
        private void PauseMusicHomepage() {
            _homepageControl.PauseMusicHomepage();
        }
        private void PauseMusicSearch()
        {
            _searchControl.PauseMusicSearch();
        }
        private void PauseMusicAlbum() {
            _albumControl.PauseMusicAlbum();
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

        public string stt
        {
            get { return lNumber.Text; }
            set { lNumber.Text = value; }
        }
        public string TrackImage
        {
            get { return picImage.ImageLocation; }
            set { picImage.ImageLocation = value; }
        }
        public string namesong
        {
            get { return lNameSong.Text; }
            set { lNameSong.Text = value; }
        }
        public string nameartist
        {
            get { return lNameSinger.Text; }
            set { lNameSinger.Text = value; }
        }
        public string namealbum
        {
            get { return lNameAlbum.Text; }
            set { lNameAlbum.Text = value; }
        }
        public string duration
        {
            get { return lTime.Text; }
            set { lTime.Text = value; }
        }

        public string fs_path
        {
            get { return _trackUrl; }
            set { _trackUrl = value; }
        }
    }
}