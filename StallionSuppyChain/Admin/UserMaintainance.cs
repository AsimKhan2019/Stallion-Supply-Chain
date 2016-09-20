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
namespace StallionSuppyChain.Admin
{
    public partial class UserMaintainance : Form
    {
        public UserMaintainance()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private void label11_Click(object sender, EventArgs e)
        {

        }
        private int rowIndex = 0;
        private void cboProjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UserMaintainance_Load(object sender, EventArgs e)
        {
            GetWorker1();
            GetModules();
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }

        private void GetWorker1()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_WORKER_WOUsers]", con);
                    adapter.Fill(dt);
                    cboWorker.DataSource = dt;

                    cboWorker.DisplayMember = "FULL_NAME";
                    cboWorker.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void GetModules()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_Modules]", con);
                    adapter.Fill(dt);
                    cboModuleID.DataSource = dt;

                    cboModuleID.DisplayMember = "ModuleName";
                    cboModuleID.ValueMember = "ModuleID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void GetModuleRoles(string ModuleID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_ModuleRoles] '" + ModuleID + "'", con);
                    adapter.Fill(dt);
                    TxtUserLevel.DataSource = dt;

                    TxtUserLevel.DisplayMember = "RoleName";
                    TxtUserLevel.ValueMember = "RoleID";


            
    

                }
                catch (Exception ex)
                {

                }
            }
        }
        private void ReadWorkerDetails(string WorkerID)
        {


            // string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;

            //connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "[dbo].[LIST_WORKERDETAILS] '" + WorkerID + "'";

            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    TxtLastName.Text = sqlReader.GetValue(0).ToString();
                    TxtFirstName.Text = sqlReader.GetValue(1).ToString();
                    TxtMiddleName.Text = sqlReader.GetValue(2).ToString();
                    TxtEmailAddress.Text = sqlReader.GetValue(3).ToString();
                    
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
             //   MessageBox.Show(ex.ToString());
            }
        }

        private void cboWorker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ReadWorkerDetails(cboWorker.SelectedValue.ToString());
            }
            catch (Exception ex)
            {


            }
           }

        private void cboModuleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                GetModuleRoles(cboModuleID.SelectedValue.ToString());
            }
            catch (Exception ex)
            {


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkerMaintainace WM = new WorkerMaintainace();
            WM.Show();

            this.Dispose();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            BtnNew.Enabled = false;
            btnSave.Enabled = true;
            cboWorker.Enabled = true;
            button3.Enabled = true;
            groupBox4.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {





          




            if (Convert.ToUInt32( cboModuleID.SelectedValue) == 0)
            {

                MessageBox.Show("Please select Module ID.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboModuleID.Focus();

            }
            else if (Convert.ToUInt32(TxtUserLevel.SelectedValue) == 0)
            {

                MessageBox.Show("Please select User Level.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtUserLevel.Focus();

            }
            else if (Convert.ToUInt32(cboWorker.SelectedValue) == 0  )
            {

                MessageBox.Show(" Please select Worker.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboWorker.Focus();

            }

            else
            {

                this.dataGridView1.Rows.Add("Remove", cboModuleID.SelectedValue.ToString(), cboModuleID.Text, TxtUserLevel.SelectedValue.ToString(), TxtUserLevel.Text.ToString());
                TxtUserLevel.SelectedValue = 0;
                cboModuleID.SelectedValue = 0;
               // ClearFields();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {










           if (Convert.ToUInt32(cboWorker.SelectedValue) == 0)
            {

                MessageBox.Show(" Please select Worker.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboWorker.Focus();

            }
           else if ( TxtUserName.Text  == "")
           {

               MessageBox.Show("This field is required", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation,
               MessageBoxDefaultButton.Button1);
               TxtUserName.Focus();

           }
            else if (Convert.ToInt32(dataGridView1.RowCount.ToString()) == 0)
            {

                MessageBox.Show("Atleast 1 user Role.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboWorker.Focus();

            }




            else
            {
                string StringReturn = "";


                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "TRAN_SAVEUSERACCESS";
                cmd.Parameters.Add("@WorkerID", SqlDbType.Int).Value = cboWorker.SelectedValue.ToString();
                cmd.Parameters.Add("@User_Name", SqlDbType.VarChar, 100).Value = TxtUserName.Text.ToString();
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar,-1 ).Value = TxtDefaultPassword.Text.ToString();
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = checkBox1.CheckState;
               


                cmd.Connection = con;
                try
                {
                    con.Open();
                    StringReturn = cmd.ExecuteScalar().ToString();
                    if (StringReturn == "EXISTS")
                    {
                        MessageBox.Show("User Name Already Exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }else  
                    {







                        saveGridData( Convert.ToInt32( StringReturn.ToString()));




                        MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                        //UserMaintainance um = new UserMaintainance();
                        //um.Show();
                        this.Dispose();
                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            this.dataGridView1.Rows.RemoveAt(this.rowIndex);
        }
        private void saveGridData(int UserID)
        {
 
         
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    var ModuleID = dataGridView1.Rows[i].Cells[1].Value;
                    var RoleID = dataGridView1.Rows[i].Cells[3].Value;

                    saveDatagrid(UserID,Convert.ToInt32 (ModuleID), Convert.ToInt32 (RoleID));
              
                
                
                }

              
             
        }




        private void saveDatagrid(int UserID, int ModuleID, int RoelID)
        {

            string StringReturn = "";


            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_SaveUserRoles";
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToUInt32(UserID);
            cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = Convert.ToUInt32(ModuleID);
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = Convert.ToUInt32( RoelID);
          
 
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }
    }
}
