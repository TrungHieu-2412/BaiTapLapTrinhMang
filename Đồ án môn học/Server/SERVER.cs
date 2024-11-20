using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
 using System.Drawing;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Server
{
    public partial class SERVER : Form
    {
        // Cần quản lý số lượng phòng
        private List<Room> roomList = new List<Room>();
        private List<User> userList = new List<User>();
        private TcpListener listener;
        private RoomManager roomManager;


        public SERVER()
        {
            InitializeComponent();
            roomManager = new RoomManager(listviewInformation, textBox_room_count, textBox_user_count);
        }


        private void btnStartServer_Click(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any, 9999);
            listener.Start();

            Thread clientListener = new Thread(Listen);
            clientListener.IsBackground = true;
            clientListener.Start();

            roomManager.WriteToLog("Start listening for incoming requests...");

            this.btnStartServer.Enabled = false;
            this.btnStopServer.Enabled = true;
        }


        private void btnStopServer_Click(object sender, EventArgs e)
        {
            roomManager.WriteToLog("Stop listening for incoming requests");
            foreach (User user in userList)
            {
                user.Client.Close();
            }
            listener.Stop();

            this.btnStopServer.Enabled = false;
        }

        private void Listen()
        {
            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Thread receiver = new Thread(Receive);
                    receiver.IsBackground = true;
                    receiver.Start(client);
                }
            }
            catch
            {
                listener = new TcpListener(IPAddress.Any, 9999);
                listener.Start();
            }
        }

        private void Receive(object obj)
        {
            TcpClient client = obj as TcpClient;
            User user = new User(client);
            userList.Add(user);

            try
            {
                string requestInJson = string.Empty;
                while (true)
                {
                    requestInJson = user.Reader.ReadLine();

                    Packet request = JsonConvert.DeserializeObject<Packet>(requestInJson);

                    switch (request.Code)
                    {
                        case 0:
                            HandleGenerateRoomStatus(user, request);
                            break;
                        case 1:
                            HandleJoinRoomStatus(user, request);
                            break;
                        case 2:
                            HandleSyncBitmapStatus(user, request);
                            break;
                        case 3:
                            HandleSendBitmapStatus(user, request);
                            break;
                        case 4:
                            HandleSendGraphicsStatus(user, request);
                            break;
                    }
                }
            }

            catch
            {
                close_client(user);
            }
        }

        private void HandleGenerateRoomStatus(User user, Packet request)
        {
            user.Username = request.Username;

            Random r = new Random();
            int roomID = r.Next(1000, 9999);
            Room newRoom = new Room();
            newRoom.roomID = roomID;

            newRoom.userList.Add(user);
            roomList.Add(newRoom);

            roomManager.WriteToLog(user.Username + " created new room. Room code: " + newRoom.roomID);
            roomManager.UpdateRoomCount(roomList.Count);
            roomManager.UpdateUserCount(userList.Count);

            Packet message = new Packet
            {
                Code = 0,
                Username = request.Username,
                RoomID = roomID.ToString()
            };

            sendSpecific(user, message);
        }

        private void HandleJoinRoomStatus(User user, Packet request)
        {
            bool roomExist = false;

            int id = int.Parse(request.RoomID.ToString());
            Room requestingRoom = new Room();
            foreach (Room room in roomList)
            {
                if (room.roomID == id)
                {
                    requestingRoom = room;
                    roomExist = true;
                    break;
                }
            }
            if (!roomExist)
            {
                request.Username = "err:thisroomdoesnotexist";
                sendSpecific(user, request);
                return;
            }

            // thêm user mới vào phòng
            user.Username = request.Username;
            requestingRoom.userList.Add(user);

            // gửi danh sách user sau khi thêm user mới cho các user cũ trong phòng
            request.Username = requestingRoom.GetUsernameListInString();

            foreach (User _user in requestingRoom.userList)
            {
                sendSpecific(_user, request);
            }

            roomManager.WriteToLog("Room " + request.RoomID + ": " + user.Username + " joined");
            roomManager.UpdateUserCount(userList.Count);
        }



        private void HandleSyncBitmapStatus(User user, Packet request)
        {
            int id = int.Parse(request.RoomID.ToString());
            Room requestingRoom = new Room();
            foreach (Room room in roomList)
            {
                if (room.roomID == id)
                {
                    requestingRoom = room;
                    break;
                }
            }

            User _user = requestingRoom.userList[0];
            sendSpecific(_user, request);
        }


        private void HandleSendBitmapStatus(User user, Packet request)
        {
            int id = int.Parse(request.RoomID.ToString());
            Room requestingRoom = new Room();
            foreach (Room room in roomList)
            {
                if (room.roomID == id)
                {
                    requestingRoom = room;
                    break;
                }
            }

            User _user = requestingRoom.userList[requestingRoom.userList.Count - 1];
            sendSpecific(_user, request);
        }

        // Mới thêm
        //private void HandleDrawGraphics(User user, Packet request)
        //{
        //    int id;
        //    if (!int.TryParse(request.RoomID, out id))
        //    {
        //        roomManager.ShowError($"Mã phòng nhận được không hợp lệ: {request.RoomID}");
        //        return;
        //    }

        //    Room requestingRoom = roomList.Find(room => room.roomID == id);
        //    if (requestingRoom == null)
        //    {
        //        roomManager.ShowError($"Không tìm thấy phòng có ID {id}.");
        //        return;
        //    }


        //    Bitmap roomBitmap = requestingRoom.bitmap ?? new Bitmap(1024, 768); // Mặc định kích thước nếu null
        //    using (Graphics g = Graphics.FromImage(roomBitmap))
        //    {
        //        Pen p = new Pen(Color.FromName(request.PenColor), request.PenWidth);
        //        // Vẽ lên Graphics g dựa trên dữ liệu request
        //        if (request.ShapeTag == 10)
        //        {
        //            for (int i = 0; i < request.Points_1.Count; i++)
        //            {
        //                g.DrawLine(p, request.Points_1[i], request.Points_2[i]);
        //            }
        //        }
        //        else
        //        {
        //            // Xử lý các hình dạng khác (11, 12, 13) ở đây sử dụng request.Position
        //            float[] pos = request.Position;
        //            int x = (int)pos[0];
        //            int y = (int)pos[1];
        //            float w = pos[2];
        //            float h = pos[3];

        //            switch (request.ShapeTag)
        //            {
        //                case 11: g.DrawLine(p, x, y, x + w, y + h); break;
        //                case 12: g.DrawRectangle(p, x, y, w, h); break;
        //                case 13: g.DrawEllipse(p, x, y, w, h); break;
        //            }
        //        }
        //    }
        //    requestingRoom.bitmap = roomBitmap;




        //    // Phát sóng đến TẤT CẢ các client trong phòng
        //    foreach (User _user in requestingRoom.userList)
        //    {
        //        try
        //        {
        //            sendSpecific(_user, request);
        //        }
        //        catch (Exception ex)
        //        {
        //            roomManager.ShowError($"Lỗi gửi dữ liệu đến người dùng {_user.Username}: {ex.Message}");
        //            // Xem xét xóa người dùng đã ngắt kết nối khỏi phòng ở đây.
        //        }
        //    }
        //}


        private void HandleSendGraphicsStatus(User user, Packet request)
        {
            int id = int.Parse(request.RoomID.ToString());
            Room requestingRoom = new Room();
            foreach (Room room in roomList)
            {
                if (room.roomID == id)
                {
                    requestingRoom = room;
                    break;
                }
            }

            foreach (User _user in requestingRoom.userList)
            {
                if (_user != user)
                {
                    sendSpecific(_user, request);
                }
            }
        }


        private void close_client(User user)
        {
            Room requestingRoom = new Room();

            // xoá client khỏi cách list client và close client
            foreach (Room room in roomList)
            {
                if (room.userList.Contains(user))
                {
                    requestingRoom = room;
                    room.userList.Remove(user);
                    break;
                }
            }
            userList.Remove(user);
            user.Client.Close();

            if (user.Username != string.Empty)
            {
                roomManager.WriteToLog(user.Username + " disconnected.\n");
            }

            // gửi thông báo về client vừa ngắt kết nối đến client khác trong phòng
            Packet message = new Packet()
            {
                Code = 1,
                Username = "!" + user.Username
            };
            if (requestingRoom.userList.Count == 0)
            {
                if (roomList.Contains(requestingRoom))
                {
                    roomList.Remove(requestingRoom);
                    roomManager.WriteToLog("Deleted room: " + requestingRoom.roomID + " - No user here.");
                }
            }
            else
            {
                foreach (User _user in requestingRoom.userList)
                {
                    sendSpecific(_user, message);
                }
            }
            roomManager.UpdateRoomCount(roomList.Count);
            roomManager.UpdateUserCount(userList.Count);


            foreach (User _user in requestingRoom.userList)
            {
                try
                {
                    sendSpecific(_user, message);
                }
                catch (Exception ex)
                {
                    roomManager.ShowError($"Lỗi gửi thông báo ngắt kết nối đến người dùng {_user.Username}: {ex.Message}");
                }
            }
        }


        private void sendSpecific(User user, Object message)
        {
            string messageInJson = JsonConvert.SerializeObject(message);
            try
            {
                user.Writer.WriteLine(messageInJson);
                user.Writer.Flush();
            }
            catch
            {
                roomManager.ShowError("Cannot send data to user: " + user.Username);
            }
        }


        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (User user in userList)
            {
                user.Client.Close();
            }
            if (listener != null)
            {
                listener.Stop();
            }
        } 
    }






