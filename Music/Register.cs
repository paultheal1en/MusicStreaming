using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Music
{
    public partial class Register : Form
    {
        private HttpClient httpClient;
        public Register()
        {
            InitializeComponent();
            btEyePW1.Hide();
            btEyePW2.Hide();
            btUnEyePW2.Show();
            btUnEyePW1.Show();
            httpClient = new HttpClient();
        }

        private void BackLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private async void btSignUp_Click(object sender, EventArgs e)
        {
            var email = tbEmail.Text;
            var password = tbPassword.Text;
            var comfirmPassword = tbCFPassword.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                lbMessage.Text = "Vui lòng nhập email.";
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                lbMessage.Text = "Vui lòng nhập mật khẩu.";
                return;
            }
            else if (string.IsNullOrWhiteSpace(comfirmPassword))
            {
                lbMessage.Text = "Vui lòng nhập mật khẩu xác nhận.";
                return;
            }
            if (comfirmPassword != password)
            {
                lbMessage.Text = "Mật khẩu và mật khẩu xác nhận không giống nhau.";
                return;
            }
            var registerRequest = new
            {
                email,
                password
            };
            string json = JsonConvert.SerializeObject(registerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("http://localhost:9999/v1/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    lbMessage.Text = "Bạn đã đăng ký thành công. Giờ bạn đã có thể đăng nhập vào ứng dụng";
                    ConfirmCode_Register CFForm = new ConfirmCode_Register(tbEmail.Text,tbPassword.Text);
                    CFForm.Show();
                    this.Hide();
                }
                else
                {
                    lbMessage.Text = "Đăng ký thất bại. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            string password = tbPassword.Text ;
            if (password.Length >= 8 && password.Length <= 24)
            {
                // Nếu mật khẩu đủ độ dài, đặt cbCheckDoDai thành true
                cbCheckDoDai.Checked = true;
            }
            else
            {
                cbCheckDoDai.Checked = false;
            }
 
            bool containsDigit = password.Any(char.IsDigit);
            bool containsUpperCase = password.Any(char.IsUpper);
            bool containsLowerCase = password.Any(char.IsLower);
            bool containsSpecialChar = password.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
            if (containsDigit && containsUpperCase && containsLowerCase && containsSpecialChar)
            {
                cbCheckKyTu.Checked = true;
            }
            else
            {
                cbCheckKyTu.Checked = false;
            }
        }
        private void UpdateSignUpButtonState()
        {
            // Kiểm tra xem cả hai ô kiểm tra có được chọn không
            if (cbCheckDoDai.Checked && cbCheckKyTu.Checked)
            {
                // Nếu cả hai đều được chọn, kích hoạt nút Sign Up
                btSignUp.Enabled = true;
            }
            else
            {
                // Nếu một trong hai không được chọn, tắt nút Sign Up
                btSignUp.Enabled = false;
            }
        }
        private void btEyePW1_Click(object sender, EventArgs e)
        {
            // Khi nhấp vào biểu tượng mắt ẩn
            btEyePW1.Hide();
            btUnEyePW1.Show();
            if (tbPassword.PasswordChar == '\0')
            {
                tbPassword.PasswordChar = '*'; // Ẩn mật khẩu
            }
        }

        private void btUnEyePW2_Click(object sender, EventArgs e)
        {
            btUnEyePW2.Hide();
            btEyePW2.Show();
            if (tbCFPassword.PasswordChar == '*')
            {
                tbCFPassword.PasswordChar = '\0'; // Ẩn mật khẩu
            }
        }

        private void btUnEyePW1_Click(object sender, EventArgs e)
        {
            btUnEyePW1.Hide();
            btEyePW1.Show();
            if (tbPassword.PasswordChar == '*')
            {
                tbPassword.PasswordChar = '\0'; // Ẩn mật khẩu
            }
        }

        private void btEyePW2_Click(object sender, EventArgs e)
        {
            // Khi nhấp vào biểu tượng mắt ẩn
            btEyePW2.Hide();
            btUnEyePW2.Show();
            if (tbCFPassword.PasswordChar == '\0')
            {
                tbCFPassword.PasswordChar = '*'; // Ẩn mật khẩu
            }
        }

        private void cbCheckDoDai_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateSignUpButtonState();
        }

        private void cbCheckKyTu_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            UpdateSignUpButtonState();
        }
    }
}
