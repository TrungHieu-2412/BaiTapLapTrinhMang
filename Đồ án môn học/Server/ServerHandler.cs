using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DrawTogether.Server;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Drawing;

namespace Server
{
    public class ServerHandler
    {
        private TcpClient client;
        private ServerRoomManager roomManager;
        private ServerNetworkManager networkManager;
        public StreamReader reader;
        public StreamWriter writer;
        private CancellationTokenSource cancellationTokenSource;
        public string Username { get; private set; }
        public string RoomID { get; set; }
        public User User { get; set; }
        //public Bitmap Bitmap { get; set; }
        public ServerHandler(TcpClient client, ServerRoomManager roomManager, ServerNetworkManager networkManager)
        {
            this.client = client;
            this.roomManager = roomManager;
            this.networkManager = networkManager;
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            cancellationTokenSource = new CancellationTokenSource();
            //Bitmap = new Bitmap(1024, 768);
        }

        public void HandleClient()
        {
            try
            {
                // Nhận thông tin client
                string messageInJson = reader.ReadLine();
                ServerPacket packet = JsonConvert.DeserializeObject<ServerPacket>(messageInJson);

                // Lưu username của client
                Username = packet.Username;
                User = new User(Username);

                // Xử lý các yêu cầu từ client
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    messageInJson = reader.ReadLine();
                    if (messageInJson != null)
                    {
                        packet = JsonConvert.DeserializeObject<ServerPacket>(messageInJson);

                        switch (packet.Code)
                        {
                            case 0: // Tạo phòng
                                HandleGenerateRoom(packet);
                                break;
                            case 1: // Tham gia phòng
                                HandleJoinRoom(packet);
                                break;
                            case 2: // Đồng bộ Bitmap
                                HandleSyncBitmap(packet);
                                break;
                            case 3: // Vẽ Bitmap
                                HandleDrawBitmap(packet);
                                break;
                            case 4: // Nhận dữ liệu vẽ
                                HandleReceiveDrawingData(packet);
                                break;
                            case 5: // Xử lý yêu cầu lấy thông tin phòng
                                HandleGetRoom(packet);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Client has disconnected.");
                        cancellationTokenSource.Cancel();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xử lý client: " + ex.Message);
                cancellationTokenSource.Cancel();
            }
            finally
            {
                // Ngắt kết nối với client và xóa client khỏi danh sách
                networkManager.RemoveClient(this);
                client.Close();
            }
        }

        private void HandleReceiveDrawingData(ServerPacket packet)
        {
            // Lấy phòng hiện tại
            Room currentRoom = roomManager.GetRoom(packet.RoomID, this);
            if (currentRoom != null)
            {
                // Cập nhật bitmap chung của phòng
                using (Graphics g = Graphics.FromImage(currentRoom.Bitmap))
                {
                    Pen p = new Pen(Color.FromName(packet.PenColor), packet.PenWidth);
                    if (packet.ShapeTag == 10) // Vẽ đường thẳng
                    {
                        for (int i = 0; i < packet.Points_1.Count; i++)
                        {
                            g.DrawLine(p, packet.Points_1[i], packet.Points_2[i]);
                        }
                    }
                    else
                    {
                        int cursorX = (int)packet.Position[0];
                        int cursorY = (int)packet.Position[1];
                        float w = packet.Position[2];
                        float h = packet.Position[3];

                        if (packet.ShapeTag == 11)
                        {
                            g.DrawLine(p, cursorX, cursorY, cursorX + w, cursorY + h);
                        }
                        else if (packet.ShapeTag == 12)
                        {
                            g.DrawRectangle(p, cursorX, cursorY, w, h);
                        }
                        else if (packet.ShapeTag == 13)
                        {
                            g.DrawEllipse(p, cursorX, cursorY, w, h);
                        }
                    }
                }

                // Gửi thông báo cập nhật nét vẽ cho các client khác
                ServerPacket syncPacket = new ServerPacket
                {
                    Code = 4,
                    ShapeTag = packet.ShapeTag,
                    PenColor = packet.PenColor,
                    PenWidth = packet.PenWidth,
                    Points_1 = packet.Points_1,
                    Points_2 = packet.Points_2,
                    Position = packet.Position,
                    RoomID = packet.RoomID
                };
                networkManager.Broadcast(syncPacket, this);
            }
        }


        private void HandleGenerateRoom(ServerPacket packet)
        {
            // Tạo phòng mới
            string roomID = roomManager.CreateRoom(User);
            packet.RoomID = roomID;
            Send(packet);

            // Thêm client vào phòng
            RoomID = roomID;
            roomManager.AddClientToRoom(roomID, this);

            ((ServerUI)Application.OpenForms["ServerUI"]).UpdateInformation($"Client {Username} đã tạo phòng {roomID}");

            Console.WriteLine("Client {0} đã tạo phòng {1}.", Username, roomID);
        }

        private void HandleJoinRoom(ServerPacket packet)
        {
            // Kiểm tra xem phòng có tồn tại không
            if (!roomManager.RoomExists(packet.RoomID))
            {
                packet.Username = "err:thisroomdoesnotexist";
                Send(packet);
                return;
            }

            // Thêm client vào phòng
            RoomID = packet.RoomID;
            roomManager.AddClientToRoom(packet.RoomID, this);

            // Cập nhật danh sách người dùng cho tất cả các client trong phòng
            string usernames = string.Join(",", roomManager.GetUsersInRoom(packet.RoomID).Select(u => u.Username));
            ServerPacket updatePacket = new ServerPacket
            {
                Code = 1,
                Username = usernames,
                RoomID = packet.RoomID
            };
            networkManager.Broadcast(updatePacket, this);

            // Gửi thông tin phòng cho client
            Send(new ServerPacket { Code = 1, RoomID = packet.RoomID });

            ((ServerUI)Application.OpenForms["ServerUI"]).UpdateInformation($"Client {Username} đã tham gia phòng {packet.RoomID}");

            Console.WriteLine("Client {0} đã tham gia phòng {1}.", Username, packet.RoomID);
        }

        private void HandleSyncBitmap(ServerPacket packet)
        {
            // Gửi bitmap hiện tại của client đến các client khác trong cùng phòng
            ServerPacket response = new ServerPacket
            {
                Code = 2,
                RoomID = packet.RoomID,
                BitmapString = packet.BitmapString,
            };
            networkManager.Broadcast(response, this);
        }

        private void HandleDrawBitmap(ServerPacket packet)
        {
            // Gửi bitmap đã vẽ đến các client khác trong cùng phòng
            ServerPacket response = new ServerPacket
            {
                Code = 3,
                RoomID = packet.RoomID,
                BitmapString = packet.BitmapString,
            };
            networkManager.Broadcast(response, this);
        }

         private void HandleGetRoom(ServerPacket packet)
        {
            // Lấy thông tin phòng từ ServerRoomManager
            Room room = roomManager.GetRoom(packet.RoomID, this);
            if (room != null)
            {
                // Gửi bitmap của phòng về cho client
                ServerPacket response = new ServerPacket
                {
                    Code = 5,
                    RoomID = packet.RoomID,
                    BitmapString = room.BitmapToString(room.Bitmap)
                };
                Send(response);
            }
            else
            {
                // Gửi lỗi về client
                ServerPacket response = new ServerPacket
                {
                    Code = 5,
                    RoomID = packet.RoomID,
                    BitmapString = null
                };
                Send(response);
            }
        }


        public void Send(ServerPacket packet)
        {
            // Gửi dữ liệu đến client
            try
            {
                string messageInJson = JsonConvert.SerializeObject(packet);
                writer.WriteLine(messageInJson);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi dữ liệu: " + ex.Message);
                cancellationTokenSource.Cancel();
            }
        }
    }
}