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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            WhatToEat WhatToEatForm = new WhatToEat();
            WhatToEatForm.Show();
            this.Close();
        }

        private void btnRTSignup_Click(object sender, EventArgs e)
        {
            Signup SignupForm = new Signup();
            SignupForm.Show();
            this.Close();
        }
    }
}