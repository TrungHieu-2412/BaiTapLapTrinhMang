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
    public partial class Cinema_CLIENT : Form
    {

        private Socket clientSocket;
        private int selectedSeat = 0;

        public Cinema_CLIENT()
        {
            InitializeComponent();
            btnComfirm.Click += new EventHandler(btnComfirm_Click);
            btnConnect.Click += new EventHandler(btnConnect_Click);
            btnSeat1.Click += new EventHandler(btnSeat1_Click);
            btnSeat2.Click += new EventHandler(btnSeat2_Click);
            btnSeat3.Click += new EventHandler(btnSeat3_Click);
            btnSeat4.Click += new EventHandler(btnSeat4_Click);
            btnSeat5.Click += new EventHandler(btnSeat5_Click);
            btnSeat6.Click += new EventHandler(btnSeat6_Click);
            btnSeat7.Click += new EventHandler(btnSeat7_Click);
            btnSeat8.Click += new EventHandler(btnSeat8_Click);
            btnSeat9.Click += new EventHandler(btnSeat9_Click);
            btnSeat10.Click += new EventHandler(btnSeat10_Click);
            btnSeat11.Click += new EventHandler(btnSeat11_Click);
            btnSeat12.Click += new EventHandler(btnSeat12_Click);
            btnSeat13.Click += new EventHandler(btnSeat13_Click);
            btnSeat14.Click += new EventHandler(btnSeat14_Click);
            btnSeat15.Click += new EventHandler(btnSeat15_Click);
            btnSeat16.Click += new EventHandler(btnSeat16_Click);
            btnSeat17.Click += new EventHandler(btnSeat17_Click);
            btnSeat18.Click += new EventHandler(btnSeat18_Click);
            btnSeat19.Click += new EventHandler(btnSeat19_Click);
            btnSeat20.Click += new EventHandler(btnSeat20_Click);
            btnSeat21.Click += new EventHandler(btnSeat21_Click);
            btnSeat22.Click += new EventHandler(btnSeat22_Click);
            btnSeat23.Click += new EventHandler(btnSeat23_Click);
            btnSeat24.Click += new EventHandler(btnSeat24_Click);
            btnSeat25.Click += new EventHandler(btnSeat25_Click);
        }

        private void Cinema_CLIENT_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Connect to the server (localhost)
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse("127.0.0.1"); // Localhost IP
                IPEndPoint remoteEndPoint = new IPEndPoint(ip, int.Parse(txtPort.Text));
                clientSocket.Connect(remoteEndPoint);

                // Enable the seat buttons
                EnableSeatButtons();
                btnConnect.Enabled = false;

                // Indicate successful connection
                MessageBox.Show("Connected to server!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Start a thread to receive seat updates
                Thread updateThread = new Thread(ReceiveSeatUpdates);
                updateThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Enable all seat buttons
        private void EnableSeatButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Name.StartsWith("btnSeat"))
                {
                    control.Enabled = true;
                }
            }
        }

        // Handle seat button clicks
        private void SeatButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int seatNumber = int.Parse(clickedButton.Text);

            // If a seat is already selected, reset it
            if (selectedSeat != 0)
            {
                ResetSelectedSeat();
            }

            // Select the clicked seat
            selectedSeat = seatNumber;
            clickedButton.BackColor = Color.LightSkyBlue;
        }

        // Reset the previously selected seat
        // Reset the previously selected seat
        private void ResetSelectedSeat()
        {
            int selectedSeatInt = 0; // Khai báo biến để lưu kết quả chuyển đổi

            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Name.StartsWith("btnSeat") &&
                    int.TryParse(control.Text, out selectedSeatInt) && selectedSeatInt == selectedSeat)
                {
                    control.BackColor = Color.White;
                    break;
                }
            }
            selectedSeat = 0;
        }



        // Send the booking request to the server
        // Send the booking request to the server
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            if (selectedSeat == 0)
            {
                MessageBox.Show("Please select a seat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Send the booking request to the server
                string bookingRequest = $"{txtName.Text},{selectedSeat}";
                byte[] data = Encoding.ASCII.GetBytes(bookingRequest);
                clientSocket.Send(data);

                // Receive the server response (not needed here)
                // ...

                // Update seat status on the client
                int selectedSeatInt = 0; // Khai báo biến selectedSeatInt
                foreach (Control control in this.Controls)
                {
                    if (control is Button && control.Name.StartsWith("btnSeat") &&
                        int.TryParse(control.Text, out selectedSeatInt) && selectedSeatInt == selectedSeat)
                    {
                        control.BackColor = Color.Gray;
                        control.Enabled = false;
                        break;
                    }
                }

                MessageBox.Show("Seat booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset the seat selection
                selectedSeat = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Booking Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Receive seat updates from the server
        private void ReceiveSeatUpdates()
        {
            while (true)
            {
                try
                {
                    // Receive data from the server
                    byte[] buffer = new byte[1024];
                    int bytesReceived = clientSocket.Receive(buffer);
                    string seatUpdates = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

                    // Process the seat updates
                    if (!string.IsNullOrEmpty(seatUpdates))
                    {
                        // Gọi hàm UpdateSeats trên UI thread
                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => UpdateSeats(seatUpdates)));
                        }
                        else
                        {
                            UpdateSeats(seatUpdates);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., server disconnect)
                    MessageBox.Show("Error receiving seat updates: " + ex.Message);
                    // Disconnect from the server if connection is lost
                    if (clientSocket != null)
                    {
                        clientSocket.Close();
                    }
                    // Disable booking actions
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => btnComfirm.Enabled = false));
                    }
                    else
                    {
                        btnComfirm.Enabled = false;
                    }
                    break;
                }
            }
        }

        // Update the seat buttons based on the received seat updates
        // Update the seat buttons based on the received seat updates
        private void UpdateSeats(string seatUpdates)
        {
            // Split the updates into individual seat information
            string[] seatInfos = seatUpdates.Split(';');

            // Update each seat button
            foreach (string seatInfo in seatInfos)
            {
                if (!string.IsNullOrEmpty(seatInfo))
                {
                    string[] parts = seatInfo.Split(','); // Giả sử dấu phân cách là dấu phẩy
                    int seatNumber = 0;
                    string clientName = "";

                    // Sử dụng int.TryParse() để kiểm tra và chuyển đổi
                    if (int.TryParse(parts[0], out seatNumber) && parts.Length > 1)
                    {
                        clientName = parts[1]; // Lấy tên người đặt

                        // Find the corresponding button
                        Button btn = this.Controls.Find($"btnSeat{seatNumber}", true).FirstOrDefault() as Button;
                        if (btn != null)
                        {
                            btn.BackColor = Color.Gray;
                            btn.Enabled = false;
                            //btn.Text = $"{seatNumber} ({clientName})";
                            btn.Text = $"{seatNumber}";

                        }
                    }
                }
            }
        }

        // Handle seat button click events
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
    }
}
