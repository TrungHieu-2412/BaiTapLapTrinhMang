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
            comboBox1.Items.Add("Vi"); // Ví dụ
            comboBox1.SelectedIndex = 0;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string email = textBox3.Text;
            string firstName = textBox4.Text;
            string lastName = textBox5.Text;
            string phone = textBox6.Text;
            DateTime birthday = dateTimePicker1.Value;
            string language = comboBox1.SelectedItem.ToString();
            string sex = radioButton1.Checked ? "Male" : "Female";

            ApiHelper apiHelper = new ApiHelper();
            var result = await apiHelper.Signup(username, password, email, firstName, lastName, phone, birthday, language, sex);

            if (result != null)
            {
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                WhatToEat WhatToEatForm = new WhatToEat();
                WhatToEatForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Lab04_Bai06 Lab04_Bai06Form = new Lab04_Bai06();
            Lab04_Bai06Form.Show();
            this.Hide();
        }
    }
}