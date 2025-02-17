namespace Music
{
    partial class CardControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardControl));
            this.lbNamePlaylist = new Bunifu.UI.WinForms.BunifuLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lbid = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.itemImage = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lbNamePlaylist
            // 
            this.lbNamePlaylist.AllowParentOverrides = false;
            this.lbNamePlaylist.AutoEllipsis = false;
            this.lbNamePlaylist.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbNamePlaylist.CursorType = System.Windows.Forms.Cursors.Default;
            this.lbNamePlaylist.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNamePlaylist.ForeColor = System.Drawing.Color.White;
            this.lbNamePlaylist.Location = new System.Drawing.Point(85, 219);
            this.lbNamePlaylist.Name = "lbNamePlaylist";
            this.lbNamePlaylist.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbNamePlaylist.Size = new System.Drawing.Size(89, 23);
            this.lbNamePlaylist.TabIndex = 1;
            this.lbNamePlaylist.Text = "tên playlist";
            this.lbNamePlaylist.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lbNamePlaylist.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEdit,
            this.btnDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 86);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEdit.Image = global::Music.Properties.Resources.icons8_edit_150;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(214, 26);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDelete.Image = global::Music.Properties.Resources.icons8_delete_30;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(214, 26);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lbid
            // 
            this.lbid.AutoSize = true;
            this.lbid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbid.Location = new System.Drawing.Point(44, 7);
            this.lbid.Name = "lbid";
            this.lbid.Size = new System.Drawing.Size(0, 16);
            this.lbid.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Image = global::Music.Properties.Resources.icons8_ellipsis_60;
            this.pictureBox1.Location = new System.Drawing.Point(208, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // itemImage
            // 
            this.itemImage.Location = new System.Drawing.Point(6, 33);
            this.itemImage.Name = "itemImage";
            this.itemImage.Size = new System.Drawing.Size(250, 187);
            this.itemImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.itemImage.TabIndex = 3;
            this.itemImage.TabStop = false;
            this.itemImage.Click += new System.EventHandler(this.itemImage_Click);
            // 
            // CardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BorderRadius = 36;
            this.Controls.Add(this.lbid);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.itemImage);
            this.Controls.Add(this.lbNamePlaylist);
            this.Name = "CardControl";
            this.Size = new System.Drawing.Size(256, 249);
            this.Load += new System.EventHandler(this.CardControl_Load_1);
            this.Click += new System.EventHandler(this.CardControl_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuLabel lbNamePlaylist;
        private System.Windows.Forms.PictureBox itemImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnEdit;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        private System.Windows.Forms.Label lbid;
        private System.Windows.Forms.Timer timer1;
    }
}
