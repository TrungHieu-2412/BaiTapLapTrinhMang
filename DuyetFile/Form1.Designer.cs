using System;
using System.Windows.Forms;

namespace Duyet_Picture
{
    partial class Form1
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
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lbThumbnails = new System.Windows.Forms.ListBox();
            this.pbMainImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(-3, -2);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(120, 36);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Mở thư mục";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // lbThumbnails
            // 
            this.lbThumbnails.FormattingEnabled = true;
            this.lbThumbnails.ItemHeight = 16;
            this.lbThumbnails.Location = new System.Drawing.Point(-3, 51);
            this.lbThumbnails.Name = "lbThumbnails";
            this.lbThumbnails.Size = new System.Drawing.Size(120, 388);
            this.lbThumbnails.TabIndex = 1;
            this.lbThumbnails.SelectedIndexChanged += new System.EventHandler(this.lbThumbnails_SelectedIndexChanged);
            // 
            // pbMainImage
            // 
            this.pbMainImage.Location = new System.Drawing.Point(123, 51);
            this.pbMainImage.Name = "pbMainImage";
            this.pbMainImage.Size = new System.Drawing.Size(665, 388);
            this.pbMainImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMainImage.TabIndex = 2;
            this.pbMainImage.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbMainImage);
            this.Controls.Add(this.lbThumbnails);
            this.Controls.Add(this.btnOpenFolder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown_1);
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).EndInit();
            this.ResumeLayout(false);

        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.ListBox lbThumbnails;
        private System.Windows.Forms.PictureBox pbMainImage;
    }
}

