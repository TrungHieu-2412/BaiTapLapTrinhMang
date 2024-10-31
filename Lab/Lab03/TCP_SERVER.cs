using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class TCP_SERVER : Form
    {
        private Thread serverThread;
        private TcpListener tcpServer;
        private bool isListening = false;

        public TCP_SERVER()
        {
            InitializeComponent();
        }

        private void btn_Listen_Click(object sender, EventArgs e)
        {
            if (isListening)
            {
                MessageBox.Show("Server đã được bật và đang lắng nghe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int port = 8080;
                string localIP = GetLocalIPAddress();
                tcpServer = new TcpListener(IPAddress.Parse(localIP), port);
                tcpServer.Start();
                isListening = true;

                serverThread = new Thread(StartListening);
                serverThread.Start();

                MessageBox.Show("Server đã bắt đầu lắng nghe trên địa chỉ IP: " + localIP + " trên cổng " + port, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi động server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartListening()
        {
            try
            {
                while (isListening)
                {
                    TcpClient client = tcpServer.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                if (isListening)
                {
                    MessageBox.Show("Lỗi trong quá trình lắng nghe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Hiển thị tin nhắn nhận được từ client lên giao diện
                Invoke((MethodInvoker)(() =>
                {
                    Console.WriteLine("Nhận được tin nhắn: " + message);
                }));

                // Phản hồi lại client
                byte[] response = Encoding.UTF8.GetBytes("Server đã nhận tin nhắn của bạn.");
                stream.Write(response, 0, response.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý client: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                client.Close();
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("không tìm thấy địa chỉ IP!");
        }
        private void TCP_SERVER_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseServer();
        }

        private void CloseServer()
        {
            if (isListening)
            {
                isListening = false;
                tcpServer?.Stop();
                serverThread?.Join(); // Đảm bảo luồng server kết thúc trước khi đóng ứng dụng

                MessageBox.Show("Server đã được đóng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
