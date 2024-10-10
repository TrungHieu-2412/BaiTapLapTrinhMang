using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace Multi_chat_TCP
{
    public partial class Server : Form
    {
        private IPEndPoint IP;
        private Socket server;
        private List<Socket> clientList;
        private Dictionary<Socket, string> clientNames; // Tên client dựa trên socket
        private int clientCount = 0; // Đếm số lượng client

        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
            clientNames = new Dictionary<Socket, string>(); // Khởi tạo từ điển tên client
        }

        private void Server_Load(object sender, EventArgs e)
        {

        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseServer();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cbClientList.SelectedItem is string selectedClientName)
            {
                foreach (var kvp in clientNames)
                {
                    if (kvp.Value == selectedClientName) // Tìm client dựa trên tên
                    {
                        Send(kvp.Key); // Gửi tin nhắn đến client được chọn
                        break;
                    }
                }
            }
            txbMessage.Clear();
        }
        private void btnSendToAll_Click(object sender, EventArgs e)
        {
            // Gửi tin nhắn đến tất cả client
            SendToAll();
            txbMessage.Clear();
        }

        // Gửi tin nhắn đến tất cả client
        private void SendToAll()
        {
            if (!string.IsNullOrEmpty(txbMessage.Text))
            {
                string message = $"Server (tất cả): {txbMessage.Text}"; // Tin nhắn từ server
                foreach (Socket client in clientList)
                {
                    client.Send(Serialize(message)); // Gửi tin nhắn đến từng client
                }

                // Hiển thị tin nhắn trên server
                AddMessage(message);
            }
        }
        // Khởi tạo kết nối
        private void Connect()
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                server.Bind(IP);
                Thread listenThread = new Thread(() =>
                {
                    try
                    {
                        while (true)
                        {
                            server.Listen(100);
                            Socket client = server.Accept();
                            clientCount++; // Tăng số lượng client kết nối
                            string clientName = $"Client{clientCount}"; // Tạo tên cho client
                            clientList.Add(client);
                            clientNames[client] = clientName; // Lưu tên client trong từ điển

                            // Cập nhật danh sách client trên giao diện
                            UpdateClientList();

                            Thread receiveThread = new Thread(() => Receive(client, clientName));
                            receiveThread.IsBackground = true;
                            receiveThread.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                        RestartServer();
                    }
                });
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Socket Error: " + ex.Message);
            }
        }

        // Cập nhật danh sách client vào combobox
        private void UpdateClientList()
        {
            cbClientList.Items.Clear();
            foreach (var name in clientNames.Values)
            {
                cbClientList.Items.Add(name);
            }
        }

        // Đóng kết nối server
        private void CloseServer()
        {
            if (server != null)
            {
                server.Close();
            }
        }

        // Khởi động lại server trong trường hợp có lỗi
        private void RestartServer()
        {
            IP = new IPEndPoint(IPAddress.Any, 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }

        // Gửi tin nhắn tới client
        private void Send(Socket client)
        {
            if (!string.IsNullOrEmpty(txbMessage.Text))
            {
                // Gửi tin nhắn với tên là "Server"
                string message = $"Server: {txbMessage.Text}";
                client.Send(Serialize(message));
            }
        }

        // Gửi tin nhắn từ client
        void Send(Socket client, string senderName)
        {
            if (!string.IsNullOrEmpty(txbMessage.Text))
            {
                // Đóng gói tin nhắn kèm với tên người gửi
                string message = $"{senderName}: {txbMessage.Text}";
                client.Send(Serialize(message));
            }
        }

        // Thêm tin nhắn vào khung chat
        private void AddMessage(string message)
        {
            LsvMessage.Items.Add(new ListViewItem() { Text = message });
        }

        // Nhận tin nhắn từ client
        private void Receive(Socket client, string clientName)
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);

                    // Deserialize tin nhắn
                    string message = (string)Deserialize(data);

                    // Hiển thị tin nhắn với tên client
                    string formattedMessage = $"{clientName}: {message}";

                    // Hiển thị tin nhắn trên server
                    AddMessage(formattedMessage);

                    // Gửi lại tin nhắn cho các client khác
                    foreach (Socket item in clientList)
                    {
                        if (item != client) // Không gửi lại cho chính người gửi
                        {
                            item.Send(Serialize(formattedMessage));
                        }
                    }
                }
            }
            catch
            {
                clientList.Remove(client);
                clientNames.Remove(client); // Xóa tên client ra khỏi từ điển
                UpdateClientList(); // Cập nhật lại danh sách client
                client.Close();
            }
        }

        // Serialize (phân mảnh) dữ liệu để gửi
        private byte[] Serialize(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }

        // Deserialize (gộp mảnh) dữ liệu nhận được
        private object Deserialize(byte[] data)
        {
            MemoryStream memoryStream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(memoryStream);
        }
    }
}
