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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            ApiHelper apiHelper = new ApiHelper();
            var token = await apiHelper.Login(email, password);

            if (token != null)
            {
                // Lưu token vào biến toàn cục hoặc session
                GlobalVariables.AccessToken = token.AccessToken;

                WhatToEat WhatToEatForm = new WhatToEat();
                WhatToEatForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRTSignup_Click(object sender, EventArgs e)
        {
            Signup SignupForm = new Signup();
            SignupForm.Show();
            this.Hide();
        }
    }
}