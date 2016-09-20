namespace StallionSuppyChain.Admin
{
    partial class UserMaintainance
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.BtnNew = new System.Windows.Forms.Button();
            this.cboWorker = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.TxtDefaultPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TxtEmailAddress = new System.Windows.Forms.TextBox();
            this.TxtFirstName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtMiddleName = new System.Windows.Forms.TextBox();
            this.TxtLastName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TxtUserLevel = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboModuleID = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ModuleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserLevelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.BtnNew);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 79);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::StallionSuppyChain.Properties.Resources.Create_New_40;
            this.btnSave.Location = new System.Drawing.Point(77, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 60);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = " &Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNew.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.Image = global::StallionSuppyChain.Properties.Resources.Add_List_48;
            this.BtnNew.Location = new System.Drawing.Point(3, 16);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(74, 60);
            this.BtnNew.TabIndex = 0;
            this.BtnNew.Text = "&New";
            this.BtnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnNew.UseVisualStyleBackColor = true;
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // cboWorker
            // 
            this.cboWorker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWorker.Enabled = false;
            this.cboWorker.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboWorker.FormattingEnabled = true;
            this.cboWorker.Location = new System.Drawing.Point(130, 19);
            this.cboWorker.Name = "cboWorker";
            this.cboWorker.Size = new System.Drawing.Size(198, 21);
            this.cboWorker.TabIndex = 24;
            this.cboWorker.SelectedIndexChanged += new System.EventHandler(this.cboWorker_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(65, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Worker :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "User Name :";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Location = new System.Drawing.Point(132, 19);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(198, 20);
            this.TxtUserName.TabIndex = 26;
            // 
            // TxtDefaultPassword
            // 
            this.TxtDefaultPassword.Enabled = false;
            this.TxtDefaultPassword.Location = new System.Drawing.Point(132, 45);
            this.TxtDefaultPassword.Name = "TxtDefaultPassword";
            this.TxtDefaultPassword.Size = new System.Drawing.Size(198, 20);
            this.TxtDefaultPassword.TabIndex = 28;
            this.TxtDefaultPassword.Text = "SCMUSER";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Default Password :";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(130, 125);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 25);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "&Add";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cboWorker);
            this.groupBox3.Controls.Add(this.TxtEmailAddress);
            this.groupBox3.Controls.Add(this.TxtFirstName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.TxtMiddleName);
            this.groupBox3.Controls.Add(this.TxtLastName);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(6, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(620, 158);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " ";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Enabled = false;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.Location = new System.Drawing.Point(334, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(164, 25);
            this.button3.TabIndex = 36;
            this.button3.Text = "&Add New Worker";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(334, 129);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 17);
            this.checkBox1.TabIndex = 35;
            this.checkBox1.Text = "Is Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TxtEmailAddress
            // 
            this.TxtEmailAddress.Enabled = false;
            this.TxtEmailAddress.Location = new System.Drawing.Point(130, 123);
            this.TxtEmailAddress.Name = "TxtEmailAddress";
            this.TxtEmailAddress.Size = new System.Drawing.Size(198, 20);
            this.TxtEmailAddress.TabIndex = 32;
            // 
            // TxtFirstName
            // 
            this.TxtFirstName.Enabled = false;
            this.TxtFirstName.Location = new System.Drawing.Point(130, 45);
            this.TxtFirstName.Name = "TxtFirstName";
            this.TxtFirstName.Size = new System.Drawing.Size(198, 20);
            this.TxtFirstName.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Email Address :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(45, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Last Name :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "First Name :";
            // 
            // TxtMiddleName
            // 
            this.TxtMiddleName.Enabled = false;
            this.TxtMiddleName.Location = new System.Drawing.Point(130, 97);
            this.TxtMiddleName.Name = "TxtMiddleName";
            this.TxtMiddleName.Size = new System.Drawing.Size(198, 20);
            this.TxtMiddleName.TabIndex = 30;
            // 
            // TxtLastName
            // 
            this.TxtLastName.Enabled = false;
            this.TxtLastName.Location = new System.Drawing.Point(130, 71);
            this.TxtLastName.Name = "TxtLastName";
            this.TxtLastName.Size = new System.Drawing.Size(198, 20);
            this.TxtLastName.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Middle Name :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TxtUserLevel);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.TxtDefaultPassword);
            this.groupBox4.Controls.Add(this.cboModuleID);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.TxtUserName);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(6, 248);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(620, 395);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " ";
            // 
            // TxtUserLevel
            // 
            this.TxtUserLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TxtUserLevel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.TxtUserLevel.FormattingEnabled = true;
            this.TxtUserLevel.Location = new System.Drawing.Point(130, 98);
            this.TxtUserLevel.Name = "TxtUserLevel";
            this.TxtUserLevel.Size = new System.Drawing.Size(198, 21);
            this.TxtUserLevel.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(46, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "User Level :";
            // 
            // cboModuleID
            // 
            this.cboModuleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModuleID.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboModuleID.FormattingEnabled = true;
            this.cboModuleID.Location = new System.Drawing.Point(130, 71);
            this.cboModuleID.Name = "cboModuleID";
            this.cboModuleID.Size = new System.Drawing.Size(198, 21);
            this.cboModuleID.TabIndex = 26;
            this.cboModuleID.SelectedIndexChanged += new System.EventHandler(this.cboModuleID_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Remove,
            this.ModuleID,
            this.ModuleName,
            this.UserLevelID,
            this.UserLevel});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(14, 156);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(588, 224);
            this.dataGridView1.TabIndex = 34;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Remove
            // 
            this.Remove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remove.HeaderText = "Remove";
            this.Remove.MinimumWidth = 100;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            this.Remove.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ModuleID
            // 
            this.ModuleID.HeaderText = "Module ID";
            this.ModuleID.Name = "ModuleID";
            this.ModuleID.ReadOnly = true;
            // 
            // ModuleName
            // 
            this.ModuleName.HeaderText = "Module Name";
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.ReadOnly = true;
            // 
            // UserLevelID
            // 
            this.UserLevelID.HeaderText = "User Level ID";
            this.UserLevelID.Name = "UserLevelID";
            this.UserLevelID.ReadOnly = true;
            // 
            // UserLevel
            // 
            this.UserLevel.HeaderText = "User Level";
            this.UserLevel.Name = "UserLevel";
            this.UserLevel.ReadOnly = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(68, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Module :";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(653, 689);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(645, 663);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // UserMaintainance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(653, 689);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserMaintainance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Maintainance";
            this.Load += new System.EventHandler(this.UserMaintainance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button BtnNew;
        private System.Windows.Forms.ComboBox cboWorker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.TextBox TxtDefaultPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TxtLastName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtFirstName;
        private System.Windows.Forms.TextBox TxtMiddleName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtEmailAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboModuleID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox TxtUserLevel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewButtonColumn Remove;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserLevelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserLevel;
    }
}