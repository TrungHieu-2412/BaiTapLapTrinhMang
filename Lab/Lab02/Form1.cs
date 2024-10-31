using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lab02_Bai01 bai1Form = new Lab02_Bai01();
            bai1Form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab02_Bai02 Bai2Form = new Lab02_Bai02();
            Bai2Form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lab02_Bai03 bai3Form = new Lab02_Bai03();
            bai3Form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lab02_Bai04 Bai4Form = new Lab02_Bai04();
            Bai4Form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Lab02_Bai05 Bai5Form = new Lab02_Bai05();
            Bai5Form.Show();
        }
    }
}
