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
namespace StallionSuppyChain.Purchase_Order
{
    public partial class POSelection : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public POSelection()
        {
            InitializeComponent();
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
        private void POSelection_Load(object sender, EventArgs e)
        {

            if (textBox2.Text != "")
            {
                ProcurementType();
                GetSupplier();
                GetCurrency();
                GetPaymentTerms();
                GetDeliveryTerms();
                POApprovedBy();
                GetProjectCode();


            }

        }
        public void LoadItem(int MRMNO, string TranLineNo
            , string POApprovedBystring, string procurementtype, string cboProject, string MRMNo, string supplier, string PaymentTerms,string DeliveryTerms,


          string currencyCode

            ,
           string DPRate
            ,
           string DPAmount,
          string Forex,


           string ForexDate,
            string  Remarks,
            
          bool  Vat_Inclusive
            ,
          bool LocalPurchase,
            string MRMID 
        )
        {
            ProcurementType();
            GetSupplier();
            GetCurrency();
            GetPaymentTerms();
            GetDeliveryTerms();
            POApprovedBy();
            GetProjectCode();

            LoadItemMaster(MRMNO, TranLineNo);
            ListPOApprovedBy.SelectedValue = POApprovedBystring.ToString();

            comboBox6.SelectedValue = procurementtype.ToString();
            cboProjectCode.SelectedValue = cboProject.ToString();
            txtMRMNo.Text = MRMNo;
            comboBox9.SelectedValue = DeliveryTerms.ToString();
            comboBox10.SelectedValue = PaymentTerms.ToString();

            txtDPRate.Text = DPRate.ToString();
            txtDPAmount.Text = DPAmount.ToString();
            dateForexDate.Text = ForexDate.ToString();
          
            txtForex.Text = Forex.ToString();
            comboBox11.SelectedValue = currencyCode.ToString();

            txtRemarks.Text = Remarks.ToString();

            comboBox5.SelectedValue = supplier;

            txtMRMID.Text = MRMID.ToString();
            checkBox3.Checked = Vat_Inclusive;
            checkBox2.Checked = LocalPurchase;
            if (textBox2.Text == "")
            {
                textBox1.Text = GenenratePONumber().ToString();
            }
        }




        private void LoadItemMaster(int MRMNO, string TranLineNo)
        { 
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadMaterialRequestDetail_PerSupplier]", con); 
                    con.Open();
                    cmd.Parameters.Add("@MRMID", SqlDbType.Int).Value = MRMNO;
                    cmd.Parameters.Add("@TranLineNo", SqlDbType.VarChar, -1).Value = TranLineNo;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
 
                    using (SqlDataReader row = cmd.ExecuteReader())
                    {
                        while (row.Read())
                        { 
                            this.dataGridView1.Rows.Add(row["POQuantity"].ToString(), row["Amount"].ToString(), row["DicountAmount"].ToString(), row["NetAmount"].ToString(),
                           row["CosDescription"].ToString(), row["ITEMCODE"].ToString(), row["MRM Quantity"].ToString(), row["MRM UOM"].ToString(), row["Mstr UOM"].ToString()
                           , row["Category1"].ToString(), row["Category2"].ToString(), row["Category3"].ToString(), row["ITEMDESCRIPTION"].ToString()
                           , row["ITEMDESCRIPTION2"].ToString(), row["ITEMSPEC1"].ToString(), row["ITEMSPEC2"].ToString(), row["ITEMSPEC3"].ToString(), row["TranLineNo"].ToString()
                           );
                        }
                    }

                }
                
            //}
            dataGridView1.Columns[17].Visible = false;
        }

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
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
          
        }

        private void dataGridView1_EditModeChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                else if (txtDPRate.Text == "")
                {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (txtDPAmount.Text == "")
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
                cmd.Parameters.Add("@RequestNO", SqlDbType.VarChar, 20).Value = textBox1.Text.ToString();
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
                    

                    // var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
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
                      //  || DiscountAmount.ToString() == "0"
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


                     SaveGrid(POQuantity.ToString(), POAmount.ToString(), DiscountAmount.ToString(), NetAmount.ToString(), CostCode.ToString(), Convert.ToInt32(TranLineNo.ToString()), textBox1.Text);
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
    }

      
}
