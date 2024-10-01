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
    public partial class Lab01_Bai05 : Form
    {
        private int currentID = 1;
        private List<ThiSinh> danhSachThiSinh = new List<ThiSinh>();

        //Lớp ThiSinh để lưu trữ thông tin của mỗi thí sinh
        public class ThiSinh
        {
            public string ID { get; set; }
            public string HoTen { get; set; }
            public string Phai { get; set; }
            public float Mon1 { get; set; }
            public float Mon2 { get; set; }
            public float Mon3 { get; set; }
            public float DTB { get; set; }
            public string XepLoai { get; set; }

            public ThiSinh(string id, string hoTen, string phai, float mon1, float mon2, float mon3, float dtb, string xepLoai)
            {
                ID = id;
                HoTen = hoTen;
                Phai = phai;
                Mon1 = mon1;
                Mon2 = mon2;
                Mon3 = mon3;
                DTB = dtb;
                XepLoai = xepLoai;
            }
        }

        public Lab01_Bai05()
        {
            InitializeComponent();
            cboPhai.Items.AddRange(new string[] { "Nam", "Nữ" });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu
            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ và tên không được để trống.");
                return;
            }
            if (hoTen.Length > 30)
            {
                MessageBox.Show("Họ và tên không được vượt quá 30 ký tự.");
                return;
            }
            string phai = cboPhai.SelectedItem?.ToString();
            if (phai == null)
            {
                MessageBox.Show("Vui lòng chọn phái.");
                return;
            }

            // Lấy điểm và kiểm tra
            if (!float.TryParse(txtMon1.Text, out float mon1) ||
                !float.TryParse(txtMon2.Text, out float mon2) ||
                !float.TryParse(txtMon3.Text, out float mon3))
            {
                MessageBox.Show("Vui lòng nhập điểm hợp lệ cho các môn.");
                return;
            }
            if (mon1 < 0 || mon1 > 10 || mon2 < 0 || mon2 > 10 || mon3 < 0 || mon3 > 10)
            {
                MessageBox.Show("Điểm các môn phải từ 0 đến 10.");
                return;
            }

            // Làm tròn đến 1 chữ số thập phân
            mon1 = (float)Math.Round(mon1, 1);
            mon2 = (float)Math.Round(mon2, 1);
            mon3 = (float)Math.Round(mon3, 1);

            // Tính điểm trung bình
            float dtb = (mon1 + mon2 + mon3) / 3;
            dtb = (float)Math.Round(dtb, 2); // Làm tròn đến 2 chữ số thập phân

            // Xếp loại
            string xepLoai = XepLoai(dtb, mon1, mon2, mon3);

            // Tạo ID
            string id = $"TS{currentID.ToString("D3")}";
            currentID++;

            // Tạo đối tượng ThiSinh và thêm vào danh sách
            ThiSinh thiSinh = new ThiSinh(id, hoTen, phai, mon1, mon2, mon3, dtb, xepLoai);
            danhSachThiSinh.Add(thiSinh);

            // Cập nhật DataGridView
            CapNhatDataGridView();

            // Xóa nhập liệu
            btnDelete_Click(sender, e);
        }

        private string XepLoai(float dtb, float mon1, float mon2, float mon3)
        {
            if (dtb >= 8 && mon1 >= 6.5f && mon2 >= 6.5f && mon3 >= 6.5f)
                return "Giỏi";
            else if (dtb >= 6.5 && mon1 >= 5f && mon2 >= 5f && mon3 >= 5f)
                return "Khá";
            else if (dtb >= 5 && mon1 >= 3.5f && mon2 >= 3.5f && mon3 >= 3.5f)
                return "Trung Bình";
            else if (dtb >= 3.5 && mon1 >= 2f && mon2 >= 2f && mon3 >= 2f)
                return "Yếu";
            else
                return "Kém";
        }

        private void CapNhatDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (ThiSinh ts in danhSachThiSinh)
            {
                dataGridView1.Rows.Add(ts.ID, ts.HoTen, ts.Phai, ts.Mon1, ts.Mon2, ts.Mon3, ts.DTB, ts.XepLoai, "Xóa");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu cột được nhấn là cột "Xóa"
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Xác nhận người dùng có muốn xóa hàng không
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thí sinh này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Nếu người dùng chọn "Yes", thực hiện xóa
                if (result == DialogResult.Yes)
                {
                    // Xóa hàng trong DataGridView
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            cboPhai.SelectedIndex = -1;
            txtMon1.Clear();
            txtMon2.Clear();
            txtMon3.Clear();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int tongSo = danhSachThiSinh.Count;
            if (tongSo == 0)
            {
                MessageBox.Show("Chưa có thí sinh nào trong danh sách.");
                return;
            }

            // Tìm thí sinh có điểm trung bình cao nhất
            float dtbCaoNhat = danhSachThiSinh.Max(t => t.DTB);
            List<ThiSinh> thiSinhDtbCaoNhat = danhSachThiSinh.Where(t => t.DTB == dtbCaoNhat).ToList();
            string tenThiSinhDtbCaoNhat = string.Join(", ", thiSinhDtbCaoNhat.Select(t => t.HoTen));

            // Thống kê số lượng xếp loại
            int soGioi = danhSachThiSinh.Count(t => t.XepLoai == "Giỏi");
            int soKha = danhSachThiSinh.Count(t => t.XepLoai == "Khá");
            int soTrungBinh = danhSachThiSinh.Count(t => t.XepLoai == "Trung Bình");
            int soKhongDat = danhSachThiSinh.Count(t => t.XepLoai == "Yếu" || t.XepLoai == "Kém");

            // Hiển thị thống kê
            string thongKe = $"Tổng số thí sinh: {tongSo}\n" +
                             $"Thí sinh có ĐTB cao nhất: {tenThiSinhDtbCaoNhat} ({dtbCaoNhat})\n" +
                             $"Số thí sinh Giỏi: {soGioi}\n" +
                             $"Số thí sinh Khá: {soKha}\n" +
                             $"Số thí sinh Trung Bình: {soTrungBinh}\n" +
                             $"Số thí sinh không đạt (Yếu, Kém): {soKhongDat}";

            MessageBox.Show(thongKe, "Thống Kê");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
