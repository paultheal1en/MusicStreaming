package controller

import (
	"database/sql"
	"log"
	"net/http"

	"github.com/gin-gonic/gin"
)

type Track struct {
	ID           string         `json:"ID" db:"id"`
	ID_ALBUM     string         `json:"ID_ALBUM" db:"id_album"`
	ID_ARTIST    string         `json:"ID_ARTIST" db:"id_artist"`
	NAME         string         `json:"NAME" db:"name"`
	FS_PATH      sql.NullString `json:"FS_PATH" db:"fs_path"`
	DURATION     sql.NullString `json:"DURATION" db:"duration"`
	LISTEN_COUNT sql.NullInt64  `json:"LISTEN_COUNT" db:"listen_count"`
	IMAGE        sql.NullString `json:"IMAGE" db:"image"`
	ALBUM_NAME   string         `json:"ALBUM_NAME" db:"album_name"`   // Thêm trường ALBUM_NAME
	ARTIST_NAME  string         `json:"ARTIST_NAME" db:"artist_name"` // Thêm trường ARTIST_NAME
}

type Album struct {
	ID           string         `json:"ID" db:"id"`
	ID_ARTIST    string         `json:"ID_ARTIST" db:"id_artist"`
	NAME         string         `json:"NAME" db:"name"`
	TOTAL_TRACKS sql.NullInt64  `json:"TOTAL_TRACKS" db:"total_tracks"`
	RELEASE_DATE sql.NullString `json:"RELEASE_DATE" db:"release_date"`
	IMAGE        sql.NullString `json:"IMAGE" db:"album_images"`
	ARTIST_NAME  string         `json:"ARTIST_NAME" db:"artist_name"` // Thêm trường ARTIST_NAME
}
type Artist struct {
	ID    string         `json:"ID" db:"id"`
	NAME  string         `json:"NAME" db:"name"`
	IMAGE sql.NullString `json:"IMAGE" db:"image"`
}

type Track_Playlist struct {
	PLAYLISTID string `json:"PLAYLIST_ID" db:"Playlist_id"`
	TRACKID    string `json:"TRACK_ID" db:"Track_id"`
	ORDER      string `json:"ORDER" db:"Order"`
}
type AddTrackToPlaylistRequest struct {
	PlaylistName string `json:"PlaylistName"`
	SongName     string `json:"SongName"`
	Image        string `json:"Image"`
}
type Playlist struct {
	ID             int    `json:"ID" db:"ID"`
	EMAIL          string `json:"EMAIL" db:"EMAIL"`
	NAME           string `json:"NAME" db:"NAME"`
	PLAYLIST_IMAGE string `json:"PLAYLIST_IMAGE" db:"PLAYLIST_IMAGE"`
}

func GetTrack(c *gin.Context) {
	// Lấy ID từ JSON
	var track Track
	if err := c.ShouldBindJSON(&track); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	var newTrack Track
	// Truy vấn dữ liệu track
	err := db.Get(&newTrack, "SELECT Tracks.*, Albums.name AS album_name, Artists.name AS artist_name "+
		"FROM Tracks "+
		"JOIN Albums ON Tracks.id_album = Albums.id "+
		"JOIN Artists ON Tracks.id_artist = Artists.id "+
		"WHERE Tracks.id = ?", track.ID)
	if err != nil {
		// Xử lý lỗi truy vấn
		if err == sql.ErrNoRows {
			c.JSON(http.StatusNotFound, gin.H{"error": "Track not found"})
		} else {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve track data"})
		}
		return
	}

	// Trả về thông tin track
	c.JSON(http.StatusOK, newTrack)
}

func GetAlbum(c *gin.Context) {
	// Lấy ID từ JSON
	var album Album
	if err := c.ShouldBindJSON(&album); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	var newAlbum Album
	// Truy vấn dữ liệu album
	err := db.Get(&newAlbum, "SELECT Albums.*, Artists.name AS artist_name "+
		"FROM Albums "+
		"JOIN Artists ON Albums.id_artist = Artists.id "+
		"WHERE Albums.id = ?", album.ID)
	if err != nil {
		// Xử lý lỗi truy vấn
		if err == sql.ErrNoRows {
			c.JSON(http.StatusNotFound, gin.H{"error": "Album not found"})
		} else {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve album data"})
		}
		return
	}

	// Trả về thông tin album
	c.JSON(http.StatusOK, newAlbum)
}

