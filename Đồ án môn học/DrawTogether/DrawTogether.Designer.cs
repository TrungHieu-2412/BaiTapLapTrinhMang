namespace DrawTogether
{
    partial class DrawTogether
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
            this.btnOffline = new System.Windows.Forms.Button();
            this.btnOnline = new System.Windows.Forms.Button();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRoomCode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtRoomCode = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOffline
            // 
            this.btnOffline.Location = new System.Drawing.Point(88, 91);
            this.btnOffline.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(140, 66);
            this.btnOffline.TabIndex = 0;
            this.btnOffline.Text = "OFFLINE";
            this.btnOffline.UseVisualStyleBackColor = true;
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // btnOnline
            // 
            this.btnOnline.Location = new System.Drawing.Point(373, 91);
            this.btnOnline.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(140, 66);
            this.btnOnline.TabIndex = 1;
            this.btnOnline.Text = "ONLINE";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.btnOnline_Click);
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Location = new System.Drawing.Point(88, 50);
            this.btnCreateRoom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(93, 37);
            this.btnCreateRoom.TabIndex = 2;
            this.btnCreateRoom.Text = "CREATE ROOM";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.Location = new System.Drawing.Point(420, 50);
            this.btnJoinRoom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(93, 37);
            this.btnJoinRoom.TabIndex = 3;
            this.btnJoinRoom.Text = "JOIN ROOM";
            this.btnJoinRoom.UseVisualStyleBackColor = true;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(88, 279);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 37);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(181, 159);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "NAME";
            // 
            // lblRoomCode
            // 
            this.lblRoomCode.AutoSize = true;
            this.lblRoomCode.Location = new System.Drawing.Point(181, 199);
            this.lblRoomCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRoomCode.Name = "lblRoomCode";
            this.lblRoomCode.Size = new System.Drawing.Size(73, 13);
            this.lblRoomCode.TabIndex = 6;
            this.lblRoomCode.Text = "ROOM CODE";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(274, 157);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(146, 20);
            this.txtName.TabIndex = 7;
            // 
            // txtRoomCode
            // 
            this.txtRoomCode.Location = new System.Drawing.Point(274, 197);
            this.txtRoomCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRoomCode.Name = "txtRoomCode";
            this.txtRoomCode.Size = new System.Drawing.Size(146, 20);
            this.txtRoomCode.TabIndex = 8;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(420, 279);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(93, 37);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // DrawTogether
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtRoomCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblRoomCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.btnOnline);
            this.Controls.Add(this.btnOffline);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DrawTogether";
            this.Text = "DrawTogether";
            this.Load += new System.EventHandler(this.DrawTogether_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOffline;
        private System.Windows.Forms.Button btnOnline;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRoomCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtRoomCode;
        private System.Windows.Forms.Button btnBack;
    }
}