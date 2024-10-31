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
            Lab04_Bai123 bai123Form = new Lab04_Bai123();
            bai123Form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lab04_Bai45 bai45Form = new Lab04_Bai45();
            bai45Form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lab04_Bai06 bai06Forn = new Lab04_Bai06();
            bai06Forn.Show();
        }
    }
}
