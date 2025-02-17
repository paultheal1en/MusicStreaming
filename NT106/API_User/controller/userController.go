package controller

import (
	"Music/auth"
	"Music/database"
	"Music/models"
	"database/sql"
	"log"
	"net/http"
	"strconv"
	"time"

	//Tạo token JWT

	"github.com/gin-gonic/gin"
	"github.com/jmoiron/sqlx" //để sử dụng biến môi trường trong .env
	_ "github.com/mattn/go-sqlite3"
	"golang.org/x/crypto/bcrypt"
)

var db *sqlx.DB

func init() {
	var err error
	db, err = database.ConnectDB()
	if err != nil {
		log.Fatalln("Cannot connect to database:", err)
	}
}
func RegisterUser(c *gin.Context) {
	var newUser models.User
	if err := c.ShouldBindJSON(&newUser); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	//Kiểm tra xem người dùng đã tồn tại chưa
	var existingUser models.User
	err := db.Get(&existingUser, "SELECT * FROM Users WHERE EMAIL = ?", newUser.EMAIL)
	if err == nil {
		c.JSON(http.StatusConflict, gin.H{"error": "User already exists"})
		return
	} else if err != sql.ErrNoRows {
		log.Println("Error querying database:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error 1"})
		return
	}
	if existingUser.ID != 0 {
		c.JSON(http.StatusConflict, gin.H{"error": "User already exists"})
		return
	}
	//Gửi mã xác nhận để xác nhận email
	// Câu lệnh SQL để tạo bảng verificationcode
	createTableSQL := `
			CREATE TABLE IF NOT EXISTS verificationcode (
				EMAIL TEXT NOT NULL,
				CODE TEXT NOT NULL,
				PASSWORD TEXT NOT NULL,
				PRIMARY KEY (EMAIL)
			);
		`
	// Hash mật khẩu trước khi lưu vào cơ sở dữ liệu
	if err := newUser.HashPassword(newUser.PASSWORD); err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to hash password"})
		return
	}
	// Thực thi câu lệnh SQL để tạo bảng
	_, err = db.Exec(createTableSQL)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to create table verificationcode"})
		return
	}
	// Kiểm tra xem người dùng đã tồn tại trong bảng verificationcode chưa
	var existingCode models.Code
	err = db.Get(&existingCode, "SELECT * FROM verificationcode WHERE EMAIL = ?", newUser.EMAIL)
	if err == nil {
		// Nếu tồn tại, xóa bản ghi cũ
		_, err := db.Exec("DELETE FROM verificationcode WHERE EMAIL = ?", newUser.EMAIL)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to delete existing user"})
			return
		}
	} else if err != sql.ErrNoRows {
		log.Println("Error querying database:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error"})
		return
	}
	// Tạo mã xác minh mới và lưu vào cơ sở dữ liệu
	verificationCode, err := models.GenerateVerificationCode(6)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to generate verification code"})
		return
	}
	_, err = db.Exec("INSERT INTO verificationcode (EMAIL, CODE,PASSWORD) VALUES (?, ?,?)", newUser.EMAIL, verificationCode, newUser.PASSWORD)
	if err != nil {
		log.Println("Error hashing password:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save verification code to database"})
		return
	}

	// Gửi email chứa mã xác minh đến địa chỉ email của người dùng
	if err := models.SendVerificationEmail(newUser.EMAIL, verificationCode); err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to send verification email"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "An email with verification code has been sent to your email address"})
	// Xóa mã xác minh sau 60 giây
	go func() {
		time.Sleep(60 * time.Second)
		db.Exec("DELETE FROM verificationcode WHERE EMAIL = ?", newUser.EMAIL)
	}()
}

func LoginUser(c *gin.Context) {
	var loginData struct {
		EMAIL    string `json:"EMAIL"`
		PASSWORD string `json:"PASSWORD"`
	}
	if err := c.ShouldBindJSON(&loginData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	// Kiểm tra thông tin đăng nhập
	var user models.User
	err := db.Get(&user, "SELECT * FROM Users WHERE EMAIL = ?", loginData.EMAIL)
	if err != nil {
		if err == sql.ErrNoRows {
			c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid credentials"})
			return
		}
		log.Println("Error querying database:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error"})
		return
	}
	// Kiểm tra mật khẩu
	if err := user.CheckPassword(loginData.PASSWORD); err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid credentials"})
		return
	}

	// Tạo token JWT
	token, err := auth.GenerateJWT(user.EMAIL)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to generate token"})
		return
	}
	//Luu token vao cookie
	c.SetCookie("token", token, 86400, "/", "", true, true)
	c.JSON(http.StatusOK, gin.H{"token": token, "user_id": user.ID})
}

