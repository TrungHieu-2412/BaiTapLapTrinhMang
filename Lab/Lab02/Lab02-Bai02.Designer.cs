namespace Lab02
{
    partial class Lab02_Bai02
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
            this.btn_Read_file = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.txt_File_name = new System.Windows.Forms.TextBox();
            this.rtxt_Hien_thi = new System.Windows.Forms.RichTextBox();
            this.lbl_File_name = new System.Windows.Forms.Label();
            this.lbl_Size = new System.Windows.Forms.Label();
            this.lbl_URL = new System.Windows.Forms.Label();
            this.lbl_Line_count = new System.Windows.Forms.Label();
            this.lbl_Words_count = new System.Windows.Forms.Label();
            this.lbl_Char_count = new System.Windows.Forms.Label();
            this.txt_Size = new System.Windows.Forms.TextBox();
            this.txt_URL = new System.Windows.Forms.TextBox();
            this.txt_Line_count = new System.Windows.Forms.TextBox();
            this.txt_Words_count = new System.Windows.Forms.TextBox();
            this.txt_Char_count = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Read_file
            // 
            this.btn_Read_file.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Read_file.Location = new System.Drawing.Point(12, 12);
            this.btn_Read_file.Name = "btn_Read_file";
            this.btn_Read_file.Size = new System.Drawing.Size(305, 43);
            this.btn_Read_file.TabIndex = 0;
            this.btn_Read_file.Text = "Read file";
            this.btn_Read_file.UseVisualStyleBackColor = true;
            this.btn_Read_file.Click += new System.EventHandler(this.btn_Read_file_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.Red;
            this.btn_Exit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.Location = new System.Drawing.Point(12, 395);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(305, 43);
            this.btn_Exit.TabIndex = 0;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // txt_File_name
            // 
            this.txt_File_name.Location = new System.Drawing.Point(99, 129);
            this.txt_File_name.Name = "txt_File_name";
            this.txt_File_name.Size = new System.Drawing.Size(218, 20);
            this.txt_File_name.TabIndex = 1;
            // 
            // rtxt_Hien_thi
            // 
            this.rtxt_Hien_thi.Location = new System.Drawing.Point(354, 12);
            this.rtxt_Hien_thi.Name = "rtxt_Hien_thi";
            this.rtxt_Hien_thi.Size = new System.Drawing.Size(568, 426);
            this.rtxt_Hien_thi.TabIndex = 2;
            this.rtxt_Hien_thi.Text = "";
            // 
            // lbl_File_name
            // 
            this.lbl_File_name.AutoSize = true;
            this.lbl_File_name.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_File_name.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_File_name.Location = new System.Drawing.Point(9, 131);
            this.lbl_File_name.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_File_name.Name = "lbl_File_name";
            this.lbl_File_name.Size = new System.Drawing.Size(61, 15);
            this.lbl_File_name.TabIndex = 3;
            this.lbl_File_name.Text = "File name";
            this.lbl_File_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Size
            // 
            this.lbl_Size.AutoSize = true;
            this.lbl_Size.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Size.Location = new System.Drawing.Point(9, 164);
            this.lbl_Size.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Size.Name = "lbl_Size";
            this.lbl_Size.Size = new System.Drawing.Size(31, 15);
            this.lbl_Size.TabIndex = 3;
            this.lbl_Size.Text = "Size";
            this.lbl_Size.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_URL
            // 
            this.lbl_URL.AutoSize = true;
            this.lbl_URL.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_URL.Location = new System.Drawing.Point(9, 193);
            this.lbl_URL.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_URL.Name = "lbl_URL";
            this.lbl_URL.Size = new System.Drawing.Size(30, 15);
            this.lbl_URL.TabIndex = 3;
            this.lbl_URL.Text = "URL";
            this.lbl_URL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Line_count
            // 
            this.lbl_Line_count.AutoSize = true;
            this.lbl_Line_count.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Line_count.Location = new System.Drawing.Point(9, 225);
            this.lbl_Line_count.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Line_count.Name = "lbl_Line_count";
            this.lbl_Line_count.Size = new System.Drawing.Size(66, 15);
            this.lbl_Line_count.TabIndex = 3;
            this.lbl_Line_count.Text = "Line count";
            this.lbl_Line_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Words_count
            // 
            this.lbl_Words_count.AutoSize = true;
            this.lbl_Words_count.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Words_count.Location = new System.Drawing.Point(9, 255);
            this.lbl_Words_count.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Words_count.Name = "lbl_Words_count";
            this.lbl_Words_count.Size = new System.Drawing.Size(80, 15);
            this.lbl_Words_count.TabIndex = 3;
            this.lbl_Words_count.Text = "Words count";
            this.lbl_Words_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Char_count
            // 
            this.lbl_Char_count.AutoSize = true;
            this.lbl_Char_count.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Char_count.Location = new System.Drawing.Point(9, 289);
            this.lbl_Char_count.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Char_count.Name = "lbl_Char_count";
            this.lbl_Char_count.Size = new System.Drawing.Size(69, 15);
            this.lbl_Char_count.TabIndex = 3;
            this.lbl_Char_count.Text = "Char count";
            this.lbl_Char_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_Size
            // 
            this.txt_Size.Location = new System.Drawing.Point(99, 162);
            this.txt_Size.Name = "txt_Size";
            this.txt_Size.Size = new System.Drawing.Size(218, 20);
            this.txt_Size.TabIndex = 1;
            // 
            // txt_URL
            // 
            this.txt_URL.Location = new System.Drawing.Point(99, 191);
            this.txt_URL.Name = "txt_URL";
            this.txt_URL.Size = new System.Drawing.Size(218, 20);
            this.txt_URL.TabIndex = 1;
            // 
            // txt_Line_count
            // 
            this.txt_Line_count.Location = new System.Drawing.Point(99, 223);
            this.txt_Line_count.Name = "txt_Line_count";
            this.txt_Line_count.Size = new System.Drawing.Size(218, 20);
            this.txt_Line_count.TabIndex = 1;
            // 
            // txt_Words_count
            // 
            this.txt_Words_count.Location = new System.Drawing.Point(99, 250);
            this.txt_Words_count.Name = "txt_Words_count";
            this.txt_Words_count.Size = new System.Drawing.Size(218, 20);
            this.txt_Words_count.TabIndex = 1;
            // 
            // txt_Char_count
            // 
            this.txt_Char_count.Location = new System.Drawing.Point(99, 287);
            this.txt_Char_count.Name = "txt_Char_count";
            this.txt_Char_count.Size = new System.Drawing.Size(218, 20);
            this.txt_Char_count.TabIndex = 1;
            // 
            // Lab02_Bai02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 450);
            this.Controls.Add(this.lbl_Char_count);
            this.Controls.Add(this.lbl_Words_count);
            this.Controls.Add(this.lbl_Line_count);
            this.Controls.Add(this.lbl_URL);
            this.Controls.Add(this.lbl_Size);
            this.Controls.Add(this.lbl_File_name);
            this.Controls.Add(this.rtxt_Hien_thi);
            this.Controls.Add(this.txt_Char_count);
            this.Controls.Add(this.txt_Words_count);
            this.Controls.Add(this.txt_Line_count);
            this.Controls.Add(this.txt_URL);
            this.Controls.Add(this.txt_Size);
            this.Controls.Add(this.txt_File_name);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_Read_file);
            this.Name = "Lab02_Bai02";
            this.Text = "Lab02_Bai02";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Read_file;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.TextBox txt_File_name;
        private System.Windows.Forms.RichTextBox rtxt_Hien_thi;
        private System.Windows.Forms.Label lbl_File_name;
        private System.Windows.Forms.Label lbl_Size;
        private System.Windows.Forms.Label lbl_URL;
        private System.Windows.Forms.Label lbl_Line_count;
        private System.Windows.Forms.Label lbl_Words_count;
        private System.Windows.Forms.Label lbl_Char_count;
        private System.Windows.Forms.TextBox txt_Size;
        private System.Windows.Forms.TextBox txt_URL;
        private System.Windows.Forms.TextBox txt_Line_count;
        private System.Windows.Forms.TextBox txt_Words_count;
        private System.Windows.Forms.TextBox txt_Char_count;
    }
}