using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private IPEndPoint IP;
        private Socket client;
        private string clientName = "Client1"; // Đặt tên client

        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        // Khi form bị đóng
        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseClient();
        }

        // Khi nhấn nút gửi tin nhắn
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
            txbMessage.Clear();
        }

        // Khởi tạo kết nối tới server
        private void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                client.Connect(IP);
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đóng kết nối client
        private void CloseClient()
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }
        }

        // Gửi tin nhắn đến server
        void SendMessage()
        {
            if (!string.IsNullOrEmpty(txbMessage.Text))
            {
                // Đóng gói tin nhắn với tên người gửi
                string message = $"{clientName}: {txbMessage.Text}";
                client.Send(Serialize(message));
            }
        }

        // Nhận tin nhắn từ server
        void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    int receivedDataSize = client.Receive(data);

                    if (receivedDataSize > 0)
                    {
                        // Giải nén tin nhắn
                        string message = (string)Deserialize(data);

                        // Hiển thị tin nhắn kèm tên người gửi
                        AddMessage(message);
                    }
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Mất kết nối tới server", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseClient();
            }
        }

        // Thêm tin nhắn vào ListView để hiển thị
        private void AddMessage(string message)
        {
            LsvMessage.Items.Add(new ListViewItem() { Text = message });
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