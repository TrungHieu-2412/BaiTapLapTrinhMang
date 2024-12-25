using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab05_Bai06
{
    public partial class ViewMail : Form
    {
        private string _email;
        private string _password;
        private string _emailId;
        public ViewMail(string emailId, string email, string password)
        {
            InitializeComponent();
            btnReply.Click += new EventHandler(btnReply_Click);
            btnClose.Click += new EventHandler(btnClose_Click);

            _emailId = emailId;
            _email = email;
            _password = password;
            LoadEmailDetails();
        }

        private void LoadEmailDetails()
        {
            try
            {
                var client = new ImapClient();
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate(_email, _password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // MimeMessage cho phép gửi các dạng khác thay vì chỉ text
                MimeMessage message = null;
                for (int i = 0; i < inbox.Count; i++)
                {
                    var m = inbox.GetMessage(i);
                    if (m.MessageId == _emailId)
                    {
                        message = m;
                        break;
                    }
                }

                if (message != null)
                {
                    lblFrom.Text = "From: " + message.From.ToString();
                    lblSubject.Text = "Subject: " + message.Subject;
                    lblDate.Text = "Date: " + message.Date.ToString("dd/MM/yyyy HH:mm");

                    if (!string.IsNullOrEmpty(message.TextBody))
                    {
                        rtbBody.Text = message.TextBody;
                    }
                    else if (!string.IsNullOrEmpty(message.HtmlBody))
                    {
                        rtbBody.Text = message.HtmlBody;
                    }
                    else
                    {
                        rtbBody.Text = "No content available.";
                    }

                    if (message.Attachments.Count() > 0)
                    {
                        StringBuilder attachmentsList = new StringBuilder("Attachments:\n");
                        foreach (var attachment in message.Attachments)
                        {
                            if (attachment is MimePart mimePart && !string.IsNullOrEmpty(mimePart.FileName))
                            {
                                attachmentsList.AppendLine(mimePart.FileName);
                            }
                        }
                        lblAttachments.Text = attachmentsList.ToString();
                    }
                    else
                    {
                        lblAttachments.Text = "No attachments.";
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy email với ID đã cho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new ImapClient();
                client.Connect("imap.gmail.com", 993, true);
                client.Authenticate(_email, _password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                MimeMessage message = null;
                for (int i = 0; i < inbox.Count; i++)
                {
                    var m = inbox.GetMessage(i);
                    if (m.MessageId == _emailId)
                    {
                        message = m;
                        break;
                    }
                }

                if (message != null)
                {
                    WriteMail writeMailForm = new WriteMail(_email, _password);
                    writeMailForm.SetReplyInfo(message.From.ToString(), "Re: " + message.Subject);
                    writeMailForm.Show();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy email để trả lời.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
