﻿namespace DrawTogether
{
    partial class CLIENT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CLIENT));
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
            this.btnEllipse = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnLine1 = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.lisUserName = new System.Windows.Forms.ListView();
            this.labPlayerName = new System.Windows.Forms.Label();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.labRoom = new System.Windows.Forms.Label();
            this.txtRoomCodeCanva = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputMess = new System.Windows.Forms.TextBox();
            this.txtInputMess = new System.Windows.Forms.MaskedTextBox();
            this.btnSendMess = new System.Windows.Forms.Button();
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
            this.saveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.BackgroundImage")));
            this.openToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.BackgroundImage")));
            this.saveToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // btnDrawing
            // 
            this.btnDrawing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDrawing.BackgroundImage")));
            this.btnDrawing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDrawing.Location = new System.Drawing.Point(203, 14);
            this.btnDrawing.Margin = new System.Windows.Forms.Padding(2);
            this.btnDrawing.Name = "btnDrawing";
            this.btnDrawing.Size = new System.Drawing.Size(50, 54);
            this.btnDrawing.TabIndex = 1;
            this.btnDrawing.UseVisualStyleBackColor = true;
            this.btnDrawing.Click += new System.EventHandler(this.btnDrawing_Click);
            // 
            // btnEraser
            // 
            this.btnEraser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEraser.BackgroundImage")));
            this.btnEraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEraser.Location = new System.Drawing.Point(284, 14);
            this.btnEraser.Margin = new System.Windows.Forms.Padding(2);
            this.btnEraser.Name = "btnEraser";
            this.btnEraser.Size = new System.Drawing.Size(50, 54);
            this.btnEraser.TabIndex = 2;
            this.btnEraser.UseVisualStyleBackColor = true;
            this.btnEraser.Click += new System.EventHandler(this.btnEraser_Click);
            // 
            // btnChooseColor
            // 
            this.btnChooseColor.Location = new System.Drawing.Point(526, 10);
            this.btnChooseColor.Margin = new System.Windows.Forms.Padding(2);
            this.btnChooseColor.Name = "btnChooseColor";
            this.btnChooseColor.Size = new System.Drawing.Size(65, 24);
            this.btnChooseColor.TabIndex = 3;
            this.btnChooseColor.Text = "Color";
            this.btnChooseColor.UseVisualStyleBackColor = true;
            this.btnChooseColor.Click += new System.EventHandler(this.btnChooseColor_Click);
            // 
            // cbBrushSize
            // 
            this.cbBrushSize.FormattingEnabled = true;
            this.cbBrushSize.Location = new System.Drawing.Point(436, 13);
            this.cbBrushSize.Margin = new System.Windows.Forms.Padding(2);
            this.cbBrushSize.Name = "cbBrushSize";
            this.cbBrushSize.Size = new System.Drawing.Size(67, 21);
            this.cbBrushSize.TabIndex = 4;
            this.cbBrushSize.SelectedIndexChanged += new System.EventHandler(this.cbBrushSize_SelectedIndexChanged);
            // 
            // picColor
            // 
            this.picColor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picColor.Location = new System.Drawing.Point(540, 39);
            this.picColor.Margin = new System.Windows.Forms.Padding(2);
            this.picColor.Name = "picColor";
            this.picColor.Size = new System.Drawing.Size(38, 37);
            this.picColor.TabIndex = 5;
            this.picColor.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(453, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // Edit
            // 
            this.Edit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Edit.Controls.Add(this.btnEllipse);
            this.Edit.Controls.Add(this.btnRectangle);
            this.Edit.Controls.Add(this.btnLine1);
            this.Edit.Controls.Add(this.btnUndo);
            this.Edit.Controls.Add(this.label1);
            this.Edit.Controls.Add(this.lisUserName);
            this.Edit.Controls.Add(this.picColor);
            this.Edit.Controls.Add(this.cbBrushSize);
            this.Edit.Controls.Add(this.labPlayerName);
            this.Edit.Controls.Add(this.btnChooseColor);
            this.Edit.Controls.Add(this.btnEraser);
            this.Edit.Controls.Add(this.btnDrawing);
            this.Edit.Dock = System.Windows.Forms.DockStyle.Top;
            this.Edit.Location = new System.Drawing.Point(0, 0);
            this.Edit.Margin = new System.Windows.Forms.Padding(2);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(995, 92);
            this.Edit.TabIndex = 0;
            // 
            // btnEllipse
            // 
            this.btnEllipse.Location = new System.Drawing.Point(20, 51);
            this.btnEllipse.Margin = new System.Windows.Forms.Padding(2);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(53, 19);
            this.btnEllipse.TabIndex = 10;
            this.btnEllipse.Text = "Elip";
            this.btnEllipse.UseVisualStyleBackColor = true;
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Location = new System.Drawing.Point(86, 15);
            this.btnRectangle.Margin = new System.Windows.Forms.Padding(2);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(58, 19);
            this.btnRectangle.TabIndex = 9;
            this.btnRectangle.Text = "Chữ nhật";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnLine1
            // 
            this.btnLine1.Location = new System.Drawing.Point(20, 15);
            this.btnLine1.Margin = new System.Windows.Forms.Padding(2);
            this.btnLine1.Name = "btnLine1";
            this.btnLine1.Size = new System.Drawing.Size(53, 19);
            this.btnLine1.TabIndex = 8;
            this.btnLine1.Text = "Đường thẳng";
            this.btnLine1.UseVisualStyleBackColor = true;
            this.btnLine1.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUndo.BackgroundImage")));
            this.btnUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUndo.Location = new System.Drawing.Point(358, 14);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(2);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(50, 54);
            this.btnUndo.TabIndex = 7;
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // lisUserName
            // 
            this.lisUserName.HideSelection = false;
            this.lisUserName.Location = new System.Drawing.Point(608, 35);
            this.lisUserName.Margin = new System.Windows.Forms.Padding(2);
            this.lisUserName.Name = "lisUserName";
            this.lisUserName.Size = new System.Drawing.Size(361, 41);
            this.lisUserName.TabIndex = 5;
            this.lisUserName.UseCompatibleStateImageBehavior = false;
            // 
            // labPlayerName
            // 
            this.labPlayerName.AutoSize = true;
            this.labPlayerName.Font = new System.Drawing.Font("MV Boli", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPlayerName.Location = new System.Drawing.Point(614, 13);
            this.labPlayerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPlayerName.Name = "labPlayerName";
            this.labPlayerName.Size = new System.Drawing.Size(71, 16);
            this.labPlayerName.TabIndex = 3;
            this.labPlayerName.Text = "Player Name";
            // 
            // Canvas
            // 
            this.Canvas.ContextMenuStrip = this.contextMenuStrip1;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 92);
            this.Canvas.Margin = new System.Windows.Forms.Padding(2);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(995, 511);
            this.Canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Canvas.TabIndex = 1;
            this.Canvas.TabStop = false;
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // labRoom
            // 
            this.labRoom.AutoSize = true;
            this.labRoom.Location = new System.Drawing.Point(11, 104);
            this.labRoom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labRoom.Name = "labRoom";
            this.labRoom.Size = new System.Drawing.Size(63, 13);
            this.labRoom.TabIndex = 2;
            this.labRoom.Text = "Room Code";
            // 
            // txtRoomCodeCanva
            // 
            this.txtRoomCodeCanva.Location = new System.Drawing.Point(10, 120);
            this.txtRoomCodeCanva.Margin = new System.Windows.Forms.Padding(2);
            this.txtRoomCodeCanva.Name = "txtRoomCodeCanva";
            this.txtRoomCodeCanva.Size = new System.Drawing.Size(76, 20);
            this.txtRoomCodeCanva.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Message";
            // 
            // txtOutputMess
            // 
            this.txtOutputMess.Location = new System.Drawing.Point(10, 163);
            this.txtOutputMess.Multiline = true;
            this.txtOutputMess.Name = "txtOutputMess";
            this.txtOutputMess.Size = new System.Drawing.Size(158, 132);
            this.txtOutputMess.TabIndex = 7;
            // 
            // txtInputMess
            // 
            this.txtInputMess.Location = new System.Drawing.Point(10, 301);
            this.txtInputMess.Name = "txtInputMess";
            this.txtInputMess.Size = new System.Drawing.Size(100, 20);
            this.txtInputMess.TabIndex = 8;
            // 
            // btnSendMess
            // 
            this.btnSendMess.Font = new System.Drawing.Font("MV Boli", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMess.Location = new System.Drawing.Point(116, 299);
            this.btnSendMess.Name = "btnSendMess";
            this.btnSendMess.Size = new System.Drawing.Size(52, 23);
            this.btnSendMess.TabIndex = 9;
            this.btnSendMess.Text = "SEND";
            this.btnSendMess.UseVisualStyleBackColor = true;
            // 
            // CLIENT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 603);
            this.Controls.Add(this.btnSendMess);
            this.Controls.Add(this.txtInputMess);
            this.Controls.Add(this.txtOutputMess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRoomCodeCanva);
            this.Controls.Add(this.labRoom);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.Edit);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CLIENT";
            this.Text = "DRAW TOGETHER";
            this.Load += new System.EventHandler(this.Canva_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
            this.Edit.ResumeLayout(false);
            this.Edit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        //private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem;
        private System.Windows.Forms.Label labRoom;
        private System.Windows.Forms.Label labPlayerName;
        private System.Windows.Forms.TextBox txtRoomCodeCanva;
        private System.Windows.Forms.ListView lisUserName;
        private System.Windows.Forms.Button btnEllipse;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnLine1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputMess;
        private System.Windows.Forms.MaskedTextBox txtInputMess;
        private System.Windows.Forms.Button btnSendMess;
    }
}