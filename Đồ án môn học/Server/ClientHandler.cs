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
    public class ClientHandler
    {
        private TcpClient client;
        private RoomManager roomManager;
        private ServerNetworkManager networkManager;
        public StreamReader reader;
        public StreamWriter writer;
        private CancellationTokenSource cancellationTokenSource;
        public string Username { get; private set; }
        public string RoomID { get; set; }
        public User User { get; set; }
        //public Bitmap Bitmap { get; set; }
        public ClientHandler(TcpClient client, RoomManager roomManager, ServerNetworkManager networkManager)
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
                Packet packet = JsonConvert.DeserializeObject<Packet>(messageInJson);

                // Lưu username của client
                Username = packet.Username;
                User = new User(Username);

                // Xử lý các yêu cầu từ client
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    messageInJson = reader.ReadLine();
                    if (messageInJson != null)
                    {
                        packet = JsonConvert.DeserializeObject<Packet>(messageInJson);

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

        private void HandleReceiveDrawingData(Packet packet)
        {
            // Lấy phòng hiện tại
            Room currentRoom = roomManager.GetRoom(packet.RoomID);
            if (currentRoom != null)
            {
                using (Graphics g = Graphics.FromImage(currentRoom.Bitmap))
                {
                    // Cập nhật Bitmap chung của phòng
                    if (packet.ShapeTag == 10)
                    {
                        // Vẽ đường thẳng
                        if (packet.Points_1 != null && packet.Points_2 != null && packet.Points_1.Count == packet.Points_2.Count)
                        {
                            for (int i = 0; i < packet.Points_1.Count; i++)
                            {


                                Pen p = new Pen(Color.FromName(packet.PenColor), packet.PenWidth);
                                g.DrawLine(p, packet.Points_1[i], packet.Points_2[i]);

                            }
                        }
                    }
                    else
                    {
                        // Vẽ hình dạng khác (đường thẳng, hình chữ nhật, hình elip)
                        int cursorX = (int)packet.Position[0];
                        int cursorY = (int)packet.Position[1];
                        float w = packet.Position[2];
                        float h = packet.Position[3];



                        Pen p = new Pen(Color.FromName(packet.PenColor), packet.PenWidth);
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
                // Gửi thông báo cập nhật Bitmap cho các client khác
                Packet syncPacket = new Packet
                {
                    Code = 2,
                    RoomID = packet.RoomID,
                    BitmapString = currentRoom.BitmapToString(currentRoom.Bitmap)
                };
                networkManager.Broadcast(syncPacket, this);
            }
        }

        private void HandleGenerateRoom(Packet packet)
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

        private void HandleJoinRoom(Packet packet)
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
            Packet updatePacket = new Packet
            {
                Code = 1,
                Username = usernames,
                RoomID = packet.RoomID
            };
            networkManager.Broadcast(updatePacket, this);

            // Gửi thông tin phòng cho client
            Send(new Packet { Code = 1, RoomID = packet.RoomID });

            ((ServerUI)Application.OpenForms["ServerUI"]).UpdateInformation($"Client {Username} đã tham gia phòng {packet.RoomID}");

            Console.WriteLine("Client {0} đã tham gia phòng {1}.", Username, packet.RoomID);
        }

        private void HandleSyncBitmap(Packet packet)
        {
            // Gửi bitmap hiện tại của client đến các client khác trong cùng phòng
            Packet response = new Packet
            {
                Code = 2,
                RoomID = packet.RoomID,
                BitmapString = packet.BitmapString,
            };
            networkManager.Broadcast(response, this);
        }

        private void HandleDrawBitmap(Packet packet)
        {
            // Gửi bitmap đã vẽ đến các client khác trong cùng phòng
            Packet response = new Packet
            {
                Code = 3,
                RoomID = packet.RoomID,
                BitmapString = packet.BitmapString,
            };
            networkManager.Broadcast(response, this);
        }

        public void Send(Packet packet)
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