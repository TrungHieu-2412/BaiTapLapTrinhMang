using System;
using System.Windows.Forms;
using MailKit.Net.Pop3;
using MimeKit;

namespace Lab05
{
    public partial class Lab05_Bai03 : Form
    {
        public Lab05_Bai03()
        {
            InitializeComponent();
            lst_HienThi.View = View.Details;
            lst_HienThi.Columns.Add("Thời gian", 150);
            lst_HienThi.Columns.Add("Người gửi", 200);
            lst_HienThi.Columns.Add("Chủ đề", 300);
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string email = txt_Email.Text.Trim();
            string password = txt_MatKhau.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Email và Mật khẩu!");
                return;
            }

            try
            {
                using (var client = new Pop3Client())
                {
                    // Kết nối tới máy chủ POP3
                    client.Connect("pop.gmail.com", 995, true);
                    client.Authenticate(email, password);

                    MessageBox.Show("Đăng nhập thành công!");

                    // Lấy số lượng email
                    int messageCount = client.Count;
                    txt_SoThu.Text = messageCount.ToString();

                    // Hiển thị danh sách email gần đây
                    lst_HienThi.Items.Clear();
                    int maxEmailsToFetch = Math.Min(10, messageCount); // Hiển thị tối đa 10 email gần nhất
                    txt_GanDay.Text = maxEmailsToFetch.ToString();

                    for (int i = 0; i < maxEmailsToFetch; i++)
                    {
                        var message = client.GetMessage(i);

                        // Thêm email vào ListView
                        var item = new ListViewItem(message.Date.LocalDateTime.ToString());
                        item.SubItems.Add(message.From.ToString());
                        item.SubItems.Add(message.Subject);
                        lst_HienThi.Items.Add(item);
                    }

                    client.Disconnect(true); // Ngắt kết nối máy chủ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
