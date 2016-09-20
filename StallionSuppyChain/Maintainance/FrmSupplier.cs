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
namespace StallionSuppyChain.Maintainance
{
    public partial class FrmSupplier : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public FrmSupplier()
        {
            InitializeComponent();
        }

        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            //Populate grid view on load
            LoadSupplier("", 99);
        }

        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }

        private void FormMode(string Action)
        {

            if (Action == "NEW")
            {
                txtSupCode.Enabled = true;
                txtSupNm.Enabled = true;
                txtTinNo.Enabled = true;
                txtVatNo.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtAddress3.Enabled = true;
                txtTelNo1.Enabled = true;
                txtTelNo2.Enabled = true;
                txtFaxNo.Enabled = true;
                cbostatus.Enabled = true;
                btnSave.Enabled = true;
                BtnReset.Enabled = true;
                TxtContactPerson.Enabled = true;
            }
            else if (Action == "EDIT")
            {
                btnEdit.Enabled = true;
                BtnReset.Enabled = true;
                txtSupCode.Enabled = false;
                txtSupNm.Enabled = true;
                txtTinNo.Enabled = true;
                txtVatNo.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtAddress3.Enabled = true;
                txtTelNo1.Enabled = true;
                txtTelNo2.Enabled = true;
                txtFaxNo.Enabled = true;
                cbostatus.Enabled = true;
                TxtContactPerson.Enabled = true;

            }
            else if (Action == "RESET")
            {
                btnSave.Enabled = false;
                BtnReset.Enabled = false;
                txtSupCode.Text = "";
                txtSupNm.Text = "";
                txtTinNo.Text = "";
                txtVatNo.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtAddress3.Text = "";
                txtTelNo1.Text = "";
                txtTelNo2.Text = "";
                txtFaxNo.Text = "";
                cboSearchBy.Enabled = true;
                txtSearch.Text = "";
                TxtContactPerson.Text = "";
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            FormMode("NEW");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtSupCode.Enabled = false;

            if (txtVatNo.Text == "")
            {
                txtVatNo.Text = "0";
            }
            if (txtSupCode.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
            else if (TxtContactPerson.Text == "") 
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtContactPerson.Focus();
            }
            else if (txtSupNm.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtSupNm.Focus();
            }
            else if (txtTinNo.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtTinNo.Focus();
            }
            else if (cbostatus.SelectedItem == null)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cbostatus.Focus();
            }
            else
            {

                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_UPDATESUPPLIER";
                cmd.Parameters.Add("@SupplierCode", SqlDbType.VarChar, 50).Value = txtSupCode.Text;
                cmd.Parameters.Add("@SupplierName", SqlDbType.VarChar, 50).Value = txtSupNm.Text;
                cmd.Parameters.Add("@VatNumber", SqlDbType.VarChar, 50).Value = txtVatNo.Text;
                cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 50).Value = txtTinNo.Text;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = txtAddress1.Text;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = txtAddress2.Text;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 50).Value = txtAddress3.Text;
                cmd.Parameters.Add("@Tel1", SqlDbType.VarChar, 50).Value = txtTelNo1.Text;
                cmd.Parameters.Add("@Tel2", SqlDbType.VarChar, 50).Value = txtTelNo2.Text;
                cmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 50).Value = txtFaxNo.Text;
                cmd.Parameters.Add("@RecordStatus", SqlDbType.VarChar, 50).Value = cbostatus.SelectedItem;
                cmd.Parameters.Add("@UserModified", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
                cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 50).Value = TxtContactPerson.Text;
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
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtVatNo.Text == "")
            {
                txtVatNo.Text = "0";
            }
            if (txtSupCode.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtSupCode.Focus();
            }

            else if (TxtContactPerson.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtContactPerson.Focus();
            }
                
            else if (txtSupNm.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtSupNm.Focus();
            }
            else if (txtTinNo.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtTinNo.Focus();
            }
            else if (cbostatus.SelectedItem == null)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cbostatus.Focus();
            }
            else
            {

                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_SAVESUPPLIER";
                cmd.Parameters.Add("@SupplierCode", SqlDbType.VarChar, 50).Value = txtSupCode.Text;
                cmd.Parameters.Add("@SupplierName", SqlDbType.VarChar, 50).Value = txtSupNm.Text;
                cmd.Parameters.Add("@VatNumber", SqlDbType.VarChar, 50).Value = txtVatNo.Text ;
                cmd.Parameters.Add("@TinNumber", SqlDbType.VarChar, 50).Value =  txtTinNo.Text ;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = txtAddress1.Text;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = txtAddress2.Text;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 50).Value = txtAddress3.Text;
                cmd.Parameters.Add("@Tel1", SqlDbType.VarChar, 50).Value = txtTelNo1.Text;
                cmd.Parameters.Add("@Tel2", SqlDbType.VarChar, 50).Value = txtTelNo2.Text;
                cmd.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 50).Value = txtFaxNo.Text;
                cmd.Parameters.Add("@RecordStatus", SqlDbType.VarChar, 50).Value = cbostatus.SelectedItem;
                cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
                cmd.Parameters.Add("@SupplierID", SqlDbType.Int ).Value = 0;
                cmd.Parameters["@SupplierID"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 50).Value = TxtContactPerson.Text;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int ret = (int)cmd.Parameters["@SupplierID"].Value;
                    if (ret == 0)
                    {
                        MessageBox.Show("Supplier code already exists", "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                        txtSupCode.Focus();
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
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Populate grid view
            txtSearch.Text = "";
            LoadSupplier("", 99);
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
                LoadSupplier(txtSearch.Text, cboSearchBy.SelectedIndex);
            }
        }

        private void LoadSupplier(string SearchParam, int SearchMode)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_SUPPLIERDETAILS]", con);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchParam", SearchParam);
                cmd.Parameters.AddWithValue("@SearchMode", SearchMode);

                con.Open();
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    grdSupplier.DataSource = dt;
                }
            }
        }

        private void ResetForm()
        {
            btnSave.Enabled = true;
            BtnReset.Enabled = false;

            txtSupCode.Text = "";
            txtSupNm.Text = "";
            txtTinNo.Text = "";
            txtVatNo.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtTelNo1.Text = "";
            txtTelNo2.Text = "";
            txtFaxNo.Text = "";
            TxtContactPerson.Text = "";
            cboSearchBy.Enabled = true;
            txtSearch.Text = "";
        }

        private void grdSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.grdSupplier.Rows[e.RowIndex];
                txtSupCode.Text = row.Cells["Supplier Code"].Value.ToString();
                txtSupNm.Text = row.Cells["Supplier Name"].Value.ToString();
                txtTinNo.Text = row.Cells["TIN No."].Value.ToString();
                txtVatNo.Text = row.Cells["VAT No."].Value.ToString();
                txtAddress1.Text = row.Cells["Address 1"].Value.ToString();
                txtAddress2.Text = row.Cells["Address 2"].Value.ToString();
                txtAddress3.Text = row.Cells["Address 3"].Value.ToString();
                txtTelNo1.Text = row.Cells["Telephone No. 1"].Value.ToString();
                txtTelNo2.Text = row.Cells["Telephone No. 2"].Value.ToString();
                txtFaxNo.Text = row.Cells["Fax No."].Value.ToString();
                cbostatus.Text = row.Cells["Status"].Value.ToString();
                cbostatus.Text = row.Cells["Status"].Value.ToString();
                TxtContactPerson.Text = row.Cells["ContactPerson"].Value.ToString();
                FormMode("EDIT");
                tbctrlSupplier.SelectTab("tabPage1");
            }
        }

        private void txtTinNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtVatNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            FormMode("RESET");
        }

        private void txtTinNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelNo1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTelNo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtFaxNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

    }
}
