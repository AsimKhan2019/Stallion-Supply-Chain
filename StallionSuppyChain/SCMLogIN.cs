using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using System.Data;
using System.Drawing;


using System.Data.SqlClient;
namespace StallionSuppyChain
{
    public partial class SCMLogIN : Form
    {
        public SCMLogIN()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TXTITEMCODE_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogIN_Click(object sender, EventArgs e)
        {
            //if (txtuserName.Text == "" || TxtPassword.Text == "")
            //{
            //        MessageBox.Show("user Name or password is required", "ERROR", MessageBoxButtons.OK,
            //        MessageBoxIcon.Exclamation,
            //        MessageBoxDefaultButton.Button1);

            //}


            //else  if (txtuserName.Text != "scmuser" && TxtPassword.Text != "scmuser")
            //{

            //    MessageBox.Show("Invalid UserName or password.", "ERROR", MessageBoxButtons.OK,
            //    MessageBoxIcon.Exclamation,
            //    MessageBoxDefaultButton.Button1);

            //}
            //else
            //{
            //    this.Hide();

            //    SCMMain ItemMaster = new SCMMain();
            //    ItemMaster.Show();



            //}
            string StringReturn = "";
            StringReturn = LogIN(txtuserName.Text, TxtPassword.Text);

            Global.UserId = Convert.ToInt32(StringReturn);

            if (StringReturn == "INACTIVE")
            {


                MessageBox.Show("User is in-active", "ERROR", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);


            }
            else if (StringReturn != "")
            {

                SCMMain formTask = new SCMMain();
                formTask.GetUserID(StringReturn);
                string connection = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ConnectionString;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connection);
                // Retrieve the DataSource property.    
                string IPAddress = builder.DataSource;
                formTask.GetConnection(IPAddress, txtuserName.Text.ToString());
                this.Hide();
                formTask.Show();


            }
            else
            {
                MessageBox.Show("Invalid UserName or password.", "ERROR", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            }

        }




        private string LogIN(string UserName, string Password)
        {
            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[USER_LOGIN]";
            cmd.Parameters.Add("@User_Name", SqlDbType.VarChar, 50).Value = UserName;
            cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar, -1).Value = Password;


            cmd.Connection = con;
            try
            {
                con.Open();
                return StringReturn = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                // throw ex;
                return "";
            }
            finally
            {
                con.Close();
                con.Dispose();
            }




        }



    }
}
