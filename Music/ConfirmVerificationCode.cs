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

namespace Music
{
    public partial class Test : Form
    {
        private HttpClient httpClient;
        public Test(string email)
        {
            InitializeComponent();
            tbEmail.Text = email;
            httpClient = new HttpClient();
        }

        private async void btResetPassword_Click(object sender, EventArgs e)
        {
            var email = tbEmail.Text;
            var code = tbCode.Text;
            if (!CheckFormat.IsValidEmail(email))
                return;
            var resetpasswordRequest = new
            {
                email,
                code
            };
            string json = JsonConvert.SerializeObject(resetpasswordRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync("http://localhost:9999/v1/ConfirmVerificationCode", content);

                if (response.IsSuccessStatusCode)
                {
                    lbMessage.Text="Chúng tôi đã gửi mật khẩu mới vào email của bạn.";
                    Login loginForm = new Login();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    lbMessage.Text="Mã xác nhận không đúng. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Forgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}