func GetArtist(c *gin.Context) {
	// Lấy ID từ JSON
	var artist Artist
	if err := c.ShouldBindJSON(&artist); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	var newArtist Artist
	// Truy vấn dữ liệu artist
	err := db.Get(&newArtist, "SELECT * FROM Artists WHERE id = ?", artist.ID)
	if err != nil {
		// Xử lý lỗi truy vấn
		if err == sql.ErrNoRows {
			c.JSON(http.StatusNotFound, gin.H{"error": "Artist not found"})
		} else {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve Artist data"})
		}
		return
	}

	// Trả về thông tin artist
	c.JSON(http.StatusOK, newArtist)
}
func GetTop10TracksRandom(c *gin.Context) {
	var topTracks []Track
	// Truy vấn dữ liệu top 10 track cùng thông tin về nghệ sĩ và album
	query := `
        SELECT 
            Tracks.*, 
            Albums.name AS album_name, 
            Artists.name AS artist_name 
        FROM Tracks 
        JOIN Albums ON Tracks.id_album = Albums.id 
        JOIN Artists ON Tracks.id_artist = Artists.id 
		ORDER BY RANDOM() LIMIT 10
    `
	err := db.Select(&topTracks, query)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top tracks data"})
		return
	}
	// Trả về top 10 track cùng thông tin nghệ sĩ và album
	c.JSON(http.StatusOK, topTracks)
}
func GetTop10AlbumsByTotalTracks(c *gin.Context) {
	var topAlbums []Album
	// Truy vấn dữ liệu top 10 album cùng thông tin về nghệ sĩ
	query := `
        SELECT 
            Albums.*, 
            Artists.name AS artist_name 
        FROM Albums 
        JOIN Artists ON Albums.id_artist = Artists.id 
        ORDER BY Albums.total_tracks DESC 
        LIMIT 10
    `
	err := db.Select(&topAlbums, query)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top albums data"})
		return
	}
	// Trả về top 10 album cùng thông tin nghệ sĩ
	c.JSON(http.StatusOK, topAlbums)
}
func GetTop10RandomArtists(c *gin.Context) {
	var topArtists []Artist
	// Truy vấn dữ liệu top 10 nghệ sĩ ngẫu nhiên
	err := db.Select(&topArtists, "SELECT * FROM Artists ORDER BY RANDOM() LIMIT 10")
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top artists data"})
		return
	}
	// Trả về top 10 nghệ sĩ
	c.JSON(http.StatusOK, topArtists)
}
func GetTop5Tracks(c *gin.Context) {
	var topTracks []Track
	err := db.Select(&topTracks, "SELECT Tracks.*, Albums.name AS album_name, Artists.name AS artist_name "+
		"FROM Tracks "+
		"JOIN Albums ON Tracks.id_album = Albums.id "+
		"JOIN Artists ON Tracks.id_artist = Artists.id "+
		"ORDER BY Tracks.listen_count DESC "+
		"LIMIT 5")

	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top tracks"})
		return
	}

	c.JSON(http.StatusOK, topTracks)
}
func GetTracksByPlaylistID(c *gin.Context) {
	playlistID := c.Query("playlistID")
	if playlistID == "" {
		c.JSON(http.StatusBadRequest, gin.H{"error": "PlaylistID is required"})
		return
	}

	query := `
		SELECT 
			t.id, t.id_album, t.id_artist, t.name, t.fs_path, t.duration, t.listen_count, t.image,
			al.name AS album_name, ar.name AS artist_name
		FROM 
			Track_Playlist tp
		JOIN 
			Tracks t ON tp.Track_id = t.id
		JOIN 
			Albums al ON t.id_album = al.id
		JOIN 
			Artists ar ON t.id_artist = ar.id
		WHERE 
			tp.Playlist_id = ?`

	var tracks []Track
	err := db.Select(&tracks, query, playlistID)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve tracks data"})
		return
	}
	log.Print(tracks)
	c.JSON(http.StatusOK, tracks)
}
func GetSeachTop10Tracks(c *gin.Context) {
	// Lấy keyword từ trường "keyword" trong JSON
	var request struct {
		Keyword string `json:"keyword"`
	}
	if err := c.ShouldBindJSON(&request); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	keyword := request.Keyword

	// Truy vấn ra top 10 bài hát có tên chứa keyword
	var topTracks []Track
	err := db.Select(&topTracks, "SELECT Tracks.*, Albums.name AS album_name, Artists.name AS artist_name "+
		"FROM Tracks "+
		"JOIN Albums ON Tracks.id_album = Albums.id "+
		"JOIN Artists ON Tracks.id_artist = Artists.id "+
		"WHERE Tracks.name LIKE '%' || ? || '%' COLLATE NOCASE "+
		"ORDER BY Tracks.listen_count DESC "+
		"LIMIT 10", keyword)

	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top tracks"})
		return
	}
	c.JSON(http.StatusOK, topTracks)
}

