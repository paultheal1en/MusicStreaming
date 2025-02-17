// định nghĩa là cấu trúc dữ liệu sẽ được lưu vào collection.
package models

import (
	"crypto/rand"
	"database/sql"
	"encoding/base64"
	"encoding/hex"
	"errors"
	"html"
	"log"
	"strings"

	//Tạo token JWT

	//để sử dụng biến môi trường trong .env

	"github.com/jmoiron/sqlx"
	"github.com/joho/godotenv"
	_ "github.com/mattn/go-sqlite3"
	"golang.org/x/crypto/bcrypt"
	"gopkg.in/gomail.v2" // Gửi gmail
)

var db *sqlx.DB

type User struct {
	ID       int            `json:"ID" db:"ID"`
	NAME     sql.NullString `json:"NAME" db:"NAME"`
	EMAIL    string         `json:"EMAIL" db:"EMAIL"`
	PASSWORD string         `json:"PASSWORD" db:"PASSWORD"`
	PHONE    sql.NullString `json:"PHONE" db:"PHONE"`
	AVATAR   []byte         `json:"AVATAR" db:"AVATAR"`
}
type Code struct {
	EMAIL    string `json:"EMAIL" db:"EMAIL"`
	PASSWORD string `json:"PASSWORD" db:"PASSWORD"`
	CODE     string `json:"CODE" db:"CODE"`
}

func (user *User) HashPassword(password string) error {
	bytes, err := bcrypt.GenerateFromPassword([]byte(password), 14)
	if err != nil {
		return err
	}
	user.PASSWORD = string(bytes)
	return nil
}

func (user *User) CheckPassword(providedPassword string) error {
	err := bcrypt.CompareHashAndPassword([]byte(user.PASSWORD), []byte(providedPassword))
	if err != nil {
		return err
	}
	return nil
}

func (user *User) Santize(data string) string {
	data = html.EscapeString(strings.TrimSpace(data))
	return data
}
func GenerateVerificationCode(length int) (string, error) {
	randomBytes := make([]byte, length)
	_, err := rand.Read(randomBytes)
	if err != nil {
		return "", err
	}
	return hex.EncodeToString(randomBytes)[:length], nil
}
func SendVerificationEmail(email, code string) error {
	err := godotenv.Load()
	if err != nil {
		log.Fatalf("Error getting env, %v", err)
	}
	// Thông tin xác thực Gmail
	emailFrom := "22521585@gm.uit.edu.vn"
	password := "Tranvantruong2609"

	// Thông tin SMTP của Gmail
	smtpHost := "smtp.gmail.com"
	smtpPort := 587

	// Tạo một đối tượng gửi email
	m := gomail.NewMessage()
	m.SetHeader("From", emailFrom)
	m.SetHeader("To", email)
	m.SetHeader("Subject", "Verification Code")
	m.SetBody("text/plain", "Your verification code is: "+code+"\nPlease verify within 1 minute")

	// Tạo một đối tượng Dialer để gửi email
	d := gomail.NewDialer(smtpHost, smtpPort, emailFrom, password)

	// Gửi email
	if err := d.DialAndSend(m); err != nil {
		log.Println(err)
		return err
	}
	return nil
}
func GenerateRandomPassword(length int) (string, error) {
	randomBytes := make([]byte, length)
	_, err := rand.Read(randomBytes)
	if err != nil {
		return "", err
	}
	return base64.URLEncoding.EncodeToString(randomBytes)[:length], nil
}
func ResetPasswordAndSendEmail(db *sqlx.DB, email, newPassword string) error {
	err := godotenv.Load()
	if err != nil {
		log.Fatalf("Error getting env, %v", err)
	}
	// Băm mật khẩu mới
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(newPassword), bcrypt.DefaultCost)
	if err != nil {
		return err
	}
	// Đặt lại mật khẩu cho người dùng trong cơ sở dữ liệu
	// Ở đây bạn cần thực hiện một truy vấn để cập nhật mật khẩu mới cho người dùng với địa chỉ email được cung cấp.
	_, err = db.Exec("UPDATE Users SET PASSWORD = ? WHERE EMAIL = ?", hashedPassword, email)
	if err != nil {
		return err
	}
	// Gửi email chứa mật khẩu mới
	// Thông tin xác thực Gmail
	emailFrom := "22521585@gm.uit.edu.vn" // Địa chỉ email của bạn
	password := "Tranvantruong2609"       // Mật khẩu email của bạn

	// Thông tin SMTP của Gmail
	smtpHost := "smtp.gmail.com"
	smtpPort := 587

	// Tạo một đối tượng gửi email
	m := gomail.NewMessage()
	m.SetHeader("From", emailFrom)
	m.SetHeader("To", email)
	m.SetHeader("Subject", "New Password")
	m.SetBody("text/plain", "Your new password is: "+newPassword+"\nPlease log in and change your new password")

	// Tạo một đối tượng Dialer để gửi email
	d := gomail.NewDialer(smtpHost, smtpPort, emailFrom, password)

	// Gửi email
	if err := d.DialAndSend(m); err != nil {
		return err
	}
	return nil
}
func GetProfileByEmail(email string, db *sqlx.DB) (*User, error) {
	// Thực hiện truy vấn để lấy thông tin profile dựa trên Email
	var profile User
	err := db.Get(&profile, "SELECT ID, NAME, EMAIL, PASSWORD, PHONE, AVATAR FROM Users WHERE EMAIL = ?", email)
	if err != nil {
		return nil, err
	}

	return &profile, nil
}

