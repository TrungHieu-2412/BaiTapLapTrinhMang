using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab03
{
    public partial class UdpClientForm : Form
    {
        public UdpClientForm()
        {
            InitializeComponent();
            this.txtIP.Text = "127.0.0.1";
            
        }
        
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string ipAddress = txtIP.Text;
                string portText = txtPort.Text;
                int port;

                // Kiểm tra IP và port hợp lệ
                if (!IPAddress.TryParse(ipAddress, out _) || !IsPortValid(portText, out port))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ IP và port hợp lệ (1-65535).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Tạo UDP client và gửi tin nhắn
                using (UdpClient udpClient = new UdpClient())
                {
                    byte[] sendBytes = Encoding.UTF8.GetBytes(txtMessage.Text);
                    udpClient.Send(sendBytes, sendBytes.Length, ipAddress, port);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi tin nhắn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


    }
}
