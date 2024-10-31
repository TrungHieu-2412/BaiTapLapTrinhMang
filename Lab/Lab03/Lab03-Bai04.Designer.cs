namespace Lab03
{
    partial class Lab03_Bai04
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
            this.btn_Open_Server = new System.Windows.Forms.Button();
            this.btn_Open_Client = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Open_Server
            // 
            this.btn_Open_Server.Location = new System.Drawing.Point(12, 12);
            this.btn_Open_Server.Name = "btn_Open_Server";
            this.btn_Open_Server.Size = new System.Drawing.Size(121, 66);
            this.btn_Open_Server.TabIndex = 0;
            this.btn_Open_Server.Text = "SERVER";
            this.btn_Open_Server.UseVisualStyleBackColor = true;
            this.btn_Open_Server.Click += new System.EventHandler(this.btn_Open_Server_Click);
            // 
            // btn_Open_Client
            // 
            this.btn_Open_Client.Location = new System.Drawing.Point(139, 12);
            this.btn_Open_Client.Name = "btn_Open_Client";
            this.btn_Open_Client.Size = new System.Drawing.Size(121, 66);
            this.btn_Open_Client.TabIndex = 0;
            this.btn_Open_Client.Text = "CLIENT";
            this.btn_Open_Client.UseVisualStyleBackColor = true;
            this.btn_Open_Client.Click += new System.EventHandler(this.btn_Open_Client_Click);
            // 
            // Lab03_Bai04
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 94);
            this.Controls.Add(this.btn_Open_Client);
            this.Controls.Add(this.btn_Open_Server);
            this.Name = "Lab03_Bai04";
            this.Text = "MULTI CHAT ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Open_Server;
        private System.Windows.Forms.Button btn_Open_Client;
    }
}