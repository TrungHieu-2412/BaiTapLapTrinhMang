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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lab03_Bai01 bai1Form = new Lab03_Bai01();
            bai1Form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab03_Bai04 bai4Form = new Lab03_Bai04();
            bai4Form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CINEMA_BOOK_TICKET bai5Form = new CINEMA_BOOK_TICKET();
            bai5Form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