func ForgotPassword(c *gin.Context) {
	var requestData struct {
		EMAIL string `json:"EMAIL"`
	}
	if err := c.ShouldBindJSON(&requestData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	// Kiểm tra xem email tồn tại trong cơ sở dữ liệu
	var user models.User
	err := db.Get(&user, "SELECT * FROM Users WHERE EMAIL = ?", requestData.EMAIL)
	if err != nil {
		if err == sql.ErrNoRows {
			c.JSON(http.StatusNotFound, gin.H{"error": "Email not found"})
			return
		}
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error"})
		return
	}
	// Câu lệnh SQL để tạo bảng verificationcode
	createTableSQL := `
			CREATE TABLE IF NOT EXISTS verificationcode_forgot (
				EMAIL TEXT NOT NULL,
				CODE TEXT NOT NULL,
				PRIMARY KEY (EMAIL)
			);
		`

	// Thực thi câu lệnh SQL để tạo bảng
	_, err = db.Exec(createTableSQL)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to create table verificationcode_forgot"})
		return
	}

	// Tạo mã xác minh mới và lưu vào cơ sở dữ liệu
	verificationCode, err := models.GenerateVerificationCode(6)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to generate verification code"})
		return
	}
	_, err = db.Exec("INSERT INTO verificationcode_forgot (EMAIL, CODE) VALUES (?, ?)", requestData.EMAIL, verificationCode)
	if err != nil {
		log.Fatalf("Error loading .env file: %v", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save verification code to database"})
		return
	}
	// Xóa mã xác minh sau 60 giây
	go func() {
		time.Sleep(60 * time.Second)
		db.Exec("DELETE FROM verificationcode_forgot WHERE EMAIL = ?", requestData.EMAIL)
	}()
	// Gửi email chứa mã xác minh đến địa chỉ email của người dùng
	if err := models.SendVerificationEmail(requestData.EMAIL, verificationCode); err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to send verification email"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "An email with verification code has been sent to your email address"})

}

func ConfirmVerificationCode(c *gin.Context) {
	var requestData struct {
		EMAIL string `json:"EMAIL"`
		CODE  string `json:"CODE"`
	}
	if err := c.ShouldBindJSON(&requestData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Kiểm tra xem mã code có hợp lệ không
	var savedCode string
	err := db.Get(&savedCode, "SELECT CODE FROM verificationcode_forgot WHERE EMAIL = ?", requestData.EMAIL)
	if err != nil {
		if err == sql.ErrNoRows {
			c.JSON(http.StatusNotFound, gin.H{"error": "Verification code not found"})
			return
		}
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error"})
		return
	}
	if savedCode != requestData.CODE {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid verification code"})
		return
	}

	// Xác nhận mã code thành công, cho phép đặt lại mật khẩu và gửi mật khẩu mới vào email
	// Điều này có thể làm bằng cách gọi một hàm khác để đặt lại mật khẩu và gửi email.
	// Ví dụ:
	newPassword, err := models.GenerateRandomPassword(16) // Đảm bảo tạo mật khẩu mạnh và ngẫu nhiên
	if err != nil {
		// Xử lý lỗi, như ghi log hoặc trả về phản hồi lỗi
		log.Println("Lỗi khi tạo mật khẩu ngẫu nhiên:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Không thể tạo mật khẩu ngẫu nhiên"})
		return
	}
	if err := models.ResetPasswordAndSendEmail(db, requestData.EMAIL, newPassword); err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to reset password and send email"})
		return
	}

	// Ở đây, chúng ta chỉ trả về một thông báo thành công
	c.JSON(http.StatusOK, gin.H{"message": "Verification code confirmed successfully"})
}

