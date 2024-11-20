using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using DrawTogether.Server;


namespace Server
{
    public class ServerRoomManager
    {
        private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        private ServerNetworkManager networkManager;

        public ServerRoomManager(ServerNetworkManager networkManager) 
        {
            this.networkManager = networkManager;
        }

        public string CreateRoom(User user)
        {
            // Tạo phòng mới
            string roomID = Guid.NewGuid().ToString();
            Room room = new Room(roomID, networkManager); // Truyền networkManager vào constructor của Room
            rooms.Add(roomID, room);
            return roomID;
        }

        public void AddClientToRoom(string roomID, ServerHandler client)
        {
            // Thêm client vào phòng
            if (RoomExists(roomID))
            {
                rooms[roomID].AddClient(client);
               
                // Cập nhật danh sách người dùng cho tất cả các client trong phòng
                string usernames = string.Join(",", rooms[roomID].GetUsers().Select(u => u.Username));
                ServerPacket updatePacket = new ServerPacket
                {
                    Code = 1,
                    Username = usernames,
                    RoomID = roomID
                };
                networkManager.Broadcast(updatePacket, client);
            }
        }

        public void RemoveClientFromRoom(string roomID, ServerHandler client)
        {
            if (RoomExists(roomID))
            {
                rooms[roomID].RemoveClient(client);

                // Cập nhật danh sách người dùng cho tất cả các client trong phòng
                string usernames = string.Join(",", rooms[roomID].GetUsers().Select(u => u.Username));
                ServerPacket updatePacket = new ServerPacket
                {
                    Code = 1,
                    Username = usernames,
                    RoomID = roomID
                };
                networkManager.Broadcast(updatePacket, client);
            }
        }

        public bool RoomExists(string roomID)
        {
            // Kiểm tra xem phòng có tồn tại không
            return rooms.ContainsKey(roomID);
        }

        public List<User> GetUsersInRoom(string roomID)
        {
            // Lấy danh sách người dùng trong phòng
            if (RoomExists(roomID))
            {
                return rooms[roomID].GetUsers();
            }
            return new List<User>();
        }

        // Phương thức trả về số lượng room
        public int GetRoomCount()
        {
            return rooms.Count;
        }

        public Room GetRoom(string roomID, ServerHandler handler)
        {
            if (RoomExists(roomID))
            {
                // Gửi bitmap của phòng về cho client
                ServerPacket response = new ServerPacket
                {
                    Code = 5,
                    RoomID = roomID,
                    BitmapString = rooms[roomID].BitmapToString(rooms[roomID].Bitmap)
                };
                handler.Send(response);
                return rooms[roomID];
            }
            return null;
        }
    }
}