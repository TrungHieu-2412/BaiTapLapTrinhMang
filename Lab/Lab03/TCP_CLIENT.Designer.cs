namespace Lab03
{
    partial class TCP_CLIENT
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
            this.txt_Display = new System.Windows.Forms.TextBox();
            this.txt_Your_Name = new System.Windows.Forms.TextBox();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.lbl_Your_Name = new System.Windows.Forms.Label();
            this.lbl_Message = new System.Windows.Forms.Label();
            this.txt_IP_Address = new System.Windows.Forms.TextBox();
            this.lbl_IP_Address = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_Display
            // 
            this.txt_Display.Location = new System.Drawing.Point(13, 13);
            this.txt_Display.Multiline = true;
            this.txt_Display.Name = "txt_Display";
            this.txt_Display.Size = new System.Drawing.Size(619, 301);
            this.txt_Display.TabIndex = 0;
            // 
            // txt_Your_Name
            // 
            this.txt_Your_Name.Location = new System.Drawing.Point(13, 345);
            this.txt_Your_Name.Name = "txt_Your_Name";
            this.txt_Your_Name.Size = new System.Drawing.Size(100, 20);
            this.txt_Your_Name.TabIndex = 1;
            // 
            // txt_Message
            // 
            this.txt_Message.Location = new System.Drawing.Point(13, 384);
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(538, 54);
            this.txt_Message.TabIndex = 2;
            // 
            // btn_Connect
            // 
            this.btn_Connect.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Connect.Location = new System.Drawing.Point(253, 345);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 3;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = false;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Send.Location = new System.Drawing.Point(557, 384);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 54);
            this.btn_Send.TabIndex = 4;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = false;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.BackColor = System.Drawing.Color.Red;
            this.btn_Disconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Disconnect.Location = new System.Drawing.Point(334, 345);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(75, 23);
            this.btn_Disconnect.TabIndex = 6;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = false;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // lbl_Your_Name
            // 
            this.lbl_Your_Name.AutoSize = true;
            this.lbl_Your_Name.Location = new System.Drawing.Point(12, 329);
            this.lbl_Your_Name.Name = "lbl_Your_Name";
            this.lbl_Your_Name.Size = new System.Drawing.Size(60, 13);
            this.lbl_Your_Name.TabIndex = 7;
            this.lbl_Your_Name.Text = "Your Name";
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Location = new System.Drawing.Point(12, 368);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(50, 13);
            this.lbl_Message.TabIndex = 8;
            this.lbl_Message.Text = "Message\r\n";
            // 
            // txt_IP_Address
            // 
            this.txt_IP_Address.Location = new System.Drawing.Point(119, 345);
            this.txt_IP_Address.Name = "txt_IP_Address";
            this.txt_IP_Address.Size = new System.Drawing.Size(128, 20);
            this.txt_IP_Address.TabIndex = 9;
            // 
            // lbl_IP_Address
            // 
            this.lbl_IP_Address.AutoSize = true;
            this.lbl_IP_Address.Location = new System.Drawing.Point(118, 329);
            this.lbl_IP_Address.Name = "lbl_IP_Address";
            this.lbl_IP_Address.Size = new System.Drawing.Size(58, 13);
            this.lbl_IP_Address.TabIndex = 10;
            this.lbl_IP_Address.Text = "IP Address";
            // 
            // TCP_CLIENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(646, 450);
            this.Controls.Add(this.lbl_IP_Address);
            this.Controls.Add(this.txt_IP_Address);
            this.Controls.Add(this.lbl_Message);
            this.Controls.Add(this.lbl_Your_Name);
            this.Controls.Add(this.btn_Disconnect);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txt_Message);
            this.Controls.Add(this.txt_Your_Name);
            this.Controls.Add(this.txt_Display);
            this.Name = "TCP_CLIENT";
            this.Text = "TCP_CLIENT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCP_CLIENT_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Display;
        private System.Windows.Forms.TextBox txt_Your_Name;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Label lbl_Your_Name;
        private System.Windows.Forms.Label lbl_Message;
        private System.Windows.Forms.TextBox txt_IP_Address;
        private System.Windows.Forms.Label lbl_IP_Address;
    }
}