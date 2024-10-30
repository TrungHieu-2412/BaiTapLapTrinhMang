using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class Cinema_SERVER : Form
    {

        // Server socket
        private Socket serverSocket;
        // List of connected clients
        private List<Socket> clients = new List<Socket>();
        // Seat status dictionary (key: seat number, value: client name)
        private Dictionary<int, string> seatStatus = new Dictionary<int, string>();


        public Cinema_SERVER()
        {
            InitializeComponent();

            btnListen.Click += new EventHandler(btnListen_Click);



            for (int i = 1; i <= 25; i++)
            {
                seatStatus[i] = "";
            }
            UpdateSeatStatus();
        }

        private void Cinema_SERVER_Load(object sender, EventArgs e)
        {

        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Bind the socket to a specific port
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, int.Parse(txtPort.Text)));
                // Start listening for incoming connections
                serverSocket.Listen(10);
                // Create a new thread to accept incoming connections
                Thread listenThread = new Thread(AcceptClients);
                listenThread.Start();

                // Disable listen button
                btnListen.Enabled = false;

                // Indicate successful listening
                MessageBox.Show("Server is listening for connections!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Accept incoming client connections
        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    // Accept a new client
                    Socket client = serverSocket.Accept();
                    // Add the client to the list of connected clients
                    clients.Add(client);
                    // Start a new thread to handle the client
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                    // Update the number of connections (safely on UI thread)
                    UpdateConnectionCount();
                }
                catch (Exception ex)
                {
                    // Handle exception gracefully
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Handle client communication
        private void HandleClient(Socket client)
        {
            while (true)
            {
                try
                {
                    // Receive data from the client
                    byte[] buffer = new byte[1024];
                    int bytesReceived = client.Receive(buffer);
                    string request = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

                    // Process the client request
                    ProcessClientRequest(client, request);
                }
                catch (Exception ex)
                {
                    // Handle exceptions gracefully, such as client disconnection
                    MessageBox.Show("Error: " + ex.Message);
                    // Remove the client from the list of connected clients
                    clients.Remove(client);
                    // Close the client socket
                    client.Close();
                    // Update the number of connections (safely on UI thread)
                    UpdateConnectionCount();
                    // Break the loop for this client
                    break;
                }
            }
        }

        // Process client booking requests
        private void ProcessClientRequest(Socket client, string request)
        {
            try
            {
                // Split the request into client name and seat number
                string[] parts = request.Split(',');
                string clientName = parts[0];
                int seatNumber = int.Parse(parts[1]);

                // Check if the seat is already booked
                if (seatStatus[seatNumber] != "")
                {
                    // Send "already booked" response to the client
                    byte[] data = Encoding.ASCII.GetBytes("already booked");
                    client.Send(data);
                }
                else
                {
                    // Book the seat and send "booked" response to the client
                    seatStatus[seatNumber] = clientName;
                    byte[] data = Encoding.ASCII.GetBytes("booked");
                    client.Send(data);

                    // Update the seat status on the server UI
                    UpdateSeatStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing client request: " + ex.Message);
            }
        }

        // Delegate for updating the number of connections label
        private delegate void UpdateConnectionCountDelegate(int connectionCount);
        private void UpdateConnectionCount(int connectionCount)
        {
            Number_of_connections.Text = connectionCount.ToString();
        }
        // Update the number of connections label on the server UI (safely on UI thread)
        private void UpdateConnectionCount()
        {
            if (Number_of_connections.InvokeRequired)
            {
                Number_of_connections.Invoke(new UpdateConnectionCountDelegate(UpdateConnectionCount), clients.Count);
            }
            else
            {
                Number_of_connections.Text = clients.Count.ToString();
            }
        }

        // Update the seat status on the server UI
        private void UpdateSeatStatus()
        {
            // Update the seat buttons based on the seatStatus dictionary
            for (int i = 1; i <= 25; i++)
            {
                Button btn = this.Controls.Find($"btnSeat{i}", true).FirstOrDefault() as Button;
                if (btn != null)
                {
                    if (seatStatus[i] != "")
                    {
                        btn.BackColor = Color.Gray;
                        btn.Enabled = false;
                    }
                    else
                    {
                        btn.BackColor = Color.White;
                        btn.Enabled = true;
                    }
                }
            }

            // Update the number of selected seats and empty seats
            Number_of_seats_selected.Text = seatStatus.Where(x => x.Value != "").Count().ToString();
            Number_of_empty_seats.Text = seatStatus.Where(x => x.Value == "").Count().ToString();
        }

        // Handle seat button click events for visual updates
        private void btnSeat1_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat2_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat3_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat4_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat5_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat6_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat7_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat8_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat9_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat10_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat11_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat12_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat13_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat14_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat15_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat16_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat17_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat18_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat19_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat20_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat21_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat22_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat23_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat24_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }
        private void btnSeat25_Click(object sender, EventArgs e) { SeatButtonClick(sender, e); }

        // Visual update for seat button clicks
        private void SeatButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int seatNumber = int.Parse(clickedButton.Text);
            if (seatStatus[seatNumber] != "")
            {
                MessageBox.Show("This seat is already booked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Simulate booking (change color, etc.)
                // ...
            }
        }
    }
}
