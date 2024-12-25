using MailKit.Net.Imap;
using MailKit;
using System;
using System.Windows.Forms;

namespace Lab05_Bai06
{
    public partial class MailManagement : Form
    {

        public MailManagement(string email, string password)
        {
            InitializeComponent();
            lstEmails.SelectedIndexChanged += listViewEmails_SelectedIndexChanged;
            btnWriteMail.Click += new EventHandler(btnWriteMail_Click);
            btnLogout.Click += new EventHandler(btnLogout_Click);


            _email = email;
            _password = password;
            lblEmail.Text = email;
            LoadEmails();
        }

        private string _email;
        private string _password;
        private void LoadEmails()
        {
            try
            {
                var client = new ImapClient();
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate(_email, _password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                lstEmails.Items.Clear();

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    var item = new ListViewItem(new[]
                    {
                        message.From.ToString(), //Địa chỉ email người gửi
                        message.Subject, //Chủ đề mail
                        message.Date.ToString("dd/MM/yyyy HH:mm") //Ngày tháng năm
                    });
                    item.Tag = message.MessageId;
                    lstEmails.Items.Add(item);
                }
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWriteMail_Click(object sender, EventArgs e)
        {
            this.Hide();
            WriteMail writeMail = new WriteMail(_email, _password);
            writeMail.ShowDialog();
            this.Show();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            var Login = new Login();
            Login.FormClosed += (s, args) => this.Close();
            Login.Show();
        }

        private void listViewEmails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEmails.SelectedItems.Count > 0)
            {
                var selectedItem = lstEmails.SelectedItems[0];
                // Tag lưu masageID, toán tử ?, cho phép truy cập an toàn
                string emailId = selectedItem.Tag?.ToString();
                if (!string.IsNullOrEmpty(emailId))
                {
                    ViewMail emailDetailsForm = new ViewMail(emailId, _email, _password);
                    emailDetailsForm.Show();
                }
                else
                {
                    MessageBox.Show("Không có ID email hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
