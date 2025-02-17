using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.Common;
using Newtonsoft.Json;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Music
{   
    public partial class Login : Form
    {   // Khai báo một thuộc tính để lưu trữ token
        private HttpClient httpClient;
        public Login()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            bunifuImageButton1.Hide();
        }
        //Chuyển sang trang quên mật khẩu
        private void Forgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword forgotPasswordForm = new ForgotPassword();
            forgotPasswordForm.Show();
            this.Hide();
        }
        //Chuyển sang trang đăng ký
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }
        
        public async void btLogin_Click(object sender, EventArgs e)
        {
            var Email = tbEmail.Text;
            var Password = tbPassword.Text;

            if (string.IsNullOrWhiteSpace(Email))
                lbFail.Text = "Vui lòng nhập email.";
            else if (string.IsNullOrWhiteSpace(Password))
                lbFail.Text = "Vui lòng nhập mật khẩu.";
            var loginRequest = new
            {
                Email,
                Password
            };
            // Chuyển loginRequest thành 1 chuỗi json
            var json = JsonConvert.SerializeObject(loginRequest);
            // StringContent chứa dữ liệu JSON và được mã hóa và loại nội dung của request để gửi tới server
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                // gửi 1 yêu cầu HTTP POST đến máy chú api để đăng nhập
                var response = await httpClient.PostAsync("http://localhost:9999/v1/Login/", content);
                if (response.IsSuccessStatusCode)
                {
                    //Đọc nội dung phản hồi của máy chủ
                    var responeContent = await response.Content.ReadAsStringAsync();
                    // chuyển chuỗi JSON thành đối tượng động dynamic
                    var responseObj = JsonConvert.DeserializeObject<dynamic>(responeContent);

                    string tokenValue = responseObj.token;
                    manageToken.AccessToken = tokenValue;
                    //var MainForm = new Player.Main(tokenValue);
                    //MainForm.FormClosed += (sender, e) => { this.Show(); };
                    //this.Hide();
                    //MainForm.Show();
                    Form1 testForm = new Form1();
                    testForm.Show();
                    this.Hide();

                }
                else
                {
                    if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                    lbFail.Text="Đăng nhập thất bại! Vui lòng nhập lại email và mật khẩu!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            // Khi nhấp vào biểu tượng mắt ẩn
            bunifuImageButton1.Hide();
            bunifuImageButton2.Show();
            if (tbPassword.PasswordChar == '\0')
            {
                tbPassword.PasswordChar = '*'; // Ẩn mật khẩu
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            // Khi nhấp vào biểu tượng mắt hiện
            bunifuImageButton2.Hide();
            bunifuImageButton1.Show();
            if (tbPassword.PasswordChar == '*') 
            { 
                tbPassword.PasswordChar = '\0'; // Hiện mật khẩu
                                               
            }
        }
    }

}
