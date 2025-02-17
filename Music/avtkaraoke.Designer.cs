namespace Music
{
    partial class avtkaraoke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(avtkaraoke));
            this.imageArtist = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.lbNameArtist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageArtist)).BeginInit();
            this.SuspendLayout();
            // 
            // imageArtist
            // 
            this.imageArtist.AllowFocused = false;
            this.imageArtist.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imageArtist.AutoSizeHeight = true;
            this.imageArtist.BorderRadius = 62;
            this.imageArtist.Image = ((System.Drawing.Image)(resources.GetObject("imageArtist.Image")));
            this.imageArtist.IsCircle = true;
            this.imageArtist.Location = new System.Drawing.Point(3, 3);
            this.imageArtist.Name = "imageArtist";
            this.imageArtist.Size = new System.Drawing.Size(124, 124);
            this.imageArtist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageArtist.TabIndex = 1;
            this.imageArtist.TabStop = false;
            this.imageArtist.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Circle;
            this.imageArtist.Click += new System.EventHandler(this.imageArtist_Click);
            // 
            // lbNameArtist
            // 
            this.lbNameArtist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNameArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameArtist.ForeColor = System.Drawing.Color.White;
            this.lbNameArtist.Location = new System.Drawing.Point(-25, 124);
            this.lbNameArtist.Name = "lbNameArtist";
            this.lbNameArtist.Size = new System.Drawing.Size(183, 26);
            this.lbNameArtist.TabIndex = 2;
            this.lbNameArtist.Text = "lbName";
            this.lbNameArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // avtkaraoke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lbNameArtist);
            this.Controls.Add(this.imageArtist);
            this.Name = "avtkaraoke";
            this.Size = new System.Drawing.Size(130, 150);
            ((System.ComponentModel.ISupportInitialize)(this.imageArtist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuPictureBox imageArtist;
        private System.Windows.Forms.Label lbNameArtist;
    }
}
