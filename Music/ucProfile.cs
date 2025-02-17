using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Imaging;
using System.IO;

namespace Music
{
    public partial class ucProfile : UserControl
    {
        private HttpClient httpClient;
        public ucProfile()
        {
            InitializeComponent();
            tbError.Hide();
        }

        private async void ucProfile_Load(object sender, EventArgs e)
        {
            using (var handler = new HttpClientHandler())
            {
                // Tạo cookie container để lưu trữ cookie
                var cookieContainer = new CookieContainer();
                handler.CookieContainer = cookieContainer;
                using (var client = new HttpClient(handler))
                {
                    // Lấy giá trị của token từ form Login
                    string token = manageToken.AccessToken;
                    var cookie = new Cookie("token", token, "/", "localhost");

                    cookieContainer.Add(cookie);
                    var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:9999/v1/profiles/profile");
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc dữ liệu JSON từ phản hồi
                        var responeContent = await response.Content.ReadAsStringAsync();
                        // Chuyển chuỗi JSON thành đối tượng động dynamic
                        var responseObj = JsonConvert.DeserializeObject<dynamic>(responeContent);

                        // Điền dữ liệu vào các ô textbox
                        txtName.Text = responseObj.name;
                        txtEmail.Text = responseObj.email;
                        txtPhone.Text = responseObj.phone;

                        // Load avatar từ database (giả sử avatar được lưu dưới dạng base64 trong responseObj.avatar)
                        if (responseObj.avatar != null)
                        {
                            // Chuyển đổi avatar từ base64 thành hình ảnh và hiển thị trong pictureBox1
                            byte[] avatarBytes = Convert.FromBase64String(responseObj.avatar.ToString());
                            using (MemoryStream ms = new MemoryStream(avatarBytes))
                            {
                                pictureBox2.Image = Image.FromStream(ms);
                            }
                        }
                        //else
                        //{
                        //    // Nếu không có avatar, hiển thị một hình ảnh mặc định hoặc thông báo rỗng tùy vào yêu cầu của bạn
                        //    // Ví dụ:
                        //    pictureBox2.Image = Properties.Resources.DefaultAvatar; // DefaultAvatar là hình ảnh mặc định đã được nhúng trong resource
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve profile information: " + response.ReasonPhrase);
                    }
                }
            }
        }

        private async void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length != 10 && txtPhone.Text.Length != 0)
            {
                tbError.Show();
                return;
            }
            using (var handler = new HttpClientHandler())
                {
                    var cookieContainer = new CookieContainer();
                    handler.CookieContainer = cookieContainer;

                    using (var client = new HttpClient(handler))
                    {
                        string token = manageToken.AccessToken;
                        var cookie = new Cookie("token", token, "/", "localhost");
                        cookieContainer.Add(cookie);

                        var name = txtName.Text;
                        var phone = txtPhone.Text;
                        var Request = new
                        {
                            name,
                            phone
                        };
                        string json = JsonConvert.SerializeObject(Request);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        try
                        {
                            // Đúng là client thay vì httpClient
                            var response = await client.PutAsync("http://localhost:9999/v1/profiles/update", content);

                            if (response.IsSuccessStatusCode)
                            {
                            MessageBox.Show("Dữ liệu đã được cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbError.Hide();
                        }
                        else
                            {
                            MessageBox.Show("Cập nhật không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}");
                        }
                    }
                }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.Show();
        }

        private async void btnEditPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.Title = "Select an image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn của hình ảnh được chọn
                    string imagePath = openFileDialog.FileName;

                    try
                    {
                        // Hiển thị hình ảnh trong pictureBox2
                        pictureBox2.Image = Image.FromFile(imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open the selected image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Gửi yêu cầu cập nhật avatar
                    using (var handler = new HttpClientHandler())
                    {
                        var cookieContainer = new CookieContainer();
                        handler.CookieContainer = cookieContainer;

                        using (var client = new HttpClient(handler))
                        {
                            string token = manageToken.AccessToken;
                            var cookie = new Cookie("token", token, "/", "localhost");
                            cookieContainer.Add(cookie);

                            // Chuyển đổi hình ảnh từ pictureBox2 thành mảng byte
                            byte[] imageData;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                pictureBox2.Image.Save(ms, ImageFormat.Jpeg);
                                imageData = ms.ToArray();
                            }

                            // Chuyển đổi mảng byte thành chuỗi base64
                            string base64Image = Convert.ToBase64String(imageData);

                            var Request = new
                            {
                                avatar = base64Image
                            };
                            string json = JsonConvert.SerializeObject(Request);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            try
                            {
                                var response = await client.PutAsync("http://localhost:9999/v1/profiles/avatar", content);

                                if (response.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Cập nhật avatar thành công");
                                }
                                else
                                {
                                    MessageBox.Show($"Failed to update avatar. Status code: {response.StatusCode}");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An error occurred: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

        
    }
}
