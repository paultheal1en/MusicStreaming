package auth

import (
	"errors"
	"log"
	"os"
	"time"

	"github.com/dgrijalva/jwt-go"
	"github.com/joho/godotenv"
)

var jwtKey []byte

func init() {
	err := godotenv.Load()
	if err != nil {
		log.Fatal("Error loading .env file")
	}
	jwtKey = []byte(os.Getenv("SECRET_JWT"))
}

type JWTClaim struct {
	Email string `json:"email"`
	jwt.StandardClaims
}

func GenerateJWT(email string) (tokenString string, err error) {

	expirationTime := time.Now().Add(12 * time.Hour)
	claims := &JWTClaim{
		Email: email,
		StandardClaims: jwt.StandardClaims{
			ExpiresAt: expirationTime.Unix(),
		},
	}
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)
	tokenString, err = token.SignedString(jwtKey)
	if err != nil {
		return "", err // Trả về giá trị lỗi nếu có lỗi xảy ra
	}
	return tokenString, nil
}
func ValidateToken(signedToken string) (err error) {
	token, err := jwt.ParseWithClaims(
		signedToken,
		&JWTClaim{},
		func(token *jwt.Token) (interface{}, error) {
			return []byte(jwtKey), nil
		},
	)
	if err != nil {
		return
	}
	claims, ok := token.Claims.(*JWTClaim)
	if !ok {
		err = errors.New("couldn't parse claims")
		return
	}
	if claims.ExpiresAt < time.Now().Local().Unix() {
		err = errors.New("token expired")
		return
	}
	return
}
func ExtractEmailFromToken(signedToken string) (string, error) {
	// Giải mã token và lấy claims
	token, err := jwt.ParseWithClaims(
		signedToken,
		&JWTClaim{},
		func(token *jwt.Token) (interface{}, error) {
			return []byte(jwtKey), nil
		},
	)
	if err != nil {
		return "", err
	}

	// Kiểm tra tính hợp lệ của token
	if !token.Valid {
		return "", errors.New("Invalid token")
	}

	// Trích xuất thông tin email từ claims
	claims, ok := token.Claims.(*JWTClaim)
	if !ok {
		return "", errors.New("Couldn't parse claims")
	}

	// Lấy email từ claims
	email := claims.Email

	return email, nil
}
