package middlewares

import (
	"Music/auth"
	"log"
	"net/http"

	//Tạo token JWT
	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv" //để sử dụng biến môi trường trong .env
	_ "github.com/mattn/go-sqlite3"
	// Gửi gmail
)

func AuthMiddleware() gin.HandlerFunc {
	err := godotenv.Load()
	if err != nil {
		log.Fatalf("Error getting env, %v", err)
	}
	return func(c *gin.Context) {
		// Lấy token từ cookie
		tokenString, err := c.Cookie("token")
		if err != nil {
			log.Print(err)
			c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized1"})
			c.Abort()
			return
		}
		// Xuất token ra màn hình để kiểm tra
		log.Println("Token:", tokenString)

		// Kiểm tra tính hợp lệ của token
		err = auth.ValidateToken(tokenString)
		if err != nil {
			c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized2"})
			c.Abort()
			return
		}

		// Nếu token hợp lệ, tiếp tục xử lý tiếp theo
		c.Next()
	}
}
