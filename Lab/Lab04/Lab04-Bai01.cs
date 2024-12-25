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
    public partial class Lab04_Bai01 : Form
    {
        public Lab04_Bai01()
        {
            InitializeComponent();
            btnGET.Click += new EventHandler(btnGET_Click);
        }

        private void Lab04_Bai01_Load(object sender, EventArgs e)
        {

        }


        private void btnGET_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text;
            string htmlContent = getHTML(url);
            richTextBoxContent.Text = htmlContent;
        }

        private string getHTML(string szURL)
        {
            WebRequest request = WebRequest.Create(szURL);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            response.Close();
            return responseFromServer;
        }
    }
}
