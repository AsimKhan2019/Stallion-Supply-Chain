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
using StallionSuppyChain.Admin;
using StallionSuppyChain.Procurement;
using StallionSuppyChain.Purchase_Order;
using StallionSuppyChain.DRM;
using StallionSuppyChain.Material_Releasing;
using StallionSuppyChain.Admin_Reset_Password;
using StallionSuppyChain.Admin_User_Role;
using StallionSuppyChain.Reports;
using StallionSuppyChain.Maintainance;
using StallionSuppyChain.Maintenance;
using StallionSuppyChain.MRI;
 

namespace StallionSuppyChain
{
    public partial class SCMMain : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        public SCMMain()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            ITEMMASTER ItemMaster = new ITEMMASTER ();
            ItemMaster.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

            StallionSuppyChain.Procurement.MaterialRequestIquiry ItemMaster2 = new StallionSuppyChain.Procurement.MaterialRequestIquiry();
            ItemMaster2.GetUserID(TxtUserID.Text);
            ItemMaster2.Show();

            //ItemMasterLookUp ItemMasterLookUp1 = new ItemMasterLookUp();
            //ItemMasterLookUp1.ShowDialog();
           // ItemMasterLookUp TEST = new ItemMasterLookUp();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            StallionSuppyChain.Purchase_Order.POInquiry PO = new StallionSuppyChain.Purchase_Order.POInquiry();
            PO.GetUserID(TxtUserID.Text);

