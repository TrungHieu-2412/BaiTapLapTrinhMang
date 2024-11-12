using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Server
{
    public class Room
    {
        private string roomID;
        private Bitmap bitmap;
        private List<ClientHandler> clients = new List<ClientHandler>();
        private Stack<Bitmap> bitmapHistory = new Stack<Bitmap>();

        public Room(string roomID)
        {
            this.roomID = roomID;
            this.bitmap = new Bitmap(1024, 768); // Khởi tạo bitmap ban đầu với kích thước mặc định
            bitmapHistory.Push((Bitmap)bitmap.Clone()); // Lưu bitmap ban đầu vào lịch sử
        }

        public string RoomID { get { return roomID; } }
        public Bitmap Bitmap { get { return bitmap; } set { bitmap = value; } }
        public List<ClientHandler> Clients { get { return clients; } }

        public void AddClient(ClientHandler client)
        {
            clients.Add(client);
        }

        public void RemoveClient(ClientHandler client)
        {
            clients.Remove(client);
        }

        public List<User> GetUsers()
        {
            return clients.Select(c => c.User).ToList();
        }

        public void Undo()
        {
            if (bitmapHistory.Count > 0)
            {
                Bitmap previousBitmap = bitmapHistory.Pop();
                Bitmap.Dispose();
                Bitmap = previousBitmap;
            }
        }

        public string BitmapToString(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public Bitmap StringToBitmap(string bitmapString)
        {
            byte[] imageBytes = Convert.FromBase64String(bitmapString);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(ms);
            }
        }
    }
}