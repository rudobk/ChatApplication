namespace SocketChatApplicationClient
{
    partial class Form1
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
            this.GuiTin = new System.Windows.Forms.Button();
            this.TinNhan = new System.Windows.Forms.TextBox();
            this.KhungChat = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GuiTin
            // 
            this.GuiTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuiTin.Location = new System.Drawing.Point(404, 347);
            this.GuiTin.Margin = new System.Windows.Forms.Padding(2);
            this.GuiTin.Name = "GuiTin";
            this.GuiTin.Size = new System.Drawing.Size(96, 26);
            this.GuiTin.TabIndex = 25;
            this.GuiTin.Text = "Gửi";
            this.GuiTin.UseVisualStyleBackColor = true;
            this.GuiTin.Click += new System.EventHandler(this.GuiTin_Click);
            // 
            // TinNhan
            // 
            this.TinNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TinNhan.Location = new System.Drawing.Point(25, 347);
            this.TinNhan.Margin = new System.Windows.Forms.Padding(2);
            this.TinNhan.Name = "TinNhan";
            this.TinNhan.Size = new System.Drawing.Size(360, 26);
            this.TinNhan.TabIndex = 24;
            // 
            // KhungChat
            // 
            this.KhungChat.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KhungChat.FormattingEnabled = true;
            this.KhungChat.HorizontalScrollbar = true;
            this.KhungChat.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KhungChat.ItemHeight = 16;
            this.KhungChat.Location = new System.Drawing.Point(11, 8);
            this.KhungChat.Margin = new System.Windows.Forms.Padding(2);
            this.KhungChat.Name = "KhungChat";
            this.KhungChat.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.KhungChat.Size = new System.Drawing.Size(489, 292);
            this.KhungChat.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 37);
            this.button1.TabIndex = 26;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.GuiTin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 381);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GuiTin);
            this.Controls.Add(this.TinNhan);
            this.Controls.Add(this.KhungChat);
            this.Name = "Form1";
            this.Text = "Khung Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GuiTin;
        private System.Windows.Forms.TextBox TinNhan;
        private System.Windows.Forms.ListBox KhungChat;
        private System.Windows.Forms.Button button1;
    }
}

