using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Thêm namespace này để sử dụng Task

public class ServerManager
{
    private Dictionary<string, List<Tuple<Socket, string>>> rooms =
        new Dictionary<string, List<Tuple<Socket, string>>>();
    private int port = 12345; // Cổng mặc định

    public void StartServer()
    {
        // 1. Tạo socket lắng nghe
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 2. Gán địa chỉ IP và cổng
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);

        // 3. Liên kết socket với địa chỉ và cổng
        listener.Bind(localEndPoint);

        // 4. Bắt đầu lắng nghe
        listener.Listen(10);

        Console.WriteLine($"Server đang lắng nghe trên cổng {port}...");

        // 5. Chấp nhận kết nối từ client
        while (true)
        {
            Socket clientSocket = listener.Accept();

            // Tạo luồng mới để xử lý client
            Thread clientThread = new Thread(() => HandleClient(clientSocket));
            clientThread.Start();
        }
    }

    // Hàm xử lý client
    private async void HandleClient(Socket clientSocket)
    {
        byte[] buffer = new byte[1024];
        int bytesReceived;

        try
        {
            while (true)
            {
                // 1. Nhận dữ liệu từ client
                bytesReceived = clientSocket.Receive(buffer);

                // 2. Chuyển đổi dữ liệu thành chuỗi
                string data = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                Console.WriteLine($"Nhận dữ liệu từ client: {data}");

                // 3. Xử lý dữ liệu
                await ProcessData(clientSocket, data); // Sử dụng async/await để xử lý dữ liệu đồng bộ
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi kết nối
            Console.WriteLine($"Lỗi kết nối: {ex.Message}");
            clientSocket.Close();
            RemoveClientFromRoom(clientSocket);
        }
    }

    private async Task ProcessData(Socket clientSocket, string data) // Thay đổi hàm ProcessData thành async Task
    {
        // Xử lý yêu cầu JoinRoom
        if (data.StartsWith("JoinRoom:"))
        {
            string[] parts = data.Substring(9).Split(',');
            string roomCode = parts[0];
            string userName = parts[1];

            await JoinRoom(clientSocket, roomCode, userName); // Sử dụng await cho JoinRoom
        }
        // Xử lý dữ liệu vẽ (ví dụ: data có format "Vẽ:x,y,color,width")
        else if (data.StartsWith("Vẽ:"))
        {
            // Lấy thông tin từ data
            // ...
            // Tìm room code của client hiện tại
            string roomCode = GetRoomCode(clientSocket);

            // Broadcast dữ liệu vẽ cho các client trong cùng phòng
            BroadcastData(roomCode, data);
        }
        // ... xử lý các loại dữ liệu khác
    }

    // Hàm xử lý JoinRoom
    private async Task JoinRoom(Socket clientSocket, string roomCode, string userName) // Thay đổi hàm JoinRoom thành async Task
    {
        if (roomCode == "")
        {
            roomCode = GenerateRoomCode(); // Tạo room code nếu chưa có
            rooms.Add(roomCode, new List<Tuple<Socket, string>>());
            Console.WriteLine($"Phòng mới được tạo: {roomCode}");
        }

        if (!rooms.ContainsKey(roomCode))
        {
            rooms.Add(roomCode, new List<Tuple<Socket, string>>());
        }
        rooms[roomCode].Add(new Tuple<Socket, string>(clientSocket, userName));

        // Gửi room code mới cho client
        clientSocket.Send(Encoding.ASCII.GetBytes($"RoomCode:{roomCode}"));

        // Cập nhật danh sách người chơi cho tất cả các client trong phòng
        UpdatePlayersList(roomCode);
    }

    private string GenerateRoomCode()
    {
        Random random = new Random();
        return random.Next(1000, 9999).ToString(); // Tạo room code gồm 4 chữ số ngẫu nhiên
    }

    // Broadcast dữ liệu cho các client trong cùng phòng
    public void BroadcastData(string roomCode, string data)
    {
        if (rooms.ContainsKey(roomCode))
        {
            foreach (Tuple<Socket, string> client in rooms[roomCode])
            {
                client.Item1.Send(Encoding.ASCII.GetBytes(data));
            }
        }
    }

    // Cập nhật danh sách người chơi cho tất cả các client trong phòng
    private void UpdatePlayersList(string roomCode)
    {
        if (rooms.ContainsKey(roomCode))
        {
            List<string> playerNames = rooms[roomCode].Select(c => c.Item2).ToList();
            string playerList = $"Players:{string.Join(",", playerNames)}";

            foreach (Tuple<Socket, string> client in rooms[roomCode])
            {
                client.Item1.Send(Encoding.ASCII.GetBytes(playerList));
            }
        }
    }

    // Hàm tìm room code của client hiện tại
    private string GetRoomCode(Socket clientSocket)
    {
        foreach (var room in rooms)
        {
            if (room.Value.Any(c => c.Item1 == clientSocket))
            {
                return room.Key;
            }
        }
        return null; // Client không thuộc phòng nào
    }

    // Hàm xóa client khỏi room
    private void RemoveClientFromRoom(Socket clientSocket)
    {
        foreach (var room in rooms)
        {
            if (room.Value.Any(c => c.Item1 == clientSocket))
            {
                room.Value.RemoveAll(c => c.Item1 == clientSocket);
                // Cập nhật danh sách người chơi cho tất cả các client trong phòng
                UpdatePlayersList(room.Key);
            }
        }
    }
}