            PO.Show();
        }

        private void SCMMain_Load(object sender, EventArgs e)
        {

        }

        public void GetConnection(string parameter1, string UserID)
        {
            toolStripStatusLabel1.Text = parameter1;
            toolStripStatusLabel2.Text = UserID;

        }
       
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

            // string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;

            //connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "[dbo].[LIST_ModuleRead] '" + TxtUserID.Text + "'";

            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    if (sqlReader["ModuleCode"].ToString() == "MSTR_MATERIAL" && sqlReader["RoleName"].ToString() == "Creator")
                    {

                        button13.Enabled = true;

                    }

                    if (sqlReader["ModuleCode"].ToString() == "TRAN_PO" && sqlReader["RoleName"].ToString() == "Requestor")
                    {

                        button14.Enabled = true;
                        linkLabel28.Enabled = true;

                    }
                    if (sqlReader["ModuleCode"].ToString() == "TRAN_PO" && sqlReader["RoleName"].ToString() == "Approver")
                    {


                        button10.Enabled = true;
                    }
                    if (sqlReader["ModuleCode"].ToString() == "TRAN_MATERIALREQ" && sqlReader["RoleName"].ToString() == "Requestor")
                    {

                        button17.Enabled = true;
                        linkLabel22.Enabled = true;
                        linkLabel23.Enabled = true;

                    }
                    if (sqlReader["ModuleCode"].ToString() == "TRAN_MATERIALREQ" && sqlReader["RoleName"].ToString() == "Approver")
                    {

                        button9.Enabled = true;

                    }
                    if (sqlReader["ModuleCode"].ToString() == "ADMIN_USERMAINTAINANCE" && sqlReader["RoleName"].ToString() == "Creator")
                    {

                        userMaintainanceToolStripMenuItem.Enabled = true;

                    }
                    if (sqlReader["ModuleCode"].ToString() == "DRM" && sqlReader["RoleName"].ToString() == "Creator")
                    {
                          	
                        button16.Enabled = true;
                        linkLabel35.Enabled = true;

                    }
                    if (sqlReader["ModuleCode"].ToString() == "ADMINADDREMOVEACCESS" && sqlReader["RoleName"].ToString() == "Admin Add Remove Access")
                    {

                        addRemoveAccessToolStripMenuItem.Enabled = true;

                    }

                    if (sqlReader["ModuleCode"].ToString() == "ADMINRESETPASS" && sqlReader["RoleName"].ToString() == "Admin Reset Password")
                    {

                        resetPasswordToolStripMenuItem.Enabled = true;

                    }

                    if (sqlReader["ModuleCode"].ToString() == "MaterialRelease" && sqlReader["RoleName"].ToString() == "Release Material")
                    {

                        button7.Enabled = true;
                        linkLabel29.Enabled = true;
                        linkLabel19.Enabled = true;
                    }

                    if (sqlReader["ModuleCode"].ToString() == "RPT_gen" && sqlReader["RoleName"].ToString() == "Report Generator")
                    {

                        button8.Enabled = true;
                         
                    }


                    if (sqlReader["ModuleCode"].ToString() == "TRP" && sqlReader["RoleName"].ToString() == "Creator")
                    {

                        linkLabel38.Enabled = true;
                        button1.Enabled = true;
                        linkLabel37.Enabled = true;

                    }

                      if (sqlReader["ModuleCode"].ToString() == "TRM" && sqlReader["RoleName"].ToString() == "Creator")
                    {

                        button2.Enabled = true;
                        linkLabel36.Enabled = true;
                        linkLabel40.Enabled = true;

                    }



                      if (sqlReader["ModuleCode"].ToString() == "TRP" && sqlReader["RoleName"].ToString().Trim() == "TRP Verify")
                      {

                          linkLabel27.Enabled = true;
                         
                      }
                      if (sqlReader["ModuleCode"].ToString() == "TRM" && sqlReader["RoleName"].ToString().Trim() == "TRM Verify")
                      {

                          linkLabel34.Enabled = true;

                      }
                      if (sqlReader["ModuleCode"].ToString() == "MaterialRelease" && sqlReader["RoleName"].ToString().Trim() == "MRI Verify")
                      {

                          linkLabel39.Enabled = true;

                      }

                }


 
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();



                gerUserInfo(Convert.ToInt32(parameter1));







                //FormMode("EDIT");
                //txtret.Text = "1";
                // tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //SqlDataReader rdr = cmd.ExecuteReader();
            //while (rdr.Read())
            //{
            //    string column = rdr["ColumnName"].ToString();
            //    int columnValue = Convert.ToInt32(rdr["ColumnName"]);
            //}

        }



        private void gerUserInfo(int userID)
        {

             SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;

            //connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "[dbo].[LIST_UserProfile] '" + userID.ToString() + "'";

            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {



                    lblName.Text = sqlReader["NAME"].ToString();
                    lblPosition.Text = sqlReader["Designation"].ToString();
                    lblEmailAddress.Text = sqlReader["EmailAddress"].ToString();
                    lblDepartment.Text = sqlReader["DepartmentName"].ToString();
                    if (sqlReader["DateModified"].ToString() == "")
                    {

                        UpdatePassword UP = new UpdatePassword();
                        UP.GetUserID(TxtUserID.Text);
                        UP.ShowDialog();
                    }



    
                }


 
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();



                





                //FormMode("EDIT");
                //txtret.Text = "1";
                // tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //SqlDataReader rdr = cmd.ExecuteReader();
            //while (rdr.Read())
            //{
            //    string column = rdr["ColumnName"].ToString();
            //    int columnValue = Convert.ToInt32(rdr["ColumnName"]);
            //}

        

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtUserID_TextChanged(object sender, EventArgs e)
        {

        }

        private void userMaintainanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserMaintainance Usermaint = new UserMaintainance();
            Usermaint.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MaterialRequestForApproval Usermaint = new MaterialRequestForApproval();
            Usermaint.GetUserID(TxtUserID.Text);
            Usermaint.ShowDialog();
         

        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            ITEMMASTER ItemMaster = new ITEMMASTER();
            ItemMaster.GetUserID(TxtUserID.Text);
            ItemMaster.Show();
        }

        private void SCMMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.Exit();

            }
            catch (Exception ex)
            {


            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PoForApproval Usermaint = new PoForApproval();
            Usermaint.GetUserID(TxtUserID.Text);
            Usermaint.ShowDialog();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            UpdatePassword UP = new UpdatePassword();
            UP.GetUserID(TxtUserID.Text);
            UP.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Material_Releasing_Inquiry MRI = new Material_Releasing_Inquiry();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("MRI");
           MRI.Show();
         
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DRMInquiry DRM= new  DRMInquiry();
            DRM.GetUserID(TxtUserID.Text);

            DRM.Show();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MaterialRequestForApproval Usermaint = new MaterialRequestForApproval();
            Usermaint.GetUserID(TxtUserID.Text);
            Usermaint.ShowDialog();
        }

        private void button10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PoForApproval Usermaint = new PoForApproval();
            Usermaint.GetUserID(TxtUserID.Text);
            Usermaint.ShowDialog();
        }

        private void linkLabel22_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            StallionSuppyChain.Procurement.MaterialRequestIquiry ItemMaster2 = new StallionSuppyChain.Procurement.MaterialRequestIquiry();
            ItemMaster2.GetUserID(TxtUserID.Text);
            ItemMaster2.Show();
        }

        private void linkLabel23_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MaterialRequestMain ItemMaster = new MaterialRequestMain();
            ItemMaster.GetUserID(TxtUserID.Text);
        
            ItemMaster.Show();
        }

        private void linkLabel28_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StallionSuppyChain.Purchase_Order.POInquiry PO = new StallionSuppyChain.Purchase_Order.POInquiry();
            PO.GetUserID(TxtUserID.Text);

            PO.Show();
        }

        private void button6_Click_2(object sender, EventArgs e)
        {

        }

        private void linkLabel35_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DRMInquiry DRM = new DRMInquiry();
            DRM.GetUserID(TxtUserID.Text);
            DRM.Show();
        }

        private void resetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset_Password RP = new Reset_Password();
            RP.Show();

        }

        private void addRemoveAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin_Role_Inquiry RP = new Admin_Role_Inquiry();
            RP.Show();

        }

        private void linkLabel29_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_Releasing_Inquiry MRI = new Material_Releasing_Inquiry();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("MRI");
            MRI.Show();
        }

        private void linkLabel19_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_Releasing_Project_Sel main = new Material_Releasing_Project_Sel();
            main.GetUserID(TxtUserID.Text);
            main.GetTranType("MRI");
           // this.Hide();
            main.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InventoryReportGenerator MRI = new InventoryReportGenerator();
            MRI.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Material_Releasing_Inquiry MRI = new Material_Releasing_Inquiry();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("TRM");
            MRI.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Material_Releasing_Inquiry MRI = new Material_Releasing_Inquiry();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("TRP");
            MRI.Show();
        }

        private void linkLabel36_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_Releasing_Inquiry MRI = new Material_Releasing_Inquiry();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("TRM");
            MRI.Show();
        }

        private void linkLabel40_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_Releasing_Project_Sel main = new Material_Releasing_Project_Sel();
            main.GetUserID(TxtUserID.Text);
            main.GetTranType("TRM");
            // this.Hide();
            main.Show();
        }

        private void linkLabel37_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_Releasing_Project_Sel main = new Material_Releasing_Project_Sel();
            main.GetUserID(TxtUserID.Text);
            main.GetTranType("TRP");
            // this.Hide();
            main.Show();
        }

        private void linkLabel38_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_Releasing_Inquiry MRI = new Material_Releasing_Inquiry();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("TRP");
            MRI.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSupplier supplier = new FrmSupplier();
            supplier.GetUserID(TxtUserID.Text);
            supplier.ShowDialog();
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProject supplier = new frmProject();
            supplier.GetUserID(TxtUserID.Text);
            supplier.ShowDialog();
        }

        private void linkLabel39_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_verifyForRelease MRI = new Material_verifyForRelease();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("MRI");
            MRI.Show();
        }

        private void linkLabel34_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_verifyForRelease MRI = new Material_verifyForRelease();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("TRM");
            MRI.Show();
        }

        private void linkLabel27_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Material_verifyForRelease MRI = new Material_verifyForRelease();
            MRI.GetUserID(TxtUserID.Text);
            MRI.GetTranType("TRP");
            MRI.Show();
        }
    } 
}
