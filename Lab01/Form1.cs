using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lab01_Bai01 bai1Form = new Lab01_Bai01();
            bai1Form.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Lab01_Bai02 bai2Form = new Lab01_Bai02();
            bai2Form.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Lab01_Bai03 bai3Form = new Lab01_Bai03();
            bai3Form.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Lab01_Bai04 bai4Form = new Lab01_Bai04();
            bai4Form.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Lab01_Bai05 bai5Form = new Lab01_Bai05();
            bai5Form.Show();
        }
    }
}
