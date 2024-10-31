namespace Lab03
{
    partial class TCP_SERVER
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
            this.txt_Display = new System.Windows.Forms.TextBox();
            this.btn_Listen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_Display
            // 
            this.txt_Display.Location = new System.Drawing.Point(12, 64);
            this.txt_Display.Multiline = true;
            this.txt_Display.Name = "txt_Display";
            this.txt_Display.Size = new System.Drawing.Size(776, 374);
            this.txt_Display.TabIndex = 0;
            // 
            // btn_Listen
            // 
            this.btn_Listen.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_Listen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Listen.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Listen.Location = new System.Drawing.Point(697, 27);
            this.btn_Listen.Name = "btn_Listen";
            this.btn_Listen.Size = new System.Drawing.Size(91, 31);
            this.btn_Listen.TabIndex = 1;
            this.btn_Listen.Text = "Listen";
            this.btn_Listen.UseVisualStyleBackColor = false;
            this.btn_Listen.Click += new System.EventHandler(this.btn_Listen_Click);
            // 
            // TCP_SERVER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Listen);
            this.Controls.Add(this.txt_Display);
            this.Name = "TCP_SERVER";
            this.Text = "TCP_SERVER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCP_SERVER_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Display;
        private System.Windows.Forms.Button btn_Listen;
    }
}