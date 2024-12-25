namespace Lab04
{
    partial class Lab04_Bai03
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnDownloadHTML = new System.Windows.Forms.Button();
            this.btnDownloadResources = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(-2, 63);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(740, 491);
            this.webView21.TabIndex = 0;
            this.webView21.ZoomFactor = 1D;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(12, 8);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(629, 20);
            this.txtURL.TabIndex = 1;
            this.txtURL.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(647, 34);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(647, 6);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "GOO";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // btnDownloadHTML
            // 
            this.btnDownloadHTML.Location = new System.Drawing.Point(377, 34);
            this.btnDownloadHTML.Name = "btnDownloadHTML";
            this.btnDownloadHTML.Size = new System.Drawing.Size(129, 23);
            this.btnDownloadHTML.TabIndex = 4;
            this.btnDownloadHTML.Text = "Download HTML";
            this.btnDownloadHTML.UseVisualStyleBackColor = true;
            // 
            // btnDownloadResources
            // 
            this.btnDownloadResources.Location = new System.Drawing.Point(512, 34);
            this.btnDownloadResources.Name = "btnDownloadResources";
            this.btnDownloadResources.Size = new System.Drawing.Size(129, 23);
            this.btnDownloadResources.TabIndex = 5;
            this.btnDownloadResources.Text = "Download Resources";
            this.btnDownloadResources.UseVisualStyleBackColor = true;
            // 
            // Lab04_Bai03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 551);
            this.Controls.Add(this.btnDownloadResources);
            this.Controls.Add(this.btnDownloadHTML);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.webView21);
            this.Name = "Lab04_Bai03";
            this.Text = "Lab04_Bai03";
            this.Load += new System.EventHandler(this.Lab04_Bai03_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnDownloadHTML;
        private System.Windows.Forms.Button btnDownloadResources;
    }
}