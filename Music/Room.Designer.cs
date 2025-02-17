namespace Music
{
    partial class Room
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Room));
            this.listmember = new System.Windows.Forms.FlowLayoutPanel();
            this.player = new AxWMPLib.AxWindowsMediaPlayer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listTrack = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btChat = new System.Windows.Forms.Button();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.lbRoomID = new System.Windows.Forms.Label();
            this.rtbKhungChat = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listmember
            // 
            this.listmember.Location = new System.Drawing.Point(3, 594);
            this.listmember.Name = "listmember";
            this.listmember.Size = new System.Drawing.Size(946, 179);
            this.listmember.TabIndex = 176;
            // 
            // player
            // 
            this.player.Enabled = true;
            this.player.Location = new System.Drawing.Point(8, 3);
            this.player.Name = "player";
            this.player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player.OcxState")));
            this.player.Size = new System.Drawing.Size(944, 382);
            this.player.TabIndex = 177;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 25);
            this.label1.TabIndex = 179;
            this.label1.Text = "Danh sách các bài hát ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 618);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 180;
            // 
            // listTrack
            // 
            this.listTrack.BackColor = System.Drawing.Color.Gray;
            this.listTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listTrack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.listTrack.FormattingEnabled = true;
            this.listTrack.ItemHeight = 25;
            this.listTrack.Location = new System.Drawing.Point(3, 31);
            this.listTrack.Name = "listTrack";
            this.listTrack.Size = new System.Drawing.Size(952, 179);
            this.listTrack.TabIndex = 181;
            this.listTrack.SelectedIndexChanged += new System.EventHandler(this.listTrack_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btChat);
            this.panel1.Controls.Add(this.rtbMessage);
            this.panel1.Controls.Add(this.lbRoomID);
            this.panel1.Controls.Add(this.rtbKhungChat);
            this.panel1.Controls.Add(this.player);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 388);
            this.panel1.TabIndex = 182;
            // 
            // btChat
            // 
            this.btChat.Location = new System.Drawing.Point(75, 356);
            this.btChat.Name = "btChat";
            this.btChat.Size = new System.Drawing.Size(113, 26);
            this.btChat.TabIndex = 186;
            this.btChat.Text = "Chat";
            this.btChat.UseVisualStyleBackColor = true;
            this.btChat.Click += new System.EventHandler(this.btChat_Click);
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(8, 324);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(215, 26);
            this.rtbMessage.TabIndex = 185;
            this.rtbMessage.Text = "";
            // 
            // lbRoomID
            // 
            this.lbRoomID.AutoSize = true;
            this.lbRoomID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoomID.ForeColor = System.Drawing.Color.White;
            this.lbRoomID.Location = new System.Drawing.Point(3, 0);
            this.lbRoomID.Name = "lbRoomID";
            this.lbRoomID.Size = new System.Drawing.Size(138, 32);
            this.lbRoomID.TabIndex = 180;
            this.lbRoomID.Text = "ID Room: \r\n";
            // 
            // rtbKhungChat
            // 
            this.rtbKhungChat.Location = new System.Drawing.Point(9, 50);
            this.rtbKhungChat.Name = "rtbKhungChat";
            this.rtbKhungChat.ReadOnly = true;
            this.rtbKhungChat.Size = new System.Drawing.Size(214, 268);
            this.rtbKhungChat.TabIndex = 184;
            this.rtbKhungChat.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.listTrack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 388);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(952, 208);
            this.panel2.TabIndex = 183;
            // 
            // Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listmember);
            this.Name = "Room";
            this.Size = new System.Drawing.Size(952, 773);
            this.Load += new System.EventHandler(this.Room_Load);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel listmember;
        private AxWMPLib.AxWindowsMediaPlayer player;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listTrack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbRoomID;
        private System.Windows.Forms.RichTextBox rtbKhungChat;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.Button btChat;
    }
}
