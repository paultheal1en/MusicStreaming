// chứa các file có nhiệm vụ thao tác với MongoDB (connect cơ sở dữ liệu ).
package database

import (
	"log"

	"github.com/jmoiron/sqlx"
	_ "github.com/mattn/go-sqlite3"
)

func ConnectDB() (*sqlx.DB, error) {
	dsn := "file:E:/NT106/finalproject/Project/NT106/NT106/Database/musicstreaming.db" // nhớ thay đổi địa chỉ file sqlite3
	db, err := sqlx.Connect("sqlite3", dsn)
	if err != nil {
		log.Fatalln("Cannot connect to sqlite3:", err)
		return nil, err
	}
	return db, nil
}
