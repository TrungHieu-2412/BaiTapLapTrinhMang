using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DrawTogether.Client.Room
{
    internal class RoomManager
    {
        private ListView lisUserName;
        private TextBox txtRoomCodeCanva;
        private string currentRoomID;

        public RoomManager(ListView lisUserName, TextBox txtRoomCodeCanva)
        {
            this.lisUserName = lisUserName;
            this.txtRoomCodeCanva = txtRoomCodeCanva;
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
