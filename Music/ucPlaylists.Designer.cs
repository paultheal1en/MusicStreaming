namespace Music
{
    partial class ucPlaylists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPlaylists));
            this.btAddPlaylists = new Bunifu.UI.WinForms.BunifuImageButton();
            this.bunifuSeparator1 = new Bunifu.UI.WinForms.BunifuSeparator();
            this.bunifuCustomLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.cardContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.deleteTimer = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btAddPlaylists
            // 
            this.btAddPlaylists.ActiveImage = null;
            this.btAddPlaylists.AllowAnimations = true;
            this.btAddPlaylists.AllowBuffering = false;
            this.btAddPlaylists.AllowToggling = false;
            this.btAddPlaylists.AllowZooming = true;
            this.btAddPlaylists.AllowZoomingOnFocus = false;
            this.btAddPlaylists.BackColor = System.Drawing.Color.Transparent;
            this.btAddPlaylists.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btAddPlaylists.ErrorImage = ((System.Drawing.Image)(resources.GetObject("btAddPlaylists.ErrorImage")));
            this.btAddPlaylists.FadeWhenInactive = false;
            this.btAddPlaylists.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.btAddPlaylists.Image = global::Music.Properties.Resources.playlist_add_48dp;
            this.btAddPlaylists.ImageActive = null;
            this.btAddPlaylists.ImageLocation = null;
            this.btAddPlaylists.ImageMargin = 20;
            this.btAddPlaylists.ImageSize = new System.Drawing.Size(34, 34);
            this.btAddPlaylists.ImageZoomSize = new System.Drawing.Size(54, 54);
            this.btAddPlaylists.InitialImage = ((System.Drawing.Image)(resources.GetObject("btAddPlaylists.InitialImage")));
            this.btAddPlaylists.Location = new System.Drawing.Point(259, -3);
            this.btAddPlaylists.Name = "btAddPlaylists";
            this.btAddPlaylists.Rotation = 0;
            this.btAddPlaylists.ShowActiveImage = true;
            this.btAddPlaylists.ShowCursorChanges = true;
            this.btAddPlaylists.ShowImageBorders = true;
            this.btAddPlaylists.ShowSizeMarkers = false;
            this.btAddPlaylists.Size = new System.Drawing.Size(54, 54);
            this.btAddPlaylists.TabIndex = 2;
            this.btAddPlaylists.ToolTipText = "";
            this.btAddPlaylists.WaitOnLoad = false;
            this.btAddPlaylists.Zoom = 20;
            this.btAddPlaylists.ZoomSpeed = 10;
            this.btAddPlaylists.Click += new System.EventHandler(this.btAddPlaylists_Click);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuSeparator1.BackgroundImage")));
            this.bunifuSeparator1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuSeparator1.DashCap = Bunifu.UI.WinForms.BunifuSeparator.CapStyles.Flat;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.bunifuSeparator1.LineStyle = Bunifu.UI.WinForms.BunifuSeparator.LineStyles.RightEdgeFaded;
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(4, 46);
            this.bunifuSeparator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Orientation = Bunifu.UI.WinForms.BunifuSeparator.LineOrientation.Horizontal;
            this.bunifuSeparator1.Size = new System.Drawing.Size(1050, 19);
            this.bunifuSeparator1.TabIndex = 3;
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AllowParentOverrides = false;
            this.bunifuCustomLabel2.AutoEllipsis = false;
            this.bunifuCustomLabel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuCustomLabel2.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(62, 2);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(161, 51);
            this.bunifuCustomLabel2.TabIndex = 1;
            this.bunifuCustomLabel2.Text = "Playlists";
            this.bunifuCustomLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuCustomLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // cardContainer
            // 
            this.cardContainer.AutoScroll = true;
            this.cardContainer.Location = new System.Drawing.Point(4, 72);
            this.cardContainer.Name = "cardContainer";
            this.cardContainer.Size = new System.Drawing.Size(1030, 789);
            this.cardContainer.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // deleteTimer
            // 
            this.deleteTimer.Enabled = true;
            this.deleteTimer.Tick += new System.EventHandler(this.deleteTimer_Tick);
            // 
            // ucPlaylists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.cardContainer);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.btAddPlaylists);
            this.Name = "ucPlaylists";
            this.Size = new System.Drawing.Size(1040, 914);
            this.Load += new System.EventHandler(this.ucPlaylists_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuImageButton btAddPlaylists;
        private Bunifu.UI.WinForms.BunifuSeparator bunifuSeparator1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuCustomLabel2;
        private System.Windows.Forms.FlowLayoutPanel cardContainer;
        private CardControl playlist_item1;
        private CardControl playlist_item2;
        private CardControl playlist_item3;
        private CardControl playlist_item4;
        private CardControl playlist_item5;
        private CardControl playlist_item6;
        private CardControl playlist_item7;
        private CardControl playlist_item8;
        private CardControl playlist_item9;
        private CardControl playlist_item10;
        private CardControl playlist_item11;
        private CardControl playlist_item12;
        private CardControl playlist_item13;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer deleteTimer;
        private System.Windows.Forms.Timer timer2;
    }
}
