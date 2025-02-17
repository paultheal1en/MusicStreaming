// định nghĩa là cấu trúc dữ liệu sẽ được lưu vào collection.
package models

import (

	//Tạo token JWT

	//để sử dụng biến môi trường trong .env

	"fmt"

	"github.com/jmoiron/sqlx"
	_ "github.com/mattn/go-sqlite3"
)

type Playlist struct {
	ID             int    `json:"ID" db:"ID"`
	EMAIL          string `json:"EMAIL" db:"EMAIL"`
	NAME           string `json:"NAME" db:"NAME"`
	PLAYLIST_IMAGE string `json:"PLAYLIST_IMAGE" db:"PLAYLIST_IMAGE"`
}

func InsertPlaylist(db *sqlx.DB, email string, playlistName string) error {
	// Chuẩn bị câu lệnh SQL để chèn dữ liệu vào cơ sở dữ liệu
	query := `INSERT INTO Playlist (EMAIL, NAME) VALUES (?, ?)`

	// Thực thi câu lệnh SQL với các tham số được truyền vào
	_, err := db.Exec(query, email, playlistName)
	if err != nil {
		return err
	}

	return nil
}
func GetPlaylistsByEmail(email string, db *sqlx.DB) ([]Playlist, error) {
	// Chuẩn bị câu lệnh SQL để lấy danh sách playlist dựa trên email
	query := `SELECT * FROM Playlist WHERE EMAIL = ?`

	// Thực thi câu lệnh SQL với email được truyền vào và lấy danh sách kết quả
	var playlists []Playlist
	err := db.Select(&playlists, query, email)
	if err != nil {
		return nil, err
	}

	return playlists, nil
}
func GetPlaylistNewest(email string, db *sqlx.DB) (*Playlist, error) {
	// Chuẩn bị câu lệnh SQL để lấy playlist mới nhất dựa trên email
	query := `SELECT * FROM Playlist WHERE EMAIL = ? ORDER BY ID DESC LIMIT 1`

	// Thực thi câu lệnh SQL với email được truyền vào và lấy kết quả
	var playlist Playlist
	err := db.Get(&playlist, query, email)
	if err != nil {
		return nil, err
	}

	return &playlist, nil
}
func GetDetailPlaylistByID(email string, id int, db *sqlx.DB) (*Playlist, error) {
	// Chuẩn bị câu lệnh SQL để lấy chi tiết playlist dựa trên email và ID
	query := `SELECT * FROM Playlist WHERE EMAIL = ? AND ID = ?`

	// Thực thi câu lệnh SQL với email và ID được truyền vào và lấy kết quả
	var playlist Playlist
	err := db.Get(&playlist, query, email, id)
	if err != nil {
		return nil, err
	}

	return &playlist, nil
}
func UpdatePlaylistName(email string, db *sqlx.DB, playlistID, newName string) error {
	// Chuẩn bị câu lệnh SQL để cập nhật tên playlist dựa trên email và ID của playlist
	query := `UPDATE Playlist SET NAME = ? WHERE EMAIL = ? AND ID = ?`

	// Thực thi câu lệnh SQL với các tham số được truyền vào
	_, err := db.Exec(query, newName, email, playlistID)
	if err != nil {
		return err
	}

	return nil
}
func DeletePlaylistByID(email string, id int, db *sqlx.DB) error {
	// Bắt đầu một giao dịch
	tx, err := db.Beginx()
	if err != nil {
		return fmt.Errorf("could not begin transaction: %v", err)
	}

	// Chuẩn bị câu lệnh SQL để xóa các bản ghi trong bảng Track_Playlist liên quan đến playlist
	deleteTrackPlaylistQuery := `DELETE FROM Track_Playlist WHERE Playlist_id = ?`

	// Thực thi câu lệnh SQL với ID của playlist
	_, err = tx.Exec(deleteTrackPlaylistQuery, id)
	if err != nil {
		tx.Rollback()
		return fmt.Errorf("could not delete from Track_Playlist: %v", err)
	}

	// Chuẩn bị câu lệnh SQL để xóa playlist dựa trên email và ID
	deletePlaylistQuery := `DELETE FROM Playlist WHERE EMAIL = ? AND ID = ?`

	// Thực thi câu lệnh SQL với email và ID được truyền vào
	result, err := tx.Exec(deletePlaylistQuery, email, id)
	if err != nil {
		tx.Rollback()
		return fmt.Errorf("could not delete playlist: %v", err)
	}

	// Kiểm tra số hàng bị ảnh hưởng
	rowsAffected, err := result.RowsAffected()
	if err != nil {
		tx.Rollback()
		return fmt.Errorf("could not retrieve affected rows: %v", err)
	}

	// Nếu không có hàng nào bị xóa, trả về lỗi
	if rowsAffected == 0 {
		tx.Rollback()
		return fmt.Errorf("no playlist found with the given ID and email")
	}

	// Commit giao dịch
	if err := tx.Commit(); err != nil {
		return fmt.Errorf("could not commit transaction: %v", err)
	}

	return nil
}