//--------------  Định nghĩa Class Packet --------------
    public class Packet
    {
        public int Code { get; set; }
        public string Username { get; set; }
        public string RoomID { get; set; }
        public string PenColor { get; set; }
        public float PenWidth { get; set; }
        public int ShapeTag { get; set; }
        public List<Point> Points_1 { get; set; }
        public List<Point> Points_2 { get; set; }
        public float[] Position { get; set; }
        public string BitmapString { get; set; }
    }



//--------------  Định nghĩa Class User --------------
    public class User
    {
        public TcpClient Client { get; set; }
        public string Username { get; set; }
        public StreamReader Reader { get; set; }
        public StreamWriter Writer { get; set; }

        public User(TcpClient client)
        {
            this.Client = client;
            this.Username = string.Empty;
            NetworkStream stream = Client.GetStream();
            this.Reader = new StreamReader(stream);
            this.Writer = new StreamWriter(stream);
        }
    }



//--------------  Định nghĩa Class RoomManager --------------
    public class RoomManager
    {
        ListView Log;
        TextBox RoomCnt;
        TextBox UserCnt;

        public RoomManager(ListView log, TextBox room_count, TextBox user_count)
        {
            this.Log = log;
            this.RoomCnt = room_count;
            this.UserCnt = user_count;
        }

        public void WriteToLog(string line)
        {
            if (Log.InvokeRequired)
            {
                Log.Invoke(new Action(() =>
                {
                    Log.Items.Add(string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm"), line ));

                }));
            }
            else
            {
                Log.Items.Add(string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm"), line));
            }
        }

        public void UpdateRoomCount(int num)
        {
            if (RoomCnt.InvokeRequired)
            {
                RoomCnt.Invoke(new Action(() =>
                {
                    RoomCnt.Text = num.ToString();
                }));
            }
            else
            {
                RoomCnt.Text = num.ToString();
            }
        }


        public void UpdateUserCount(int num)
        {
            if (UserCnt.InvokeRequired)
            {
                UserCnt.Invoke(new Action(() =>
                {
                    UserCnt.Text = num.ToString();
                }));
            }
            else
            {
                UserCnt.Text = num.ToString();
            }
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }



    //--------------  Định nghĩa Class Room --------------
    public class Room
    {
        public int roomID;
        public List<User> userList = new List<User>();
        public Bitmap bitmap;

        public string GetUsernameListInString()
        {
            List<string> usernames = new List<string>();
            foreach (User user in userList)
            {
                usernames.Add(user.Username);
            }
            string[] s = usernames.ToArray();
            string res = string.Join(",", s);

            return res;
        }

    }
}