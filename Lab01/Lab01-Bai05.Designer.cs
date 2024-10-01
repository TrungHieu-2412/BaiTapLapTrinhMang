namespace Lab01
{
    partial class Lab01_Bai05
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
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtMon3 = new System.Windows.Forms.TextBox();
            this.txtMon2 = new System.Windows.Forms.TextBox();
            this.txtMon1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPhai = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMon1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMon2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMon3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrungBinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXepLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeleteCell = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(66, 31);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(163, 20);
            this.txtHoTen.TabIndex = 0;
            this.txtHoTen.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtMon3
            // 
            this.txtMon3.Location = new System.Drawing.Point(388, 83);
            this.txtMon3.Name = "txtMon3";
            this.txtMon3.Size = new System.Drawing.Size(126, 20);
            this.txtMon3.TabIndex = 1;
            // 
            // txtMon2
            // 
            this.txtMon2.Location = new System.Drawing.Point(388, 57);
            this.txtMon2.Name = "txtMon2";
            this.txtMon2.Size = new System.Drawing.Size(126, 20);
            this.txtMon2.TabIndex = 2;
            this.txtMon2.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtMon1
            // 
            this.txtMon1.Location = new System.Drawing.Point(388, 31);
            this.txtMon1.Name = "txtMon1";
            this.txtMon1.Size = new System.Drawing.Size(126, 20);
            this.txtMon1.TabIndex = 3;
            this.txtMon1.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Họ và tên";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cboPhai
            // 
            this.cboPhai.FormattingEnabled = true;
            this.cboPhai.Location = new System.Drawing.Point(66, 57);
            this.cboPhai.Name = "cboPhai";
            this.cboPhai.Size = new System.Drawing.Size(163, 21);
            this.cboPhai.TabIndex = 5;
            this.cboPhai.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Phái";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Điểm Môn 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Điểm Môn 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Điểm Môn 3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnThongKe);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtHoTen);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboPhai);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMon3);
            this.groupBox1.Controls.Add(this.txtMon2);
            this.groupBox1.Controls.Add(this.txtMon1);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 148);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhập Thí Sinh";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(90, 106);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 106);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colHoTen,
            this.colPhai,
            this.colMon1,
            this.colMon2,
            this.colMon3,
            this.colTrungBinh,
            this.colXepLoai,
            this.colDeleteCell});
            this.dataGridView1.Location = new System.Drawing.Point(12, 252);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(939, 173);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            // 
            // colHoTen
            // 
            this.colHoTen.HeaderText = "Họ và Tên";
            this.colHoTen.Name = "colHoTen";
            // 
            // colPhai
            // 
            this.colPhai.HeaderText = "Phái";
            this.colPhai.Name = "colPhai";
            this.colPhai.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colMon1
            // 
            this.colMon1.HeaderText = "Môn 1";
            this.colMon1.Name = "colMon1";
            // 
            // colMon2
            // 
            this.colMon2.HeaderText = "Môn 2";
            this.colMon2.Name = "colMon2";
            // 
            // colMon3
            // 
            this.colMon3.HeaderText = "Môn 3";
            this.colMon3.Name = "colMon3";
            // 
            // colTrungBinh
            // 
            this.colTrungBinh.HeaderText = "Trung bình";
            this.colTrungBinh.Name = "colTrungBinh";
            // 
            // colXepLoai
            // 
            this.colXepLoai.HeaderText = "Xếp loại";
            this.colXepLoai.Name = "colXepLoai";
            this.colXepLoai.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colXepLoai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDeleteCell
            // 
            this.colDeleteCell.HeaderText = "Xóa";
            this.colDeleteCell.Name = "colDeleteCell";
            this.colDeleteCell.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Danh Sách Thí Sinh";
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(171, 106);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(75, 23);
            this.btnThongKe.TabIndex = 13;
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // Lab01_Bai05
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(963, 437);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Lab01_Bai05";
            this.Text = "Lab01_Bai05";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtMon3;
        private System.Windows.Forms.TextBox txtMon2;
        private System.Windows.Forms.TextBox txtMon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPhai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMon1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMon2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMon3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrungBinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXepLoai;
        private System.Windows.Forms.DataGridViewButtonColumn colDeleteCell;
        private System.Windows.Forms.Button btnThongKe;
    }
}