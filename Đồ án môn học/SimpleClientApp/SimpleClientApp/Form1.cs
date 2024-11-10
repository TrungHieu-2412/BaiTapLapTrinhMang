using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnSendMessage.Click += new EventHandler(btnSendMessage_Click);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string serverIp = "100.27.231.216"; // Địa chỉ IP công khai của server
            int port = 9999; // Cổng server đang lắng nghe

            try
            {
                // Tạo kết nối tới server
                using (TcpClient client = new TcpClient())
                {
                    lblStatus.Text = "Đang kết nối tới server...";
                    client.Connect(serverIp, port);

                    // Chuẩn bị tin nhắn
                    string message = txtMessage.Text;
                    byte[] buffer = Encoding.UTF8.GetBytes(message);

                    // Gửi tin nhắn qua stream
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();

                    lblStatus.Text = "Tin nhắn đã gửi thành công!";
                }
            }
            catch (SocketException ex)
            {
                lblStatus.Text = $"Không thể kết nối tới server: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Lỗi: {ex.Message}";
            }
        }
    }
}
