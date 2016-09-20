namespace StallionSuppyChain.Admin
{
    partial class UpdatePassword
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
            this.TxtDefaultPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtPasswordConfirm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.TxtUserID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TxtDefaultPassword
            // 
            this.TxtDefaultPassword.Enabled = false;
            this.TxtDefaultPassword.Location = new System.Drawing.Point(137, 12);
            this.TxtDefaultPassword.Name = "TxtDefaultPassword";
            this.TxtDefaultPassword.Size = new System.Drawing.Size(198, 20);
            this.TxtDefaultPassword.TabIndex = 30;
            this.TxtDefaultPassword.Text = "SCMUSER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Default Password :";
            // 
            // TxtPassword
            // 
            this.TxtPassword.BackColor = System.Drawing.Color.White;
            this.TxtPassword.ForeColor = System.Drawing.Color.Black;
            this.TxtPassword.Location = new System.Drawing.Point(137, 38);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(198, 20);
            this.TxtPassword.TabIndex = 31;
            this.TxtPassword.UseSystemPasswordChar = true;
            // 
            // TxtPasswordConfirm
            // 
            this.TxtPasswordConfirm.BackColor = System.Drawing.Color.White;
            this.TxtPasswordConfirm.ForeColor = System.Drawing.Color.Black;
            this.TxtPasswordConfirm.Location = new System.Drawing.Point(137, 64);
            this.TxtPasswordConfirm.Name = "TxtPasswordConfirm";
            this.TxtPasswordConfirm.Size = new System.Drawing.Size(198, 20);
            this.TxtPasswordConfirm.TabIndex = 32;
            this.TxtPasswordConfirm.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Confirm Password :";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(137, 90);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(198, 25);
            this.btnSearch.TabIndex = 35;
            this.btnSearch.Text = "&Update";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // TxtUserID
            // 
            this.TxtUserID.Enabled = false;
            this.TxtUserID.Location = new System.Drawing.Point(23, 137);
            this.TxtUserID.Name = "TxtUserID";
            this.TxtUserID.Size = new System.Drawing.Size(198, 20);
            this.TxtUserID.TabIndex = 36;
            this.TxtUserID.Visible = false;
            // 
            // UpdatePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(345, 129);
            this.ControlBox = false;
            this.Controls.Add(this.TxtUserID);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtPasswordConfirm);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtDefaultPassword);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UpdatePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Password";
            this.Load += new System.EventHandler(this.UpdatePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtDefaultPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.TextBox TxtPasswordConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox TxtUserID;
    }
}