namespace StallionSuppyChain.Reports
{
    partial class InventoryReportGenerator
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbModules = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboCategory1 = new System.Windows.Forms.ComboBox();
            this.chkIsFixedAsset = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboCostCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboCategory2 = new System.Windows.Forms.ComboBox();
            this.TXTITEMCODE = new System.Windows.Forms.TextBox();
            this.cboCategory3 = new System.Windows.Forms.ComboBox();
            this.cboProjectCode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtNeededfrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.cmbType);
            this.groupBox2.Controls.Add(this.cmbModules);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(623, 106);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report Type";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(197, 74);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 17);
            this.checkBox1.TabIndex = 69;
            this.checkBox1.Text = "No Filter";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(197, 47);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(329, 21);
            this.cmbType.TabIndex = 57;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbModules
            // 
            this.cmbModules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModules.FormattingEnabled = true;
            this.cmbModules.Location = new System.Drawing.Point(197, 20);
            this.cmbModules.Name = "cmbModules";
            this.cmbModules.Size = new System.Drawing.Size(329, 21);
            this.cmbModules.TabIndex = 56;
            this.cmbModules.SelectedIndexChanged += new System.EventHandler(this.cmbModules_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Type :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Module :";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox4.Controls.Add(this.cboCategory1);
            this.groupBox4.Controls.Add(this.chkIsFixedAsset);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cboCostCode);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.cboCategory2);
            this.groupBox4.Controls.Add(this.TXTITEMCODE);
            this.groupBox4.Controls.Add(this.cboCategory3);
            this.groupBox4.Controls.Add(this.cboProjectCode);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 106);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(623, 216);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parameters";
            // 
            // cboCategory1
            // 
            this.cboCategory1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboCategory1.FormattingEnabled = true;
            this.cboCategory1.Location = new System.Drawing.Point(197, 47);
            this.cboCategory1.Name = "cboCategory1";
            this.cboCategory1.Size = new System.Drawing.Size(238, 21);
            this.cboCategory1.TabIndex = 66;
            this.cboCategory1.SelectedIndexChanged += new System.EventHandler(this.cboCategory1_SelectedIndexChanged);
            // 
            // chkIsFixedAsset
            // 
            this.chkIsFixedAsset.AutoSize = true;
            this.chkIsFixedAsset.Location = new System.Drawing.Point(197, 182);
            this.chkIsFixedAsset.Name = "chkIsFixedAsset";
            this.chkIsFixedAsset.Size = new System.Drawing.Size(100, 17);
            this.chkIsFixedAsset.TabIndex = 68;
            this.chkIsFixedAsset.Text = "For Purchase";
            this.chkIsFixedAsset.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(107, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 69;
            this.label9.Text = "Category 1  :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(107, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 70;
            this.label8.Text = "Category 2  :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(107, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 71;
            this.label7.Text = "Category 3  :";
            // 
            // cboCostCode
            // 
            this.cboCostCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCostCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboCostCode.FormattingEnabled = true;
            this.cboCostCode.Location = new System.Drawing.Point(197, 155);
            this.cboCostCode.Name = "cboCostCode";
            this.cboCostCode.Size = new System.Drawing.Size(238, 21);
            this.cboCostCode.TabIndex = 66;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(114, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 73;
            this.label5.Text = "Item Code :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(114, 163);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 67;
            this.label15.Text = "Cost Code :";
            // 
            // cboCategory2
            // 
            this.cboCategory2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboCategory2.FormattingEnabled = true;
            this.cboCategory2.Location = new System.Drawing.Point(197, 74);
            this.cboCategory2.Name = "cboCategory2";
            this.cboCategory2.Size = new System.Drawing.Size(238, 21);
            this.cboCategory2.TabIndex = 67;
            this.cboCategory2.SelectedIndexChanged += new System.EventHandler(this.cboCategory2_SelectedIndexChanged);
            // 
            // TXTITEMCODE
            // 
            this.TXTITEMCODE.BackColor = System.Drawing.Color.White;
            this.TXTITEMCODE.ForeColor = System.Drawing.Color.Black;
            this.TXTITEMCODE.Location = new System.Drawing.Point(197, 128);
            this.TXTITEMCODE.Name = "TXTITEMCODE";
            this.TXTITEMCODE.Size = new System.Drawing.Size(134, 21);
            this.TXTITEMCODE.TabIndex = 72;
            // 
            // cboCategory3
            // 
            this.cboCategory3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboCategory3.FormattingEnabled = true;
            this.cboCategory3.Location = new System.Drawing.Point(197, 101);
            this.cboCategory3.Name = "cboCategory3";
            this.cboCategory3.Size = new System.Drawing.Size(238, 21);
            this.cboCategory3.TabIndex = 68;
            this.cboCategory3.SelectedIndexChanged += new System.EventHandler(this.cboCategory3_SelectedIndexChanged);
            // 
            // cboProjectCode
            // 
            this.cboProjectCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProjectCode.FormattingEnabled = true;
            this.cboProjectCode.Location = new System.Drawing.Point(197, 20);
            this.cboProjectCode.Name = "cboProjectCode";
            this.cboProjectCode.Size = new System.Drawing.Size(238, 21);
            this.cboProjectCode.TabIndex = 56;
            this.cboProjectCode.SelectedIndexChanged += new System.EventHandler(this.cboProjectCode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Project :";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.txtNeededfrom);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 322);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(623, 93);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Date Coverage";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(360, 40);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(132, 21);
            this.dateTimePicker1.TabIndex = 51;
            this.dateTimePicker1.Value = new System.DateTime(2016, 5, 29, 15, 27, 34, 0);
            // 
            // txtNeededfrom
            // 
            this.txtNeededfrom.Checked = false;
            this.txtNeededfrom.CustomFormat = "dd/MM/yyyy";
            this.txtNeededfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtNeededfrom.Location = new System.Drawing.Point(181, 40);
            this.txtNeededfrom.Name = "txtNeededfrom";
            this.txtNeededfrom.ShowCheckBox = true;
            this.txtNeededfrom.Size = new System.Drawing.Size(132, 21);
            this.txtNeededfrom.TabIndex = 50;
            this.txtNeededfrom.Value = new System.DateTime(2016, 5, 29, 15, 27, 34, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(325, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "From :";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(409, 421);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(187, 37);
            this.btnSearch.TabIndex = 44;
            this.btnSearch.Text = "Generate Report";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StallionSuppyChain.Properties.Resources.Print_48;
            this.pictureBox1.Location = new System.Drawing.Point(25, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 55);
            this.pictureBox1.TabIndex = 70;
            this.pictureBox1.TabStop = false;
            // 
            // InventoryReportGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(623, 483);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InventoryReportGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Generator";
            this.Load += new System.EventHandler(this.InventoryReportGenerator_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbModules;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboProjectCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboCostCode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkIsFixedAsset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker txtNeededfrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboCategory1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCategory2;
        private System.Windows.Forms.TextBox TXTITEMCODE;
        private System.Windows.Forms.ComboBox cboCategory3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}