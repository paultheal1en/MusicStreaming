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
    public partial class Artist : Bunifu.UI.WinForms.BunifuUserControl
    {
        public Artist()
        {
            InitializeComponent();
        }
        public Artist(string imageUrl, string nameartist)
        {
            InitializeComponent();
            imageArtist.ImageLocation = imageUrl;
            lbNameArtist.Text = nameartist;

            RegisterMouseDownEvent(this);
        }

        private void RegisterMouseDownEvent(Control control)
        {
            control.MouseDown += Artist_MouseDown;

            foreach (Control child in control.Controls)
            {
                RegisterMouseDownEvent(child);
            }
        }

        private void Artist_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnClick(e);
        }

        public string ItemImage
        {
            get
            {
                return imageArtist.ImageLocation;
            }
            set
            {
                imageArtist.ImageLocation = value;
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