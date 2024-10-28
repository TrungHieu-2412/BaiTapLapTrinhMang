using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class Lab03_Bai01 : Form
    {
        public Lab03_Bai01()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            UdpServer serverForm = new UdpServer();
            serverForm.Show();
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
            UdpClientForm clientForm = new UdpClientForm();
            clientForm.Show();
        }
    }
}
