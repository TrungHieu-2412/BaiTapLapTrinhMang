using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DrawTogether.Client.Model;
using Newtonsoft.Json;


namespace DrawTogether.Client.Networking
{
    internal class ClientNetworkManager
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private IPEndPoint serverEndPoint;
        private CancellationTokenSource cancellationTokenSource;

        // Constructor sử dụng client, reader, writer, serverEndPoint đã được cung cấp
        public ClientNetworkManager(IPEndPoint serverEndPoint)
        {
            this.serverEndPoint = serverEndPoint;
            cancellationTokenSource = new CancellationTokenSource();
        }
        public NetworkStream GetStream()
        {
            // Logic để trả về NetworkStream từ kết nối
            if (client != null && client.Connected)
            {
                return client.GetStream();
            }
            else
            {
                return null; // Hoặc ném ngoại lệ
            }
        }
        public bool Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverEndPoint);
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
                return false;
            }
        }

        public void Send(Packet packet)
        {
            try
            {
                string messageInJson = JsonConvert.SerializeObject(packet);
                writer.WriteLine(messageInJson);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi dữ liệu: " + ex.Message);
            }
        }

        public Packet Receive()
        {
            try
            {
                string responseInJson = reader.ReadLine();
                return JsonConvert.DeserializeObject<Packet>(responseInJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nhận dữ liệu: " + ex.Message);
                return null;
            }
        }

        public void Close()
        {
            client.Close();
            cancellationTokenSource.Cancel();
        }
    }
}