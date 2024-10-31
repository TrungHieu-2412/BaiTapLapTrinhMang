namespace Lab03
{
    partial class CINEMA_BOOK_TICKET
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
            this.btnStartClient = new System.Windows.Forms.Button();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartClient
            // 
            this.btnStartClient.Location = new System.Drawing.Point(259, 30);
            this.btnStartClient.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartClient.Name = "btnStartClient";
            this.btnStartClient.Size = new System.Drawing.Size(82, 42);
            this.btnStartClient.TabIndex = 3;
            this.btnStartClient.Text = "Client";
            this.btnStartClient.UseVisualStyleBackColor = true;
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(31, 30);
            this.btnStartServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(82, 42);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            // 
            // CINEMA_BOOK_TICKET
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 118);
            this.Controls.Add(this.btnStartClient);
            this.Controls.Add(this.btnStartServer);
            this.Name = "CINEMA_BOOK_TICKET";
            this.Text = "CINEMA APP";
            this.Load += new System.EventHandler(this.CINEMA_BOOK_TICKET_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartClient;
        private System.Windows.Forms.Button btnStartServer;
    }
}