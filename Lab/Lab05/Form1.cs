using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lab05_Bai01 Bai01Form = new Lab05_Bai01();
            Bai01Form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab05_Bai02 Bai02Form = new Lab05_Bai02();
            Bai02Form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lab05_Bai03 Bai03Form = new Lab05_Bai03();
            Bai03Form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lab05_Bai04 Bai04Form = new Lab05_Bai04();
            Bai04Form.Show();
        }
    }
}
