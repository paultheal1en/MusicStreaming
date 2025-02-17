using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net;
namespace Music
{
    public partial class AddPlaylist : MaterialForm
    {
        MaterialSkinManager skinManager;
        public AddPlaylist()
        {
            InitializeComponent();
            skinManager = MaterialSkinManager.Instance;
            skinManager.ColorScheme = new ColorScheme(Primary.LightGreen200, Primary.LightGreen500, Primary.LightGreen500, Accent.LightGreen200, TextShade.WHITE);
        }

        private void btSavePlaylist_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        bool update = false;
        private void AddPlaylist_Load_1(object sender, EventArgs e)
        {
            if (CardControl.view == true)
            {
                getDetail();
                btSavePlaylist.Text = "Update";
                update = true;
            }
            else
            {
                btSavePlaylist.Text = "Save";
                update = false;
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        public static bool refresh = false;
        public static bool isUpdate = false;
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (update == false)
            {
                ClassCard save = new ClassCard();
                save.Name = tbNamePlaylist.Text;
                save.Save();
                refresh = true;
            }
            else
            {
                ClassCard up = new ClassCard();
                up.UpdatePlaylistName(CardControl.public_id, tbNamePlaylist.Text);
                isUpdate = true;
            }
        }

        private async void getDetail()
        {
            ClassCard get = new ClassCard();
            await get.GetDetails(CardControl.public_id);
            tbNamePlaylist.Text = get.PlaylistData.PlaylistName;
        }
       
    }
}