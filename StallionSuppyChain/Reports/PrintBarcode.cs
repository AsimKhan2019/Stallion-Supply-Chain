using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace StallionSuppyChain.Reports
{
    public partial class PrintBarcode : Form
    {
        ReportDocument crystal = new ReportDocument();
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public List<Int32> ProductCode { get; set; }

        public PrintBarcode()
        {
            InitializeComponent();
        }

        private void PrintBarcode_Load(object sender, EventArgs e)
        {
            crystal.Load("Reports\\Barcode.rpt");
        }

        private void btnShowBarcodes_Click(object sender, EventArgs e)
        {
            if (chkIndividualPrinting.Checked)
                PreviewIndividualBarcode();
            else
                PreviewListOfBarcodes();

        }

        private void PreviewIndividualBarcode()
        {
            if (txtProductCode.Text == "")
            {
                MessageBox.Show("Please fill a valid Product Code to preview barcode.", "Barcode Preview");
                return;
            }

            string sql = "SELECT * FROM MSTR_Products WHERE ProductId=@ProductId";
            for (int i = 1; i < int.Parse(txtNoOfCopy.Text); i++)
            {
                sql += " UNION ALL SELECT * FROM MSTR_Products WHERE ProductId=@ProductId";
            }

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductId", txtProductCode.Text);
                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds, "MSTR_Products");
                            crystal.SetDataSource(ds);
                            crystalReportViewer1.ReportSource = crystal;
                        }
                    }
                }
            }
        }

        private void PreviewListOfBarcodes()
        {
            string sql = "";
            var index = 0;
            List<SqlParameter> param = new List<SqlParameter>();
            foreach (var p in ProductCode)
            {
                sql += " SELECT * FROM MSTR_Products WHERE ProductId=@ProductId" + index;
                for (int i = 1; i < int.Parse(txtNoOfCopy.Text); i++)
                {
                    sql += " UNION ALL SELECT * FROM MSTR_Products WHERE ProductId=@ProductId" + index + " ";
                }

                param.Add(new SqlParameter("@ProductId" + index, (object)p));

                index++;
                sql += " UNION ALL ";
            }

            sql = sql.Remove(sql.Length - 10); //Removes the text UNION ALL at the last query

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddRange(param.ToArray());

                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds, "MSTR_Products");
                            crystal.SetDataSource(ds);
                            crystalReportViewer1.ReportSource = crystal;
                        }
                    }
                }
            }
        }

        private void chkIndividualPrinting_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndividualPrinting.Checked)
                txtProductCode.Enabled = true;
            else
                txtProductCode.Enabled = false;
        }

        private void txtNoOfCopy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
