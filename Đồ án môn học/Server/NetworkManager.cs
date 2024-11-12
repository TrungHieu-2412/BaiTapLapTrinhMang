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

namespace Server
{
    public class NetworkManager
    {
        private TcpListener listener;
        private List<ClientHandler> clients = new List<ClientHandler>();
        private RoomManager roomManager;

        public NetworkManager()
        {
            roomManager = new RoomManager();
        }

        public void StartServer(int port)
        {
            if (listener != null) 
            {
                listener.Stop(); // Dừng listener cũ
                Console.WriteLine("Server đã dừng.");
            }

            // Khởi tạo listener mới
            listener = new TcpListener(IPAddress.Any, port);

            try
            {
                listener.Start();
                Console.WriteLine("Server đang chạy trên cổng {0}", port);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khởi động server: " + ex.Message); 
            }
            // Bắt đầu luồng lắng nghe kết nối
            Thread listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }
        private void ListenForClients()
        {
            while (true)
            {
                // Chấp nhận kết nối từ client
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Kết nối mới từ {0}", client.Client.RemoteEndPoint);

                // Tạo luồng xử lý cho client
                ClientHandler handler = new ClientHandler(client, roomManager, this);
                clients.Add(handler);

                // Khởi tạo reader và writer cho ClientHandler
                handler.reader = new StreamReader(client.GetStream());
                handler.writer = new StreamWriter(client.GetStream());

                Thread clientThread = new Thread(handler.HandleClient);
                clientThread.Start();
            }
        }

        public void Broadcast(Packet packet, ClientHandler sender)
        {
            // Gửi dữ liệu đến tất cả các client trong cùng phòng với sender
            foreach (ClientHandler client in clients)
            {
                if (client != sender && client.RoomID == sender.RoomID)
                {
                    client.Send(packet);
                }
            }
        }

        public void RemoveClient(ClientHandler client)
        {
            // Xóa client khỏi danh sách clients
            clients.Remove(client);
            // Xóa client khỏi phòng
            if (!string.IsNullOrEmpty(client.RoomID))
            {
                roomManager.RemoveClientFromRoom(client.RoomID, client);

                // Gửi thông báo client rời phòng đến các client còn lại trong phòng
                Packet leavePacket = new Packet
                {
                    Code = 1,
                    Username = "!" + client.Username,
                    RoomID = client.RoomID
                };
                Broadcast(leavePacket, client);
            }
            Console.WriteLine("Client {0} đã ngắt kết nối.", client.Username);
        }

        public void StopServer()
        {
            // Dừng server
            if (listener != null)
            {
                listener.Stop();
                Console.WriteLine("Server đã dừng.");
            }
        }
    }

    public class ClientHandler
    {
        private TcpClient client;
        private RoomManager roomManager;
        private NetworkManager networkManager;
        //private StreamReader reader;
        public StreamWriter writer;
        public StreamReader reader
        {
            get { return reader; }
            set { reader = value; }
        }
        public string Username { get; private set; }
        public string RoomID { get; set; }
        public User User { get; set; }

        public ClientHandler(TcpClient client, RoomManager roomManager, NetworkManager networkManager)
        {
            this.client = client;
            this.roomManager = roomManager;
            this.networkManager = networkManager;
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
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
                while (true)
                {
                    messageInJson = reader.ReadLine();
                    packet = JsonConvert.DeserializeObject<Packet>(messageInJson);

                    switch (packet.Code)
                    {
                        case 0:  // Tạo phòng
                            HandleGenerateRoom(packet);
                            break;
                        case 1:  // Tham gia phòng
                            HandleJoinRoom(packet);
                            break;
                        case 2:  // Đồng bộ Bitmap
                            HandleSyncBitmap(packet);
                            break;
                        case 3:  // Vẽ Bitmap
                            HandleDrawBitmap(packet);
                            break;
                        case 4:  // Nhận dữ liệu vẽ
                            HandleReceiveDrawingData(packet);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xử lý client: " + ex.Message);
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
            // Gửi dữ liệu vẽ đến các client khác trong cùng phòng
            networkManager.Broadcast(packet, this);
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
            }
        }
    }
}