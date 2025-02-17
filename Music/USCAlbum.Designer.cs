namespace Music
{
    partial class USCAlbum
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(USCAlbum));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pAlbumImage = new System.Windows.Forms.PictureBox();
            this.lAlbumName = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel6 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel5 = new Bunifu.UI.WinForms.BunifuLabel();
            this.lTotalTracks = new Bunifu.UI.WinForms.BunifuLabel();
            this.lDate = new Bunifu.UI.WinForms.BunifuLabel();
            this.lArtistName = new Bunifu.UI.WinForms.BunifuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanelTrack = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pAlbumImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(216)))), ((int)(((byte)(87)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.pAlbumImage);
            this.panel2.Controls.Add(this.lAlbumName);
            this.panel2.Controls.Add(this.bunifuLabel6);
            this.panel2.Controls.Add(this.bunifuLabel5);
            this.panel2.Controls.Add(this.lTotalTracks);
            this.panel2.Controls.Add(this.lDate);
            this.panel2.Controls.Add(this.lArtistName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(793, 246);
            this.panel2.TabIndex = 4;
            // 
            // pAlbumImage
            // 
            this.pAlbumImage.Location = new System.Drawing.Point(15, 20);
            this.pAlbumImage.Name = "pAlbumImage";
            this.pAlbumImage.Size = new System.Drawing.Size(207, 207);
            this.pAlbumImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pAlbumImage.TabIndex = 11;
            this.pAlbumImage.TabStop = false;
            // 
            // lAlbumName
            // 
            this.lAlbumName.AllowParentOverrides = false;
            this.lAlbumName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lAlbumName.AutoEllipsis = true;
            this.lAlbumName.CursorType = null;
            this.lAlbumName.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAlbumName.ForeColor = System.Drawing.Color.White;
            this.lAlbumName.Location = new System.Drawing.Point(244, 26);
            this.lAlbumName.Name = "lAlbumName";
            this.lAlbumName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lAlbumName.Size = new System.Drawing.Size(254, 65);
            this.lAlbumName.TabIndex = 10;
            this.lAlbumName.Text = "Thiên Lý Ơi";
            this.lAlbumName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lAlbumName.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel6
            // 
            this.bunifuLabel6.AllowParentOverrides = false;
            this.bunifuLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bunifuLabel6.AutoEllipsis = true;
            this.bunifuLabel6.CursorType = null;
            this.bunifuLabel6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel6.ForeColor = System.Drawing.Color.White;
            this.bunifuLabel6.Location = new System.Drawing.Point(244, 138);
            this.bunifuLabel6.Name = "bunifuLabel6";
            this.bunifuLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel6.Size = new System.Drawing.Size(169, 33);
            this.bunifuLabel6.TabIndex = 9;
            this.bunifuLabel6.Text = "Ngày sáng tác: ";
            this.bunifuLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel6.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel5
            // 
            this.bunifuLabel5.AllowParentOverrides = false;
            this.bunifuLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bunifuLabel5.AutoEllipsis = true;
            this.bunifuLabel5.AutoSize = false;
            this.bunifuLabel5.AutoSizeHeightOnly = true;
            this.bunifuLabel5.CursorType = null;
            this.bunifuLabel5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel5.ForeColor = System.Drawing.Color.White;
            this.bunifuLabel5.Location = new System.Drawing.Point(276, 177);
            this.bunifuLabel5.Name = "bunifuLabel5";
            this.bunifuLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel5.Size = new System.Drawing.Size(78, 33);
            this.bunifuLabel5.TabIndex = 8;
            this.bunifuLabel5.Text = "bài hát";
            this.bunifuLabel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel5.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // lTotalTracks
            // 
            this.lTotalTracks.AllowParentOverrides = false;
            this.lTotalTracks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lTotalTracks.AutoEllipsis = true;
            this.lTotalTracks.CursorType = null;
            this.lTotalTracks.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTotalTracks.ForeColor = System.Drawing.Color.White;
            this.lTotalTracks.Location = new System.Drawing.Point(244, 177);
            this.lTotalTracks.Name = "lTotalTracks";
            this.lTotalTracks.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lTotalTracks.Size = new System.Drawing.Size(14, 33);
            this.lTotalTracks.TabIndex = 7;
            this.lTotalTracks.Text = "1";
            this.lTotalTracks.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lTotalTracks.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // lDate
            // 
            this.lDate.AllowParentOverrides = false;
            this.lDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lDate.AutoEllipsis = true;
            this.lDate.CursorType = null;
            this.lDate.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDate.ForeColor = System.Drawing.Color.White;
            this.lDate.Location = new System.Drawing.Point(418, 138);
            this.lDate.Name = "lDate";
            this.lDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lDate.Size = new System.Drawing.Size(132, 33);
            this.lDate.TabIndex = 6;
            this.lDate.Text = "22-02-2024";
            this.lDate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lDate.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // lArtistName
            // 
            this.lArtistName.AllowParentOverrides = false;
            this.lArtistName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lArtistName.AutoEllipsis = true;
            this.lArtistName.CursorType = null;
            this.lArtistName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lArtistName.ForeColor = System.Drawing.Color.White;
            this.lArtistName.Location = new System.Drawing.Point(244, 98);
            this.lArtistName.Name = "lArtistName";
            this.lArtistName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lArtistName.Size = new System.Drawing.Size(101, 33);
            this.lArtistName.TabIndex = 5;
            this.lArtistName.Text = "Jack - 97";
            this.lArtistName.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lArtistName.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanelTrack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 382);
            this.panel1.TabIndex = 5;
            // 
            // flowLayoutPanelTrack
            // 
            this.flowLayoutPanelTrack.AutoScroll = true;
            this.flowLayoutPanelTrack.AutoSize = true;
            this.flowLayoutPanelTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTrack.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelTrack.Name = "flowLayoutPanelTrack";
            this.flowLayoutPanelTrack.Size = new System.Drawing.Size(793, 382);
            this.flowLayoutPanelTrack.TabIndex = 0;
            // 
            // USCAlbum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "USCAlbum";
            this.Size = new System.Drawing.Size(793, 628);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pAlbumImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuLabel lTotalTracks;
        private Bunifu.UI.WinForms.BunifuLabel lDate;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel5;
        private Bunifu.UI.WinForms.BunifuLabel lArtistName;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel6;
        private Bunifu.UI.WinForms.BunifuLabel lAlbumName;
        private System.Windows.Forms.PictureBox pAlbumImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTrack;
    }
}