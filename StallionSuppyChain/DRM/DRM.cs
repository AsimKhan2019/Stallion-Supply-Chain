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

namespace StallionSuppyChain.DRM
{
    public partial class DRM : Form
    {
        public DRM()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        public void GetPOID(string parameter1, string parameter2)
        {

            if (parameter2.ToString() == "ApprovedPO" || parameter2.ToString() == "DRAFTDRM")
            {
                btnSave.Enabled = true;
                btnEdit.Enabled = true;
            }
            GetSupplier();
            WorkerDetails();
            GetProjectCode();
            GetProjectCode1();
            WorkerDetails1();
            GetCurrency();

            TXtPOID.Text = parameter1;
            if (parameter2.ToString() == "DRAFTDRM" || parameter2.ToString() == "SUBMITTEDTAB")
            {
                DRMDetails(parameter1);
                LoadEditHistory( Convert.ToInt32(parameter1));
            }
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[LIST_RETRIEVEPODETAILS] '" + TXtPOID.Text + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    TxtPoNumber.Text = sqlReader["Request_No"].ToString();
                    txtMRMID.Text = sqlReader["MRMID"].ToString();
                    cboSupplier.SelectedValue = sqlReader["SupplierID"].ToString();
                    cboCostCode.SelectedValue = sqlReader["Currency_Code"].ToString();
                 
                   // txtGrossAmount.Text = sqlReader["Amount"].ToString();
                  
                    checkBox1.Checked = Convert.ToBoolean(sqlReader["Vat_Inclusive"].ToString());
                    GetMRMID(txtMRMID.Text);
                    LoadItemMaster(Convert.ToInt32(txtMRMID.Text), TxtPoNumber.Text);

                   
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void DRMDetails( string DRMID)
        {

            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[READ_DRMDetails] '" + DRMID.ToString() + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    txtDRMID.Text = DRMID.ToString();

                    TxtDRMNo.Text = sqlReader["DRMNo"].ToString();
                    CboRecievedBy.SelectedValue = sqlReader["RecievedBy"].ToString();
                    cboCertifiedby.SelectedValue = sqlReader["Certifiedby"].ToString();
                    InvDrNo.Text = sqlReader["InvoiceNO"].ToString();
                    checkBox3.Checked = Convert.ToBoolean(sqlReader["WithIssue"]);
                    txtremarks.Text = sqlReader["Remarks"].ToString();
                    TXtPOID.Text = sqlReader["Tran_PO_ID"].ToString();
                    cboStatus.Text = sqlReader["Status"].ToString();
                    txtGrossAmount.Text = sqlReader["GROSSAmount"].ToString();
                    txtdiscountAmount.Text = sqlReader["DiscountAmount"].ToString();

                    if (checkBox1.Checked == true)
                    {

                        txtNetOfVat.Text = sqlReader["GROSSAmount"].ToString();
                    }
                    else
                    {

                        txtNetOfVat.Text = Convert.ToDouble((Convert.ToDouble(sqlReader["GROSSAmount"]) * .10) + Convert.ToDouble(sqlReader["GROSSAmount"])).ToString();

                    }//(SUM(NetAmount) * .10 ) + SUM(NetAmount)


                    if (sqlReader["Status"].ToString() == "Submitted")
                    {
                        groupBox3.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox4.Enabled = false;


                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.ReadOnly = true;
                    }
                  
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();
            }
            catch (Exception ex)
            {


            }

        }


