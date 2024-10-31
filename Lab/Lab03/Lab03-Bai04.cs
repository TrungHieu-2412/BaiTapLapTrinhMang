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
    public partial class Lab03_Bai04 : Form
    {
        public Lab03_Bai04()
        {
            InitializeComponent();
        }

        private void btn_Open_Server_Click(object sender, EventArgs e)
        {
            TCP_SERVER tCP_SERVER = new TCP_SERVER();
            tCP_SERVER.Show();
        }

        private void btn_Open_Client_Click(object sender, EventArgs e)
        {
            TCP_CLIENT tCP_CLIENT = new TCP_CLIENT();
            tCP_CLIENT.Show();
        }
    }
}
