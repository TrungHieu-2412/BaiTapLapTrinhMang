namespace Lab01
{
    partial class Lab01_Bai01
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
            this.txtSothu1 = new System.Windows.Forms.TextBox();
            this.txtSothu2 = new System.Windows.Forms.TextBox();
            this.txtSothu3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtSolonnhat = new System.Windows.Forms.TextBox();
            this.txtSonhonhat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSothu1
            // 
            this.txtSothu1.Location = new System.Drawing.Point(103, 63);
            this.txtSothu1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSothu1.Name = "txtSothu1";
            this.txtSothu1.Size = new System.Drawing.Size(69, 20);
            this.txtSothu1.TabIndex = 0;
            this.txtSothu1.TextChanged += new System.EventHandler(this.txtSothu1_TextChanged);
            // 
            // txtSothu2
            // 
            this.txtSothu2.Location = new System.Drawing.Point(296, 63);
            this.txtSothu2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSothu2.Name = "txtSothu2";
            this.txtSothu2.Size = new System.Drawing.Size(76, 20);
            this.txtSothu2.TabIndex = 1;
            // 
            // txtSothu3
            // 
            this.txtSothu3.Location = new System.Drawing.Point(494, 66);
            this.txtSothu3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSothu3.Name = "txtSothu3";
            this.txtSothu3.Size = new System.Drawing.Size(76, 20);
            this.txtSothu3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Số thứ nhất";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Số thứ hai";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(442, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Số thứ ba";
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(103, 158);
            this.btnTim.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(68, 31);
            this.btnTim.TabIndex = 6;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(296, 158);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 31);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(494, 158);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 31);
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtSolonnhat
            // 
            this.txtSolonnhat.Location = new System.Drawing.Point(103, 266);
            this.txtSolonnhat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSolonnhat.Name = "txtSolonnhat";
            this.txtSolonnhat.ReadOnly = true;
            this.txtSolonnhat.Size = new System.Drawing.Size(76, 20);
            this.txtSolonnhat.TabIndex = 9;
            // 
            // txtSonhonhat
            // 
            this.txtSonhonhat.Location = new System.Drawing.Point(494, 258);
            this.txtSonhonhat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSonhonhat.Name = "txtSonhonhat";
            this.txtSonhonhat.ReadOnly = true;
            this.txtSonhonhat.Size = new System.Drawing.Size(76, 20);
            this.txtSonhonhat.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 271);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Số lớn nhất";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(431, 262);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Số nhỏ nhất";
            // 
            // Lab01_Bai01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 366);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSonhonhat);
            this.Controls.Add(this.txtSolonnhat);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSothu3);
            this.Controls.Add(this.txtSothu2);
            this.Controls.Add(this.txtSothu1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Lab01_Bai01";
            this.Text = "Lab01-Bai01";
            this.Load += new System.EventHandler(this.Lab01_Bai01_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSothu1;
        private System.Windows.Forms.TextBox txtSothu2;
        private System.Windows.Forms.TextBox txtSothu3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox txtSolonnhat;
        private System.Windows.Forms.TextBox txtSonhonhat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}