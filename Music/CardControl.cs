using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    public partial class CardControl : Bunifu.UI.WinForms.BunifuUserControl
    {
        public CardControl()
        {
            InitializeComponent();
        }

        public async void cardDetails(ClassCard e)
        {
            lbid.Text = e.PlaylistData.id.ToString();
            lbNamePlaylist.Text = e.PlaylistData.PlaylistName;
            itemImage.ImageLocation = e.PlaylistData.PlaylistImage;
        }

        public async void displayNew()
        {
            ClassCard get = new ClassCard();
            ClassCard.Playlist playlist = await get.GetNewInsertedData();
            lbNamePlaylist.Text = playlist.PlaylistName;
            itemImage.ImageLocation = "https://static.vecteezy.com/system/resources/previews/001/918/233/non_2x/music-and-sound-line-icon-with-headphones-vector.jpg";
            lbid.Text = playlist.id.ToString();
        }

        private void CardControl_Load_1(object sender, EventArgs e)
        {
            if (AddPlaylist.refresh == true)
            {
                displayNew();
            }
        }

        public static bool view = false;
        public static string public_id;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            timer1.Start();
            view = true;
            public_id = lbid.Text;
            AddPlaylist form = new AddPlaylist();
            form.ShowDialog();
        }

        private async Task LoadPlaylistDetails()
        {
            try
            {
                ClassCard get = new ClassCard();
                await get.GetDetails(lbid.Text);
                lbNamePlaylist.Text = get.PlaylistData.PlaylistName;
                AddPlaylist.isUpdate = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (AddPlaylist.isUpdate == true)
            {
                await LoadPlaylistDetails();
            }
        }

        public static bool isDeleted = false;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            isDeleted = true;
            ClassCard get = new ClassCard();
            get.DeleteById(lbid.Text);
        }

        public static bool isOpened = false;

        private void itemImage_Click(object sender, EventArgs e)
        {
            SharedData.PlaylistID = lbid.Text;
            isOpened = true;
        }

        private void CardControl_Click(object sender, EventArgs e)
        {
            SharedData.PlaylistID = lbid.Text;
            isOpened = true;
        }

    }
}