        private void LoadItemMaster(int MRMNO, string Request_No)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_DRMDetails]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MRMID", MRMNO);
                cmd.Parameters.AddWithValue("@Request_No", Request_No);


                
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        this.dataGridView1.Rows.Add(
                         "", 
                        row["DRMQuantity"].ToString(), 
                        row["Amount"].ToString(), 
                        row["DiscountAmount"].ToString(), 
                        row["DRMNetAmount"].ToString(),
                        row["CostCode"].ToString(),
                        row["PoBalance"].ToString(),
                        row["ITEMCODE"].ToString() ,
                        row["MRMQuantity"].ToString(), 
                        row["POQuantity"].ToString(), 
                        row["Category1"].ToString(), 
                        row["Category2"].ToString(), 
                        row["Category3"].ToString(), 
                        row["DRM_ItemID"].ToString() ,
                        row["CurrentDRMQTY"].ToString(),
                        row["TranPODetails_ID"].ToString(),
                        row["UOMID"].ToString(),
                        row["ItemMasterID"].ToString(),
                        row["CurrentDRMQTYNEW"].ToString(),
                        row["CostCode"].ToString(),
                        row["Amount"].ToString()
                        );
                    }

                }
            }
        }
        private void LoadEditHistory(int DRMID)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_DRM_EditHistory]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DRMID", DRMID);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView2.DataSource = dt;

                }
            }
        }

        public void GetMRMID(string MRMID)
        {
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[LIST_RetrieveMRMDetails] '" + MRMID.ToString() + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    cboProjectCode.SelectedValue = sqlReader["ProjectID"].ToString();

                    if (txtDRMID.Text == "")
                    {
                        TxtDRMNo.Text = GenerateDRMCode(cboProjectCode.SelectedValue.ToString());

                    }

                    cboDeliveredTo.SelectedValue = sqlReader["DeliveredtoID"].ToString();
                    TxtMRMNo.Text = sqlReader["MRMNo"].ToString();
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }
        private void DRM_Load(object sender, EventArgs e)
        {


         
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
            sql = "[dbo].[TRAN_GenDRMNumber] " + Convert.ToString(ProjectCode) + ",' ' ";
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
        private void GetCurrency()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[MSTR_LIST_CURRENCY]", con);
                    adapter.Fill(dt);

                    cboCostCode.DataSource = dt;
                    cboCostCode.DisplayMember = "CurrencyCode";
                    cboCostCode.ValueMember = "CurrencyCode";
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void GetProjectCode()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cboProjectCode.DataSource = dt;
                    cboProjectCode.DisplayMember = "ProjectName";
                    cboProjectCode.ValueMember = "ProjectID";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void GetProjectCode1()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cboDeliveredTo.DataSource = dt;
                    cboDeliveredTo.DisplayMember = "ProjectName";
                    cboDeliveredTo.ValueMember = "ProjectID";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void WorkerDetails()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);
                    cboCertifiedby.DataSource = dt;
                    cboCertifiedby.DisplayMember = "FULL_NAME";
                    cboCertifiedby.ValueMember = "WorkerID";
                }
                catch (Exception ex)
                {
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
                    CboRecievedBy.DataSource = dt;
                    CboRecievedBy.DisplayMember = "FULL_NAME";
                    CboRecievedBy.ValueMember = "WorkerID";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void GetSupplier()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_GET_SUPPLIER_CODE]", con);
                    adapter.Fill(dt);
                    cboSupplier.DataSource = dt;
                    cboSupplier.DisplayMember = "SUPPLIER";
                    cboSupplier.ValueMember = "SupplierID";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);

            if (dataGridView1.CurrentCell.ColumnIndex == 0) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {



            if (!char.IsControl(e.KeyChar)
     && !char.IsDigit(e.KeyChar)
     && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal total;
                decimal DrmNetAmount;
                var row = dataGridView1.Rows[e.RowIndex];
                decimal quantity = 0;
                decimal OLDquantity = 0;
                if (row.Cells[0].Value == null || row.Cells[0].Value == "")
                {
                    row.Cells[0].Value = 0;
                    quantity = 0;
                }
                else
                {
                    quantity = decimal.Parse(row.Cells[0].Value.ToString());

                }


                if (row.Cells[1].Value == null || row.Cells[1].Value == "")
                {
                    row.Cells[1].Value = 0;
                    OLDquantity = 0;
                }
                else
                {
                    OLDquantity = decimal.Parse(row.Cells[1].Value.ToString());

                }

                quantity = quantity + OLDquantity;


                decimal sellingPrice;
                if (row.Cells[2].Value == null || row.Cells[2].Value == "")
                {
                    row.Cells[2].Value = 0;
                    sellingPrice = 0;
                }
                else
                {
                    sellingPrice = decimal.Parse(row.Cells[2].Value.ToString());

                }



                decimal PoQuantity = 0;


                if (row.Cells[9].Value == null || row.Cells[9].Value == "")
                {
                    row.Cells[9].Value = 0;
                    PoQuantity = 0;
                }
                else
                {
                    PoQuantity = decimal.Parse(row.Cells[9].Value.ToString());
                }
                decimal CueentDRMQuantity = 0;

                CueentDRMQuantity = decimal.Parse(row.Cells[14].Value.ToString());
                if (quantity > PoQuantity)
                {
                        MessageBox.Show("DRM Quantity Cannot be Grater than PO Quantity", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                        row.Cells[0].Value = "";
                        return;
                }
                if (quantity < CueentDRMQuantity)
                {
                    MessageBox.Show("DRM quantity Cannot be lessthan than current DRM quantity", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    row.Cells[0].Value = "";
                    return;
                }
                decimal CurrentQuantity = 0;
                if (row.Cells[14].Value == null || row.Cells[9].Value == "")
                {
                    row.Cells[14].Value = 0;
                    CurrentQuantity = 0;
                }
                else
                {
                    PoQuantity = decimal.Parse(row.Cells[9].Value.ToString());
                }
                total = (quantity / PoQuantity) * sellingPrice;
                if (total < 0)
                {
                    total = 0;
                }
                row.Cells[3].Value = Convert.ToInt32( total);
                if (CurrentQuantity == 0)
                {
                    CurrentQuantity = quantity;
                }
               // DrmNetAmount = ((quantity - CurrentQuantity) * sellingPrice) - total;
                DrmNetAmount = (quantity / PoQuantity);
                if (DrmNetAmount < 0)
                {
                    DrmNetAmount = 0;
                }
                row.Cells[4].Value = Convert.ToInt32(DrmNetAmount);
            }
            catch (Exception ex)
            {


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {

                #region validation of required fields

                if (CboRecievedBy.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                }

                else if (cboCertifiedby.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (InvDrNo.Text == "")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }

                else if (txtremarks.Text == "")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }
               

                #endregion

                //string saveData = saveGridData();

                //if (saveData == "FAILED")
                //{
                //    MessageBox.Show("Please validate values on grid.", "Error", MessageBoxButtons.OK,
                //    MessageBoxIcon.Exclamation,
                //    MessageBoxDefaultButton.Button1);
                //    return;

                //}

 

                save("Saved");



                MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                DRMInquiry poInquiry = new DRMInquiry();
                poInquiry.GetUserID(TxtUserID.Text);
                poInquiry.Show();
                this.Hide();


            }
            catch (Exception ex)
            {
                string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                MessageBox.Show(errMessage);
            }
        }
        private void save( string status)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[TRAN_DRM_SAVE]";
            cmd.Parameters.Add("@DRMID", SqlDbType.VarChar, 50).Value = txtDRMID.Text;
            cmd.Parameters.Add("@Tran_PO_ID", SqlDbType.Int).Value = Convert.ToInt32(TXtPOID.Text.ToString());
            cmd.Parameters.Add("@DRMNo", SqlDbType.VarChar, 20).Value = TxtDRMNo.Text;
            cmd.Parameters.Add("@InvoiceNO", SqlDbType.VarChar, 15).Value = InvDrNo.Text;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = status;
            cmd.Parameters.Add("@RecievedBy", SqlDbType.Int).Value = CboRecievedBy.SelectedValue;
            cmd.Parameters.Add("@Certifiedby", SqlDbType.Int).Value = cboCertifiedby.SelectedValue;
            cmd.Parameters.Add("@WithIssue", SqlDbType.Bit).Value = Convert.ToBoolean(checkBox3.Checked);
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = txtremarks.Text;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = TxtUserID.Text;

            cmd.Connection = con;
            con.Open();
            string StringReturn;
            StringReturn = cmd.ExecuteScalar().ToString();
            //SAVE GRID
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                var DRMQTY = "";
                var OLDDRMQTY = "";


                string DiscountAmount  = "";
                string DRMNetAmount = "";
                string DRM_ItemID = "";
                string PO_LineID = "";
                string UOM = "";
                string ITEMID = "";
                string CostCode = "";
                decimal TotalRecieved = 0;

             DRMQTY =   dataGridView1.Rows[i].Cells[0].Value.ToString();
             OLDDRMQTY = dataGridView1.Rows[i].Cells[1].Value.ToString();
             DiscountAmount = dataGridView1.Rows[i].Cells[3].Value.ToString();
             DRMNetAmount = dataGridView1.Rows[i].Cells[4].Value.ToString();
             DRM_ItemID = dataGridView1.Rows[i].Cells[13].Value.ToString();
             PO_LineID = dataGridView1.Rows[i].Cells[15].Value.ToString();
             UOM = dataGridView1.Rows[i].Cells[16].Value.ToString();
             ITEMID = dataGridView1.Rows[i].Cells[17].Value.ToString();
             CostCode = dataGridView1.Rows[i].Cells[19].Value.ToString();

                 if (DRMQTY == "")
                 {
                     DRMQTY = "0";
                 }
                 if (OLDDRMQTY == "")
                 {
                     OLDDRMQTY = "0";
                 }
                 TotalRecieved =    Convert.ToDecimal( DRMQTY) +   Convert.ToDecimal( OLDDRMQTY);


                SaveGrid(DRM_ItemID.ToString(), StringReturn, TotalRecieved.ToString(), DiscountAmount.ToString(), DRMNetAmount.ToString(), TxtUserID.Text, daterecieved.Text, PO_LineID.ToString(), UOM.ToString(), cboDeliveredTo.SelectedValue.ToString(), ITEMID.ToString(), CostCode);
            }
        }
        private void SaveGrid(string DRM_ItemID, string DRMID, string DRMQTY, string DiscountAmount, string DRMNetAmount, string UserCreated, string DateRecieved, string PO_LineID, string UOMID, string ProjectID, string ITEMID, string CostCode)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[SAVE_DRM_Details]";
                cmd.Parameters.Add("@DRM_ItemID", SqlDbType.VarChar,50).Value =   DRM_ItemID;
                cmd.Parameters.Add("@DRMID", SqlDbType.Int).Value = DRMID;
                cmd.Parameters.Add("@DRMQTY", SqlDbType.Float).Value = DRMQTY;
                cmd.Parameters.Add("@DiscountAmount", SqlDbType.Float).Value = DiscountAmount;
                cmd.Parameters.Add("@DRMNetAmount", SqlDbType.Float).Value = DRMNetAmount;
                cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = UserCreated;
                cmd.Parameters.Add("@DateRecieved", SqlDbType.DateTime).Value = DateRecieved;
                cmd.Parameters.Add("@TranPODetails_ID", SqlDbType.Int).Value = PO_LineID;
                cmd.Parameters.Add("@ProjectIDDelevedTO", SqlDbType.Int).Value = ProjectID;
                cmd.Parameters.Add("@UOMID", SqlDbType.VarChar, 30).Value = UOMID;
                cmd.Parameters.Add("@ITEMID", SqlDbType.Int).Value = ITEMID;
                cmd.Parameters.Add("@CostCode", SqlDbType.VarChar, 30).Value = CostCode;

                
                
   
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }



        //Comment for 0 validation
        //private string saveGridData()
        //{
        //    string ret = "";
        //    try
        //    {
        //        for (int i = 0; i < dataGridView1.RowCount; i++)
        //        {
        //            var POQuantity = dataGridView1.Rows[i].Cells[0].Value;
        //            if (POQuantity == null || POQuantity == "")
        //            {
        //                ret = "FAILED";
        //                return ret;
        //            }
        //            else if (POQuantity.ToString() == "0")
        //            {
        //                ret = "FAILED";
        //                return ret;
        //            }
        //        }

        //        ret = "SUCCESS";
        //        return ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = "FAILED";
        //        return ret;
        //    }
        //}
        private void CboRecievedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboRecievedBy.Text == " -- Select-- ")
            {
                label31.Show();
            }
            else
            {
                label31.Hide();
            }
        }

        private void cboCertifiedby_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCertifiedby.Text == " -- Select-- ")
            {
                label19.Show();
            }
            else
            {
                label19.Hide();
            }
        }

        private void InvDrNo_TextChanged(object sender, EventArgs e)
        {

            if (InvDrNo.Text == "")
            {
                label20.Show();
            }
            else
            {
                label20.Hide();
            }
        }

        private void txtremarks_TextChanged(object sender, EventArgs e)
        {
            if (txtremarks.Text == "")
            {
                label22.Show();
            }
            else
            {
                label22.Hide();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                #region validation of required fields

                if (CboRecievedBy.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                }

                else if (cboCertifiedby.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (InvDrNo.Text == "")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }

                else if (txtremarks.Text == "")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }


                #endregion

                //string saveData = saveGridData();

                //if (saveData == "FAILED")
                //{
                //    MessageBox.Show("Please validate values on grid.", "Error", MessageBoxButtons.OK,
                //    MessageBoxIcon.Exclamation,
                //    MessageBoxDefaultButton.Button1);
                //    return;

                //}



                //string status = "";
                //if (cboStatus.Text == "Open")
                //{
                //    status = "Saved";
                //}



                save("Submitted");



                MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                DRMInquiry poInquiry = new DRMInquiry();
                poInquiry.GetUserID(TxtUserID.Text);
                poInquiry.Show();
                this.Hide();


            }
            catch (Exception ex)
            {
                string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                MessageBox.Show(errMessage);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
