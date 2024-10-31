namespace Lab02
{
    partial class Lab02_Bai01
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
            this.btn_Doc_File = new System.Windows.Forms.Button();
            this.btn_Ghi_File = new System.Windows.Forms.Button();
            this.rtxt_Hien_Thi = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_Doc_File
            // 
            this.btn_Doc_File.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Doc_File.Location = new System.Drawing.Point(12, 12);
            this.btn_Doc_File.Name = "btn_Doc_File";
            this.btn_Doc_File.Size = new System.Drawing.Size(224, 90);
            this.btn_Doc_File.TabIndex = 0;
            this.btn_Doc_File.Text = "Đọc File";
            this.btn_Doc_File.UseVisualStyleBackColor = true;
            this.btn_Doc_File.Click += new System.EventHandler(this.btn_Doc_File_Click);
            // 
            // btn_Ghi_File
            // 
            this.btn_Ghi_File.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Ghi_File.Location = new System.Drawing.Point(12, 108);
            this.btn_Ghi_File.Name = "btn_Ghi_File";
            this.btn_Ghi_File.Size = new System.Drawing.Size(224, 90);
            this.btn_Ghi_File.TabIndex = 0;
            this.btn_Ghi_File.Text = "Ghi File";
            this.btn_Ghi_File.UseVisualStyleBackColor = true;
            this.btn_Ghi_File.Click += new System.EventHandler(this.btn_Ghi_File_Click);
            // 
            // rtxt_Hien_Thi
            // 
            this.rtxt_Hien_Thi.Location = new System.Drawing.Point(260, 12);
            this.rtxt_Hien_Thi.Name = "rtxt_Hien_Thi";
            this.rtxt_Hien_Thi.Size = new System.Drawing.Size(528, 426);
            this.rtxt_Hien_Thi.TabIndex = 1;
            this.rtxt_Hien_Thi.Text = "";
            // 
            // Lab02_Bai01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtxt_Hien_Thi);
            this.Controls.Add(this.btn_Ghi_File);
            this.Controls.Add(this.btn_Doc_File);
            this.Name = "Lab02_Bai01";
            this.Text = "Lab02_Bai01";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Doc_File;
        private System.Windows.Forms.Button btn_Ghi_File;
        private System.Windows.Forms.RichTextBox rtxt_Hien_Thi;
    }
}