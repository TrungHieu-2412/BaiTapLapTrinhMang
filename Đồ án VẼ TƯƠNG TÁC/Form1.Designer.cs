namespace Ungdungvecoban
{
    partial class Form_Client
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Client));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDrawing = new System.Windows.Forms.Button();
            this.btnEraser = new System.Windows.Forms.Button();
            this.btnChooseColor = new System.Windows.Forms.Button();
            this.cbBrushSize = new System.Windows.Forms.ComboBox();
            this.picColor = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Edit = new System.Windows.Forms.Panel();
            this.btnUndo = new System.Windows.Forms.Button();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.messagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
            this.Edit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.messagesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 82);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // btnDrawing
            // 
            this.btnDrawing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrawing.BackgroundImage")));
            this.btnDrawing.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawing.Image")));
            this.btnDrawing.Location = new System.Drawing.Point(29, 16);
            this.btnDrawing.Name = "btnDrawing";
            this.btnDrawing.Size = new System.Drawing.Size(77, 71);
            this.btnDrawing.TabIndex = 1;
            this.btnDrawing.UseVisualStyleBackColor = true;
            this.btnDrawing.Click += new System.EventHandler(this.btnDrawing_Click);
            // 
            // btnEraser
            // 
            this.btnEraser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEraser.BackgroundImage")));
            this.btnEraser.Image = ((System.Drawing.Image)(resources.GetObject("btnEraser.Image")));
            this.btnEraser.Location = new System.Drawing.Point(181, 16);
            this.btnEraser.Name = "btnEraser";
            this.btnEraser.Size = new System.Drawing.Size(77, 71);
            this.btnEraser.TabIndex = 2;
            this.btnEraser.UseVisualStyleBackColor = true;
            this.btnEraser.Click += new System.EventHandler(this.btnEraser_Click);
            // 
            // btnChooseColor
            // 
            this.btnChooseColor.Location = new System.Drawing.Point(701, 12);
            this.btnChooseColor.Name = "btnChooseColor";
            this.btnChooseColor.Size = new System.Drawing.Size(87, 30);
            this.btnChooseColor.TabIndex = 3;
            this.btnChooseColor.Text = "Color";
            this.btnChooseColor.UseVisualStyleBackColor = true;
            this.btnChooseColor.Click += new System.EventHandler(this.btnChooseColor_Click);
            // 
            // cbBrushSize
            // 
            this.cbBrushSize.FormattingEnabled = true;
            this.cbBrushSize.Location = new System.Drawing.Point(581, 16);
            this.cbBrushSize.Name = "cbBrushSize";
            this.cbBrushSize.Size = new System.Drawing.Size(88, 24);
            this.cbBrushSize.TabIndex = 4;
            this.cbBrushSize.SelectedIndexChanged += new System.EventHandler(this.cbBrushSize_SelectedIndexChanged);
            // 
            // picColor
            // 
            this.picColor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picColor.Location = new System.Drawing.Point(720, 48);
            this.picColor.Name = "picColor";
            this.picColor.Size = new System.Drawing.Size(51, 46);
            this.picColor.TabIndex = 5;
            this.picColor.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(604, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // Edit
            // 
            this.Edit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Edit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Edit.BackgroundImage")));
            this.Edit.Controls.Add(this.btnUndo);
            this.Edit.Controls.Add(this.label1);
            this.Edit.Controls.Add(this.picColor);
            this.Edit.Controls.Add(this.cbBrushSize);
            this.Edit.Controls.Add(this.btnChooseColor);
            this.Edit.Controls.Add(this.btnEraser);
            this.Edit.Controls.Add(this.btnDrawing);
            this.Edit.Dock = System.Windows.Forms.DockStyle.Top;
            this.Edit.Location = new System.Drawing.Point(0, 0);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(800, 113);
            this.Edit.TabIndex = 0;
            // 
            // btnUndo
            // 
            this.btnUndo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUndo.BackgroundImage")));
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.Location = new System.Drawing.Point(338, 16);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(77, 71);
            this.btnUndo.TabIndex = 7;
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // Canvas
            // 
            this.Canvas.ContextMenuStrip = this.contextMenuStrip1;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 113);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(800, 337);
            this.Canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Canvas.TabIndex = 1;
            this.Canvas.TabStop = false;
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // messagesToolStripMenuItem
            // 
            this.messagesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("messagesToolStripMenuItem.Image")));
            this.messagesToolStripMenuItem.Name = "messagesToolStripMenuItem";
            this.messagesToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.messagesToolStripMenuItem.Text = "Messages";
            // 
            // Form_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.Edit);
            this.Name = "Form_Client";
            this.Text = "Canva";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
            this.Edit.ResumeLayout(false);
            this.Edit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button btnDrawing;
        private System.Windows.Forms.Button btnEraser;
        private System.Windows.Forms.Button btnChooseColor;
        private System.Windows.Forms.ComboBox cbBrushSize;
        private System.Windows.Forms.PictureBox picColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Edit;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem;
    }
}

