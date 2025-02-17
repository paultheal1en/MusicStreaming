namespace Music
{
    partial class AddTrack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTrack));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.bunifuColorTransition1 = new Bunifu.UI.WinForms.BunifuColorTransition(this.components);
            this.btSavePlaylist = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.tbNamePlaylist = new Bunifu.UI.WinForms.BunifuTextBox();
            this.bunifuPictureBox2 = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuColorTransition1
            // 
            this.bunifuColorTransition1.AutoTransition = false;
            this.bunifuColorTransition1.ColorArray = new System.Drawing.Color[] {
        System.Drawing.Color.Purple,
        System.Drawing.Color.LightBlue,
        System.Drawing.Color.Orange};
            this.bunifuColorTransition1.EndColor = System.Drawing.Color.White;
            this.bunifuColorTransition1.Interval = 10;
            this.bunifuColorTransition1.ProgessValue = 0;
            this.bunifuColorTransition1.StartColor = System.Drawing.Color.White;
            this.bunifuColorTransition1.TransitionControl = null;
            // 
            // btSavePlaylist
            // 
            this.btSavePlaylist.AllowAnimations = true;
            this.btSavePlaylist.AllowMouseEffects = true;
            this.btSavePlaylist.AllowToggling = false;
            this.btSavePlaylist.AnimationSpeed = 200;
            this.btSavePlaylist.AutoGenerateColors = false;
            this.btSavePlaylist.AutoRoundBorders = false;
            this.btSavePlaylist.AutoSizeLeftIcon = true;
            this.btSavePlaylist.AutoSizeRightIcon = true;
            this.btSavePlaylist.BackColor = System.Drawing.Color.Transparent;
            this.btSavePlaylist.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btSavePlaylist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSavePlaylist.BackgroundImage")));
            this.btSavePlaylist.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSavePlaylist.ButtonText = "Save";
            this.btSavePlaylist.ButtonTextMarginLeft = 0;
            this.btSavePlaylist.ColorContrastOnClick = 45;
            this.btSavePlaylist.ColorContrastOnHover = 45;
            this.btSavePlaylist.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btSavePlaylist.CustomizableEdges = borderEdges1;
            this.btSavePlaylist.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btSavePlaylist.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btSavePlaylist.DisabledFillColor = System.Drawing.Color.Empty;
            this.btSavePlaylist.DisabledForecolor = System.Drawing.Color.Empty;
            this.btSavePlaylist.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btSavePlaylist.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btSavePlaylist.ForeColor = System.Drawing.Color.White;
            this.btSavePlaylist.IconLeft = null;
            this.btSavePlaylist.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSavePlaylist.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btSavePlaylist.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btSavePlaylist.IconMarginLeft = 11;
            this.btSavePlaylist.IconPadding = 10;
            this.btSavePlaylist.IconRight = null;
            this.btSavePlaylist.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSavePlaylist.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btSavePlaylist.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btSavePlaylist.IconSize = 25;
            this.btSavePlaylist.IdleBorderColor = System.Drawing.Color.Empty;
            this.btSavePlaylist.IdleBorderRadius = 0;
            this.btSavePlaylist.IdleBorderThickness = 0;
            this.btSavePlaylist.IdleFillColor = System.Drawing.Color.Empty;
            this.btSavePlaylist.IdleIconLeftImage = null;
            this.btSavePlaylist.IdleIconRightImage = null;
            this.btSavePlaylist.IndicateFocus = false;
            this.btSavePlaylist.Location = new System.Drawing.Point(275, 204);
            this.btSavePlaylist.Name = "btSavePlaylist";
            this.btSavePlaylist.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.btSavePlaylist.OnDisabledState.BorderRadius = 1;
            this.btSavePlaylist.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSavePlaylist.OnDisabledState.BorderThickness = 1;
            this.btSavePlaylist.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btSavePlaylist.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btSavePlaylist.OnDisabledState.IconLeftImage = null;
            this.btSavePlaylist.OnDisabledState.IconRightImage = null;
            this.btSavePlaylist.onHoverState.BorderColor = System.Drawing.Color.Green;
            this.btSavePlaylist.onHoverState.BorderRadius = 1;
            this.btSavePlaylist.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSavePlaylist.onHoverState.BorderThickness = 1;
            this.btSavePlaylist.onHoverState.FillColor = System.Drawing.Color.Green;
            this.btSavePlaylist.onHoverState.ForeColor = System.Drawing.Color.White;
            this.btSavePlaylist.onHoverState.IconLeftImage = null;
            this.btSavePlaylist.onHoverState.IconRightImage = null;
            this.btSavePlaylist.OnIdleState.BorderColor = System.Drawing.Color.Green;
            this.btSavePlaylist.OnIdleState.BorderRadius = 1;
            this.btSavePlaylist.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSavePlaylist.OnIdleState.BorderThickness = 1;
            this.btSavePlaylist.OnIdleState.FillColor = System.Drawing.Color.Green;
            this.btSavePlaylist.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.btSavePlaylist.OnIdleState.IconLeftImage = null;
            this.btSavePlaylist.OnIdleState.IconRightImage = null;
            this.btSavePlaylist.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btSavePlaylist.OnPressedState.BorderRadius = 1;
            this.btSavePlaylist.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btSavePlaylist.OnPressedState.BorderThickness = 1;
            this.btSavePlaylist.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.btSavePlaylist.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.btSavePlaylist.OnPressedState.IconLeftImage = null;
            this.btSavePlaylist.OnPressedState.IconRightImage = null;
            this.btSavePlaylist.Size = new System.Drawing.Size(142, 36);
            this.btSavePlaylist.TabIndex = 3;
            this.btSavePlaylist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btSavePlaylist.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.btSavePlaylist.TextMarginLeft = 0;
            this.btSavePlaylist.TextPadding = new System.Windows.Forms.Padding(0);
            this.btSavePlaylist.UseDefaultRadiusAndThickness = true;
            this.btSavePlaylist.Click += new System.EventHandler(this.btSavePlaylist_Click);
            // 
            // tbNamePlaylist
            // 
            this.tbNamePlaylist.AcceptsReturn = false;
            this.tbNamePlaylist.AcceptsTab = false;
            this.tbNamePlaylist.AnimationSpeed = 200;
            this.tbNamePlaylist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.tbNamePlaylist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.tbNamePlaylist.AutoSizeHeight = true;
            this.tbNamePlaylist.BackColor = System.Drawing.Color.Transparent;
            this.tbNamePlaylist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbNamePlaylist.BackgroundImage")));
            this.tbNamePlaylist.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.tbNamePlaylist.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.tbNamePlaylist.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.tbNamePlaylist.BorderColorIdle = System.Drawing.Color.Silver;
            this.tbNamePlaylist.BorderRadius = 1;
            this.tbNamePlaylist.BorderThickness = 1;
            this.tbNamePlaylist.CharacterCase = Bunifu.UI.WinForms.BunifuTextBox.CharacterCases.Normal;
            this.tbNamePlaylist.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.tbNamePlaylist.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbNamePlaylist.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.tbNamePlaylist.DefaultText = "";
            this.tbNamePlaylist.FillColor = System.Drawing.Color.White;
            this.tbNamePlaylist.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.tbNamePlaylist.HideSelection = true;
            this.tbNamePlaylist.IconLeft = null;
            this.tbNamePlaylist.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.tbNamePlaylist.IconPadding = 10;
            this.tbNamePlaylist.IconRight = null;
            this.tbNamePlaylist.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.tbNamePlaylist.Lines = new string[0];
            this.tbNamePlaylist.Location = new System.Drawing.Point(157, 111);
            this.tbNamePlaylist.MaxLength = 32767;
            this.tbNamePlaylist.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbNamePlaylist.Modified = false;
            this.tbNamePlaylist.Multiline = false;
            this.tbNamePlaylist.Name = "tbNamePlaylist";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbNamePlaylist.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.tbNamePlaylist.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbNamePlaylist.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.DarkOliveGreen;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.tbNamePlaylist.OnIdleState = stateProperties4;
            this.tbNamePlaylist.Padding = new System.Windows.Forms.Padding(3);
            this.tbNamePlaylist.PasswordChar = '\0';
            this.tbNamePlaylist.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.tbNamePlaylist.PlaceholderText = "Nhập tên Playlist";
            this.tbNamePlaylist.ReadOnly = false;
            this.tbNamePlaylist.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbNamePlaylist.SelectedText = "";
            this.tbNamePlaylist.SelectionLength = 0;
            this.tbNamePlaylist.SelectionStart = 0;
            this.tbNamePlaylist.ShortcutsEnabled = true;
            this.tbNamePlaylist.Size = new System.Drawing.Size(260, 43);
            this.tbNamePlaylist.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.tbNamePlaylist.TabIndex = 2;
            this.tbNamePlaylist.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbNamePlaylist.TextMarginBottom = 0;
            this.tbNamePlaylist.TextMarginLeft = 3;
            this.tbNamePlaylist.TextMarginTop = 1;
            this.tbNamePlaylist.TextPlaceholder = "Nhập tên Playlist";
            this.tbNamePlaylist.UseSystemPasswordChar = false;
            this.tbNamePlaylist.WordWrap = true;
            // 
            // bunifuPictureBox2
            // 
            this.bunifuPictureBox2.AllowFocused = false;
            this.bunifuPictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bunifuPictureBox2.AutoSizeHeight = true;
            this.bunifuPictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bunifuPictureBox2.BorderRadius = 0;
            this.bunifuPictureBox2.Image = global::Music.Properties.Resources.icons8_playlist_100;
            this.bunifuPictureBox2.IsCircle = true;
            this.bunifuPictureBox2.Location = new System.Drawing.Point(22, 86);
            this.bunifuPictureBox2.Name = "bunifuPictureBox2";
            this.bunifuPictureBox2.Size = new System.Drawing.Size(107, 107);
            this.bunifuPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuPictureBox2.TabIndex = 1;
            this.bunifuPictureBox2.TabStop = false;
            this.bunifuPictureBox2.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Square;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // AddTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(446, 252);
            this.Controls.Add(this.btSavePlaylist);
            this.Controls.Add(this.tbNamePlaylist);
            this.Controls.Add(this.bunifuPictureBox2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddTrack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Track";
            this.Load += new System.EventHandler(this.AddTrack_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuPictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuPictureBox bunifuPictureBox2;
        private Bunifu.UI.WinForms.BunifuColorTransition bunifuColorTransition1;
        private Bunifu.UI.WinForms.BunifuTextBox tbNamePlaylist;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btSavePlaylist;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
