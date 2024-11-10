using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using DrawTogether.Client.Networking;
using DrawTogether.Client.Model;
using DrawTogether.Client.Room;
using Newtonsoft.Json;
using System.Runtime.Remoting.Contexts;

namespace DrawTogether
{
    public partial class DrawTogether : Form
    {
        private bool isOffline;
        public bool IsCreateRoom { get; private set; }
        public string UserName { get; private set; }
        public string RoomCode { get; private set; }
        public string ServerIP { get; private set; }

        public DrawTogether()
        {
            InitializeComponent();
            // Ẩn các control khi chưa chọn chế độ online/offline
            HideOnlineControls();
        }

        private void HideOnlineControls()
        {
            btnCreateRoom.Visible = false;
            btnJoinRoom.Visible = false;
            lblName.Visible = false;
            lblRoomCode.Visible = false;
            txtName.Visible = false;
            txtRoomCode.Visible = false;
            btnStart.Visible = false;
            btnBack.Visible = false;
            lblServerIP.Visible = false;
            txtServerIP.Visible = false;
        }

        private void ShowOnlineControls()
        {
            btnCreateRoom.Visible = true;
            btnJoinRoom.Visible = true;
            lblName.Visible = true;
            lblRoomCode.Visible = true;
            txtName.Visible = true;
            txtRoomCode.Visible = true;
            btnStart.Visible = true;
            btnBack.Visible = true;
            lblServerIP.Visible = true;
            txtServerIP.Visible = true;
        }

        private void btnOffline_Click(object sender, EventArgs e)
        {
            // Mở form Canva ở chế độ offline
            Canva CanvaForm = new Canva(true, 0, "", "", "");
            CanvaForm.FormClosed += (s, args) => this.Show();
            CanvaForm.Show();
            this.Hide();
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            btnOnline.Visible = false;
            btnOffline.Visible = false;
            ShowOnlineControls();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            HideOnlineControls();
            btnOnline.Visible = true;
            btnOffline.Visible = true;
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            IsCreateRoom = true;
            btnStart.Text = "Create Room";
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            IsCreateRoom = false;
            btnStart.Text = "Join Room";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            UserName = txtName.Text;
            ServerIP = txtServerIP.Text;

            if (string.IsNullOrEmpty(UserName))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsCreateRoom)
            {
                // Mở form Canva ở chế độ online, tạo phòng
                Canva CanvaForm = new Canva(false, 0, UserName, "", ServerIP);
                CanvaForm.FormClosed += (s, args) => this.Show();
                CanvaForm.Show();
                this.Hide();
            }
            else
            {
                RoomCode = txtRoomCode.Text;

                if (string.IsNullOrEmpty(RoomCode))
                {
                    MessageBox.Show("Vui lòng nhập mã phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở form Canva ở chế độ online, tham gia phòng
                Canva CanvaForm = new Canva(false, 1, UserName, RoomCode, ServerIP);
                CanvaForm.FormClosed += (s, args) => this.Show();
                CanvaForm.Show();
                this.Hide();
            }
        }
    }
}