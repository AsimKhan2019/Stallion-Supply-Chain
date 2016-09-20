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

        public PrintBarcode()
        {
            InitializeComponent();
        }

        private void PrintBarcode_Load(object sender, EventArgs e)
        {
            crystal.Load("Reports\\Barcode.rpt");
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnShowBarcodes_Click(object sender, EventArgs e)
        {
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


    }
}
