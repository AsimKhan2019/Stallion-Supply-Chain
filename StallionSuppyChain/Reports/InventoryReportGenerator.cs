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
    public partial class InventoryReportGenerator : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        public InventoryReportGenerator()
        {
            InitializeComponent();
        }

        private void InventoryReportGenerator_Load(object sender, EventArgs e)
        {
            LoadCategory();
            GetProjectCode();
            LoadCostCode();
            LoadModules();
           
        }


        private void LoadReportTypes( int moduleID)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[Report_ReportType] " + moduleID.ToString(), con);
                    adapter.Fill(dt);
                    cmbType.DataSource = dt;
                    cmbType.DisplayMember = "ReportType";
                    cmbType.ValueMember = "ReportID";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void LoadModules()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_ReportModules]", con);
                    adapter.Fill(dt);
                    cmbModules.DataSource = dt;
                    cmbModules.DisplayMember = "ModuleDescription";
                    cmbModules.ValueMember = "ModuleCode";
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void LoadCategory()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY]", con);
                    adapter.Fill(dt);
                    cboCategory1.DataSource = dt;
                    cboCategory1.DisplayMember = "CategoryCode";
                    cboCategory1.ValueMember = "CategoryID";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void LoadCostCode()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCostCode] ", con);
                    adapter.Fill(dt);
                    cboCostCode.DataSource = dt;
                    cboCostCode.DisplayMember = "CosDescription";
                    cboCostCode.ValueMember = "CostCode";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void cboCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadCategory3(cboCategory2.SelectedValue.ToString());
            LoadCategory2(cboCategory1.SelectedValue.ToString());
        }


        private void LoadCategory2(string CategoryID1)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY2] " + CategoryID1, con);
                    adapter.Fill(dt);
                    cboCategory2.DataSource = dt;
                    cboCategory2.DisplayMember = "CategoryCode2";
                    cboCategory2.ValueMember = "CategoryID2";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void LoadCategory3(string CategoryID2)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY3] " + CategoryID2, con);
                    adapter.Fill(dt);
                    cboCategory3.DataSource = dt;
                    cboCategory3.DisplayMember = "CategoryCode3";
                    cboCategory3.ValueMember = "CategoryID3";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void cboCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory3(cboCategory2.SelectedValue.ToString());
        }

        private string GenerateItemCode(string Category1, string Category2, string Category3)
        {

            if (Category1 == "")
            {

                Category1 = "0";

            }
            if (Category2 == "")
            {

                Category2 = "0";

            }
            if (Category3 == "")
            {

                Category3 = "0";

            }
            //  string connetionString = null;
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string SRet = "";

            //  connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "[dbo].[TRAN_GenItemCode_REPORTS] " + Convert.ToString(Category1) + ',' + Convert.ToString(Category2) + ',' + Convert.ToString(Category3);


            try
            {

                //  cnn = new SqlConnection(conStr);
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    SRet = cmd.ExecuteScalar().ToString();
                    con.Close();
                    cmd.Dispose();



                }


            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.ToString());
            }
            return SRet;

        }

        private void cboCategory3_SelectedIndexChanged(object sender, EventArgs e)
        {
         TXTITEMCODE.Text =   GenerateItemCode(cboCategory1.SelectedValue.ToString(), cboCategory2.SelectedValue.ToString(), cboCategory3.SelectedValue.ToString());
        }

        private void GetProjectCode()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cboProjectCode.DataSource = dt;
                    cboProjectCode.DisplayMember = "ProjectName";
                    cboProjectCode.ValueMember = "ProjectID";
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void cboProjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            if (cmbModules.SelectedValue == null || cmbModules.SelectedValue.ToString() == "0")
            {

                MessageBox.Show("Please select module.", "Error", MessageBoxButtons.OK,
                  MessageBoxIcon.Exclamation,
                  MessageBoxDefaultButton.Button1);
                return;
            }
            if (cmbType.SelectedValue == null || cmbType.SelectedValue.ToString() == "0")
            {

                MessageBox.Show("Please select report type.", "Error", MessageBoxButtons.OK,
                  MessageBoxIcon.Exclamation,
                  MessageBoxDefaultButton.Button1);
                return;
            }

            DateTime strNeededfrom  ;
                if(txtNeededfrom.Checked == false)
                {
                    strNeededfrom = Convert.ToDateTime("01/01/1990");
                }
                else
                {
                    strNeededfrom = Convert.ToDateTime( txtNeededfrom.Value.ToString());
                }
                DateTime strNeededTO ;
                if(dateTimePicker1.Checked == false)
                {
                    strNeededTO =  Convert.ToDateTime ("01/01/1990");
                }
                else
                {
                    strNeededTO = Convert.ToDateTime( dateTimePicker1.Value.ToString());
                }
            if (checkBox1.Checked == false)
            {
                if (cboProjectCode.SelectedValue == null || cboProjectCode.SelectedValue.ToString() == "0")
                {

                    MessageBox.Show("Please select project.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (cmbType.SelectedValue.ToString() == "1"  )
            {

                ReportForm asdd = new ReportForm();
                string isnotfilter = "";
                if(checkBox1.Checked == true)
                {
                    isnotfilter = "1";
                }
                else
                {
                     isnotfilter = "0";

                }


                 string chkIsforPurchase = "";
                if(chkIsFixedAsset.Checked == true)
                {
                    chkIsforPurchase = "1";
                }
                else
                {
                     chkIsforPurchase = "0";

                }

               asdd.LoadReport(cmbModules.SelectedValue.ToString(), isnotfilter, TXTITEMCODE.Text.ToString(), cboCostCode.SelectedValue.ToString(), cboProjectCode.SelectedValue.ToString(), cmbType.SelectedValue.ToString(),    strNeededfrom ,strNeededTO, chkIsforPurchase);
                asdd.ShowDialog();


            }
            if (cmbType.SelectedValue.ToString() == "2" || cmbType.SelectedValue.ToString() == "3" || cmbType.SelectedValue.ToString() == "4")
            {

                ReportForm asdd = new ReportForm();
                string isnotfilter = "";
                if (checkBox1.Checked == true)
                {
                    isnotfilter = "1";
                }
                else
                {
                    isnotfilter = "0";

                }


                string chkIsforPurchase = "";
                if (chkIsFixedAsset.Checked == true)
                {
                    chkIsforPurchase = "1";
                }
                else
                {
                    chkIsforPurchase = "0";

                }

                asdd.LoadReport(cmbModules.SelectedValue.ToString(), isnotfilter, TXTITEMCODE.Text.ToString(), cboCostCode.SelectedValue.ToString(), cboProjectCode.SelectedValue.ToString(), cmbType.SelectedValue.ToString(),strNeededfrom ,strNeededTO, chkIsforPurchase);
                asdd.ShowDialog();


            }
            if (cmbType.SelectedValue.ToString() == "5" || cmbType.SelectedValue.ToString() == "6" || cmbType.SelectedValue.ToString() == "7")
            {

                ReportForm asdd = new ReportForm();
                string isnotfilter = "";
                if (checkBox1.Checked == true)
                {
                    isnotfilter = "1";
                }
                else
                {
                    isnotfilter = "0";

                }


                string chkIsforPurchase = "";
                if (chkIsFixedAsset.Checked == true)
                {
                    chkIsforPurchase = "1";
                }
                else
                {
                    chkIsforPurchase = "0";

                }

                asdd.LoadReport(cmbModules.SelectedValue.ToString(), isnotfilter, TXTITEMCODE.Text.ToString(), cboCostCode.SelectedValue.ToString(), cboProjectCode.SelectedValue.ToString(), cmbType.SelectedValue.ToString(), strNeededfrom ,strNeededTO, chkIsforPurchase);
                asdd.ShowDialog();


            }
            else
            {

                groupBox3.Enabled = true;
              
            }
            
            
         
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedValue.ToString() == "1")
            {
                chkIsFixedAsset.Enabled = false;
                groupBox3.Enabled = false;

            }
            else
            {
                chkIsFixedAsset.Enabled = true;
                groupBox3.Enabled = true;


            }
        }

        private void cmbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadReportTypes(Convert.ToInt32(cmbModules.SelectedValue.ToString()));



                if (cmbModules.SelectedValue.ToString() == "1")
                {
                    chkIsFixedAsset.Enabled = false;
                    groupBox3.Enabled = false;
                }

            }
            catch(Exception ex)
            {


            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked ==  true)
            {
                
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
           
            }
            else
            {

                groupBox3.Enabled = true;
                groupBox4.Enabled = true; 
                try
                {
                    if (cmbType.SelectedValue.ToString() == "1")
                    {
                        chkIsFixedAsset.Enabled = false;
                        groupBox3.Enabled = false;
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
