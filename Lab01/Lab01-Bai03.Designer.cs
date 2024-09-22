using System;

namespace Lab01
{
    partial class Lab01_Bai03
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
            this.btnHienthi = new System.Windows.Forms.Button();
            this.lblNhapso = new System.Windows.Forms.Label();
            this.txtHienthichu = new System.Windows.Forms.TextBox();
            this.txtNhapso = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnHienthi
            // 
            this.btnHienthi.Location = new System.Drawing.Point(132, 92);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(202, 49);
            this.btnHienthi.TabIndex = 0;
            this.btnHienthi.Text = "Hiển thị số bằng chữ";
            this.btnHienthi.UseVisualStyleBackColor = true;
            this.btnHienthi.Click += new System.EventHandler(this.btnHienthi_Click);
            // 
            // lblNhapso
            // 
            this.lblNhapso.AutoSize = true;
            this.lblNhapso.Location = new System.Drawing.Point(39, 28);
            this.lblNhapso.Name = "lblNhapso";
            this.lblNhapso.Size = new System.Drawing.Size(58, 16);
            this.lblNhapso.TabIndex = 1;
            this.lblNhapso.Text = "Nhập số";
            // 
            // txtHienthichu
            // 
            this.txtHienthichu.Location = new System.Drawing.Point(88, 208);
            this.txtHienthichu.Multiline = true;
            this.txtHienthichu.Name = "txtHienthichu";
            this.txtHienthichu.ReadOnly = true;
            this.txtHienthichu.Size = new System.Drawing.Size(605, 166);
            this.txtHienthichu.TabIndex = 2;
            // 
            // txtNhapso
            // 
            this.txtNhapso.Location = new System.Drawing.Point(132, 28);
            this.txtNhapso.Name = "txtNhapso";
            this.txtNhapso.Size = new System.Drawing.Size(202, 22);
            this.txtNhapso.TabIndex = 3;
            // 
            // Lab01_Bai03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtNhapso);
            this.Controls.Add(this.txtHienthichu);
            this.Controls.Add(this.lblNhapso);
            this.Controls.Add(this.btnHienthi);
            this.Name = "Lab01_Bai03";
            this.Text = "Lab01_Bai03";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHienthi;
        private System.Windows.Forms.Label lblNhapso;
        private System.Windows.Forms.TextBox txtHienthichu;
        private System.Windows.Forms.TextBox txtNhapso;
    }
}