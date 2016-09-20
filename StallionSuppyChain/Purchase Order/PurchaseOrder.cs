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
using StallionSuppyChain.Purchase_Order;
using StallionSuppyChain.Reports;

namespace StallionSuppyChain.Procurement
{
    public partial class PurchaseOrder : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public PurchaseOrder()
        {
            InitializeComponent();
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "MM/dd/yyyy";
            txtDate.Format = DateTimePickerFormat.Custom;
            txtDate.CustomFormat = "MM/dd/yyyy";
             
        }


        private void LoadItemMaster(int MRMNO)
        {

            //e2
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadMaterialRequestDetail]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MRMID", MRMNO);
 
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    //dataGridView1.DataSource = dt;

                    foreach (DataRow row in dt.Rows)
                    {

                        this.dataGridView1.Rows.Add(row["POQuantity"].ToString(), row["Amount"].ToString(), row["DicountAmount"].ToString(), row["NetAmount"].ToString(),
                        row["CosDescription"].ToString(), row["ITEMCODE"].ToString(), row["MRM Quantity"].ToString(), row["MRM UOM"].ToString(), row["Mstr UOM"].ToString()
                        , row["Category1"].ToString(), row["Category2"].ToString(), row["Category3"].ToString(), row["ITEMDESCRIPTION"].ToString()
                        , row["ITEMDESCRIPTION2"].ToString(), row["ITEMSPEC1"].ToString(), row["ITEMSPEC2"].ToString(), row["ITEMSPEC3"].ToString(), row["TranLineNo"].ToString()
                        );


                    } 

                }
            }
          
            dataGridView1.Columns[17].Visible = false;


        }
        private void LoadItemMasterSubmitted(int MRMNO)
        {

            //e2
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadMaterialRequestDetail]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MRMID", MRMNO);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;

                    

                }
            }

            dataGridView1.Columns[17].Visible = false;


        }
        private void LoadItemMasterPO(int MRMNO,string Requestno)
        {
 
 

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadPOMaterialRequestDetail]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MRMID", MRMNO);
                cmd.Parameters.AddWithValue("@Request_No", Requestno);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        this.dataGridView1.Rows.Add(row["POQuantity"].ToString(), row["Amount"].ToString(), row["DicountAmount"].ToString(), row["NetAmount"].ToString(),
                        row["CosDescription"].ToString(), row["ITEMCODE"].ToString(), row["MRM Quantity"].ToString(), row["MRM UOM"].ToString(), row["Mstr UOM"].ToString()
                        , row["Category1"].ToString(), row["Category2"].ToString(), row["Category3"].ToString(), row["ITEMDESCRIPTION"].ToString()
                        , row["ITEMDESCRIPTION2"].ToString(), row["ITEMSPEC1"].ToString(), row["ITEMSPEC2"].ToString(), row["ITEMSPEC3"].ToString(), row["TranLineNo"].ToString()
                        );
                    } 
                     
                    
                }
            }
            

            dataGridView1.Columns[17].Visible = false;

        }

        private void retrievedata(int MRMNO)
        {
            txtDate.Format = DateTimePickerFormat.Custom;
            txtDate.CustomFormat = "MM/dd/yyyy";
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[LIST_RetrieveMRMDetails] '" + MRMNO.ToString() + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    cboProjectCode.SelectedValue = sqlReader.GetValue(16).ToString();
                    txtMRMNo.Text = sqlReader.GetValue(1).ToString();
                    cboCategoryType.SelectedValue = sqlReader.GetValue(3).ToString();
                    ckpurchase.Checked = Convert.ToBoolean(sqlReader.GetValue(12));
                    txtDate.Text = sqlReader.GetValue(14).ToString();
                    dateTimePicker4.Text = sqlReader.GetValue(13).ToString();
                    cborquestedby.SelectedValue = sqlReader.GetValue(5).ToString();
                    cboRecomendedby.SelectedValue = sqlReader.GetValue(7).ToString();
                    cboApproveby.SelectedValue = sqlReader.GetValue(17).ToString();
                    cboDeleveredto.SelectedValue = sqlReader.GetValue(10).ToString();
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }


            if (txtPOID.Text == "")
            {
                dataGridView1.Columns[4].HeaderText = "Cost Code";
                LoadItemMaster(MRMNO);
                txtMRMID.Text = MRMNO.ToString();
                textBox3.Text = GenenratePONumber().ToString();
            }

        }
        private void retrievePOdata(int POID)
        {
            
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[LIST_RETRIEVEPODETAILS] '" + POID.ToString() + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    comboBox6.SelectedValue = sqlReader.GetValue(3).ToString();
                    txtPOID.Text = sqlReader.GetValue(0).ToString();
                    txtPOID.Text = sqlReader.GetValue(0).ToString();
                    textBox3.Text = sqlReader.GetValue(1).ToString();
 
                    ListPOApprovedBy.SelectedValue = sqlReader.GetValue(7).ToString();
                    txtMRMID.Text = sqlReader.GetValue(2).ToString();
                    comboBox5.SelectedValue = sqlReader.GetValue(5).ToString();
                    comboBox10.SelectedValue = sqlReader.GetValue(9).ToString();
                    comboBox9.SelectedValue = sqlReader.GetValue(11).ToString();
                    txtDPRate.Text = sqlReader.GetValue(13).ToString();
                    txtDPAmount.Text = sqlReader.GetValue(14).ToString();
                    txtForex.Text = sqlReader.GetValue(15).ToString();
                    txtDiscountAmount.Text = sqlReader.GetValue(18).ToString();
                    txtPOAmount.Text = sqlReader.GetValue(19).ToString();
 
                    dateForexDate.Text = sqlReader.GetValue(20).ToString();
              
                    txtNEtOfVat.Text = sqlReader.GetValue(24).ToString();
                    comboBox11.SelectedValue = sqlReader.GetValue(25).ToString();
                    txtRemarks.Text = sqlReader.GetValue(26).ToString();
                    checkBox1.Checked = Convert.ToBoolean(sqlReader.GetValue(21));
                    checkBox2.Checked = Convert.ToBoolean(sqlReader.GetValue(16));
                    checkBox3.Checked = Convert.ToBoolean(sqlReader.GetValue(17));

                    comboBox12.Text = sqlReader.GetValue(27).ToString();


                    if (sqlReader.GetValue(27).ToString() == "Submitted")
                    {


                        btnSave.Enabled = false;
                        comboBox6.Enabled = false;
                        btnEdit.Enabled = false;
                        groupBox3.Enabled = false;
                        //dataGridView1.Enabled = false;
                        txtDiscountAmount.Visible = true;
                        txtPOAmount.Visible = true;
                        txtNEtOfVat.Visible = true;
                        label20.Visible = true;
                        label19.Visible = true;
                        label25.Visible = true;
                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                    }


                    if (sqlReader.GetValue(27).ToString() == "Approved" || sqlReader.GetValue(27).ToString() == "Rejected")
                    {


                        btnSave.Enabled = false;

                        btnEdit.Enabled = false;
                        groupBox3.Enabled = false;
                        //dataGridView1.Enabled = false;
                        txtDiscountAmount.Visible = true;
                        txtPOAmount.Visible = true;
                        txtNEtOfVat.Visible = true;
                        label20.Visible = true;
                        label19.Visible = true;
                        label25.Visible = true;
                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        if (sqlReader.GetValue(27).ToString() == "Approved")
                        {
                            btnPrint.Visible = true;
                        }


                        btnEdit.Visible = true;

                        btnSave.Visible = true;



                        button1.Visible = false;
                        button2.Visible = false;



                        textBox1.Visible = true;

                        textBox1.Visible = true;
                        label22.Visible = true;

                        groupBox4.Enabled = false;
                        comboBox6.Enabled = false;
                    }
             

                    txtDiscountAmount.Text = sqlReader.GetValue(28).ToString();
                    txtPOAmount.Text = sqlReader.GetValue(29).ToString();
                    txtNEtOfVat.Text = sqlReader.GetValue(30).ToString();

                    textBox1.Text = sqlReader.GetValue(31).ToString();


                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }

            
        }

        
        private string GenenratePONumber()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string SRet = "";
            sql = "[dbo].[TRAN_GenPONumber] "; 
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
                MessageBox.Show(ex.ToString());
            }
            return SRet;
        }

        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
  
            if (txtPOID.Text == "")
            {

                ProcurementType();
   
                GetSupplier();
    
                GetCurrency();
                GetPaymentTerms();
                GetDeliveryTerms();
            
                POApprovedBy();
                comboBox6.SelectedValue = cboCategoryType.SelectedValue;
            }
        }







        #region populate value of dropdow list

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

        private void GetProjectCodeMRM()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cboDeleveredto.DataSource = dt;
                    cboDeleveredto.DisplayMember = "ProjectName";
                    cboDeleveredto.ValueMember = "ProjectID";

                }
                catch (Exception ex)
                {

                }
            }
        }
        

        private void ProcurementType()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_PROCUREMENT_TYPE]", con);
                    adapter.Fill(dt);
                    comboBox6.DataSource = dt;
                    comboBox6.DisplayMember = "Description";
                    comboBox6.ValueMember = "ProcurementID";


                }
                catch (Exception ex)
                {

                }
            }
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
        private void WorkerDetails()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);

                    //REQUESTED BY
                    cborquestedby.DataSource = dt;
                    cborquestedby.DisplayMember = "FULL_NAME";
                    cborquestedby.ValueMember = "WorkerID";

                   

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

                    cboRecomendedby.DataSource = dt;
                    cboRecomendedby.DisplayMember = "FULL_NAME";
                    cboRecomendedby.ValueMember = "WorkerID";



                }
                catch (Exception ex)
                {

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


                    ////APPROVED BY
                    cboApproveby.DataSource = dt;
                    cboApproveby.DisplayMember = "FULL_NAME";
                    cboApproveby.ValueMember = "WorkerID";



                }
                catch (Exception ex)
                {

                }
            }
        }
         //////RECOMMENDED BY


        private void GetSupplier()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_GET_SUPPLIER_CODE]", con);
                    adapter.Fill(dt);

                    comboBox5.DataSource = dt;
                    comboBox5.DisplayMember = "SUPPLIER";
                    comboBox5.ValueMember = "SupplierID";
                }

                catch (Exception ex)
                {
                }
            }

        }

        private void GetProcurementCategory()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_GET_PROCUREMENT_CATEGORY]", con);
                    adapter.Fill(dt);

                    cboCategoryType.DataSource = dt;
                    cboCategoryType.DisplayMember = "Category";
                    cboCategoryType.ValueMember = "ProcurementCategoryID";
                }
                catch (Exception ex)
                {
                }
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

                    comboBox11.DataSource = dt;
                    comboBox11.DisplayMember = "CurrencyCode";
                    comboBox11.ValueMember = "CurrencyCode";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void GetPaymentTerms()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_GET_PAYMENT_TERMS]", con);
                    adapter.Fill(dt);

                    comboBox10.DataSource = dt;
                    comboBox10.DisplayMember = "PaymentCode";
                    comboBox10.ValueMember = "PaymentTermsID";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void GetDeliveryTerms()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_GET_DELIVERY_TERMS]", con);
                    adapter.Fill(dt);

                    comboBox9.DataSource = dt;
                    comboBox9.DisplayMember = "Delivery";
                    comboBox9.ValueMember = "DeliveryTermsID";
                }
                catch (Exception ex)
                {
                }
            }
        }

        //POApprovedBy


        private void POApprovedBy()
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);

                    ListPOApprovedBy.DataSource = dt;
                    ListPOApprovedBy.DisplayMember = "FULL_NAME";
                    ListPOApprovedBy.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {

                }
            }
        }

         

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

            //            PO - Purchase Order
            //SA - Supply Agreement
            //WO - Work Order

            if (comboBox6.Text == " -- Select-- ")
            {
                label11.Text = "        :";
                label50.Show();
            }
            if (comboBox6.Text == "PO - Purchase Order")
            {
                label11.Text = "PO # :";
                label50.Hide();
            }
            else if (comboBox6.Text == "SA - Supply Agreement")
            {
                label11.Text = "SA # :";
                label50.Hide();
            }
            else if (comboBox6.Text == "WO - Work Order")
            {
                label11.Text = "WO# :";
                label50.Hide();
            }

        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            Transaction("Saved");
        }

        private void Transaction(string Action)
        {


            try
            {

                #region validation of required fields

                if (comboBox6.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                }
             
                else if (ListPOApprovedBy.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (comboBox5.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (comboBox10.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (comboBox9.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
               
                
               
                
                else if (comboBox11.Text == " -- Select-- ")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (txtRemarks.Text == "")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    return;
                }



                #endregion

                string saveData = saveGridData();

                if (saveData == "FAILED")
                {
                    MessageBox.Show("Please validate values on grid.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                    
                }


                if (txtForex.Text == "")
                {
                    txtForex.Text = "0";

                }

                 
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_SAVE_PO"; 
                cmd.Parameters.Add("@MRMID", SqlDbType.Int).Value = Convert.ToInt32(txtMRMID.Text);
                cmd.Parameters.Add("@RequestNO", SqlDbType.VarChar, 20).Value = textBox3.Text.ToString();
                cmd.Parameters.Add("@ProcurementID", SqlDbType.VarChar, 4).Value = comboBox6.SelectedValue.ToString();
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = Action; 
                cmd.Parameters.Add("@WorkerID", SqlDbType.Int).Value = Convert.ToInt32(ListPOApprovedBy.SelectedValue);
                cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(comboBox5.SelectedValue);
                cmd.Parameters.Add("@PaymentTermsID", SqlDbType.Int).Value = Convert.ToInt32(comboBox10.SelectedValue);
                cmd.Parameters.Add("@DeliveryTermsID", SqlDbType.Int).Value = Convert.ToInt32(comboBox9.SelectedValue);
                cmd.Parameters.Add("@DP_Rate", SqlDbType.Float).Value = txtDPRate.Text;
                cmd.Parameters.Add("@DP_Amount", SqlDbType.Float).Value = txtDPAmount.Text;
                cmd.Parameters.Add("@Forex", SqlDbType.Float).Value = txtForex.Text;
                cmd.Parameters.Add("@Local_Purchase", SqlDbType.Bit).Value = checkBox2.CheckState;
                cmd.Parameters.Add("@Vat_Inclusive", SqlDbType.Bit).Value = checkBox3.CheckState;
                cmd.Parameters.Add("@Forex_Date", SqlDbType.DateTime).Value = dateForexDate.Value;
               
                cmd.Parameters.Add("@Currency_Code", SqlDbType.VarChar, 3).Value = comboBox11.SelectedValue.ToString();
                cmd.Parameters.Add("@Remarks", SqlDbType.Text).Value = txtRemarks.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                POInquiry poInquiry = new POInquiry();
                poInquiry.Show();
                this.Hide();


            }
            catch (Exception ex)
            {
                string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                MessageBox.Show(errMessage);
            }

        }
        private string saveGridData()
        {

            string ret = "";

            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    var POQuantity = dataGridView1.Rows[i].Cells[0].Value;
                    var POAmount = dataGridView1.Rows[i].Cells[1].Value;
                    var DiscountAmount = dataGridView1.Rows[i].Cells[2].Value;
                    var NetAmount = dataGridView1.Rows[i].Cells[3].Value;
                    var CostCode = dataGridView1.Rows[i].Cells[4].Value;
                    var TranLineNo = dataGridView1.Rows[i].Cells[17].Value;

                    if (POQuantity == null
                        || POAmount == null
                        || DiscountAmount == null
                        || NetAmount == null
                        || TranLineNo == null)
                    {
                        ret = "FAILED";
                        return ret;
                    }
                    else if (POQuantity.ToString() == "0"
                        || POAmount.ToString() == "0"
                        || NetAmount.ToString() == "0"
                         || CostCode.ToString() == " -- Select-- "
                         

                        )
                    {
                        ret = "FAILED";
                        return ret;
                    }


                     if (ValidateNumber(POQuantity.ToString()) ==  false)
                    {
                        ret = "FAILED";
                        return ret;
                    }

                     if (ValidateNumber(POAmount.ToString()) == false)
                     {
                         ret = "FAILED";
                         return ret;
                     }
                     if (ValidateNumber(DiscountAmount.ToString()) == false)
                     {
                         ret = "FAILED";
                         return ret;
                     }
                     if (ValidateNumber(NetAmount.ToString()) == false)
                     {
                         ret = "FAILED";
                         return ret;
                     }


                     SaveGrid(POQuantity.ToString(), POAmount.ToString(), DiscountAmount.ToString(), NetAmount.ToString(), CostCode.ToString(), Convert.ToInt32(TranLineNo.ToString()), textBox3.Text);
                }

                ret = "SUCCESS";
                return ret;
            }
            catch (Exception ex)
            {
                ret = "FAILED";
                return ret;
            }
        }


        private void SaveGrid(string POQuantity, string POAmount, string DiscountAmount, string NetAmount, string CostCode, int TranLineNo, string Request_No)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_SAVE_PO_DETAILS";

                cmd.Parameters.Add("@TranLineNo", SqlDbType.Int).Value = TranLineNo;
                cmd.Parameters.Add("@MRMNo", SqlDbType.VarChar, 20).Value = txtMRMNo.Text;
                cmd.Parameters.Add("@POQuantity", SqlDbType.Float).Value = POQuantity;
                cmd.Parameters.Add("@Amount", SqlDbType.Float).Value = POAmount;
                cmd.Parameters.Add("@DicountAmount", SqlDbType.Float).Value = DiscountAmount;
                cmd.Parameters.Add("@NetAmount", SqlDbType.Float).Value = NetAmount;
                cmd.Parameters.Add("@CostCode", SqlDbType.VarChar, 500).Value = CostCode;
                cmd.Parameters.Add("@Request_No", SqlDbType.VarChar, 25).Value = Request_No;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }
        private void Clear_Request( )
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_ClearPODetails";
                cmd.Parameters.Add("@MRMNo", SqlDbType.VarChar, 20).Value = txtMRMNo.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        #region validation of required fields


        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox12.Text == " -- Select-- ")
            {
                label49.Show();
            }
            else
            {
                label49.Hide();
            }
        }

        private void ListPOApprovedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListPOApprovedBy.Text == " -- Select-- ")
            {
                label31.Show();
            }
            else
            {
                label31.Hide();
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == " -- Select-- ")
            {
                label32.Show();
            }
            else
            {
                label32.Hide();
            }
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox10.Text == " -- Select-- ")
            {
                label33.Show();
            }
            else
            {
                label33.Hide();
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text == " -- Select-- ")
            {
                label34.Show();
            }
            else
            {
                label34.Hide();
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox11.Text == " -- Select-- ")
            {
                label45.Show();
            }
            else
            {
                label45.Hide();
            }
        }

        private void txtDPRate_TextChanged(object sender, EventArgs e)
        {


            if (txtDPRate != null)
            {
                txtDPRate.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
           
        }

        private void txtDPAmount_TextChanged(object sender, EventArgs e)
        {




            if (txtDPAmount != null)
            {
                txtDPAmount.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
            
        }

        private void txtForex_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void txtPOAmount_TextChanged(object sender, EventArgs e)
        {
             
        }

       

        private void txtGrossOfVat_TextChanged(object sender, EventArgs e)
        { 
        }

        private void txtRemarks_TextChanged(object sender, EventArgs e)
        {
            if (txtRemarks.Text == "")
            {
                label44.Show();
            }
            else
            {
                label44.Hide();
            }
        }

        #endregion

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ProcurementType();
            GetSupplier();
            GetCurrency();
            GetPaymentTerms();
            GetDeliveryTerms();
            POApprovedBy();
            txtDPRate.Text = "";
            txtDPAmount.Text = "";
            txtForex.Text = "";
            txtDiscountAmount.Text = "";
            txtPOAmount.Text = "";
            txtNEtOfVat.Text = "";
            txtRemarks.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
        }


        public void RetrievePO(string parameter1)
        {
            GetProjectCode();
            GetProjectCodeMRM();
            ProcurementType();
            WorkerDetails();
            WorkerDetails1();
            WorkerDetails2();
            GetSupplier();
            GetProcurementCategory();
            GetCurrency();
            GetPaymentTerms();
            GetDeliveryTerms();

            retrievedata(Convert.ToInt32(parameter1));
            comboBox6.SelectedValue = cboCategoryType.SelectedValue;
        }

        private void PurchaseOrder_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (stat.Text != "")
            {



                PoForApproval poInquiry = new PoForApproval();
                poInquiry.GetUserID(TxtUserID.Text);
                poInquiry.Show();
                this.Hide();


            }
            else
            {

                POInquiry poInquiry = new POInquiry();
                poInquiry.GetUserID(TxtUserID.Text);
                poInquiry.Show();
                this.Hide();

            }
            




        }


        public void RetrievePOForApproval(string parameter1, string Action)
        {


            GetProjectCode();
            GetProjectCodeMRM();
            ProcurementType();
            WorkerDetails();
            WorkerDetails1();
            WorkerDetails2();
            GetSupplier();
            GetProcurementCategory();
          
            GetCurrency();
            GetPaymentTerms();
            GetDeliveryTerms();


            ProcurementType();
            GetSupplier();
           
            GetCurrency();
            GetPaymentTerms();
            GetDeliveryTerms();
            POApprovedBy();
             

            txtPOID.Text = parameter1;

            retrievePOdata(Convert.ToInt32(parameter1));




            retrievedata(Convert.ToInt32(txtMRMID.Text));

            LoadItemMasterPO(Convert.ToInt32(txtMRMID.Text),textBox3.Text);

            stat.Text = Action;
            if (Action == "Approval")
            {
            
                btnEdit.Visible = false;

                btnSave.Visible = false;

                button1.Visible = true;

                button2.Visible = true;


                textBox1.Visible = true;

                textBox1.Visible = true;
                label22.Visible = true;
            }

            if (Action == "Rejected")
            {

                btnEdit.Visible = true;

                btnSave.Visible = true;

                button1.Visible = false;

                button2.Visible = false;


                textBox1.Visible = true;

                textBox1.Visible = true;
                label22.Visible = true;
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Transaction("Submitted");
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal total;
                var row = dataGridView1.Rows[e.RowIndex];
                decimal quantity = 0;
                if (row.Cells[0].Value == null || row.Cells[0].Value == "")
                {
                    row.Cells[0].Value = 0;
                    quantity = 0;
                }
                else
                {
                    quantity = decimal.Parse(row.Cells[0].Value.ToString());

                }
                decimal sellingPrice;
                if (row.Cells[1].Value == null || row.Cells[1].Value == "")
                {
                    row.Cells[1].Value = 0;
                    sellingPrice = 0;
                }
                else
                {
                    sellingPrice = decimal.Parse(row.Cells[1].Value.ToString());

                }
                decimal DiscountAmmount = 0;
                if (row.Cells[2].Value == null || row.Cells[2].Value == "")
                {
                    row.Cells[2].Value = 0;
                    DiscountAmmount = 0;
                }
                else
                {
                    DiscountAmmount = decimal.Parse(row.Cells[2].Value.ToString());
                }
                total = (quantity * sellingPrice) - DiscountAmmount;
                if (total < 0)
                {
                    total = 0;
                }
                row.Cells[3].Value = total;

            }
            catch (Exception ex)
            {


            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

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
            if (dataGridView1.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 2) //Desired Column
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

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Approval Remarks is required.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                return;
            }

            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_ApprovePO";
            cmd.Parameters.Add("@Tran_PO_ID", SqlDbType.Int).Value = Convert.ToInt32(txtPOID.Text);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@ApprovalRemarks", SqlDbType.VarChar ,-1).Value = textBox1.Text.ToString();
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
                    PoForApproval formTask = new PoForApproval();
                    formTask.GetUserID(TxtUserID.Text);
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
            if (textBox1.Text == "")
            {
                MessageBox.Show("Rejection Remarks is required.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                return;
            }

            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_RejectPO";
            cmd.Parameters.Add("@Tran_PO_ID", SqlDbType.Int).Value = Convert.ToInt32(txtPOID.Text);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@ApprovalRemarks", SqlDbType.VarChar, -1).Value = textBox1.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                if (StringReturn == "Approved")
                {
                    MessageBox.Show("This Request is already Approved!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
                else
                {

                    MessageBox.Show("Request has been rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    PoForApproval formTask = new PoForApproval();
                    formTask.GetUserID(TxtUserID.Text);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            TESTREPORT TEST = new TESTREPORT();

            TEST.GetPOID(txtPOID.Text.ToString());
            TEST.ShowDialog();
        }

    }
}
