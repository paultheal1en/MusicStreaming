namespace Music
{
    partial class Artist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Artist));
            this.NgheSi = new Bunifu.UI.WinForms.BunifuLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbNameArtist = new System.Windows.Forms.Label();
            this.imageArtist = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageArtist)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NgheSi
            // 
            this.NgheSi.AllowParentOverrides = false;
            this.NgheSi.AutoEllipsis = false;
            this.NgheSi.Cursor = System.Windows.Forms.Cursors.Default;
            this.NgheSi.CursorType = System.Windows.Forms.Cursors.Default;
            this.NgheSi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NgheSi.ForeColor = System.Drawing.Color.Silver;
            this.NgheSi.Location = new System.Drawing.Point(47, 181);
            this.NgheSi.Margin = new System.Windows.Forms.Padding(2);
            this.NgheSi.Name = "NgheSi";
            this.NgheSi.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NgheSi.Size = new System.Drawing.Size(46, 19);
            this.NgheSi.TabIndex = 3;
            this.NgheSi.Text = "Nghệ sĩ";
            this.NgheSi.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.NgheSi.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbNameArtist);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 162);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 21);
            this.panel2.TabIndex = 6;
            // 
            // lbNameArtist
            // 
            this.lbNameArtist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNameArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameArtist.Location = new System.Drawing.Point(2, 0);
            this.lbNameArtist.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNameArtist.Name = "lbNameArtist";
            this.lbNameArtist.Size = new System.Drawing.Size(137, 21);
            this.lbNameArtist.TabIndex = 0;
            this.lbNameArtist.Text = "lbNameArtist";
            this.lbNameArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageArtist
            // 
            this.imageArtist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageArtist.BackColor = System.Drawing.Color.Transparent;
            this.imageArtist.Location = new System.Drawing.Point(10, 21);
            this.imageArtist.Margin = new System.Windows.Forms.Padding(0);
            this.imageArtist.Name = "imageArtist";
            this.imageArtist.Size = new System.Drawing.Size(120, 120);
            this.imageArtist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageArtist.TabIndex = 4;
            this.imageArtist.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageArtist);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 162);
            this.panel1.TabIndex = 4;
            // 
            // Artist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.Black;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BorderRadius = 36;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NgheSi);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Artist";
            this.Size = new System.Drawing.Size(140, 202);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageArtist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuLabel NgheSi;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbNameArtist;
        private System.Windows.Forms.PictureBox imageArtist;
        private System.Windows.Forms.Panel panel1;
    }
}