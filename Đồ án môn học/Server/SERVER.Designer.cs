namespace Server
{
    partial class SERVER
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
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.textBox_room_count = new System.Windows.Forms.TextBox();
            this.textBox_user_count = new System.Windows.Forms.TextBox();
            this.lblCountRoom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listviewInformation = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartServer.Location = new System.Drawing.Point(29, 58);
            this.btnStartServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(109, 53);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "START SERVER";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnStopServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopServer.Location = new System.Drawing.Point(29, 136);
            this.btnStopServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(109, 53);
            this.btnStopServer.TabIndex = 2;
            this.btnStopServer.Text = "STOP SERVER";
            this.btnStopServer.UseVisualStyleBackColor = false;
            // 
            // textBox_room_count
            // 
            this.textBox_room_count.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_room_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_room_count.Location = new System.Drawing.Point(29, 245);
            this.textBox_room_count.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_room_count.Name = "textBox_room_count";
            this.textBox_room_count.Size = new System.Drawing.Size(110, 23);
            this.textBox_room_count.TabIndex = 3;
            // 
            // textBox_user_count
            // 
            this.textBox_user_count.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_user_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_user_count.Location = new System.Drawing.Point(29, 327);
            this.textBox_user_count.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_user_count.Name = "textBox_user_count";
            this.textBox_user_count.Size = new System.Drawing.Size(110, 23);
            this.textBox_user_count.TabIndex = 4;
            // 
            // lblCountRoom
            // 
            this.lblCountRoom.AutoSize = true;
            this.lblCountRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountRoom.Location = new System.Drawing.Point(29, 227);
            this.lblCountRoom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCountRoom.Name = "lblCountRoom";
            this.lblCountRoom.Size = new System.Drawing.Size(51, 17);
            this.lblCountRoom.TabIndex = 5;
            this.lblCountRoom.Text = "ROOM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 309);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "USER";
            // 
            // listviewInformation
            // 
            this.listviewInformation.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewInformation.HideSelection = false;
            this.listviewInformation.LabelWrap = false;
            this.listviewInformation.Location = new System.Drawing.Point(152, 14);
            this.listviewInformation.Name = "listviewInformation";
            this.listviewInformation.Size = new System.Drawing.Size(436, 340);
            this.listviewInformation.TabIndex = 7;
            this.listviewInformation.UseCompatibleStateImageBehavior = false;
            // 
            // SERVER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.listviewInformation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCountRoom);
            this.Controls.Add(this.textBox_user_count);
            this.Controls.Add(this.textBox_room_count);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.btnStartServer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SERVER";
            this.Text = "ServerUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.TextBox textBox_room_count;
        private System.Windows.Forms.TextBox textBox_user_count;
        private System.Windows.Forms.Label lblCountRoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listviewInformation;
    }
}