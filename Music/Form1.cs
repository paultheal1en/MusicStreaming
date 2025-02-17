using System;
using System.Windows.Forms;

namespace Music
{
    public partial class Form1 : Form
    {
        Homepage homepage = new Homepage();
        ucProfile ucProfile = new ucProfile();
        private USCPlay uscPlayControl;
        private Panel panel5;
        public UserControl PlayingControl;
        public Panel Panel3 => panel3;

        public Form1()
        {
            InitializeComponent();
            homepage.ParentForm = this;
            panel5 = new Panel();
            panel5.Dock = DockStyle.Fill;
            panel3.Controls.Add(panel5);
            addUserControl(homepage);
        }
        public void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(userControl);
            userControl.BringToFront();
        }
        public void AddUSCPlay(USCPlay uscPlay)
        {
            if (uscPlayControl != null)
            {
                panel3.Controls.Remove(uscPlayControl);
                uscPlayControl.Dispose();
            }

            uscPlay.Dock = DockStyle.Bottom;
            uscPlayControl = uscPlay;
            panel3.Controls.Add(uscPlay);
            uscPlay.BringToFront();

            panel5.Dock = DockStyle.Top;
            panel5.Height = panel3.Height - uscPlay.Height;
        }
        public void RemoveUSCPlay()
        {
            if (uscPlayControl != null)
            {
                //if (PlayingControl is USCRankMusic || PlayingControl is Homepage || PlayingControl is USCSinger || PlayingControl is USCAlbum || PlayingControl is List)
                {
                    panel3.Controls.Remove(uscPlayControl);
                    uscPlayControl.Dispose();
                    uscPlayControl = null;

                    panel5.Dock = DockStyle.Fill;
                    panel5.Height = 0;
                }
            }
        }
        private void bunifuButton22_Click_1(object sender, EventArgs e)
        {
            homepage.ParentForm = this;
            addUserControl(homepage);
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            USCRankMusic ucRank = new USCRankMusic();
            ucRank.ParentForm = this;
            addUserControl(ucRank);
        }

        private void bunifuButton26_Click_1(object sender, EventArgs e)
        {
            ucPlaylists ucPlaylists = new ucPlaylists();
            addUserControl(ucPlaylists);
        }

        private void bunifuButton27_Click_1(object sender, EventArgs e)
        {
            addUserControl(ucProfile);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text == null)
            {   
                return;
            }
            Search search = new Search(tbSearch.Text);
            search.ParentForm = this;
            addUserControl(search);
        }
        private void btCreateRoom_Click(object sender, EventArgs e)
        {
            Room room = new Room();
            addUserControl(room);
        }

        private void btJoinRoom_Click(object sender, EventArgs e)
        {

        }

        private void openTimer_Tick(object sender, EventArgs e)
        {
            if (CardControl.isOpened)
            {
                panel5.Controls.Clear();
                List list = new List();
                list.ParentForm = this ;
                addUserControl(list);
                CardControl.isOpened = false;
            }
            if(CardControl.isDeleted)
            {
                panel5.Controls.Clear();
                ucPlaylists list = new ucPlaylists();
                addUserControl(list);
                CardControl.isDeleted = false;
            }
        }
        public void SetPlayingControl(UserControl control)
        {
            if (PlayingControl != null && PlayingControl != control)
            {
                if (PlayingControl is USCRankMusic rankMusic)
                {
                    rankMusic.StopMusic();
                }
                else if (PlayingControl is List list)
                {
                    list.StopMusic();
                }
                else if (PlayingControl is USCSinger singer)
                {
                    singer.StopMusic();
                }
                else if (PlayingControl is USCAlbum album)
                {
                    album.StopMusic();
                }
                else if (PlayingControl is Homepage page)
                {
                    page.StopMusic();
                }
                else if (PlayingControl is Search search)
                {
                    search.StopMusic();
                }
            }
            PlayingControl = control;
        }


    }
}
