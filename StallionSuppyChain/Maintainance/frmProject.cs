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

namespace StallionSuppyChain.Maintenance
{
    public partial class frmProject : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public frmProject()
        {
            InitializeComponent();
        }
        private void frmProject_Load(object sender, EventArgs e)
        {
            //Populate grid view on load
            LoadProject("", 99);
        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }

        private void FormMode(string Action)
        {

            if (Action == "NEW")
            {
                btnSave.Enabled = true;
                BtnReset.Enabled = true;

                txtProjCode.Enabled = true;
                txtProjNm.Enabled = true;
                txtbudget.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtAddress3.Enabled = true;
                dtStartDt.Enabled = true;
                dtEndDt.Enabled = true;

            }
            else if (Action == "EDIT")
            {
                btnEdit.Enabled = true;
                BtnReset.Enabled = true;
                txtbudget.Enabled = true;
                txtProjCode.Enabled = false;
                txtProjNm.Enabled = true;
                
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtAddress3.Enabled = true;
                dtStartDt.Enabled = true;
                dtEndDt.Enabled = true;

            }
            else if (Action == "RESET")
            {
                btnSave.Enabled = true;
                BtnReset.Enabled = false;

                txtProjCode.Text = "";
                txtProjNm.Text = "";
                txtbudget.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtAddress3.Text = "";
                dtStartDt.Value = DateTime.Now;
                dtEndDt.Value = DateTime.Now;

                cboSearchBy.Enabled = true;
                txtSearch.Text = "";
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            FormMode("NEW");
        }
        public static bool ValidateNumber(string number)
        {

            if (number == " " || number == "")
            {

                number = "0.0";
            }

            try
            {
                double _num = Convert.ToDouble(number.Trim());
            }
            catch
            {
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {


            bool isValidNumeric2 = ValidateNumber(txtbudget.Text);
            




            if (txtProjCode.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtProjCode.Focus();
            }
           
            else if (isValidNumeric2 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtbudget.Focus();
            }
            else if (txtProjNm.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtProjNm.Focus();
            }
            else if (dtStartDt.Value > dtEndDt.Value)//Nothing selected
            {
                MessageBox.Show("Start date cannot be greater than end date!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                dtEndDt .Focus();
            }
            else
            {

                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_SAVEPROJECT";
                cmd.Parameters.Add("@ProjectCode", SqlDbType.VarChar, 4).Value = txtProjCode.Text;
                cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar, 50).Value = txtProjNm.Text;
                cmd.Parameters.Add("@Budget", SqlDbType.Float).Value = txtbudget.Text;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = txtAddress1.Text;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = txtAddress2.Text;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 50).Value = txtAddress3.Text;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtStartDt.Value;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtEndDt.Value;
                cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
                cmd.Parameters.Add("@ProjectID", SqlDbType.Int, 1).Value = 0;
                cmd.Parameters["@ProjectID"].Direction = ParameterDirection.Output;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int ret = (int)cmd.Parameters["@ProjectID"].Value;
                    if (ret == 0)
                    {
                        MessageBox.Show("Project code already exists", "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                        txtProjCode.Focus();
                    }
                    else
                    {
                        ResetForm();
                        FormMode("INITIAL");
                        MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
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


                //Populate grid view
                txtSearch.Text = "";
                LoadProject("", 99);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtProjCode.Enabled = false;


            bool isValidNumeric2 = ValidateNumber(txtbudget.Text);
            
            if (txtProjCode.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else if (isValidNumeric2 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtbudget.Focus();
            }
            else if (txtProjNm.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtProjNm.Focus();
            }
            else if (dtStartDt.Value > dtEndDt.Value)//Nothing selected
            {
                MessageBox.Show("End date cannot be greater than Start date!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                dtEndDt.Focus();
            }
            else
            {

                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_UPDATEPROJECT";
                cmd.Parameters.Add("@ProjectCode", SqlDbType.VarChar, 4).Value = txtProjCode.Text;
                cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar, 50).Value = txtProjNm.Text;
                cmd.Parameters.Add("@Budget", SqlDbType.Float).Value = txtbudget.Text;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = txtAddress1.Text;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = txtAddress2.Text;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 50).Value = txtAddress3.Text;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dtStartDt.Value;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dtEndDt.Value;
                cmd.Parameters.Add("@UserModified", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);

                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    ResetForm();

                    FormMode("INITIAL");
                    MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

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



                //Populate grid view
                txtSearch.Text = "";
                LoadProject("", 99);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Populate grid view
            txtSearch.Text = "";
            LoadProject("", 99);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Input search keyword", "Information", MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
                txtSearch.Focus();

            }
            else if (cboSearchBy.SelectedItem == null)
            {
                MessageBox.Show("Select search mode", "Information", MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
                cboSearchBy.Focus();
            }
            else
            {
                LoadProject(txtSearch.Text, cboSearchBy.SelectedIndex);
            }
        }

        private void LoadProject(string SearchParam, int SearchMode)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_PROJECTDETAILS]", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchParam", SearchParam);
                cmd.Parameters.AddWithValue("@SearchMode", SearchMode);

                con.Open();
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    grdProject.DataSource = dt;
                }
            }
        }


        private void ResetForm()
        {
            btnSave.Enabled = true;
            BtnReset.Enabled = false;

            txtProjCode.Text = "";
            txtProjNm.Text = "";
            txtbudget.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            dtStartDt.Value = DateTime.Now;
            dtEndDt.Value = DateTime.Now;

            cboSearchBy.Enabled = true;
            txtSearch.Text = "";
        }

        private void grdProject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.grdProject.Rows[e.RowIndex];
                txtProjCode.Text = row.Cells["Project Code"].Value.ToString();
                txtProjNm.Text = row.Cells["Project Name"].Value.ToString();
                txtbudget.Text = row.Cells["Budget"].Value.ToString();
                txtAddress1.Text = row.Cells["Address 1"].Value.ToString();
                txtAddress2.Text = row.Cells["Address 2"].Value.ToString();
                txtAddress3.Text = row.Cells["Address 3"].Value.ToString();
                dtStartDt.Value = Convert.ToDateTime(row.Cells["Start Date"].Value.ToString());
                dtEndDt.Value = Convert.ToDateTime(row.Cells["End Date"].Value.ToString());

                FormMode("EDIT");
                tbctrlProject.SelectTab("tabPage1");
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            FormMode("RESET");
        }

    }
}
