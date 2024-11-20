using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server 
{
    public class Room
    {
        private string roomID;
        private Bitmap bitmap;
        private List<ServerHandler> clients = new List<ServerHandler>();
        private Stack<Bitmap> bitmapHistory = new Stack<Bitmap>();
        private ServerNetworkManager networkManager;


        public Room(string roomID, ServerNetworkManager networkManager) // Thêm tham số networkManager vào constructor
        {
            this.roomID = roomID;
            this.networkManager = networkManager;
            this.bitmap = new Bitmap(1024, 768); // Khởi tạo bitmap ban đầu với kích thước mặc định
            bitmapHistory.Push((Bitmap)bitmap.Clone());
        }

        public string RoomID { get { return roomID; } }
        public Bitmap Bitmap { get { return bitmap; } set { bitmap = value; } }
        public List<ServerHandler> Clients { get { return clients; } }

        public ServerHandler Client { get; set; }

        public void AddClient(ServerHandler client)
        {
            clients.Add(client);
        }

        public void RemoveClient(ServerHandler client)
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