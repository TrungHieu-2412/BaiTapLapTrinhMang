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
    public partial class WhatToEat : Form
    {
        private int currentPage = 1;
        private int pageSize = 10;
        public WhatToEat()
        {
            InitializeComponent();
            LoadDishes();
        }

        private async void LoadDishes()
        {
            ApiHelper apiHelper = new ApiHelper();

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (!string.IsNullOrEmpty(GlobalVariables.AccessToken))
            {
                apiHelper.SetAuthorizationHeader(GlobalVariables.AccessToken);
            }

            var dishes = await apiHelper.GetDishes(currentPage, pageSize);
            if (dishes != null)
            {
                // Hiển thị danh sách món ăn trong ListView
                listViewDishes.Items.Clear();
                foreach (var dish in dishes)
                {
                    var item = new ListViewItem(dish.TenMonAn);
                    item.SubItems.Add(dish.Gia);
                    item.SubItems.Add(dish.DiaChi);
                    item.SubItems.Add(dish.NguoiDongGop); // Thêm tên người đóng góp vào cột mới
                    listViewDishes.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy danh sách món ăn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddDishes_Click(object sender, EventArgs e)
        {
            AddDishes AddDishesForm = new AddDishes();
            AddDishesForm.Show();
            this.Hide();
        }

        private async Task btnChoose_ClickAsync(object sender, EventArgs e)
        {
            // Gợi ý món ăn ngẫu nhiên
            ApiHelper apiHelper = new ApiHelper();
            if (!string.IsNullOrEmpty(GlobalVariables.AccessToken))
            {
                apiHelper.SetAuthorizationHeader(GlobalVariables.AccessToken);
            }

            // Gợi ý món ăn ngẫu nhiên
            var dishes = await apiHelper.GetDishes(currentPage, pageSize); // Lấy danh sách món ăn

            if (dishes != null && dishes.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(dishes.Count);
                var randomDish = dishes[randomIndex];

                // Hiển thị món ăn được chọn
                MessageBox.Show($"Hôm nay ăn {randomDish.TenMonAn} nhé!", "Gợi ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có món ăn nào trong danh sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Đăng xuất
            GlobalVariables.AccessToken = null; // Xóa token xác thực
            Lab04_Bai06 MainForm = new Lab04_Bai06();
            MainForm.Show();
            this.Hide();
        }
    }
}