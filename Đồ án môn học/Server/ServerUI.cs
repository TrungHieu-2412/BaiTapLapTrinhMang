using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using DrawTogether.Server;


namespace Server
{
    public partial class ServerUI : Form
    {
        private NetworkManager networkManager;
        private RoomManager roomManager;
        private TextBox txtPort;
        //private TextBox txtCountRoom;
        //private TextBox txtCountUser;
        public ServerUI()
        {
            InitializeComponent();
            networkManager = new NetworkManager();
            roomManager = new RoomManager();

            txtPort = new TextBox();
            txtPort.Location = new Point(10, 10);
            txtPort.Text = "9999"; // Gán giá trị mặc định
            this.Controls.Add(txtPort);

        }
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            int port = int.Parse(txtPort.Text); // Lấy port từ textbox
            networkManager.StartServer(port);
            lisInformation.Items.Add($"Server đang chạy trên cổng {port}...");
        }

        private void ServerUI_Load (object sender, EventArgs e)
        {
            lisInformation.Items.Add("Server chưa chạy...");
        }

      
    }
}