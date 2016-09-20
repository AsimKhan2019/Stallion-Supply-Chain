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

namespace StallionSuppyChain.Admin_Reset_Password
{
    public partial class Admin_Update : Form
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Admin_Update()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            back();
        }

        private void back()
        {
            Reset_Password Form = new Reset_Password();
            Form.Show();
            this.Dispose();
        }


        private void Admin_Update_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reset_Password Form = new Reset_Password();
            Form.Show();
            this.Dispose();
        }


        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }

        public void GetDetails(string UserID, string UserName)
        {
            textBox1.Text = UserID;
            textBox2.Text = UserName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_SAVE_Admin_reset";

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar,30).Value = textBox2.Text;

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                InsertAuditLog();
                back();

            }
            catch (Exception ex)
            {
                string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                MessageBox.Show(errMessage);
            }
            

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
                cmd.Parameters.Add("@Module", SqlDbType.VarChar, 50).Value = "ADMIN RESET PASSWORD";
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = "RESET USER TO DEFAULT PASSWORD";
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

        private void Admin_Update_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
