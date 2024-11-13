using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lab04_Bai123 Bai123Form = new Lab04_Bai123();
            Bai123Form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab04_Bai04 Bai04Form = new Lab04_Bai04();
            Bai04Form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lab04_Bai05 Bai05Form = new Lab04_Bai05();
            Bai05Form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lab04_Bai06 Bai06Form = new Lab04_Bai06();
            Bai06Form.Show();
            this.Hide();
        }
    }
}
