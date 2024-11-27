namespace Lab05
{
    partial class Lab05_Bai03
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
            this.lbl_Email = new System.Windows.Forms.Label();
            this.lbl_MatKhau = new System.Windows.Forms.Label();
            this.lbl_SoThu = new System.Windows.Forms.Label();
            this.lbl_GanDay = new System.Windows.Forms.Label();
            this.txt_Email = new System.Windows.Forms.TextBox();
            this.txt_MatKhau = new System.Windows.Forms.TextBox();
            this.txt_SoThu = new System.Windows.Forms.TextBox();
            this.txt_GanDay = new System.Windows.Forms.TextBox();
            this.btn_DangNhap = new System.Windows.Forms.Button();
            this.lst_HienThi = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lbl_Email
            // 
            this.lbl_Email.AutoSize = true;
            this.lbl_Email.Location = new System.Drawing.Point(31, 34);
            this.lbl_Email.Name = "lbl_Email";
            this.lbl_Email.Size = new System.Drawing.Size(32, 13);
            this.lbl_Email.TabIndex = 0;
            this.lbl_Email.Text = "Email";
            // 
            // lbl_MatKhau
            // 
            this.lbl_MatKhau.AutoSize = true;
            this.lbl_MatKhau.Location = new System.Drawing.Point(31, 60);
            this.lbl_MatKhau.Name = "lbl_MatKhau";
            this.lbl_MatKhau.Size = new System.Drawing.Size(52, 13);
            this.lbl_MatKhau.TabIndex = 1;
            this.lbl_MatKhau.Text = "Mật khẩu";
            // 
            // lbl_SoThu
            // 
            this.lbl_SoThu.AutoSize = true;
            this.lbl_SoThu.Location = new System.Drawing.Point(31, 96);
            this.lbl_SoThu.Name = "lbl_SoThu";
            this.lbl_SoThu.Size = new System.Drawing.Size(38, 13);
            this.lbl_SoThu.TabIndex = 2;
            this.lbl_SoThu.Text = "Số thư";
            // 
            // lbl_GanDay
            // 
            this.lbl_GanDay.AutoSize = true;
            this.lbl_GanDay.Location = new System.Drawing.Point(165, 96);
            this.lbl_GanDay.Name = "lbl_GanDay";
            this.lbl_GanDay.Size = new System.Drawing.Size(48, 13);
            this.lbl_GanDay.TabIndex = 3;
            this.lbl_GanDay.Text = "Gần đây";
            // 
            // txt_Email
            // 
            this.txt_Email.Location = new System.Drawing.Point(89, 31);
            this.txt_Email.Name = "txt_Email";
            this.txt_Email.Size = new System.Drawing.Size(256, 20);
            this.txt_Email.TabIndex = 4;
            // 
            // txt_MatKhau
            // 
            this.txt_MatKhau.Location = new System.Drawing.Point(89, 60);
            this.txt_MatKhau.Name = "txt_MatKhau";
            this.txt_MatKhau.Size = new System.Drawing.Size(256, 20);
            this.txt_MatKhau.TabIndex = 5;
            // 
            // txt_SoThu
            // 
            this.txt_SoThu.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_SoThu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_SoThu.Location = new System.Drawing.Point(88, 95);
            this.txt_SoThu.Multiline = true;
            this.txt_SoThu.Name = "txt_SoThu";
            this.txt_SoThu.Size = new System.Drawing.Size(62, 20);
            this.txt_SoThu.TabIndex = 6;
            // 
            // txt_GanDay
            // 
            this.txt_GanDay.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_GanDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_GanDay.Location = new System.Drawing.Point(219, 96);
            this.txt_GanDay.Multiline = true;
            this.txt_GanDay.Name = "txt_GanDay";
            this.txt_GanDay.Size = new System.Drawing.Size(62, 20);
            this.txt_GanDay.TabIndex = 6;
            // 
            // btn_DangNhap
            // 
            this.btn_DangNhap.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_DangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_DangNhap.Location = new System.Drawing.Point(523, 31);
            this.btn_DangNhap.Name = "btn_DangNhap";
            this.btn_DangNhap.Size = new System.Drawing.Size(164, 85);
            this.btn_DangNhap.TabIndex = 7;
            this.btn_DangNhap.Text = "Đăng Nhập";
            this.btn_DangNhap.UseVisualStyleBackColor = false;
            this.btn_DangNhap.Click += new System.EventHandler(this.btn_DangNhap_Click);
            // 
            // lst_HienThi
            // 
            this.lst_HienThi.HideSelection = false;
            this.lst_HienThi.Location = new System.Drawing.Point(29, 181);
            this.lst_HienThi.Name = "lst_HienThi";
            this.lst_HienThi.Size = new System.Drawing.Size(658, 257);
            this.lst_HienThi.TabIndex = 8;
            this.lst_HienThi.UseCompatibleStateImageBehavior = false;
            // 
            // Lab05_Bai03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(722, 450);
            this.Controls.Add(this.lst_HienThi);
            this.Controls.Add(this.btn_DangNhap);
            this.Controls.Add(this.txt_GanDay);
            this.Controls.Add(this.txt_SoThu);
            this.Controls.Add(this.txt_MatKhau);
            this.Controls.Add(this.txt_Email);
            this.Controls.Add(this.lbl_GanDay);
            this.Controls.Add(this.lbl_SoThu);
            this.Controls.Add(this.lbl_MatKhau);
            this.Controls.Add(this.lbl_Email);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Name = "Lab05_Bai03";
            this.Text = "Lab05_Bai03";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Email;
        private System.Windows.Forms.Label lbl_MatKhau;
        private System.Windows.Forms.Label lbl_SoThu;
        private System.Windows.Forms.Label lbl_GanDay;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.TextBox txt_MatKhau;
        private System.Windows.Forms.TextBox txt_SoThu;
        private System.Windows.Forms.TextBox txt_GanDay;
        private System.Windows.Forms.Button btn_DangNhap;
        private System.Windows.Forms.ListView lst_HienThi;
    }
}