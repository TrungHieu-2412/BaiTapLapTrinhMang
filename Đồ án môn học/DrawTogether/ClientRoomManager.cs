using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DrawTogether.Client.Model;
using DrawTogether.Client.Networking;
using Newtonsoft.Json;
using Server;
//using DrawTogether.Server;
namespace DrawTogether.Client.Room
{
    internal class ClientRoomManager
    {
        private ListView lisUserName;
        private TextBox txtRoomCodeCanva;
        private string currentRoomID;

        private ClientNetworkManager networkManager;

        public ClientRoomManager(ListView lisUserName, TextBox txtRoomCodeCanva, ClientNetworkManager networkManager)
        {
            this.lisUserName = lisUserName;
            this.txtRoomCodeCanva = txtRoomCodeCanva;
            this.networkManager = networkManager;
        }

        public ClientRoomManager(ListView lisUserName, TextBox txtRoomCodeCanva)
        {
            this.lisUserName = lisUserName;
            this.txtRoomCodeCanva = txtRoomCodeCanva;
        }

        // Sử dụng riêng biệt để xử lý yêu cầu GetRoom
        public ClientRoom GetRoom(string roomID)
        {
            ClientPacket request = new ClientPacket { Code = 5, RoomID = roomID };
            networkManager.Send(request);

            ClientPacket response = networkManager.Receive();

            if (response == null || response.BitmapString == null)
            {
                ShowError("Lỗi khi lấy thông tin phòng!");
                return null;
            }

            try
            {
                Bitmap bitmap = StringToBitmap(response.BitmapString);
                ClientRoom clientRoom = new ClientRoom(roomID, bitmap);  // Khởi tạo ClientRoom
                return clientRoom;
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi xử lý hình ảnh: {ex.Message}");
                return null;
            }
        }

        public void UpdateRoomID(string roomID)
        {
            currentRoomID = roomID;
            txtRoomCodeCanva.Text = roomID;
        }

        public void AddToUserListView(string username)
        {
            lisUserName.Items.Add(username);
        }

        public void RemoveFromUserListView(string username)
        {
            foreach (ListViewItem item in lisUserName.Items)
            {
                if (item.Text == username)
                {
                    lisUserName.Items.Remove(item);
                    break;
                }
            }
        }

        public void ClearUserListView()
        {
            lisUserName.Items.Clear();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
