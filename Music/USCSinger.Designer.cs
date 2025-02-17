namespace Music
{
    partial class USCSinger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(USCSinger));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pArtistImage = new System.Windows.Forms.PictureBox();
            this.lArtistName = new Bunifu.UI.WinForms.BunifuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanelTrack = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pArtistImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(216)))), ((int)(((byte)(87)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.pArtistImage);
            this.panel2.Controls.Add(this.lArtistName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1057, 303);
            this.panel2.TabIndex = 3;
            // 
            // pArtistImage
            // 
            this.pArtistImage.Location = new System.Drawing.Point(37, 37);
            this.pArtistImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pArtistImage.Name = "pArtistImage";
            this.pArtistImage.Size = new System.Drawing.Size(240, 222);
            this.pArtistImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pArtistImage.TabIndex = 5;
            this.pArtistImage.TabStop = false;
            // 
            // lArtistName
            // 
            this.lArtistName.AllowParentOverrides = false;
            this.lArtistName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lArtistName.AutoEllipsis = true;
            this.lArtistName.CursorType = null;
            this.lArtistName.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lArtistName.ForeColor = System.Drawing.Color.White;
            this.lArtistName.Location = new System.Drawing.Point(315, 97);
            this.lArtistName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lArtistName.Name = "lArtistName";
            this.lArtistName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lArtistName.Size = new System.Drawing.Size(439, 81);
            this.lArtistName.TabIndex = 3;
            this.lArtistName.Text = "Sơn Tùng M-TP";
            this.lArtistName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lArtistName.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanelTrack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 303);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 543);
            this.panel1.TabIndex = 5;
            // 
            // flowLayoutPanelTrack
            // 
            this.flowLayoutPanelTrack.AutoScroll = true;
            this.flowLayoutPanelTrack.AutoSize = true;
            this.flowLayoutPanelTrack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTrack.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelTrack.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelTrack.Name = "flowLayoutPanelTrack";
            this.flowLayoutPanelTrack.Size = new System.Drawing.Size(1057, 543);
            this.flowLayoutPanelTrack.TabIndex = 0;
            // 
            // USCSinger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "USCSinger";
            this.Size = new System.Drawing.Size(1057, 846);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pArtistImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuLabel lArtistName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pArtistImage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTrack;
    }
}