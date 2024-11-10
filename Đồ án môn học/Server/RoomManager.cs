using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Server
{
    public class RoomManager
    {
        private Dictionary<string, Room> rooms = new Dictionary<string, Room>();

        public string CreateRoom(User user)
        {
            // Tạo phòng mới
            string roomID = Guid.NewGuid().ToString();
            Room room = new Room(roomID);
            rooms.Add(roomID, room);
            return roomID;
        }

        public void AddClientToRoom(string roomID, ClientHandler client)
        {
            // Thêm client vào phòng
            if (RoomExists(roomID))
            {
                rooms[roomID].AddClient(client);
            }
        }

        public void RemoveClientFromRoom(string roomID, ClientHandler client)
        {
            // Xóa client khỏi phòng
            if (RoomExists(roomID))
            {
                rooms[roomID].RemoveClient(client);
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
    }
}