func ConFirmRegister(c *gin.Context) {
	var requestData struct {
		EMAIL    string `json:"EMAIL"`
		CODE     string `json:"CODE"`
		PASSWORD string `json:"PASSWORD"`
	}
	if err := c.ShouldBindJSON(&requestData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Kiểm tra xem mã code có hợp lệ không
	var savedCode string
	err := db.Get(&savedCode, "SELECT CODE FROM verificationcode WHERE EMAIL = ?", requestData.EMAIL)
	if err != nil {
		if err == sql.ErrNoRows {
			c.JSON(http.StatusNotFound, gin.H{"error": "Verification code not found"})
			return
		}
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error"})
		return
	}
	if savedCode != requestData.CODE {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid verification code"})
		return
	}
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(requestData.PASSWORD), bcrypt.DefaultCost)
	if err != nil {
		log.Println("Error hashing password:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to hash password"})
		return
	}
	//Thêm new user vào database
	_, err = db.Exec("INSERT INTO Users (EMAIL, PASSWORD) VALUES (?, ?)", requestData.EMAIL, hashedPassword)
	if err != nil {
		log.Println("Error querying database:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Database error 2"})
		return
	}

	c.JSON(http.StatusCreated, gin.H{"message": "User registered successfully"})
}
func GetProfileByEmail(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	// Gọi hàm lấy thông tin profile từ models dựa trên email
	profile, err := models.GetProfileByEmail(email, db)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	// Trả về thông tin profile kèm theo avatar
	c.JSON(http.StatusOK, gin.H{"name": profile.NAME.String, "email": profile.EMAIL, "phone": profile.PHONE.String, "avatar": profile.AVATAR})
}

func Update(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	var updateData struct {
		Name  string `json:"name"`
		Phone string `json:"phone"`
	}
	if err := c.ShouldBindJSON(&updateData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Gọi hàm cập nhật tên trong models
	err = models.UpdateName(email, db, updateData.Name)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}
	err = models.UpdatePhone(email, db, updateData.Phone)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Name updated successfully"})
}

func UpdatePassword(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	var updateData struct {
		CurrentPassword string `json:"current_password"`
		NewPassword     string `json:"new_password"`
	}
	if err := c.ShouldBindJSON(&updateData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Gọi hàm cập nhật mật khẩu trong models, đi kèm với kiểm tra mật khẩu hiện tại
	err = models.UpdatePassword(email, db, updateData.CurrentPassword, updateData.NewPassword)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Password updated successfully"})
}

// UpdateAvatar cập nhật avatar của user
func UpdateAvatar(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	var requestData struct {
		Avatar string `json:"avatar"`
	}
	if err := c.ShouldBindJSON(&requestData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Gọi hàm cập nhật avatar trong models
	err = models.UpdateAvatar(email, db, requestData.Avatar)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Avatar updated successfully"})
}

// ////////////////////////PLAYLIST/////////////////////////////////////
func SaveNamePlaylist(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	// Lấy dữ liệu cập nhật từ request body
	var updateData struct {
		NAME string `json:"name"`
	}
	if err := c.ShouldBindJSON(&updateData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Gọi hàm cập nhật tên trong models
	err = models.InsertPlaylist(db, email, updateData.NAME)
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Name playlist updated successfully"})
}
func GetAllPlaylistsByEmail(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	// Gọi hàm lấy danh sách playlist từ models dựa trên email
	playlists, err := models.GetPlaylistsByEmail(email, db)
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}
	log.Print(playlists)
	// Trả về danh sách playlist
	c.JSON(http.StatusOK, playlists)
}
func GetNewPlaylistInserted(c *gin.Context) { // Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	// Gọi hàm lấy danh sách playlist từ models dựa trên email
	playlists, err := models.GetPlaylistNewest(email, db)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	// Trả về danh sách playlist
	c.JSON(http.StatusOK, playlists)
}
func GetDetailPlaylistByID(c *gin.Context) {
	// Kiểm tra token trong cookie
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	// Lấy ID từ query string
	idStr := c.Query("ID")
	id, err := strconv.Atoi(idStr)
	if err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": "Invalid ID format"})
		return
	}

	// Gọi hàm lấy chi tiết playlist từ models dựa trên email và ID
	playlist, err := models.GetDetailPlaylistByID(email, id, db)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}
	log.Print(playlist)
	// Trả về chi tiết playlist
	c.JSON(http.StatusOK, playlist)
}
func DeletePlaylistByID(c *gin.Context) {
	// Kiểm tra token trong cookie
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	// Lấy ID từ query string
	idStr := c.Query("ID")
	id, err := strconv.Atoi(idStr)
	if err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": "Invalid ID format"})
		return
	}

	// Gọi hàm xóa playlist từ models dựa trên email và ID
	err = models.DeletePlaylistByID(email, id, db)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	// Trả về phản hồi thành công
	c.JSON(http.StatusOK, gin.H{"message": "Playlist deleted successfully"})
}

