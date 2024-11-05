using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace DrawTogether
{
    public partial class DrawTogether : Form
    {
        public string UserName { get; private set; }
        public string RoomCode { get; private set; }
        public bool IsCreateRoom { get; private set; }
        private ClientManager clientManager;
        private Canva CanvaForm;

        public DrawTogether()
        {
            InitializeComponent();
            // Ẩn các control khi chưa chọn chế độ online/offline
            HideOnlineControls();
            // Khởi tạo ClientManager
            clientManager = new ClientManager();
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
            
        }

        private void btnOffline_Click(object sender, EventArgs e)
        {
            Canva CanvaForm = new Canva();
            CanvaForm.FormClosed += (s, args) => this.Show(); // Hiển thị lại form cũ khi form mới đóng
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
            IsCreateRoom = true; // Đánh dấu chế độ tạo phòng
            btnStart.Text = "Tạo Phòng"; // Đổi tên nút Start
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            IsCreateRoom = false; // Đánh dấu chế độ tham gia phòng
            btnStart.Text = "Tham Gia"; // Đổi tên nút Start
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            UserName = txtName.Text;

            if (IsCreateRoom)
            {
                // Gửi yêu cầu tạo phòng đến server
                clientManager.JoinRoom("", UserName);
            }
            else
            {
                RoomCode = txtRoomCode.Text;
                // Gửi yêu cầu tham gia phòng đến server
                clientManager.JoinRoom(RoomCode, UserName);
            }

            // Mở form Canva và truyền dữ liệu
            CanvaForm = new Canva(clientManager, RoomCode, UserName);
            CanvaForm.FormClosed += (s, args) => this.Show(); // Hiển thị lại form cũ khi form mới đóng
            CanvaForm.Show();
            this.Hide();
        }
    }
}