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
    public partial class AddDishes : Form
    {
        public AddDishes()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string gia = textBox3.Text;
            string diaChi = textBox4.Text;
            string hinhAnh = textBox5.Text;
            string mota = textBox1.Text;

            ApiHelper apiHelper = new ApiHelper();
            apiHelper.SetAuthorizationHeader(GlobalVariables.AccessToken);
            var result = await apiHelper.AddDish(name, gia, diaChi, hinhAnh, mota);

            if (result != null)
            {
                MessageBox.Show("Thêm món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WhatToEat WhatToEatForm = new WhatToEat();
                WhatToEatForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Thêm món ăn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            WhatToEat WhatToEatForm = new WhatToEat();
            WhatToEatForm.Show();
            this.Hide();
        }
    }
}