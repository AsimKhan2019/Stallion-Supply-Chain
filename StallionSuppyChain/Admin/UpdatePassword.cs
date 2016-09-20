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
    public partial class UpdatePassword : Form
    {
        public UpdatePassword()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private void UpdatePassword_Load(object sender, EventArgs e)
        {

        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
             if (TxtPasswordConfirm.Text == "")
            {
                MessageBox.Show("Password did not match", "Error", MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation,
              MessageBoxDefaultButton.Button1);
                TxtPasswordConfirm.Focus();


            }
            else if (TxtPassword.Text == "")
            {
                MessageBox.Show("Password did not match", "Error", MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation,
              MessageBoxDefaultButton.Button1);
                TxtPasswordConfirm.Focus();


            }
             else if (TxtPasswordConfirm.Text != TxtPassword.Text)
             {
                 MessageBox.Show("Password did not match", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation,
               MessageBoxDefaultButton.Button1);
                 TxtPasswordConfirm.Focus();


             }
             else
             {

                 string StringReturn = "";


                 SqlConnection con = new SqlConnection(conStr);
                 SqlCommand cmd = new SqlCommand();
                 cmd.CommandType = CommandType.StoredProcedure;

                 cmd.CommandText = "TRAN_UpdatePassword";
                 cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar, -1).Value = TxtPassword.Text.ToString();
                 cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32( TxtUserID.Text.ToString());
            

                 cmd.Connection = con;
                 try
                 {
                     con.Open();
                     cmd.ExecuteNonQuery();
                  
                     this.Dispose();
                     


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
}
