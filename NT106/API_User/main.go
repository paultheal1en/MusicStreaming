package main

import (
	"Music/controller"
	"Music/middlewares"
	"log"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	_ "github.com/mattn/go-sqlite3"
)

func main() {
	err := godotenv.Load()
	if err != nil {
		log.Fatalf("Error getting env, %v", err)
	}

	router := gin.Default()
	v1 := router.Group("/v1")
	{
		v1.POST("/Login", controller.LoginUser)
		v1.POST("/Register", controller.RegisterUser)
		v1.POST("/Register/Confirm", controller.ConFirmRegister)
		v1.POST("/Forgot", controller.ForgotPassword)
		v1.POST("/ConfirmVerificationCode", controller.ConfirmVerificationCode)
		secured := v1.Group("/secured").Use(middlewares.AuthMiddleware())
		{
			secured.GET("/ping", controller.Ping)
		}
		v1.GET("/profiles/profile", middlewares.AuthMiddleware(), controller.GetProfileByEmail)
		v1.PUT("/profiles/update", middlewares.AuthMiddleware(), controller.Update)
		v1.PUT("/profiles/password", middlewares.AuthMiddleware(), controller.UpdatePassword)
		v1.PUT("/profiles/avatar", middlewares.AuthMiddleware(), controller.UpdateAvatar)
		//spotifyController
		v1.GET("/DatamusicTrack", controller.GetDataMusic)
		v1.GET("/DatamusicAlbum", controller.GetDataMusicAlbum)
		v1.GET("/DatamusicArtist", controller.GetDataMusicArtist)
		//playlistController
		v1.POST("/playlist/addname", middlewares.AuthMiddleware(), controller.SaveNamePlaylist)
		v1.GET("/playlist/getall", middlewares.AuthMiddleware(), controller.GetAllPlaylistsByEmail)
		v1.GET("/playlist/getnewinserted", middlewares.AuthMiddleware(), controller.GetNewPlaylistInserted)
		v1.GET("/playlist/getdetailbyid", middlewares.AuthMiddleware(), controller.GetDetailPlaylistByID)
		v1.PUT("/playlist/updatename", middlewares.AuthMiddleware(), controller.UpdateNamePlaylist)
		v1.DELETE("/playlist/deletebyid", middlewares.AuthMiddleware(), controller.DeletePlaylistByID)
		v1.POST("/playlist/AddTrackToPlaylist", middlewares.AuthMiddleware(), controller.AddTrackToPlaylistHandler)

		//getdataController
		v1.POST("/Track", controller.GetTrack)
		v1.GET("/Top10Tracks", controller.GetTop10TracksRandom)
		v1.GET("/Top10Albums", controller.GetTop10AlbumsByTotalTracks)
		v1.GET("/Top10Artists", controller.GetTop10RandomArtists)
		//rankmusic + album +artist
		v1.POST("/Album", controller.GetAlbum)
		v1.POST("/Artist", controller.GetArtist)
		v1.GET("/Top5Tracks", controller.GetTop5Tracks)
		//tracktoplaylist
		v1.GET("/playlist/tracks", controller.GetTracksByPlaylistID)
		v1.POST("/playlist/removetrack", controller.RemoveTrackFromPlaylistHandler)
		//Search keyword name
		v1.POST("/Search/Tracks", controller.GetSeachTop10Tracks)
		v1.POST("/Search/Albums", controller.GetSeachTop5Album)
		v1.POST("/Search/Artists", controller.GetSearchTop5Artist)
		//Get tracks by artist name, album name
		v1.GET("/Artist/Tracks/:name", controller.GetTracksByArtistName)
		v1.GET("/Album/Tracks/:name", controller.GetTracksByAlbumName)
		v1.POST("/Track/increment/:nametrack", controller.IncrementListenCount)
	}
	router.Run(":9999")
}
