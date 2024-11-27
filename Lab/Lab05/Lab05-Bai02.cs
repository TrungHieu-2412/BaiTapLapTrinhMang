using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab05
{
    public partial class Lab05_Bai02 : Form
    {
        public Lab05_Bai02()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            lisBody.View = View.Details;
            lisBody.Columns.Add("Subject", 200);
            lisBody.Columns.Add("From", 150);
            lisBody.Columns.Add("Date", 150);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập Email và Password!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);
                    client.Authenticate(email, password);
                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    int totalEmails = inbox.Count;
                    int recentEmails = 0;
                    DateTime oneDayAgo = DateTime.Now.AddDays(-1);

                    lisBody.Items.Clear();

                    int limit = Math.Min(10, totalEmails); 

                    for (int i = 0; i < limit; i++)
                    {
                        var message = inbox.GetMessage(i);
                        string subject = message.Subject;
                        string from = message.From.ToString();
                        string date = message.Date.ToString("dd/MM/yyyy HH:mm:ss");

                        ListViewItem item = new ListViewItem(subject);
                        item.SubItems.Add(from);
                        item.SubItems.Add(date);
                        lisBody.Items.Add(item);

                        if (message.Date >= oneDayAgo)
                        {
                            recentEmails++;
                        }
                    }

                    txtTotal.Text = totalEmails.ToString();
                    txtRecent.Text = recentEmails.ToString();

                    client.Disconnect(true);
                }

                MessageBox.Show("Lấy danh sách email thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lấy email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}