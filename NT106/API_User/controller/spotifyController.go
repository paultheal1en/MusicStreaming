package controller

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
	_ "github.com/mattn/go-sqlite3"
	"github.com/zmb3/spotify"
	"golang.org/x/oauth2/clientcredentials"
)

const (
	clientID     = "cf1f481809f247f6a167f5765cd34217"
	clientSecret = "6ee7ef7f340444bcb7f6648370030003"
)

func GetDataMusic(c *gin.Context) {
	// Parse dữ liệu JSON từ yêu cầu và lưu vào biến Track
	var Track struct {
		ID string `json:"ID"`
	}
	if err := c.ShouldBindJSON(&Track); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Kiểm tra sự tồn tại của track trong cơ sở dữ liệu
	var trackExists bool
	err := db.QueryRow("SELECT EXISTS (SELECT 1 FROM Tracks WHERE id = ?)", Track.ID).Scan(&trackExists)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check track existence"})
		return
	}
	if trackExists {
		c.JSON(http.StatusBadRequest, gin.H{"error": "Track ID already exists"})
		return
	}

	// Khởi tạo cấu hình OAuth
	config := &clientcredentials.Config{
		ClientID:     clientID,
		ClientSecret: clientSecret,
		TokenURL:     spotify.TokenURL,
	}
	client := config.Client(context.Background())

	client.Timeout = 5 * time.Second

	auth := spotify.NewAuthenticator("")
	token, err := config.Token(context.Background())
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve token"})
		return
	}
	spotifyClient := auth.NewClient(token)

	track, err := spotifyClient.GetTrack(spotify.ID(Track.ID))
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve track information"})
		return
	}

	var imageUrl string
	if len(track.Album.Images) > 0 {
		imageUrl = track.Album.Images[0].URL
	}
	artistID := track.Artists[0].ID
	artist, err := spotifyClient.GetArtist(artistID)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve artist information"})
		return
	}
	duration := fmt.Sprintf("%d:%02d", track.Duration/60000, (track.Duration/1000)%60)
	_, err = db.Exec("INSERT INTO Tracks (id,id_album,id_artist,name,duration,image,fs_path) VALUES (?,?,?,?,?,?,?)", track.ID, track.Album.ID, artist.ID, track.Name, duration, imageUrl, track.PreviewURL)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data track to database"})
		return
	}
	album, err := spotifyClient.GetAlbum(spotify.ID(track.Album.ID))
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve track information"})
		return
	}
	_, err = db.Exec("INSERT INTO Albums (id,name,album_images,total_tracks,release_date,id_artist) VALUES (?,?,?,?,?,?)", album.ID, album.Name, album.Images[0].URL, album.Tracks.Total, album.ReleaseDate, album.Artists[0].ID)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data album to database"})
		return
	}
	_, err = db.Exec("INSERT INTO Artists (id,name,image) VALUES (?,?,?)", artist.ID, artist.Name, artist.Images[0].URL)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data artist to database"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"response": "Lưu thành công"})
}
func GetDataMusicAlbum(c *gin.Context) {
	// Parse dữ liệu JSON từ yêu cầu và lưu vào biến Album
	var Album struct {
		ID string `json:"ID"`
	}
	if err := c.ShouldBindJSON(&Album); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	// Khởi tạo cấu hình OAuth2
	config := &clientcredentials.Config{
		ClientID:     clientID,
		ClientSecret: clientSecret,
		TokenURL:     spotify.TokenURL,
	}
	client := config.Client(context.Background())

	client.Timeout = 5 * time.Second

	auth := spotify.NewAuthenticator("")
	token, err := config.Token(context.Background())
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve token"})
		return
	}
	spotifyClient := auth.NewClient(token)

	album, err := spotifyClient.GetAlbum(spotify.ID(Album.ID))
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve album information"})
		return
	}

	// Kiểm tra xem album đã tồn tại trong cơ sở dữ liệu chưa
	var albumExists bool
	err = db.QueryRow("SELECT EXISTS (SELECT 1 FROM Albums WHERE id = ?)", Album.ID).Scan(&albumExists)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check album existence"})
		return
	}
	if !albumExists {
		// Nếu album chưa tồn tại, thêm album vào cơ sở dữ liệu
		_, err = db.Exec("INSERT INTO Albums (id,name,album_images,total_tracks,release_date,id_artist) VALUES (?,?,?,?,?,?)", album.ID, album.Name, album.Images[0].URL, album.Tracks.Total, album.ReleaseDate, album.Artists[0].ID)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data album to database"})
			return
		}
	}

	for _, track := range album.Tracks.Tracks {
		// Kiểm tra xem track đã tồn tại trong cơ sở dữ liệu chưa
		var trackExists bool
		err := db.QueryRow("SELECT EXISTS (SELECT 1 FROM Tracks WHERE id = ?)", track.ID).Scan(&trackExists)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check track existence"})
			continue // Tiếp tục với track tiếp theo nếu có lỗi
		}
		if trackExists {
			// Nếu track đã tồn tại, bỏ qua và tiếp tục với track tiếp theo
			continue
		}

		// Lấy thông tin về nghệ sĩ
		artistID := track.Artists[0].ID
		var artistExists bool
		err = db.QueryRow("SELECT EXISTS (SELECT 1 FROM Artists WHERE id = ?)", artistID).Scan(&artistExists)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check artist existence"})
			continue
		}
		if !artistExists {
			// Nếu nghệ sĩ chưa tồn tại, thêm thông tin của nghệ sĩ vào cơ sở dữ liệu
			artist, err := spotifyClient.GetArtist(artistID)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve artist information"})
				continue
			}
			_, err = db.Exec("INSERT INTO Artists (id,name,image) VALUES (?,?,?)", artist.ID, artist.Name, artist.Images[0].URL)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data artist to database"})
				continue
			}
		}

		var imageUrl string
		if len(album.Images) > 0 {
			imageUrl = album.Images[0].URL
		}

		duration := fmt.Sprintf("%d:%02d", track.Duration/60000, (track.Duration/1000)%60)

		// Thêm thông tin về track vào cơ sở dữ liệu
		_, err = db.Exec("INSERT INTO Tracks (id,id_album,id_artist,name,duration,image,fs_path) VALUES (?,?,?,?,?,?,?)", track.ID, Album.ID, artistID, track.Name, duration, imageUrl, track.PreviewURL)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data track to database"})
			continue // Tiếp tục với track tiếp theo nếu có lỗi
		}
	}

	c.JSON(http.StatusOK, gin.H{"response": "Lưu thành công"})
}

