using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawTogether
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


        //Cập nhật RoomID lên Textbox Client
        public void UpdateRoomID(string roomID)
        {
            if (txtRoomCodeCanva.InvokeRequired)
            {
                txtRoomCodeCanva.Invoke(new Action(() =>
                {
                    txtRoomCodeCanva.Text = "" + roomID;
                }));
            }
            else
            {
                txtRoomCodeCanva.Text = "" + roomID;
            }
        }

        public void AddToUserListView(string line)
        {
            if (lisUserName.InvokeRequired)
            {
                lisUserName.Invoke(new Action(() =>
                {
                    lisUserName.Items.Add(new ListViewItem(line));
                }));
            }
            else
            {
                lisUserName.Items.Add(new ListViewItem(line));
            }
        }

        public void RemoveFromUserListView(string line)
        {
            Action action = () =>
            {
                foreach (ListViewItem item in lisUserName.Items)
                {
                    if (item.Text == line)
                    {
                        lisUserName.Items.Remove(item);
                        break;
                    }
                }
            };
            if (lisUserName.InvokeRequired)
            {
                lisUserName.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public void ClearUserListView()
        {
            Action action = () =>
            {
                ListViewItem firstLine = lisUserName.Items[0];
                lisUserName.Clear();
                lisUserName.Items.Add(firstLine);
            };
            if (lisUserName.InvokeRequired)
            {
                lisUserName.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public string BitmapToString(Bitmap bitmap)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = stream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }

        public Bitmap StringToBitmap(string base64string)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length);
            stream.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(stream, true);
            Bitmap bitmap = new Bitmap(image);

            return bitmap;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
