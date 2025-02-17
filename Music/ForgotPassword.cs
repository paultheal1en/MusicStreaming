using Newtonsoft.Json;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Music
{
    public partial class ForgotPassword : Form
    { 
        private HttpClient httpClient;
        public ForgotPassword()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private void Forgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();

            loginForm.Show();
            this.Hide();
        }

        private async void btResetPassword_Click(object sender, EventArgs e)
        {
            var email = tbEmail.Text;
            if (string.IsNullOrWhiteSpace(email))
                lbMessage.Text = "Vui lòng nhập email.";
            var resetpasswordRequest = new
            {
                email
            };
            string json = JsonConvert.SerializeObject(resetpasswordRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync("http://localhost:9999/v1/Forgot", content);

                if (response.IsSuccessStatusCode)
                {
                    lbMessage.Text="Chúng tôi đã gửi mã xác thực vào email của bạn.";
                    Test CFForm = new Test(tbEmail.Text);
                    CFForm.Show();
                    this.Hide();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(email))
                        lbMessage.Text = "Gửi mã xác thực thất bại, vui lòng kiểm tra lại địa chỉ email.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