func GetDataMusicArtist(c *gin.Context) {
	// Parse dữ liệu JSON từ yêu cầu và lưu vào biến Artist
	var Artist struct {
		ID string `json:"ID"`
	}
	if err := c.ShouldBindJSON(&Artist); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}
	// Khởi tạo cấu hình OAuth2
	config := &clientcredentials.Config{
		ClientID:     clientID,
		ClientSecret: clientSecret,
		TokenURL:     spotify.TokenURL,
	}
	client := config.Client(context.Background())

	client.Timeout = 5 * time.Second

	auth := spotify.NewAuthenticator("")
	token, err := config.Token(context.Background())
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve token"})
		return
	}
	spotifyClient := auth.NewClient(token)

	// Lấy danh sách album của nghệ sĩ
	albums, err := spotifyClient.GetArtistAlbums(spotify.ID(Artist.ID))
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve artist's albums"})
		return
	}

	for _, album := range albums.Albums {
		// Kiểm tra xem album đã tồn tại trong cơ sở dữ liệu chưa
		var albumExists bool
		err := db.QueryRow("SELECT EXISTS (SELECT 1 FROM Albums WHERE id = ?)", album.ID).Scan(&albumExists)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check album existence"})
			continue // Tiếp tục với album tiếp theo nếu có lỗi
		}
		if !albumExists {
			tracks, err := spotifyClient.GetAlbumTracks(album.ID)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check track existence"})
				return
			}
			// Đếm số lượng track trong danh sách
			totalTracks := len(tracks.Tracks)
			// Nếu album chưa tồn tại, thêm album vào cơ sở dữ liệu
			_, err = db.Exec("INSERT INTO Albums (id,name,album_images,total_tracks,release_date,id_artist) VALUES (?,?,?,?,?,?)", album.ID, album.Name, album.Images[0].URL, totalTracks, album.ReleaseDate, album.Artists[0].ID)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data album to database"})
				continue // Tiếp tục với album tiếp theo nếu có lỗi
			}
		}

		// Lấy thông tin về nghệ sĩ
		artistID := album.Artists[0].ID // Lấy ID của nghệ sĩ từ album
		var artistExists bool
		err = db.QueryRow("SELECT EXISTS (SELECT 1 FROM Artists WHERE id = ?)", artistID).Scan(&artistExists)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check artist existence"})
			continue
		}
		if !artistExists {
			// Nếu nghệ sĩ chưa tồn tại, thêm thông tin của nghệ sĩ vào cơ sở dữ liệu
			artist, err := spotifyClient.GetArtist(artistID)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve artist information"})
				continue
			}
			_, err = db.Exec("INSERT INTO Artists (id,name,image) VALUES (?,?,?)", artist.ID, artist.Name, artist.Images[0].URL)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data artist to database"})
				continue
			}
		}

		// Lấy danh sách các bài hát trong album
		tracks, err := spotifyClient.GetAlbumTracks(album.ID)
		if err != nil {
			log.Println(err)
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve tracks from album"})
			continue
		}

		for _, track := range tracks.Tracks {
			// Kiểm tra xem track đã tồn tại trong cơ sở dữ liệu chưa
			var trackExists bool
			err := db.QueryRow("SELECT EXISTS (SELECT 1 FROM Tracks WHERE id = ?)", track.ID).Scan(&trackExists)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to check track existence"})
				continue // Tiếp tục với track tiếp theo nếu có lỗi
			}
			if trackExists {
				// Nếu track đã tồn tại, bỏ qua và tiếp tục với track tiếp theo
				continue
			}

			var imageUrl string
			if len(album.Images) > 0 {
				imageUrl = album.Images[0].URL
			}

			duration := fmt.Sprintf("%d:%02d", track.Duration/60000, (track.Duration/1000)%60)
			artistID := track.Artists[0].ID
			artist, err := spotifyClient.GetArtist(artistID)
			if err != nil {
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to retrieve artist information"})
				return
			}
			// Thêm thông tin về track vào cơ sở dữ liệu
			_, err = db.Exec("INSERT INTO Tracks (id,id_album,id_artist,name,duration,image,fs_path) VALUES (?,?,?,?,?,?,?)", track.ID, album.ID, artist.ID, track.Name, duration, imageUrl, track.PreviewURL)
			if err != nil {
				log.Println(err)
				c.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to save data track to database"})
				continue // Tiếp tục với track tiếp theo nếu có lỗi
			}
		}
	}

	c.JSON(http.StatusOK, gin.H{"response": "Lưu thành công"})
}
