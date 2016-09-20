namespace StallionSuppyChain
{
    partial class SCMLogIN
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
            this.txtuserName = new System.Windows.Forms.TextBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.btnLogIN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtuserName
            // 
            this.txtuserName.BackColor = System.Drawing.Color.White;
            this.txtuserName.ForeColor = System.Drawing.Color.Black;
            this.txtuserName.Location = new System.Drawing.Point(130, 193);
            this.txtuserName.Name = "txtuserName";
            this.txtuserName.Size = new System.Drawing.Size(134, 20);
            this.txtuserName.TabIndex = 7;
            this.txtuserName.TextChanged += new System.EventHandler(this.TXTITEMCODE_TextChanged);
            // 
            // TxtPassword
            // 
            this.TxtPassword.BackColor = System.Drawing.Color.White;
            this.TxtPassword.ForeColor = System.Drawing.Color.Black;
            this.TxtPassword.Location = new System.Drawing.Point(129, 229);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(134, 20);
            this.TxtPassword.TabIndex = 8;
            this.TxtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogIN
            // 
            this.btnLogIN.BackColor = System.Drawing.Color.White;
            this.btnLogIN.BackgroundImage = global::StallionSuppyChain.Properties.Resources.login1;
            this.btnLogIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogIN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogIN.Location = new System.Drawing.Point(346, 196);
            this.btnLogIN.Name = "btnLogIN";
            this.btnLogIN.Size = new System.Drawing.Size(94, 84);
            this.btnLogIN.TabIndex = 9;
            this.btnLogIN.UseVisualStyleBackColor = false;
            this.btnLogIN.Click += new System.EventHandler(this.btnLogIN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::StallionSuppyChain.Properties.Resources.login_copy;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(619, 471);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SCMLogIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(619, 471);
            this.Controls.Add(this.btnLogIN);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.txtuserName);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SCMLogIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCM";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtuserName;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Button btnLogIN;
    }
}