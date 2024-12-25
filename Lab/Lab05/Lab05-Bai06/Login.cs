using System;
using System.Windows.Forms;


namespace Lab05_Bai06
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            btnLogin.Click += new EventHandler(btnLogin_Click);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;


            try
            {
                var client = new MailKit.Net.Imap.ImapClient();
                // (Địa chỉ máy chủ, cổng kết nối,  SSL/TLS)
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate(email, password);
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                client.Disconnect(true);

                // Nếu đăng nhập thành công thì mở 
                this.Hide(); // Ẩn form đăng nhập
                var mailManagement = new MailManagement(email, password);
                mailManagement.FormClosed += (s, args) => this.Close();
                mailManagement.Show(); 
            }
            catch (MailKit.Security.AuthenticationException)
            {
                MessageBox.Show("Đăng nhập thất bại! Vui lòng kiểm tra email hoặc mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
