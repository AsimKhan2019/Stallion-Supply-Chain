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
using StallionSuppyChain.MRI;

namespace StallionSuppyChain.Material_Releasing
{
    public partial class Material_Releasing_Main : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Material_Releasing_Main()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text == "")
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                return;
            }
            else if (cmbCurrencyCode.Text == " -- Select-- " || cmbIssuedBy.Text == " -- Select-- " ||
                cmbVerifiedBy.Text == " -- Select-- " || cmbCurrencyCode.Text == " -- Select-- ")
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                if (btnProceed.Text == "Proceed")
                {
                    TranSaveHeader("ADDNEW");
                }
                else if (btnProceed.Text == "Add More Item(s)")
                {

                    TranSaveHeader("ADDNEW");

                }
            }




           
        }

        private void TranSaveHeader(string Action)
        {
            string StringReturn = "";
            string status = "";
            if (Action == "SAVE" || Action == "ADDNEW")
            {
                status = "Saved";
            }
            if (Action == "SUBMIT")
            {
                status = "Submitted";
            }
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TRAN_SAVE_MRI_HEADER";
            cmd.Parameters.Add("@MRIID", SqlDbType.VarChar, 50).Value = txtMRIID.Text;
            cmd.Parameters.Add("@MRINO", SqlDbType.VarChar, 50).Value = txtMRINo.Text;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = status;
            cmd.Parameters.Add("@DateOfRelease", SqlDbType.DateTime).Value = txtDateReleased.Text;
            cmd.Parameters.Add("@IssuedFrom", SqlDbType.Int).Value = Convert.ToInt32( cmbIssueFrom.SelectedValue);
            cmd.Parameters.Add("@IssuedTo", SqlDbType.Int).Value = Convert.ToInt32(cmbIssueTo.SelectedValue);
            cmd.Parameters.Add("@RequestedBy", SqlDbType.Int).Value = Convert.ToInt32(cmbRequestedBy.SelectedValue);
            cmd.Parameters.Add("@IssuedBy", SqlDbType.Int).Value = Convert.ToInt32(cmbIssuedBy.SelectedValue);
            cmd.Parameters.Add("@VerifiedBy", SqlDbType.Int).Value = Convert.ToInt32(cmbVerifiedBy.SelectedValue);
            cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@WithIssue", SqlDbType.Bit).Value = Convert.ToBoolean(chkIssues.Checked); //Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = rtxtRemarks.Text; //Convert.ToBoolean(chkIssues.Checked); //Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar, 10).Value = txtReferenceNo.Text; //rtxtRemarks.Text; //Convert.ToBoolean(chkIssues.Checked); //Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 10).Value = cmbCurrencyCode.SelectedValue.ToString(); //rtxtRemarks.Text; //Convert.ToBoolean(chkIssues.Checked); //Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@TranType", SqlDbType.VarChar, 10).Value =  txttrantype.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                ItemMasterLookUp formTask = new ItemMasterLookUp();
                if (Action == "SAVE" || Action == "SUBMIT")
                {
                    if (Action == "SAVE")
                    {
                        MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    }
                    if (Action == "SUBMIT")
                    {
                        MessageBox.Show("Record Submitted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    }
                    Material_Releasing_Inquiry formTask2 = new Material_Releasing_Inquiry();
                    formTask2.GetUserID(TxtUserID.Text);
                    formTask2.GetTranType(txttrantype.Text);
                    
                    this.Dispose();
                    formTask2.Show();
                }
                if (Action == "ADDNEW")
                {
                    formTask.Test(StringReturn, txttrantype.Text.ToString());
                    formTask.GetUserID(TxtUserID.Text);
                    formTask.GetTranType(txttrantype.Text, cmbIssueFrom.SelectedValue.ToString());
                    this.Dispose();
                    formTask.Show();
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



        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
            // retrievedata(Convert.ToInt32(parameter1.ToString()));
            WorkerDetails1();
            WorkerDetails2();
            WorkerDetails3();
            GetCurrency();
            POApprovedBy();
            GetProjectCode();
           

        }
        private string GenerateDRMCode(string ProjectCode)
        {
            if (ProjectCode == "0")
            {
                ProjectCode = "0";
            }
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string SRet = "";
            sql = "[dbo].[TRAN_GenMRINumber] " + Convert.ToString(ProjectCode) + ",' ' ";
            try
            {
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
            }
            return SRet;

        }
        public void GetParameters(string parameter1, string parameter2)
        {
            POApprovedBy();
            GetProjectCode();
            cmbIssueFrom.SelectedValue = parameter1.ToString();
            cmbIssueTo.SelectedValue = parameter2.ToString();
            // retrievedata(Convert.ToInt32(parameter1.ToString()));

            if (txtMRIID.Text == "")
            {
                txtMRINo.Text = GenerateDRMCode(cmbIssueTo.SelectedValue.ToString());

            }

        }
        #region Required Field Validation

        private void txtReferenceNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenceNo.Text))
            {
                lblValidation1.Show();
            }
            else
            {
                lblValidation1.Hide();
            }
        }

        //private void cmbRequestedBy_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbRequestedBy.Text == " -- Select-- ")
        //    {
        //        lblValidation2.Show();
        //    }
        //    else
        //    {
        //        lblValidation2.Hide();
        //    }
        //}

        private void cmbIssuedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIssuedBy.Text == " -- Select-- ")
            {
                lblValidation3.Show();
            }
            else
            {
                lblValidation3.Hide();
            }
        }

        private void cmbVerifiedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVerifiedBy.Text == " -- Select-- ")
            {
                lblValidation4.Show();
            }
            else
            {
                lblValidation4.Hide();
            }
        }

        private void cmbCurrencyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCurrencyCode.Text == " -- Select-- ")
            {
                lblValidation5.Show();
            }
            else
            {
                lblValidation5.Hide();
            }
        }

        #endregion

        private void GetProjectCode()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cmbIssueFrom.DataSource = dt;
                    cmbIssueFrom.DisplayMember = "ProjectName";
                    cmbIssueFrom.ValueMember = "ProjectID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void POApprovedBy()
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);

                    cmbIssueTo.DataSource = dt;
                    cmbIssueTo.DisplayMember = "ProjectName";
                    cmbIssueTo.ValueMember = "ProjectID";

                }
                catch (Exception ex)
                {

                }
            }
        }
        public void GetTranType(string parameter1)
        {
            txttrantype.Text = parameter1;
            label1.Text = parameter1 + " #:";
        }


        public void GetAction(string status)
        {

            if (status == "ForApproval")
            {
                groupBox1.Enabled = true;
                btnEdit.Visible = true;
                btnSave.Visible = true;
                btnEdit.Enabled = false;
                btnSave.Visible = false;
                button1.Visible = true;
                button2.Visible = true;
             
            }
            if (status == "Approved")
            {
                groupBox2.Enabled = false;
                groupBox1.Enabled = false;
                groupBox4.Enabled = false;
                groupBox3.Enabled = false;
                dataGridView1.Columns[0].Visible = false;
                btnProceed.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = false;
            }
            if (status == "Rejected")
            {
                groupBox2.Enabled = false;
                groupBox1.Enabled = false;
                groupBox4.Enabled = false;
                groupBox3.Enabled = false;
                dataGridView1.Columns[0].Visible = false;
                btnProceed.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = false;
            }
        }
        private void Material_Releasing_Main_Load(object sender, EventArgs e)
        {
            
            
        }

        private void LoadItemMaster(int MRMNO)
        {


            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadMRIDetails]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MRIID", MRMNO);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                  //  dataGridView1.Columns[13].Width = 0;
                }
            }
            //  dataGridView2.Columns[17].Width = 0; 



        }

        public void Retrieverequest(string MRIID)
        {

            WorkerDetails1();
            WorkerDetails2();
            WorkerDetails3();
            GetCurrency();
            POApprovedBy();
            GetProjectCode();

            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[READ_MRI_DETAILS] '" + MRIID.ToString() + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    txtMRIID.Text = MRIID;
                    txtMRINo.Text = sqlReader["MRINO"].ToString();
                    txtReferenceNo.Text = sqlReader["ReferenceNo"].ToString();
                   txtDateReleased.Text = sqlReader["DateOfRelease"].ToString();
                   dateTimePicker1.Text = sqlReader["DateCreated"].ToString();
                   cmbIssueFrom.SelectedValue = sqlReader["IssuedFrom"].ToString();
                   cmbIssueTo.SelectedValue = sqlReader["IssuedTo"].ToString();
                   cmbRequestedBy.SelectedValue = sqlReader["RequestedBy"].ToString();
                   cmbIssuedBy.SelectedValue = sqlReader["IssuedBy"].ToString();
                   cmbVerifiedBy.SelectedValue = sqlReader["VerifiedBy"].ToString();
                   cboStatus.Text = sqlReader["Status"].ToString();
                   rtxtRemarks.Text = sqlReader["Remarks"].ToString();
                   chkIssues.Checked = Convert.ToBoolean(sqlReader["WithIssue"].ToString());
                   cmbCurrencyCode.SelectedValue = sqlReader["CurrencyCode"].ToString();

                   btnSave.Enabled = true;

                   btnEdit.Enabled = true;
                   if (sqlReader["Status"].ToString() == "Submitted")
                   {
                       groupBox2.Enabled = false;
                       groupBox4.Enabled = false;
                       groupBox3.Enabled = false;
                       dataGridView1.ReadOnly = true;
                       dataGridView1.Columns[0].Visible = false;
                       groupBox1.Enabled = false;

                   }
                   btnProceed.Text = "Add More Item(s)";
                   LoadItemMaster(Convert.ToInt32(MRIID));

                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();
            }
            catch (Exception ex)
            {


            }

        }
        private void GetCurrency()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[MSTR_LIST_CURRENCY]", con);
                    adapter.Fill(dt);

                    cmbCurrencyCode.DataSource = dt;
                    cmbCurrencyCode.DisplayMember = "CurrencyCode";
                    cmbCurrencyCode.ValueMember = "CurrencyCode";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error :" + ex.StackTrace + " " + ex.InnerException);
                }
            }
        }

        private void WorkerDetails1()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);

                    //REQUESTED BY
                    cmbRequestedBy.DataSource = dt;
                    cmbRequestedBy.DisplayMember = "FULL_NAME";
                    cmbRequestedBy.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error :" + ex.StackTrace + " " + ex.InnerException);
                }
            }
        }

        private void WorkerDetails2()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);



                    //ISSUED BY
                    cmbIssuedBy.DataSource = dt;
                    cmbIssuedBy.DisplayMember = "FULL_NAME";
                    cmbIssuedBy.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error :" + ex.StackTrace + " " + ex.InnerException);
                }
            }
        }

        private void WorkerDetails3()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);

                    //VERIFIED BY
                    cmbVerifiedBy.DataSource = dt;
                    cmbVerifiedBy.DisplayMember = "FULL_NAME";
                    cmbVerifiedBy.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error :" + ex.StackTrace + " " + ex.InnerException);
                }
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtReferenceNo.Text = "";
                WorkerDetails1();
                WorkerDetails2();
                WorkerDetails3();
                GetCurrency();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.StackTrace + " " + ex.InnerException);
            }
        }

        private void cmbRequestedBy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbRequestedBy.Text == " -- Select-- ")
            {
                lblValidation2.Show();
            }
            else
            {
                lblValidation2.Hide();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            TranSaveHeader("SAVE");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["BtnRemove"].Index && e.RowIndex >= 0)
            {


                var index = dataGridView1.CurrentRow.Cells[13].Value.ToString();


                TranDeleteItemInMRM(Convert.ToInt32(index));

            }

        }
        private void TranDeleteItemInMRM(int TranLineNo)
        {

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_DELETE_MRIDetail";
            cmd.Parameters.Add("@TranLineNo", SqlDbType.Int).Value = TranLineNo;


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


            LoadItemMaster(Convert.ToInt32(txtMRIID.Text));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            TranSaveHeader("SUBMIT");
        }

        private void Material_Releasing_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

             

            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_SaveInventoryTransfer";
            cmd.Parameters.Add("@MRIID", SqlDbType.Int).Value = Convert.ToInt32(txtMRIID.Text);
            
            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                if (StringReturn == "Rejected")
                {
                    MessageBox.Show("This Request is already rejected!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("Request has been approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Material_verifyForRelease formTask = new Material_verifyForRelease();
                    formTask.GetUserID(TxtUserID.Text);
                    formTask.GetTranType( txttrantype.Text);
                    this.Dispose();
                    formTask.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {



            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_RejectInventoryTransfer";
            cmd.Parameters.Add("@MRIID", SqlDbType.Int).Value = Convert.ToInt32(txtMRIID.Text);

            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                if (StringReturn == "Approved")
                {
                    MessageBox.Show("This Request is already approved!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("Request has been rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Material_verifyForRelease formTask = new Material_verifyForRelease();
                    formTask.GetUserID(TxtUserID.Text);
                    formTask.GetTranType(txttrantype.Text);
                    this.Dispose();
                    formTask.Show();
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
}