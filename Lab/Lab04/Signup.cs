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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            WhatToEat WhatToEatForm = new WhatToEat();
            WhatToEatForm.Show();
            this.Close();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Lab04_Bai06 Lab04_Bai06Form = new Lab04_Bai06();
            Lab04_Bai06Form.Show();
            this.Close();
        }
    }
}