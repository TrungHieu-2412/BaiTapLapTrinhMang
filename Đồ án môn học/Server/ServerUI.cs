using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using DrawTogether.Server;
using System.Drawing;

namespace Server
{
    public partial class ServerUI : Form
    {
        private ServerNetworkManager networkManager;
        private ServerRoomManager roomManager;
        private TextBox txtPort;

        public ServerUI()
        {
            InitializeComponent();
            networkManager = new ServerNetworkManager(this); // Truyền this (ServerUI) vào constructor của NetworkManager
            roomManager = new ServerRoomManager(networkManager);
            txtPort = new TextBox();
            txtPort.Location = new Point(10, 10);
            txtPort.Text = "9999";
            this.Controls.Add(txtPort);

            
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            int port = int.Parse(txtPort.Text);
            networkManager.StartServer(port);
            txtInformation.Text += $"Server đang chạy trên cổng {port}...\r\n";
        }

        // Hàm cập nhật thông tin lên TextBox
        public void UpdateInformation(string message)
        {
            if (txtInformation.InvokeRequired)
            {
                txtInformation.Invoke(new Action(() =>
                {
                    txtInformation.Text += message + "\r\n";
                }));
            }
            else
            {
                txtInformation.Text += message + "\r\n";
            }
        }
    }
}