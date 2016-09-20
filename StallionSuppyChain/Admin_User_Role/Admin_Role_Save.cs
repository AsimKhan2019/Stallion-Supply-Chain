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

namespace StallionSuppyChain.Admin_User_Role
{
    public partial class Admin_Role_Save : Form
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Admin_Role_Save()
        {
            InitializeComponent();
        }

        private void Admin_Role_Save_Load(object sender, EventArgs e)
        {
            LoadUserName();
            LoadRole();
            LoadModule();
        }

        private void LoadUserName()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_ADMIN_USER_ROLE]", con);
                    adapter.Fill(dt);

                    cmbUsername.DataSource = dt;
                    cmbUsername.DisplayMember = "User_Name";
                    cmbUsername.ValueMember = "UserID";

                }
                catch (Exception ex)
                {
                    string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    MessageBox.Show(errMessage);
                }
            }
        }
        #region loading data to dropdowm
        private void LoadRole()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_ADMIN_USER_ROLE_NAME]", con);
                    adapter.Fill(dt);

                    cmbRole.DataSource = dt;
                    cmbRole.DisplayMember = "RoleName";
                    cmbRole.ValueMember = "RoleID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void LoadModule()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_ADMIN_USER_ROLE_Module]", con);
                    adapter.Fill(dt);

                    cmbModule.DataSource = dt;
                    cmbModule.DisplayMember = "ModuleDescription";
                    cmbModule.ValueMember = "ModuleID";

                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            LoadUserName();
            LoadRole();
            LoadModule();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Convert.ToString(cmbModule.SelectedValue)) == "0" ||
                (Convert.ToString(cmbRole.SelectedValue)) == "0" ||
                (Convert.ToString(cmbUsername.SelectedValue)) == "0" )
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(conStr);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TRAN_SAVE_ADMIN_ROLE";

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(cmbUsername.SelectedValue);
                    cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = Convert.ToInt32(cmbModule.SelectedValue);
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = Convert.ToInt32(cmbRole.SelectedValue);
                    
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    InsertAuditLog();
                    back();

                }
                catch (Exception ex)
                {
                    string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    MessageBox.Show(errMessage);
                }
            }
        }

        private void Admin_Role_Save_FormClosing(object sender, FormClosingEventArgs e)
        {
            back();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void back()
        {
            Admin_Role_Inquiry Form = new Admin_Role_Inquiry();
            Form.Show();
            this.Dispose();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void InsertAuditLog()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_INSERT_ADMIN_AUDIT_LOG";

                cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 30).Value = TxtUserID.Text;
                cmd.Parameters.Add("@Module", SqlDbType.VarChar, 50).Value = "ADMIN ROLE";
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = "ADDITIONAL ADMIN ROLE";
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = "SUCCESS";

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                MessageBox.Show(errMessage);
            }
        }

        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }
    }
}
