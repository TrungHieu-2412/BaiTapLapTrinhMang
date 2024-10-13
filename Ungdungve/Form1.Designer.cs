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
            this.btnEraser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // drawingPanel
            // 
            this.drawingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingPanel.Location = new System.Drawing.Point(21, 47);
            this.drawingPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(807, 471);
            this.drawingPanel.TabIndex = 0;
            // 
            // btnChooseColor
            // 
            this.btnChooseColor.Location = new System.Drawing.Point(21, 10);
            this.btnChooseColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChooseColor.Name = "btnChooseColor";
            this.btnChooseColor.Size = new System.Drawing.Size(92, 29);
            this.btnChooseColor.TabIndex = 1;
            this.btnChooseColor.Text = "Color";
            this.btnChooseColor.UseVisualStyleBackColor = true;
            // 
            // cbBrushSize
            // 
            this.cbBrushSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrushSize.FormattingEnabled = true;
            this.cbBrushSize.Location = new System.Drawing.Point(720, 13);
            this.cbBrushSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbBrushSize.Name = "cbBrushSize";
            this.cbBrushSize.Size = new System.Drawing.Size(108, 24);
            this.cbBrushSize.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(119, 10);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(92, 29);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(217, 10);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(92, 29);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // btnEraser
            // 
            this.btnEraser.Location = new System.Drawing.Point(315, 10);
            this.btnEraser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEraser.Name = "btnEraser";
            this.btnEraser.Size = new System.Drawing.Size(92, 29);
            this.btnEraser.TabIndex = 5;
            this.btnEraser.Text = "Eraser";
            this.btnEraser.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(671, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 534);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEraser);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbBrushSize);
            this.Controls.Add(this.btnChooseColor);
            this.Controls.Add(this.drawingPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Drawing Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.Button btnChooseColor;
        private System.Windows.Forms.ComboBox cbBrushSize;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnEraser;
        private System.Windows.Forms.Label label1;
    }
}
