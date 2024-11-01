using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lab03
{
    public partial class TCP_CLIENT : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;

        public TCP_CLIENT()
        {
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                MessageBox.Show("Đã kết nối với server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string serverIP = txt_IP_Address.Text; // Lấy địa chỉ IP từ TextBox
            int port = 8080;

            if (string.IsNullOrEmpty(serverIP) || !IPAddress.TryParse(serverIP, out _))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ IP hợp lệ của server.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                client = new TcpClient();
                client.Connect(serverIP, port);
                stream = client.GetStream();
                isConnected = true;

                // Gửi tên client đến server
                string clientName = txt_Your_Name.Text; // TextBox chứa tên client
                byte[] nameData = Encoding.UTF8.GetBytes(clientName);
                stream.Write(nameData, 0, nameData.Length);

                MessageBox.Show("Kết nối thành công với server!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Bắt đầu lắng nghe tin nhắn từ server
                Thread listenThread = new Thread(ListenForMessages);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Hãy kết nối tới server trước.");
                return;
            }

            string message = txt_Message.Text;
            string clientName = txt_Your_Name.Text;

            if (!string.IsNullOrEmpty(message))
            {
                string fullMessage = $"{clientName}: {message}"; // Thêm tên client vào tin nhắn
                byte[] data = Encoding.UTF8.GetBytes(fullMessage);
                stream.Write(data, 0, data.Length);

                AppendText($"Me: {message}\r\n"); // Hiển thị tin nhắn cho chính client
                txt_Message.Clear();
            }
        }

        private void ListenForMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while (isConnected) // Kiểm tra xem kết nối có còn mở không
                {
                    // Kiểm tra thêm điều kiện để ngắt kết nối
                    if (stream != null && stream.DataAvailable) // Kiểm tra xem có dữ liệu không
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // Hiển thị tin nhắn nhận được lên txt_Display
                        Invoke((MethodInvoker)(() =>
                        {
                            txt_Display.AppendText(message + Environment.NewLine);
                        }));
                    }
                    Thread.Sleep(100); // Giảm thiểu tài nguyên CPU khi không có dữ liệu
                }
            }
            catch (Exception ex)
            {
                if (isConnected) // Chỉ hiển thị lỗi khi đang kết nối
                {
                    MessageBox.Show("Lỗi khi nhận tin nhắn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Send_File_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Chưa kết nối với server.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] fileData = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                        stream.Write(fileData, 0, fileData.Length);

                        MessageBox.Show("Đã gửi file thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi gửi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
            {
                string clientName = txt_Your_Name.Text;

                // Gửi thông báo ngắt kết nối kèm tên client đến server
                byte[] disconnectMessage = Encoding.UTF8.GetBytes($"{clientName} đã rời khỏi đoạn chat.");
                stream.Write(disconnectMessage, 0, disconnectMessage.Length);

                AppendText($"Bạn đã rời khỏi đoạn chat.\r\n");
            }
        }

        private void TCP_CLIENT_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
        }

        private void CloseConnection()
        {
            if (isConnected)
            {
                if (isConnected)
                {
                    stream.Close();
                    client.Close();
                    isConnected = false;

                    MessageBox.Show("Đã ngắt kết nối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => txt_Display.AppendText(text)));
            }
            else
            {
                txt_Display.AppendText(text);
            }
        }
    }
}
