using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    public partial class album_item : Bunifu.UI.WinForms.BunifuUserControl
    {
        public album_item()
        {
            InitializeComponent();
        }
        public album_item(string imageUrl, string nameartist, string namealbum)
        {
            InitializeComponent();
            itemImage.ImageLocation = imageUrl;
            lbNameArtist.Text = nameartist;
            lbNameAlbum.Text = namealbum;

            RegisterMouseDownEvent(this);
        }

        private void RegisterMouseDownEvent(Control control)
        {
            control.MouseDown += Album_MouseDown;

            foreach (Control child in control.Controls)
            {
                RegisterMouseDownEvent(child);
            }
        }

        private void Album_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnClick(e);
        }

        public string ItemImage
        {
            get
            {
                return itemImage.ImageLocation;
            }
            set
            {
                itemImage.ImageLocation = value;
            }
        }
        public string NameAlbum
        {
            get
            {
                return lbNameAlbum.Text;
            }
            set
            {
                lbNameAlbum.Text = value;
            }
        }
        public string NameArtist
        {
            get
            {
                return lbNameArtist.Text;
            }
            set
            {
                lbNameArtist.Text = value;
            }
        }
    }
}