// UpdateName cập nhật tên của profile vào cơ sở dữ liệu dựa trên token
func UpdateName(email string, db *sqlx.DB, newName string) error {

	// Thực hiện truy vấn cập nhật tên của profile dựa trên Email
	_, err := db.Exec("UPDATE Users SET NAME = ? WHERE EMAIL = ?", newName, email)
	if err != nil {
		return err
	}

	return nil
}

// UpdatePassword cập nhật mật khẩu của profile vào cơ sở dữ liệu dựa trên token
func UpdatePassword(email string, db *sqlx.DB, currentPassword string, newPassword string) error {
	// Kiểm tra mật khẩu hiện tại của người dùng
	var currentPasswordDB string
	err := db.Get(&currentPasswordDB, "SELECT PASSWORD FROM Users WHERE EMAIL = ?", email)
	if err != nil {
		return err
	}

	// So sánh mật khẩu hiện tại với mật khẩu được cung cấp
	err = bcrypt.CompareHashAndPassword([]byte(currentPasswordDB), []byte(currentPassword))
	if err != nil {
		return errors.New("incorrect current password")
	}

	// Hash mật khẩu mới trước khi lưu vào cơ sở dữ liệu
	hashedNewPassword, err := bcrypt.GenerateFromPassword([]byte(newPassword), bcrypt.DefaultCost)
	if err != nil {
		return err
	}

	// Thực hiện truy vấn cập nhật mật khẩu của profile dựa trên email
	_, err = db.Exec("UPDATE Users SET PASSWORD = ? WHERE EMAIL = ?", hashedNewPassword, email)
	if err != nil {
		return err
	}

	return nil
}

// UpdatePhone cập nhật số điện thoại của profile vào cơ sở dữ liệu dựa trên token
func UpdatePhone(email string, db *sqlx.DB, newPhone string) error {
	// Thực hiện truy vấn cập nhật số điện thoại của profile dựa trên email
	_, err := db.Exec("UPDATE Users SET PHONE = ? WHERE EMAIL = ?", newPhone, email)
	if err != nil {
		return err
	}

	return nil
}

// UpdateAvatar cập nhật avatar của profile vào cơ sở dữ liệu dựa trên token

func UpdateAvatar(email string, db *sqlx.DB, newAvatar string) error {
	// Chuyển đổi avatar từ base64 sang []byte
	avatarBytes, err := base64.StdEncoding.DecodeString(newAvatar)
	if err != nil {
		return err
	}

	// Thực hiện truy vấn cập nhật avatar của user dựa trên Email
	_, err = db.Exec("UPDATE users SET avatar = ? WHERE email = ?", avatarBytes, email)
	if err != nil {
		return err
	}
	return nil
}
