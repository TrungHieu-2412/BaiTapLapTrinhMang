using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03
{
    public partial class UdpServer : Form
    {
        private Thread serverThread;
        private UdpClient udpServer;
        private bool isListening = false;
        public UdpServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (isListening)
            {
                MessageBox.Show("Server đã được bật và đang lắng nghe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string portText = txtPortServer.Text;
            if (IsPortValid(portText, out int port))
            {
                StartServer(port);
                MessageBox.Show("Server đang lắng nghe...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập Port hợp lệ (1-65535).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void serverThreadMethod(int port)
        {
            try
            {
                udpServer = new UdpClient(port);
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receiveBytes = udpServer.Receive(ref remoteEndPoint);
                    string receivedData = Encoding.UTF8.GetString(receiveBytes);
                    string message = $"{remoteEndPoint.Address}: {receivedData}";
                    ShowMessage(message);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Lỗi Socket: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowMessage(string message)
        {
            if (txtMessages.InvokeRequired)
            {
                txtMessages.Invoke(new Action(() => AppendMessage(message)));
            }
            else
            {
                AppendMessage(message);
            }
        }

        private void AppendMessage(string message)
        {
            txtMessages.AppendText(message + Environment.NewLine);
            txtMessages.SelectionStart = txtMessages.Text.Length;
            txtMessages.ScrollToCaret();
        }

        private void UdpServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpServer?.Close();
            serverThread?.Abort();
            isListening = false;
        }
        private bool IsPortValid(string portText, out int port)
        {
            // Kiểm tra xem port có phải là số hay không và không có khoảng trắng hoặc ký tự đặc biệt
            if (int.TryParse(portText.Trim(), out port) && port > 0 && port <= 65535)
            {
                return true;
            }

            port = -1; // Nếu không hợp lệ, gán giá trị không hợp lệ cho port
            return false;
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            string portText = txtPortServer.Text;
            if (IsPortValid(portText, out int newPort))
            {
                // Dừng server hiện tại
                StopServer();

                // Khởi động lại server với port mới
                StartServer(newPort);
                MessageBox.Show($"Server đã thay đổi sang port mới: {newPort}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập port hợp lệ (1-65535).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Dừng server hiện tại
        private void StopServer()
        {
            try
            {
                if (isListening)
                {
                    udpServer?.Close();  // Đóng UdpClient hiện tại
                    serverThread?.Abort();  // Dừng luồng lắng nghe
                    isListening = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi dừng server: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Khởi động server với port mới
        private void StartServer(int port)
        {
            serverThread = new Thread(() => serverThreadMethod(port))
            {
                IsBackground = true
            };
            serverThread.Start();
            isListening = true;
        }

    }
}