func UpdateNamePlaylist(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	var updateData struct {
		PlaylistID string `json:"playlist_id"`
		NewName    string `json:"new_name"`
	}
	if err := c.ShouldBindJSON(&updateData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Gọi hàm cập nhật tên playlist trong models
	err = models.UpdatePlaylistName(email, db, updateData.PlaylistID, updateData.NewName)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Playlist name updated successfully"})
}

func AddTrackToPlaylistHandler(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	var req AddTrackToPlaylistRequest
	// Decode JSON request
	if err := c.ShouldBindJSON(&req); err != nil {
		log.Println("Error decoding JSON:", err)
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	var playlistID, trackID string

	// Check if the playlist exists for the given user
	err = db.QueryRow("SELECT ID FROM Playlist WHERE NAME = ? AND EMAIL = ?", req.PlaylistName, email).Scan(&playlistID)
	if err == sql.ErrNoRows {
		// Playlist does not exist, create it
		result, err := db.Exec("INSERT INTO Playlist (NAME, EMAIL, PLAYLIST_IMAGE) VALUES (?, ?, ?)", req.PlaylistName, email, req.Image)
		if err != nil {
			log.Println("Error inserting playlist:", err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}
		playlistID64, _ := result.LastInsertId()
		playlistID = strconv.FormatInt(playlistID64, 10)
	} else if err != nil {
		log.Println("Error checking playlist existence:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	} else {
		// Playlist exists, update the playlist image
		_, err = db.Exec("UPDATE Playlist SET PLAYLIST_IMAGE = ? WHERE ID = ?", req.Image, playlistID)
		if err != nil {
			log.Println("Error updating playlist image:", err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
			return
		}
	}

	// Check if the track exists
	err = db.QueryRow("SELECT ID FROM Tracks WHERE NAME = ?", req.SongName).Scan(&trackID)
	if err == sql.ErrNoRows {
		log.Println("Track not found:", req.SongName)
		c.JSON(http.StatusNotFound, gin.H{"error": "Track not found"})
		return
	} else if err != nil {
		log.Println("Error checking track existence:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	// Insert into Track_Playlist
	_, err = db.Exec("INSERT INTO Track_Playlist (Playlist_id, Track_id) VALUES (?, ?)", playlistID, trackID)
	if err != nil {
		log.Println("Error inserting into Track_Playlist:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Track added to playlist successfully"})
}
func RemoveTrackFromPlaylistHandler(c *gin.Context) {
	// Kiểm tra xem token có tồn tại trong cookie không
	tokenString, err := c.Cookie("token")
	if err != nil {
		log.Print(err)
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
		c.Abort()
		return
	}

	// Trích xuất email từ token
	email, err := auth.ExtractEmailFromToken(tokenString)
	if err != nil {
		c.JSON(http.StatusUnauthorized, gin.H{"error": "Invalid token"})
		return
	}

	var req struct {
		PlaylistID string `json:"playlistID"`
		TrackID    string `json:"trackID"`
	}
	// Decode JSON request
	if err := c.ShouldBindJSON(&req); err != nil {
		log.Println("Error decoding JSON:", err)
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Kiểm tra xem playlist có tồn tại và thuộc về user không
	var playlistID string
	err = db.QueryRow("SELECT ID FROM Playlist WHERE ID = ? AND EMAIL = ?", req.PlaylistID, email).Scan(&playlistID)
	if err == sql.ErrNoRows {
		log.Println("Playlist not found:", req.PlaylistID)
		c.JSON(http.StatusNotFound, gin.H{"error": "Playlist not found"})
		return
	} else if err != nil {
		log.Println("Error checking playlist existence:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	// Xóa track khỏi playlist
	_, err = db.Exec("DELETE FROM Track_Playlist WHERE Playlist_id = ? AND Track_id = ?", req.PlaylistID, req.TrackID)
	if err != nil {
		log.Println("Error deleting track from playlist:", err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Track removed from playlist successfully"})
}
