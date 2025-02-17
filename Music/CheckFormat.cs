using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music
{
    internal class CheckFormat
    {
        public static bool ValidateInput(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.");
                return false;
            }
            return true;
        }
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_.]).{8,24}$";
            if (Regex.IsMatch(password, pattern))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Yêu cầu về mật khẩu:\r\n\t" +
                    "- Độ dài từ 8 đến 24 ký tự\r\n\t" +
                    "- Bao gồm ít nhất một chữ cái thường\r\n\t" +
                    "- Bao gồm ít nhất một chữ cái hoa\r\n\t" +
                    "- Bao gồm ít nhất một chữ số\r\n\t" +
                    "- Bao gồm ít nhất một ký tự đặc biệt\r\n");
                return false;
            }
        }
        public static bool IsValidEmail (string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng của địa chỉ email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Kiểm tra chuỗi email với biểu thức chính quy
            if (Regex.IsMatch(email, pattern))
            {
                return true; // Địa chỉ email hợp lệ
            }
            else
            {
                MessageBox.Show("Email không đúng định dạng, vui lòng kiểm tra và nhập lại chính xác.");
                return false; // Địa chỉ email không hợp lệ
            }
        }
    }
}
