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
    public partial class FromInventoryReport : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public FromInventoryReport()
        {
            InitializeComponent();
        }

        private void FromInventoryReport_Load(object sender, EventArgs e)
        {
           
        }


        public void LoadReport(string Module, string nofilter, string Item_Code, string CostCode, string ProjectCode, string ReportType , DateTime DateFrom, DateTime DateTo, string ForPurchase)
        {


 


            if (Module == "1")
            {

                InventoryReports(nofilter, Item_Code, CostCode, ProjectCode);

            }
            if (Module == "3")
            {

                MRMReport(nofilter, Item_Code, CostCode, ProjectCode,ReportType,DateFrom,DateTo,ForPurchase);

            }
            if (Module == "2")
            {

                POReport(nofilter, Item_Code, CostCode, ProjectCode, ReportType, DateFrom, DateTo, ForPurchase);

            }
        }

        private void InventoryReports(string nofilter, string Item_Code, string CostCode, string ProjectCode)
        {
 
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("Inventory_Reports '" + nofilter + "','" + Item_Code + "','" + CostCode + "'" + ",'" + ProjectCode + "'", con);

            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "Inventory_Reports";
            InventoryReport_Live bill = new InventoryReport_Live();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();  
        }



        private void MRMReport(string nofilter, string Item_Code, string CostCode, string ProjectCode, string ReportType, DateTime DateFrom, DateTime DateTo, string ForPurchase)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("MRM_REPORT '" + nofilter + "','" + Item_Code + "','" + CostCode + "'" + ",'" + ProjectCode + "'" + ",'" + ReportType + "'" + ",'" + Convert.ToDateTime(DateFrom) + "'" + ",'" + Convert.ToDateTime(DateTo) + "'" + ",'" + ForPurchase + "'", con);

            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "MRMList";
            MRM_Report bill = new MRM_Report();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();
        }

        private void POReport(string nofilter, string Item_Code, string CostCode, string ProjectCode, string ReportType, DateTime DateFrom, DateTime DateTo, string ForPurchase)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("POM_REPORT '" + nofilter + "','" + Item_Code + "','" + CostCode + "'" + ",'" + ProjectCode + "'" + ",'" + ReportType + "'" + ",'" + DateFrom.ToString("MM/dd/yyyy") + "'" + ",'" + DateTo.ToString("MM/dd/yyyy") + "'" + ",'" + ForPurchase + "'", con);

            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "POMList";
            POM_Reports bill = new POM_Reports();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
