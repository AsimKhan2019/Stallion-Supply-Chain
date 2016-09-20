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
    public partial class RequestBatch : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public RequestBatch()
        {
            InitializeComponent();
        }

        private void RequestBatch_Load(object sender, EventArgs e)
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
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

        }
        private void LoadItemMasterSubmitted(int MRMNO)
        {

            //e2
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadMaterialRequestDetail_BATCH]", con);
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

            dataGridView1.Columns[1].Visible = false;
           dataGridView1.Columns[0].ReadOnly = false;
           dataGridView1.Columns[2].ReadOnly = true;
           dataGridView1.Columns[3].ReadOnly = true;
           dataGridView1.Columns[4].ReadOnly = true;
           dataGridView1.Columns[5].ReadOnly = true;
           dataGridView1.Columns[6].ReadOnly = true;
           dataGridView1.Columns[7].ReadOnly = true;
           dataGridView1.Columns[8].ReadOnly = true;
           dataGridView1.Columns[9].ReadOnly = true;
           dataGridView1.Columns[10].ReadOnly = true;
           dataGridView1.Columns[11].ReadOnly = true;

           
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


            LoadItemMasterSubmitted(Convert.ToInt32(parameter1));
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
                //dataGridView1.Columns[4].HeaderText = "Cost Code";
               // LoadItemMaster(MRMNO);
                txtMRMID.Text = MRMNO.ToString();
               // textBox3.Text = GenenratePONumber().ToString();
            }

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
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

            int countChecked = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                {
                    countChecked = countChecked + 1;
                }
            }

            if (countChecked <= 0)
            {
                MessageBox.Show("Please select atleast one record in the grid.", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                return;
            }

            





            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                var POQuantity = dataGridView1.Rows[i].Cells[1].Value;

                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                {
                    textBox1.Text = textBox1.Text +  POQuantity.ToString()  + ",";
                }

             
             
            }
            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            POSelection formTask = new POSelection();
            formTask.LoadItem(Convert.ToInt32(txtMRMID.Text), textBox1.Text, 
                
                ListPOApprovedBy.SelectedValue.ToString(),

                 comboBox6.SelectedValue.ToString(),
                 cboProjectCode.SelectedValue.ToString(),
                txtMRMNo.Text,
                comboBox5.SelectedValue.ToString(),

                comboBox10.SelectedValue.ToString(),
                comboBox9.SelectedValue.ToString(),
                comboBox11.SelectedValue.ToString(),
              txtDPRate.Text,
            txtDPAmount.Text,txtForex.Text,dateForexDate.Text,txtRemarks.Text
            , checkBox3.Checked, checkBox2.Checked,
         txtMRMID.Text   );
            formTask.Show();
            this.Dispose();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
         
        }

        private void txtDPRate_TextChanged(object sender, EventArgs e)
        {


            if (txtDPRate != null)
            {
                txtDPRate.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
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

        private void txtDPAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtDPAmount != null)
            {
                txtDPAmount.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }

        private void txtForex_TextChanged(object sender, EventArgs e)
        {
            if (txtForex != null)
            {
                txtForex.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }

    }
}
