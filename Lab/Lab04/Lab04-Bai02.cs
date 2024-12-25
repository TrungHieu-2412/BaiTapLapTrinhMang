using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class Lab04_Bai02 : Form
    {
        public Lab04_Bai02()
        {
            InitializeComponent();
            btnDown.Click += new EventHandler(btnDown_Click);


        }

        private void Lab04_Bai02_Load(object sender, EventArgs e)
        {

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text;
            string filePath = txtFilePath.Text;

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ URL và đường dẫn lưu file!", "Thông báo");
                return;
            }

            try
            {
                // Tải nội dung trang web về
                WebClient client = new WebClient();
                client.DownloadFile(url, filePath);
                MessageBox.Show("Tải xuống thành công!", "Thông báo");

                // Hiển thị nội dung lên Form
                string htmlContent = File.ReadAllText(filePath);
                richTextBoxContent.Text = htmlContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải xuống: " + ex.Message, "Thông báo");
            }
        }
    }
}

