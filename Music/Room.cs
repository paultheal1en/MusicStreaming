using AxWMPLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Music
{
    public partial class Room : UserControl
    {
        private HttpClient httpClient;
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private string roomId;
        private string name;
        private string image;
        private string[] paths, files;
        private bool isMicOn = true;

        public Room()
        {
            InitializeComponent();
            ConnectToServer();
        }
        public Room(string roomId)
        {
            InitializeComponent();
            this.roomId = roomId;
            ConnectToServer();
        }

        private async void Room_Load(object sender, EventArgs e)
        {
            await LoadProfile();

            if (string.IsNullOrEmpty(roomId))
            {
                await CreateRoom();
            }
            else
            {
                await JoinRoom(roomId);
            }
            roomId = "ID Room: " + roomId;
            MessageBox.Show(roomId);
            lbRoomID.Text = roomId;
            // Mở thư mục và thêm các tệp vào listTrack
            string directoryPath = "D:\\NT106\\projects\\NT106\\Karaoke(mp4)";
            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetFiles(directoryPath, "*.mp4");
                paths = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    paths[i] = files[i];
                    listTrack.Items.Add(Path.GetFileName(files[i]));
                }
            }
            else
            {
                MessageBox.Show($"Directory {directoryPath} does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadProfile()
        {
            using (var handler = new HttpClientHandler())
            {
                var cookieContainer = new CookieContainer();
                handler.CookieContainer = cookieContainer;

                using (var client = new HttpClient(handler))
                {
                    string token = manageToken.AccessToken;
                    var cookie = new Cookie("token", token, "/", "localhost");

                    cookieContainer.Add(cookie);
                    var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:9999/v1/profiles/profile");
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responeContent = await response.Content.ReadAsStringAsync();
                        var responseObj = JsonConvert.DeserializeObject<dynamic>(responeContent);

                        name = responseObj.name;
                        if (name == null || name == "") name = responseObj.email;
                        image = responseObj.image;
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve profile information: " + response.ReasonPhrase);
                    }
                }
            }
        }
        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888); // Địa chỉ IP và cổng của máy chủ
                stream = client.GetStream();
                receiveThread = new Thread(ListenForServerMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Task.Delay(5000).ContinueWith(t => ConnectToServer());
            }
        }
        private void ListenForServerMessages()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    HandleServerMessage(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while reading from stream: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Handle the exception as needed, possibly close resources
                    break;
                }
            }
        }
        private void HandleServerMessage(string message)
        {
            if (message.StartsWith("JOIN_ROOM_SUCCESS") || message.StartsWith("CREATE_ROOM_SUCCESS"))
            {
                this.roomId = message.Split(' ')[1];
            }
            else if (message.StartsWith("VIDEO_CONTROL"))
            {
                // Xử lý các lệnh điều khiển video
                var jsonData = message.Substring("VIDEO_CONTROL ".Length);
                var controlData = JsonConvert.DeserializeObject<JObject>(jsonData);

                string action = controlData["action"].ToString();
                string path = controlData["videoPath"].ToString();
                int currentTime = controlData["currentTime"].Value<int>();
                // Xử lý thông điệp từ server
                switch (action)
                {
                    case "play":
                        PlayVideoOnClient(path);
                        break;
                    case "pause":
                        player.Ctlcontrols.pause();
                        break;
                    case "seek":
                        SeekVideo(currentTime);
                        break;
                    default:
                        MessageBox.Show("Unknown action received from server: " + action, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else if (message.StartsWith("CHAT_MESSAGE"))
            {
                var jsonData = message.Substring("CHAT_MESSAGE ".Length);
                var controlData = JsonConvert.DeserializeObject<JObject>(jsonData);
                string senderName = controlData["sender"].ToString();
                string chatMessage = controlData["message"].ToString();

                // Update UI with the received chat message
                UpdateChatUI($"{senderName}: {chatMessage}");
            }
            else
            {
                try
                {
                    dynamic data = JsonConvert.DeserializeObject(message);
                    if (data.type == "EXISTING_CLIENT_INFO" || data.type == "NEW_CLIENT_JOIN")
                    {
                        string clientName = data.name;
                        string clientImage = data.image;
                        this.Invoke((MethodInvoker)delegate
                        {
                            // Cập nhật giao diện để hiển thị thông tin client mới hoặc đã tồn tại
                            AddClientToUI(clientName, clientImage);
                        });
                    }
                    else if (data.type == "PLAY_VIDEO")
                    {
                        string videoPath = data.videoPath;
                        this.Invoke((MethodInvoker)delegate
                        {
                            PlayVideoOnClient(videoPath);
                        });
                    }
                    else if (data.type == "VIDEO_CONTROL")
                    {
                        string action = data.action;
                        int currentTime = data.currentTime;

                        // Xử lý thông điệp từ server
                        switch (action)
                        {
                            case "play":
                                PlayVideoOnClient(data.videoPath);
                                break;
                            case "pause":
                                player.Ctlcontrols.pause();
                                break;
                            case "seek":
                                SeekVideo(currentTime);
                                break;
                            default:
                                MessageBox.Show("Unknown action received from server: " + action, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }

                }
                catch (JsonReaderException ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"Error parsing JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            }
        }
        private void UpdateChatUI(string message)
        {
            if (rtbKhungChat.InvokeRequired)
            {
                rtbKhungChat.Invoke((MethodInvoker)delegate
                {
                    rtbKhungChat.AppendText(message + Environment.NewLine);
                    // Optionally, scroll to the end of the chat box
                    rtbKhungChat.ScrollToCaret();
                });
            }
            else
            {
                rtbKhungChat.AppendText(message + Environment.NewLine);
                // Optionally, scroll to the end of the chat box
                rtbKhungChat.ScrollToCaret();
            }
        }
        private void PlayVideoOnClient(string videoPath)
        {
            // Mở và phát video tại videoPath
            player.URL = videoPath;
            player.Ctlcontrols.play();
        }
        private void SeekVideo(int currentTime)
        {
            // Tua đến thời gian currentTime (được gửi từ server)
            player.Ctlcontrols.currentPosition = currentTime / 1000.0; // Chuyển đổi về giây
        }
        private void AddClientToUI(string clientName, string clientImage)
        {
            avtkaraoke avtkaraoke = new avtkaraoke(clientName, clientImage);
            listmember.Controls.Add(avtkaraoke);
        }
        public async Task JoinRoom(string roomId)
        {
            var data = new
            {
                roomId = roomId,
                name = name,
                image = image
            };

            string jsonData = JsonConvert.SerializeObject(data);
            string joinMessage = $"JOIN_ROOM {jsonData}";
            byte[] joinMessageBytes = Encoding.ASCII.GetBytes(joinMessage);

            await stream.WriteAsync(joinMessageBytes, 0, joinMessageBytes.Length);
        }

        public async Task CreateRoom()
        {
            var data = new
            {
                name = name,
                image = image
            };

            string jsonData = JsonConvert.SerializeObject(data);
            string createMessage = $"CREATE_ROOM {jsonData}";
            byte[] createMessageBytes = Encoding.ASCII.GetBytes(createMessage);

            await stream.WriteAsync(createMessageBytes, 0, createMessageBytes.Length);
        }

        private async void listTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTrack.SelectedIndex != -1)
            {
                await PlayVideo(paths[listTrack.SelectedIndex]);
            }
        }
        private async Task PlayVideo(string videoPath)
        {
            try
            {
                // Gửi yêu cầu phát video tới server
                var data = new
                {
                    roomId = roomId,
                    action = "play",
                    videoPath = videoPath,
                    currentTime = 0,
                };

                string jsonData = JsonConvert.SerializeObject(data);
                string playMessage = $"VIDEO_CONTROL {jsonData}";
                byte[] playMessageBytes = Encoding.ASCII.GetBytes(playMessage);

                await stream.WriteAsync(playMessageBytes, 0, playMessageBytes.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing video: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            SendVideoControlMessage("play", GetVideoCurrentTime());
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            SendVideoControlMessage("pause", GetVideoCurrentTime());
        }
        private int GetVideoCurrentTime()
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                return (int)player.Ctlcontrols.currentPosition * 1000; // Chuyển đổi sang miliseconds
            }
            return 0;
        }

        private async void btChat_Click(object sender, EventArgs e)
        {
            string message = rtbMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                await SendMessageToServer(message);
                rtbMessage.Text = ""; // Clear the message box after sending
            }
        }
        private async Task SendMessageToServer(string message)
        {
            try
            {
                var data = new
                {
                    roomId = roomId,
                    message = message,
                    sender = name // Assuming 'name' is the client's name or identifier
                };

                string jsonData = JsonConvert.SerializeObject(data);
                string sendMessage = $"CHAT_MESSAGE {jsonData}";
                byte[] sendMessageBytes = Encoding.ASCII.GetBytes(sendMessage);

                await stream.WriteAsync(sendMessageBytes, 0, sendMessageBytes.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendVideoControlMessage(string action, int currentTime)
        {
            var data = new
            {
                roomId = roomId,
                action = action,
                currentTime = currentTime
            };

            string jsonData = JsonConvert.SerializeObject(data);
            string controlMessage = $"VIDEO_CONTROL {jsonData}";
            byte[] controlMessageBytes = Encoding.ASCII.GetBytes(controlMessage);

            stream.Write(controlMessageBytes, 0, controlMessageBytes.Length);
        }
    }
}
