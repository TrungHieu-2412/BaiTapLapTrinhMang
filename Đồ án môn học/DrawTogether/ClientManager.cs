using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class ClientManager
{
    private Socket clientSocket;
    private IPEndPoint serverEndPoint;
    public event EventHandler<DataReceivedEventArgs> DataReceived;

    public ClientManager()
    {
        // Cấu hình kết nối đến server (IP address và port)
        // Thay đổi IP address và port cho phù hợp với Web App trên Azure
        serverEndPoint = new IPEndPoint(IPAddress.Parse("YOUR_SERVER_IP"), 12345);
    }

    public void JoinRoom(string roomCode, string userName)
    {
        try
        {
            // Tạo socket và kết nối đến server
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverEndPoint);

            // Gửi yêu cầu join room code và tên người chơi đến server
            string request = $"JoinRoom:{roomCode},{userName}";
            byte[] buffer = Encoding.ASCII.GetBytes(request);
            clientSocket.Send(buffer);

            // Bắt đầu lắng nghe dữ liệu từ server
            Thread receiveThread = new Thread(() => ReceiveData());
            receiveThread.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
    }

    public void SendData(string data)
    {
        if (clientSocket != null && clientSocket.Connected)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                clientSocket.Send(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi dữ liệu: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Chưa kết nối đến server.");
        }
    }

    private void ReceiveData()
    {
        byte[] buffer = new byte[1024];
        int bytesReceived;

        try
        {
            while (true)
            {
                // Nhận dữ liệu từ server
                bytesReceived = clientSocket.Receive(buffer);
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

                // Gọi event DataReceived
                OnDataReceived(new DataReceivedEventArgs(dataReceived));
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi kết nối
            MessageBox.Show($"Lỗi kết nối: {ex.Message}");
        }
        finally
        {
            // Đóng kết nối
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Close();
            }
        }
    }

    // Gọi event DataReceived
    protected virtual void OnDataReceived(DataReceivedEventArgs e)
    {
        DataReceived?.Invoke(this, e);
    }
}

// Class DataReceivedEventArgs
public class DataReceivedEventArgs : EventArgs
{
    public string Data { get; private set; }

    public DataReceivedEventArgs(string data)
    {
        Data = data;
    }
}