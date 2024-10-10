namespace Lab02
{
    partial class Lab02_Bai05
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
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.lblDocument = new System.Windows.Forms.Label();
            this.lblImage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDocument
            // 
            this.txtDocument.Location = new System.Drawing.Point(289, 33);
            this.txtDocument.Multiline = true;
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(479, 203);
            this.txtDocument.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(251, 521);
            this.treeView1.TabIndex = 1;
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(289, 290);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(479, 203);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImage.TabIndex = 2;
            this.picImage.TabStop = false;
            // 
            // lblDocument
            // 
            this.lblDocument.AutoSize = true;
            this.lblDocument.Location = new System.Drawing.Point(286, 0);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(68, 16);
            this.lblDocument.TabIndex = 3;
            this.lblDocument.Text = "Document";
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(286, 254);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(45, 16);
            this.lblImage.TabIndex = 4;
            this.lblImage.Text = "Image";
            // 
            // Lab02_Bai05
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 521);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.lblDocument);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.txtDocument);
            this.Name = "Lab02_Bai05";
            this.Text = "Lab02_Bai05";
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDocument;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblDocument;
        private System.Windows.Forms.Label lblImage;
    }
}