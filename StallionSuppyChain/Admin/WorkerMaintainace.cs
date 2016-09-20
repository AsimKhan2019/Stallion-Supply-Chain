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
using System.Windows.Forms;

namespace StallionSuppyChain.Admin
{
    public partial class WorkerMaintainace : Form
    {
        public WorkerMaintainace()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private void WorkerMaintainace_Load(object sender, EventArgs e)
        {
            GetPosition();
            GetDepartmentns();
        }


        private void GetPosition()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[DBO].LIST_DESIGNATIONS", con);
                    adapter.Fill(dt);
                    cboPOstion.DataSource = dt;

                    cboPOstion.DisplayMember = "Designation";
                    cboPOstion.ValueMember = "DesignationCode";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void GetDepartmentns()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[DBO].LIST_DEPARTMENTS", con);
                    adapter.Fill(dt);
                    cboDepartments.DataSource = dt;

                    cboDepartments.DisplayMember = "DepartmentName";
                    cboDepartments.ValueMember = "DeptCode";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtIDNUMBER.Text == "")
            {

                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtIDNUMBER.Focus();



            }
            else if (TxtFirstName.Text == "")
            {

                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtFirstName.Focus();



            }
            else if (TxtLastName.Text == "")
            {

                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtLastName.Focus();



            }
            else if (TxtMiddleName.Text == "")
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtLastName.Focus();
            }

            else if (TxtEmailAddress.Text == "")
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtEmailAddress.Focus();
            }

            else if (cboPOstion.SelectedValue.ToString() == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboPOstion.Focus();
            }
            else if (cboDepartments.SelectedValue.ToString() == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboDepartments.Focus();



            }

            else
            {
                string StringReturn = "";
               

                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "TRAN_SAVEWORKER";
                cmd.Parameters.Add("@IDRef", SqlDbType.VarChar, 15).Value = txtIDNUMBER.Text.ToString();

                cmd.Parameters.Add("@Lastname", SqlDbType.VarChar, 100).Value = TxtLastName.Text.ToString();
                cmd.Parameters.Add("@Firstname", SqlDbType.VarChar, 100).Value = TxtFirstName.Text.ToString();
                cmd.Parameters.Add("@MidName", SqlDbType.VarChar, 100).Value = TxtMiddleName.Text.ToString();
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100).Value = TxtEmailAddress.Text.ToString();
                cmd.Parameters.Add("@DesignationCode", SqlDbType.VarChar, 4).Value = cboPOstion.SelectedValue.ToString();

                cmd.Parameters.Add("@DeptCode", SqlDbType.VarChar, 15).Value = cboDepartments.SelectedValue.ToString();
              
                cmd.Connection = con;
                try
                {
                    con.Open();
                    StringReturn = cmd.ExecuteScalar().ToString();
                    if (StringReturn == "EXISTS")
                    {
                        MessageBox.Show("ID Number Already Exists", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                       
                        
                    }



                    if (StringReturn == "DONE")
                    {
                        MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                        UserMaintainance um = new UserMaintainance();


                        um.Show();
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

        private void BtnNew_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            btnSave.Enabled = true;
          
        }
    }
}
