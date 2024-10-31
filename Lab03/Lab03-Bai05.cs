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
    public partial class CINEMA_BOOK_TICKET : Form
    {
        public CINEMA_BOOK_TICKET()
        {
            InitializeComponent();
            btnStartClient.Click += new EventHandler(btnStartClient_Click);
            btnStartServer.Click += new EventHandler(btnStartServer_Click);

        }

        private void CINEMA_BOOK_TICKET_Load(object sender, EventArgs e)
        {

        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            Cinema_SERVER serverForm = new Cinema_SERVER();
            serverForm.Show();
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
            Cinema_CLIENT clientForm = new Cinema_CLIENT();
            clientForm.Show();
        }
    }
}