func GetSeachTop5Album(c *gin.Context) {
	// Lấy keyword từ trường "keyword" trong JSON
	var request struct {
		Keyword string `json:"keyword"`
	}
	if err := c.ShouldBindJSON(&request); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	keyword := request.Keyword

	// Truy vấn ra top 5 album có tên chứa keyword
	var topAlbums []Album
	err := db.Select(&topAlbums, "SELECT Albums.*, Artists.name AS artist_name "+
		"FROM Albums "+
		"JOIN Artists ON Albums.id_artist = Artists.id "+
		"WHERE Albums.name LIKE '%' || ? || '%' COLLATE NOCASE "+
		"ORDER BY Albums.total_tracks DESC "+
		"LIMIT 5", keyword)

	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top albums"})
		return
	}
	c.JSON(http.StatusOK, topAlbums)
}
func GetSearchTop5Artist(c *gin.Context) {
	// Lấy keyword từ trường "keyword" trong JSON
	var request struct {
		Keyword string `json:"keyword"`
	}
	if err := c.ShouldBindJSON(&request); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	keyword := request.Keyword

	// Truy vấn ra top 5 nghệ sĩ có tên chứa keyword
	var topArtists []Artist
	err := db.Select(&topArtists, "SELECT * FROM Artists WHERE name LIKE '%' || ? || '%' COLLATE NOCASE ORDER BY RANDOM() LIMIT 5", keyword)

	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve top artists"})
		return
	}
	c.JSON(http.StatusOK, topArtists)
}
func GetTracksByArtistName(c *gin.Context) {
	artistName := c.Param("name")
	var tracks []Track
	err := db.Select(&tracks, "SELECT Tracks.*, Artists.name AS artist_name "+
		"FROM Tracks "+
		"JOIN Artists ON Tracks.id_artist = Artists.id "+
		"WHERE Artists.name = ?", artistName)

	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve tracks by artist name"})
		return
	}
	c.JSON(http.StatusOK, tracks)
}

func GetTracksByAlbumName(c *gin.Context) {
	albumName := c.Param("name")
	var tracks []Track
	err := db.Select(&tracks, "SELECT Tracks.*, Albums.name AS album_name "+
		"FROM Tracks "+
		"JOIN Albums ON Tracks.id_album = Albums.id "+
		"WHERE Albums.name = ?", albumName)

	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve tracks by album name"})
		return
	}
	c.JSON(http.StatusOK, tracks)
}
func IncrementListenCount(c *gin.Context) {
	trackName := c.Param("nametrack")

	result, err := db.Exec("UPDATE Tracks SET listen_count = COALESCE(listen_count, 0) + 1 WHERE name = ?", trackName)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to update listen count"})
		return
	}

	rowsAffected, err := result.RowsAffected()
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to get rows affected"})
		return
	}

	if rowsAffected == 0 {
		c.JSON(http.StatusNotFound, gin.H{"error": "Track not found"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"message": "Listen count increased successfully"})
}
