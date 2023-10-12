namespace SocketChatApplicationClient
{
    partial class Register
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
            this.RegisterBT = new System.Windows.Forms.Button();
            this.RePasswdReg = new System.Windows.Forms.TextBox();
            this.PasswdReg = new System.Windows.Forms.TextBox();
            this.UsernameReg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RegisterBT
            // 
            this.RegisterBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterBT.Location = new System.Drawing.Point(195, 217);
            this.RegisterBT.Margin = new System.Windows.Forms.Padding(2);
            this.RegisterBT.Name = "RegisterBT";
            this.RegisterBT.Size = new System.Drawing.Size(96, 40);
            this.RegisterBT.TabIndex = 36;
            this.RegisterBT.Text = "Đăng Kí";
            this.RegisterBT.UseVisualStyleBackColor = true;
            this.RegisterBT.Click += new System.EventHandler(this.RegisterBT_Click);
            // 
            // RePasswdReg
            // 
            this.RePasswdReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RePasswdReg.Location = new System.Drawing.Point(174, 178);
            this.RePasswdReg.Margin = new System.Windows.Forms.Padding(2);
            this.RePasswdReg.Name = "RePasswdReg";
            this.RePasswdReg.Size = new System.Drawing.Size(136, 24);
            this.RePasswdReg.TabIndex = 35;
            // 
            // PasswdReg
            // 
            this.PasswdReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswdReg.Location = new System.Drawing.Point(174, 124);
            this.PasswdReg.Margin = new System.Windows.Forms.Padding(2);
            this.PasswdReg.Name = "PasswdReg";
            this.PasswdReg.Size = new System.Drawing.Size(136, 24);
            this.PasswdReg.TabIndex = 34;
            // 
            // UsernameReg
            // 
            this.UsernameReg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameReg.Location = new System.Drawing.Point(174, 71);
            this.UsernameReg.Margin = new System.Windows.Forms.Padding(2);
            this.UsernameReg.Name = "UsernameReg";
            this.UsernameReg.Size = new System.Drawing.Size(136, 24);
            this.UsernameReg.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(191, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 37;
            this.label1.Text = "Tên tài khoản";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(203, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 38;
            this.label2.Text = "Mật khẩu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(174, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "Nhập lại mật khẩu";
            // 
            // Register
            // 
            this.AcceptButton = this.RegisterBT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 324);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RegisterBT);
            this.Controls.Add(this.RePasswdReg);
            this.Controls.Add(this.PasswdReg);
            this.Controls.Add(this.UsernameReg);
            this.Name = "Register";
            this.Text = "Đăng Kí Tài Khoản";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Register_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RegisterBT;
        private System.Windows.Forms.TextBox RePasswdReg;
        private System.Windows.Forms.TextBox PasswdReg;
        private System.Windows.Forms.TextBox UsernameReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}