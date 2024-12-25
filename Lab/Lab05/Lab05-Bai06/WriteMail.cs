using MimeKit;
using System;
using System.Windows.Forms;
using MailKit.Net.Smtp;

namespace Lab05_Bai06
{
    public partial class WriteMail : Form
    {
        private string senderEmail;
        private string senderPassword;
        public WriteMail(string senderEmail, string senderPassword)
        {
            InitializeComponent();
            btnBrowse.Click += new EventHandler(btnBrowse_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnSend.Click += new EventHandler(btnSend_Click);

            this.senderEmail = senderEmail;
            txtFrom.Text = senderEmail;
            txtFrom.ReadOnly = false;
            this.senderPassword = senderPassword;
        }

        // Tự điền sẵn Mail người gửi
        public void SetReplyInfo(string toEmail, string subject)
        {
            txtTo.Text = toEmail;
            txtSubject.Text = subject;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy các thông tin cần thiết
                string toEmail = txtTo.Text.Trim(); //.trim() để loại bỏ khoảng trắng đầu cuối
                string subject = txtSubject.Text.Trim();
                string body = rtbMail.Text;
                string attachmentPath = txtAttachment.Text;

                if (string.IsNullOrEmpty(toEmail))
                {
                    MessageBox.Show("Vui lòng nhập email người nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //  thiết lập các thông tin cơ bản
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Người gửi", senderEmail));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { TextBody = body };

                // Check đường dẫn file đính kèm
                if (!string.IsNullOrEmpty(attachmentPath) && System.IO.File.Exists(attachmentPath))
                {
                    bodyBuilder.Attachments.Add(attachmentPath);
                }

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    // Thiết lập kết nối, TLS đê mã hóa
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(senderEmail, senderPassword);
                    // Gửi massage đến máy chủ qua SMTP
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                MessageBox.Show("Gửi email thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gửi email thất bại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp đính kèm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtAttachment.Text = openFileDialog.FileName;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
