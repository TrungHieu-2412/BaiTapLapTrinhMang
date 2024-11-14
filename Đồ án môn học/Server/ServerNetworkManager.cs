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

namespace Server
{
    public class ServerNetworkManager
    {
        private TcpListener listener;
        private List<ClientHandler> clients = new List<ClientHandler>();
        private RoomManager roomManager;

        public ServerNetworkManager(ServerUI serverUI)
        {
            roomManager = new RoomManager(this);
        }

        public void StartServer(int port)
        {
            if (listener != null)
            {
                listener.Stop();
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

                ((ServerUI)Application.OpenForms["ServerUI"]).UpdateInformation($"Client {handler.Username} đã kết nối");

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

           ((ServerUI)Application.OpenForms["ServerUI"]).UpdateInformation($"Client {client.Username} đã ngắt kết nối");

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
}