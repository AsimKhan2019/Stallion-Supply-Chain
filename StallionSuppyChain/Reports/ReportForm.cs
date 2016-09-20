using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace StallionSuppyChain.Reports
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public void LoadReport(string moduleID, string isnotfilter, string ItemCode, string CostCode, string projectCode, string type, DateTime StrFrom, DateTime strTO, string forpurchase)
        {

            if (moduleID == "2")
            {
                GetPOID(isnotfilter, CostCode == "" ? "''" : CostCode, ItemCode == "" ? "''" : ItemCode, projectCode.Replace(" ", "''"), type.Replace(" ", "''"), StrFrom, strTO, forpurchase.Replace(" ", "''"));
                 
            }

            if (moduleID == "3")
            {
                GetMaterialReport(isnotfilter, CostCode == "" ? "''" : CostCode, ItemCode == "" ? "''" : ItemCode, projectCode.Replace(" ", "''"), type.Replace(" ", "''"), StrFrom, strTO, forpurchase.Replace(" ", "''"));

            }

            if (moduleID == "1")
            {
                GetInventoryReports(isnotfilter, CostCode == "" ? "''" : CostCode, ItemCode == "" ? "''" : ItemCode, projectCode.Replace(" ", "''"));

            }




        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

        }


        public void GetPOID(string nofilter, string CostCode, string Item_Code, string ProjectCode, string ReportTypeID, DateTime DateFrom, DateTime DateTo, string ForPurchase)
        {
              
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("POM_REPORT  " + nofilter + "," + Item_Code + "," + CostCode + "," + ProjectCode + "," + ReportTypeID + ",'" + DateFrom  + "','" + DateTo  + "'," + ForPurchase, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "POMList";

            POM_Reports bill = new POM_Reports();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();
        }



        public void GetMaterialReport(string nofilter, string CostCode, string Item_Code, string ProjectCode, string ReportTypeID, DateTime DateFrom, DateTime DateTo, string ForPurchase)
        {

            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("MRM_REPORT  " + nofilter + "," + Item_Code + "," + CostCode + "," + ProjectCode + "," + ReportTypeID + ",'" + DateFrom + "','" + DateTo + "'," + ForPurchase, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "MRMList";

            MRM_Report bill = new MRM_Report();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();
        }

        public void GetInventoryReports(string nofilter, string CostCode, string Item_Code, string ProjectCode )
        {

            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter sda = new SqlDataAdapter("Inventory_Reports  " + nofilter + "," + Item_Code + "," + CostCode + "," + ProjectCode  , con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ds.Tables[0].TableName = "Inventory_Reports";

            InventoryReport_Live bill = new InventoryReport_Live();
            bill.SetDataSource(ds);
            bill.VerifyDatabase();
            crystalReportViewer1.ReportSource = bill;
            crystalReportViewer1.RefreshReport();
        }
    }
}
