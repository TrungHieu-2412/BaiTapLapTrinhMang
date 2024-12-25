using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using HtmlAgilityPack;


namespace Lab04
{
    public partial class Lab04_Bai03 : Form
    {
        public Lab04_Bai03()
        {
            InitializeComponent();
            btnGo.Click += new EventHandler(BtnGo_Click);
            btnDownloadHTML.Click += new EventHandler(BtnDownloadHTML_Click);
            btnDownloadResources.Click += new EventHandler(BtnDownloadResources_Click);
            btnReload.Click += new EventHandler(BtnReload_Click); 
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void Lab04_Bai03_Load(object sender, EventArgs e)
        {

        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text;
            if (!string.IsNullOrEmpty(url))
            {
                webView21.Source = new Uri(url);
            }
        }

        private void BtnDownloadHTML_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    WebClient client = new WebClient();
                    string htmlContent = client.DownloadString(url);
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "HTML Files (*.html)|*.html";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, htmlContent);
                        MessageBox.Show("Tải xuống HTML thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải xuống: " + ex.Message, "Thông báo");
                }
            }
        }

        private void BtnDownloadResources_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    // Download the HTML
                    WebClient client = new WebClient();
                    string htmlContent = client.DownloadString(url);

                    // Parse HTML using HTMLAgilityPack
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    // Get the base URL of the website
                    Uri baseUri = new Uri(url);

                    // Download Path (set a default path)
                    string downloadPath = @"C:\Download\Images";

                    // Create the directory if it doesn't exist
                    Directory.CreateDirectory(downloadPath);

                    // Download Images
                    int imageCount = 0;
                    foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//img[@src]"))
                    {
                        string imageUrl = img.GetAttributeValue("src", "");
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            // Construct the full URL if it's relative
                            Uri fullImageUrl = new Uri(baseUri, imageUrl);

                            // Create a unique file name for each image
                            string fileName = $"image_{imageCount++}.{Path.GetExtension(fullImageUrl.ToString())}";

                            // Download the image (with error handling)
                            string filePath = Path.Combine(downloadPath, fileName);
                            try
                            {
                                client.DownloadFile(fullImageUrl, filePath);
                                Console.WriteLine($"Downloaded {fileName} to {filePath}");
                            }
                            catch (Exception ex)
                            {
                                // Log the error or display a message to the user
                                Console.WriteLine($"Error downloading {fileName}: {ex.Message}");
                                // You could add code to retry the download here, if desired.
                            }
                        }
                    }

                    MessageBox.Show($"Downloaded {imageCount} images successfully!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error downloading images: " + ex.Message, "Error");
                }
            }
        }

        private void BtnReload_Click(object sender, EventArgs e)
        {
            webView21.Reload();
        }
    }

}
