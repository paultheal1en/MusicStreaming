using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Music
{
    public partial class cardTrack : Bunifu.UI.WinForms.BunifuUserControl
    {
        private string _trackUrl;
        private UserControl _listControl;
        public cardTrack()
        {
            InitializeComponent();
        }
        public cardTrack(string stt, string imageUrl, string nametrack, string nameartist, string namealbum, string duration,string fs_path, UserControl us)
        {
            InitializeComponent();
            _listControl = us;
            _trackUrl = fs_path;
            // Gán các giá trị cho các thuộc tính
            STT.Text = stt;
            string image = imageUrl;
            if (image != null)
            {
                ImageArtist.ImageLocation = imageUrl;
            }
            NameTrack.Text = nametrack;
            NameArtist.Text = nameartist;
            NameAlbum.Text = namealbum;
            Duration.Text = duration;
        }
        public cardTrack(string stt, string imageUrl, string nametrack, string nameartist, string namealbum, string duration, string fs_path)
        {
            InitializeComponent();
            // Gán các giá trị cho các thuộc tính
            STT.Text = stt;
            string image = imageUrl;
            if (image != null)
            {
                ImageArtist.ImageLocation = imageUrl;
            }
            NameTrack.Text = nametrack;
            NameArtist.Text = nameartist;
            NameAlbum.Text = namealbum;
            Duration.Text = duration;
        }
        public string stt
        {
            get
            {
                return STT.Text;
            }
            set
            {
                STT.Text = value;
            }
        }
        public string TrackImage
        {
            get
            {
                return ImageArtist.ImageLocation;
            }
            set
            {
                ImageArtist.ImageLocation = value;
            }
        }
        public string nametrack
        {
            get
            {
                return NameTrack.Text;
            }
            set
            {
                NameTrack.Text = value;
            }
        }
        public string nameartist
        {
            get
            {
                return NameArtist.Text;
            }
            set
            {
                NameArtist.Text = value;
            }
        }
        public string namealbum
        {
            get
            {
                return NameAlbum.Text;
            }
            set
            {
                NameAlbum.Text = value;
            }
        }
        public string duration
        {
            get
            {
                return Duration.Text;
            }
            set
            {
                Duration.Text = value;
            }
        }
        public string fs_path
        {
            get { return _trackUrl; }
            set { _trackUrl = value; }
        }
    }
}