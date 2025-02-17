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
using MaterialSkin.Controls;
using MaterialSkin;
using Newtonsoft.Json;
using static Music.ClassCard;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Net;
using System.Drawing.Imaging;
using System.IO;

namespace Music
{
    public partial class AddTrack : MaterialForm
    {
        MaterialSkinManager skinManager;
        public AddTrack()
        {
            InitializeComponent();
            skinManager = MaterialSkinManager.Instance;
            skinManager.ColorScheme = new ColorScheme(Primary.LightGreen200, Primary.LightGreen500, Primary.LightGreen500, Accent.LightGreen200, TextShade.WHITE);
        }

        private void btSavePlaylist_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        private void AddTrack_Load_1(object sender, EventArgs e)
        {
            btSavePlaylist.Text = "Save";
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        public static bool refresh = false;
        private async void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // Tạo HttpClientHandler và HttpClient
                using (var handler = new HttpClientHandler())
                {
                    var cookieContainer = new CookieContainer();
                    handler.CookieContainer = cookieContainer;

                    using (var client = new HttpClient(handler))
                    {
                        // Lấy token từ manageToken.AccessToken
                        string token = manageToken.AccessToken;

                        // Tạo cookie từ token và thêm vào cookieContainer
                        var cookie = new Cookie("token", token, "/", "localhost");
                        cookieContainer.Add(cookie);

                        if (USCPlay.CurrentTrack.TrackImage != null)
                        {

                            // Tạo đối tượng trackInfo
                            var trackInfo = new
                            {
                                PlaylistName = tbNamePlaylist.Text,
                                SongName = USCPlay.CurrentTrack.NameSong,
                                Image = USCPlay.CurrentTrack.TrackImage
                            };

                            // Chuyển đổi trackInfo sang JSON
                            var jsonContent = JsonConvert.SerializeObject(trackInfo);
                            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                            try
                            {
                                // Gửi yêu cầu POST đến endpoint /playlist/AddTrackToPlaylist
                                var response = await client.PostAsync("http://localhost:9999/v1/playlist/AddTrackToPlaylist", content).ConfigureAwait(false);

                                if (response.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Succeeded to add track to playlist");
                                }
                                else
                                {
                                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                    MessageBox.Show($"Failed to add track to playlist: {responseContent}");
                                }
                            }
                            catch (HttpRequestException httpEx)
                            {
                                // Xử lý lỗi mạng
                                MessageBox.Show($"Network error: {httpEx.Message}");
                            }
                            catch (Exception ex)
                            {
                                // Xử lý các lỗi khác
                                MessageBox.Show($"An error occurred: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác ngoài HttpClient
                MessageBox.Show($"An error occurred in the background worker: {ex.Message}");
            }
        }
    }
}