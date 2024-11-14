namespace Server
{
    partial class ServerUI
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
            this.txtCountRoom = new System.Windows.Forms.TextBox();
            this.txtCountUser = new System.Windows.Forms.TextBox();
            this.lblCountRoom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInformation = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.Color.Cyan;
            this.btnStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartServer.Location = new System.Drawing.Point(39, 72);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(145, 65);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "START SERVER";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.BackColor = System.Drawing.Color.Cyan;
            this.btnStopServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopServer.Location = new System.Drawing.Point(39, 167);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(145, 65);
            this.btnStopServer.TabIndex = 2;
            this.btnStopServer.Text = "STOP SERVER";
            this.btnStopServer.UseVisualStyleBackColor = false;
            // 
            // txtCountRoom
            // 
            this.txtCountRoom.BackColor = System.Drawing.SystemColors.Control;
            this.txtCountRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountRoom.Location = new System.Drawing.Point(39, 302);
            this.txtCountRoom.Name = "txtCountRoom";
            this.txtCountRoom.Size = new System.Drawing.Size(145, 27);
            this.txtCountRoom.TabIndex = 3;
            // 
            // txtCountUser
            // 
            this.txtCountUser.BackColor = System.Drawing.SystemColors.Control;
            this.txtCountUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountUser.Location = new System.Drawing.Point(39, 403);
            this.txtCountUser.Name = "txtCountUser";
            this.txtCountUser.Size = new System.Drawing.Size(145, 27);
            this.txtCountUser.TabIndex = 4;
            // 
            // lblCountRoom
            // 
            this.lblCountRoom.AutoSize = true;
            this.lblCountRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountRoom.Location = new System.Drawing.Point(39, 279);
            this.lblCountRoom.Name = "lblCountRoom";
            this.lblCountRoom.Size = new System.Drawing.Size(61, 20);
            this.lblCountRoom.TabIndex = 5;
            this.lblCountRoom.Text = "ROOM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "USER";
            // 
            // txtInformation
            // 
            this.txtInformation.Location = new System.Drawing.Point(218, 20);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.Size = new System.Drawing.Size(570, 418);
            this.txtInformation.TabIndex = 7;
            // 
            // ServerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtInformation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCountRoom);
            this.Controls.Add(this.txtCountUser);
            this.Controls.Add(this.txtCountRoom);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.btnStartServer);
            this.Name = "ServerUI";
            this.Text = "ServerUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.TextBox txtCountRoom;
        private System.Windows.Forms.TextBox txtCountUser;
        private System.Windows.Forms.Label lblCountRoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInformation;
    }
}