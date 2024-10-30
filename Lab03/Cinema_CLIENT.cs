using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        private void ResetSelectedSeat()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Name.StartsWith("btnSeat") &&
                    int.Parse(control.Text) == selectedSeat)
                {
                    control.BackColor = Color.White;
                    break;
                }
            }
            selectedSeat = 0;
        }

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

                // Receive the server response
                byte[] buffer = new byte[1024];
                int bytesReceived = clientSocket.Receive(buffer);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

                // Update seat status based on server response
                if (response == "booked")
                {
                    // Disable the booked seat
                    foreach (Control control in this.Controls)
                    {
                        if (control is Button && control.Name.StartsWith("btnSeat") &&
                            int.Parse(control.Text) == selectedSeat)
                        {
                            control.BackColor = Color.Gray;
                            control.Enabled = false;
                            break;
                        }
                    }

                    MessageBox.Show("Seat booked successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response == "already booked")
                {
                    MessageBox.Show("Seat is already booked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetSelectedSeat();
                }
                else
                {
                    MessageBox.Show("Booking failed: " + response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Reset the seat selection
                selectedSeat = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Booking Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
