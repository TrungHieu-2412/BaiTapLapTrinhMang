using System;

namespace ungdungve
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
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.btnChooseColor = new System.Windows.Forms.Button();
            this.cbBrushSize = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingPanel.Location = new System.Drawing.Point(24, 59);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(908, 308);
            this.drawingPanel.TabIndex = 0;
            this.drawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingPanel_Paint);
            // 
            // btnChooseColor
            // 
            this.btnChooseColor.Location = new System.Drawing.Point(13, 13);
            this.btnChooseColor.Name = "btnChooseColor";
            this.btnChooseColor.Size = new System.Drawing.Size(75, 23);
            this.btnChooseColor.TabIndex = 1;
            this.btnChooseColor.Text = "Color";
            this.btnChooseColor.UseVisualStyleBackColor = true;
            this.btnChooseColor.Click += new System.EventHandler(this.btnChooseColor_Click);
            // 
            // cbBrushSize
            // 
            this.cbBrushSize.BackColor = System.Drawing.SystemColors.Menu;
            this.cbBrushSize.FormattingEnabled = true;
            this.cbBrushSize.Location = new System.Drawing.Point(109, 13);
            this.cbBrushSize.Name = "cbBrushSize";
            this.cbBrushSize.Size = new System.Drawing.Size(121, 24);
            this.cbBrushSize.TabIndex = 2;
            this.cbBrushSize.Click += new System.EventHandler(this.cbBrushsize_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(262, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(383, 13);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "Hoàn tác";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 392);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbBrushSize);
            this.Controls.Add(this.btnChooseColor);
            this.Controls.Add(this.drawingPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        private void cbBrushsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Button btnChooseColor;
        private System.Windows.Forms.ComboBox cbBrushSize;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnUndo;
    }
}

