using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace StallionSuppyChain.Reports
{
    public partial class TESTREPORT : Form
    {
        public TESTREPORT()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public void GetPOID(string parameter1)
        {
            txtPOID.Text = parameter1;
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("Report_ApprovedPO  " + txtPOID.Text.ToString(), con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "Supplier";
            ds.Tables[1].TableName = "PoDetails";
            ApprovedPO bill = new ApprovedPO();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();  
        }

        private void TESTREPORT_Load(object sender, EventArgs e)
        {
           

        }
    }
}
