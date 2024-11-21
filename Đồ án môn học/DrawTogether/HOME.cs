using System;
using System.Windows.Forms;


namespace DrawTogether
{
    public partial class HOME : Form
    {
        private bool isOffline;
        public bool IsCreateRoom { get; private set; }
        public string UserName { get; private set; }
        public string RoomCode { get; private set; }
        public string ServerIP { get; private set; }

        public HOME()
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
            lblRoomCode.Visible = false;
            txtName.Visible = true;
            txtRoomCode.Visible = false;
            btnStart.Visible = true;
            btnBack.Visible = true;
            lblServerIP.Visible = true;
            txtServerIP.Visible = true;
            btnStart.Enabled = false;
        }

        private void btnOffline_Click(object sender, EventArgs e)
        {
            // Mở form Canva ở chế độ offline
            CLIENT CanvaForm = new CLIENT(true, 0, "", "", "");
            CanvaForm.FormClosed += (s, args) => this.Show();
            CanvaForm.Show();
            this.Hide();
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            btnOnline.Visible = false;
            btnOffline.Visible = false;
            ShowOnlineControls();
            txtServerIP.Text = "34.201.71.75";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            HideOnlineControls();
            btnOnline.Visible = true;
            btnOffline.Visible = true;
            btnStart.Text = "Start";

        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            IsCreateRoom = true;
            btnStart.Text = "Create Room";
            txtRoomCode.Visible = false;
            lblRoomCode.Visible = false;
            btnStart.Enabled = true;

        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            IsCreateRoom = false;
            btnStart.Text = "Join Room";
            txtRoomCode.Visible = true;
            lblRoomCode.Visible = true;
            btnStart.Enabled = true;
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
                // Chổ này phải tự Generate một cái RoomID
                RoomCode = HOME.GenerateRandomString();

                // Mở form Canva ở chế độ online, tạo phòng
                CLIENT CanvaForm = new CLIENT(false, 0, UserName, RoomCode, ServerIP);
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
                CLIENT CanvaForm = new CLIENT(false, 1, UserName, RoomCode, ServerIP);
                CanvaForm.FormClosed += (s, args) => this.Show();
                CanvaForm.Show();
                this.Hide();
            }
        }

        public static string GenerateRandomString()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10000); // Tạo số ngẫu nhiên từ 0 đến 9999
            return randomNumber.ToString("D4"); // Định dạng thành chuỗi 4 ký tự
        }
    }
}