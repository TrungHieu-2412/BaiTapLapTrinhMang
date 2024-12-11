using System;
using System.Windows.Forms;
using MailKit.Net.Smtp;
using MimeKit;

namespace Lab05
{
    public partial class Lab05_Bai01 : Form
    {
        public Lab05_Bai01()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin email từ các TextBox
                string from = txtFrom.Text;
                string to = txtTo.Text;
                string subject = txtSubject.Text;
                string body = txtBody.Text;

                if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", from));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = body
                };


                // Cấu hình SMTP client
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true); 
                    client.Authenticate("nguyentrunghieu170221@gmail.com", "lvwj kubu hnyb hbda"); 
                    client.Send(message);
                    client.Disconnect(true);
                }

                MessageBox.Show("Email đã được gửi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi gửi email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
    }
}
