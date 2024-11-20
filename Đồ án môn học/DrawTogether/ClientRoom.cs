using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace DrawTogether.Client.Model 
{
    public class ClientRoom
    {
        public string RoomID { get; set; }
        public Bitmap Bitmap { get; set; }
        public List<string> Usernames { get; set; } = new List<string>(); // Thêm danh sách usernames

        public ClientRoom(string roomID, Bitmap bitmap)
        {
            RoomID = roomID;
            Bitmap = bitmap;
        }

        // Thêm phương thức để cập nhật danh sách người dùng
        public void UpdateUsernames(List<string> usernames)
        {
            Usernames = usernames;
        }

        // Phương thức để thêm người dùng vào danh sách
        public void AddUsername(string username)
        {
            Usernames.Add(username);
        }

        // Phương thức để xóa người dùng khỏi danh sách
        public void RemoveUsername(string username)
        {
            Usernames.Remove(username);
        }


        // Thêm các thuộc tính khác nếu cần thiết (ví dụ: tên phòng,...)
    }
}