namespace StallionSuppyChain.Reports
{
    partial class PrintBarcode
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnShowBarcodes = new System.Windows.Forms.Button();
            this.txtNoOfCopy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1254, 533);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnShowBarcodes);
            this.panel1.Controls.Add(this.txtNoOfCopy);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtProductCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1254, 52);
            this.panel1.TabIndex = 1;
            // 
            // btnShowBarcodes
            // 
            this.btnShowBarcodes.Location = new System.Drawing.Point(416, 14);
            this.btnShowBarcodes.Name = "btnShowBarcodes";
            this.btnShowBarcodes.Size = new System.Drawing.Size(75, 23);
            this.btnShowBarcodes.TabIndex = 4;
            this.btnShowBarcodes.Text = "Show";
            this.btnShowBarcodes.UseVisualStyleBackColor = true;
            this.btnShowBarcodes.Click += new System.EventHandler(this.btnShowBarcodes_Click);
            // 
            // txtNoOfCopy
            // 
            this.txtNoOfCopy.Location = new System.Drawing.Point(299, 16);
            this.txtNoOfCopy.Name = "txtNoOfCopy";
            this.txtNoOfCopy.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfCopy.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "No. of copy:";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(93, 16);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(100, 20);
            this.txtProductCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Code:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1254, 533);
            this.panel2.TabIndex = 0;
            // 
            // PrintBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 585);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PrintBarcode";
            this.Text = "PrintBarcode";
            this.Load += new System.EventHandler(this.PrintBarcode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnShowBarcodes;
        private System.Windows.Forms.TextBox txtNoOfCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label1;
    }
}