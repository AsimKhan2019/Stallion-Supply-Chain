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
namespace StallionSuppyChain
{

    public partial class ITEMMASTER : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        public ITEMMASTER()
        {
            InitializeComponent();
            txtDescription1.Validated += new EventHandler(textBox_Validated);
            txtDescription2.Validated += new EventHandler(textBox_Validated);
        }
        public void textBox_Validated(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                errorProvider1.SetError(tb, "This field is required.!");
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            FormMode("NEW");
        }


        private void FormMode(string Action)
        {

            if (Action == "NEW")
            {

                LoadCategory();
                LoadCategorySearch();
                LoadCurrency();
                LoadCostCode();
                LoadUOM();

                cbostatus.Enabled = true;
                cboisinventory.Enabled = true;
                BtnNew.Enabled = false;
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
           
                BtnReset.Enabled = true;
                
                grpCategory.Enabled = true;
                grpCategory.Enabled = true;
                grpSepecs.Enabled = true;
                grpDesc.Enabled = true;
                grpCostInfo.Enabled = true;
                grpItemParameters.Enabled = true;
                ResetForm();
                cboCategory1.Focus();
                cbostatus.Enabled = true;
                cboisinventory.Enabled = true;
                txtret.Text = "0";

            }
            else if (Action == "INITIAL")
            {
         
                cbostatus.Enabled = true;
                cboisinventory.Enabled = true;
                btnSave.Enabled = false;
                btnEdit.Enabled = false;
                BtnReset.Enabled = true;
                BtnNew.Enabled = true;
                grpCategory.Enabled = false;
                grpCategory.Enabled = false;
                grpSepecs.Enabled = false;
                grpDesc.Enabled = false;
                grpCostInfo.Enabled = false;
                grpItemParameters.Enabled = false;
                ResetForm();
                cboCategory1.Focus();
                cboCategory3.Text = "";
                txtret.Text = "0";
                TXTITEMCODE.Text = "";
                cbostatus.Enabled = false;
                cbostatus.Enabled = false;
                cboisinventory.Enabled = false;
                cboCategory3.DataSource = null;

            }
            else if (Action == "EDIT" || Action == "DELETE")
            {
                BtnNew.Enabled = false;
                btnSave.Enabled = false;
                btnEdit.Enabled = true;
                BtnReset.Enabled = false;
                grpCategory.Enabled = true;
                grpCategory.Enabled = true;
                grpSepecs.Enabled = true;
                grpDesc.Enabled = true;
                grpCostInfo.Enabled = true;
                grpItemParameters.Enabled = true;
                grpCategory.Enabled = false;
                cbostatus.Enabled = true;
                cboisinventory.Enabled = true;
                txtret.Text = "0";
              

            }
            else if (Action == "RESET")
            {

                ResetForm();
                cboCategory3.DataSource = null;
                txtret.Text = "0";
            }


        }

        private void ResetForm()
        {
    
            cboCategory1.SelectedValue = 0;
            cboCategory2.Text = "";
            cboCategory3.Text = "";
            txtDescription1.Text = "";

            txtDescription2.Text = "";
            cboUOM.SelectedValue = 0;

            cboCurrency.SelectedValue = 0;
            cboCostCode.SelectedValue = 0;

            txtdefaultPrice.Text = "";

            txtSpecs1.Text = "";
            txtSpecs2.Text = "";
            txtSpecs3.Text = "";
            txtMaximumQty.Text = "";
            txtMinimumQty.Text = "";


            cbostatus.Text = "Active";
            cboisinventory.Text = "Yes";

            LoadCategory();
            LoadCurrency();
            LoadCostCode();
            LoadUOM();

            PopulateList("0", "0", "0", "0", "3");
        }

        private void PopulateList( string ItemCOde,   string Category1, string Category2, string Category3, string proceessID)
        {
            //string CS = "Server = server-sql1;Database=Accounts;Trusted_connection=true";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MSTRGETITEMMASTER]", con);

                //specify that it is a stored procedure and not a normal proc
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //list the parameters required and what they should be
                 cmd.Parameters.AddWithValue("@ITEMCODE", ItemCOde);

                 cmd.Parameters.AddWithValue("@CategoryID", Category1);
                 cmd.Parameters.AddWithValue("@CategoryID2", Category2);
                 cmd.Parameters.AddWithValue("@CategoryID3", Category3);

                 cmd.Parameters.AddWithValue("@ProcessID", proceessID);
 
                 cmd.Parameters.AddWithValue("@ProjectID", "");
                 cmd.Parameters.AddWithValue("@TranType", "");

 
             
                con.Open();
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }

        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

        }
        private void ITEMMASTER_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sCM_STALLIONDataSet1.LIST_MSTRGETITEMMASTER' table. You can move, or remove it, as needed.
          //  this.lIST_MSTRGETITEMMASTERTableAdapter.Fill(this.sCM_STALLIONDataSet1.LIST_MSTRGETITEMMASTER);
            LoadCategory();
            LoadCategorySearch();
            LoadCurrency();
            LoadCostCode();
            LoadUOM();



            string Cat1 = "0";
            string Cat2 = "0";
            string Cat3 = "0";
            if (cboCategory1.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1.SelectedValue == null)
            {
                Cat1 = "0";

            }
            else
            {

                Cat1 = cboCategory1.SelectedValue.ToString();

            }
            if (cboCategory2.ValueMember == "")
            {
                Cat2 = "0";

            }
            else if (cboCategory2.SelectedValue == null)
            {
                Cat2 = "0";

            }
            else
            {

                Cat2 = cboCategory2.SelectedValue.ToString();

            }
            if (cboCategory3.ValueMember == "")
            {
                Cat3 = "0";

            }
            else if (cboCategory3.SelectedValue == null)
            {
                Cat3 = "0";

            }
            else
            {

                Cat3 = cboCategory3.SelectedValue.ToString();

            }






            PopulateList(txtSpecificItemCode.Text, Cat1, Cat2, Cat3, "3");
        }





        private void LoadCategory()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY]", con);
                    adapter.Fill(dt);
                    cboCategory1.DataSource = dt;
                    cboCategory1.DisplayMember = "CategoryCode";
                    cboCategory1.ValueMember = "CategoryID";
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void LoadCategorySearch()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY]", con);
                    adapter.Fill(dt);
                    cboCategory1Search.DataSource = dt;
                    cboCategory1Search.DisplayMember = "CategoryCode";
                    cboCategory1Search.ValueMember = "CategoryID";
                }
                catch (Exception ex)
                {
                }
            }
        }



        private void LoadCategory2(string CategoryID1)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY2] " + CategoryID1, con);
                    adapter.Fill(dt);
                    cboCategory2.DataSource = dt;
                    cboCategory2.DisplayMember = "CategoryCode2";
                    cboCategory2.ValueMember = "CategoryID2";
                }
                catch (Exception ex)
                {
                }
            }
        }



        private void LoadCategory2Search(string CategoryID1)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY2] " + CategoryID1, con);
                    adapter.Fill(dt);
                    cboCategory2Search.DataSource = dt;
                    cboCategory2Search.DisplayMember = "CategoryCode2";
                    cboCategory2Search.ValueMember = "CategoryID2";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void LoadCategory3(string CategoryID2)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY3] " + CategoryID2, con);
                    adapter.Fill(dt);
                    cboCategory3.DataSource = dt;
                    cboCategory3.DisplayMember = "CategoryCode3";
                    cboCategory3.ValueMember = "CategoryID3";
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void LoadCategory3Search(string CategoryID2)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCATEGORY3] " + CategoryID2, con);
                    adapter.Fill(dt);
                    cboCategory3Search.DataSource = dt;
                    cboCategory3Search.DisplayMember = "CategoryCode3";
                    cboCategory3Search.ValueMember = "CategoryID3";
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void LoadCurrency()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCURRENCY]", con);
                    adapter.Fill(dt);
                    cboCurrency.DataSource = dt;
                    cboCurrency.DisplayMember = "CountryName";
                    cboCurrency.ValueMember = "MOneyCode";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void LoadCountry()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCURRENCY]", con);
                    adapter.Fill(dt);
                    cboCurrency.DataSource = dt;
                    cboCurrency.DisplayMember = "CountryName";
                    cboCurrency.ValueMember = "MOneyCode";
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void LoadCostCode()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRCostCode] ", con);
                    adapter.Fill(dt);
                    cboCostCode.DataSource = dt;
                    cboCostCode.DisplayMember = "CosDescription";
                    cboCostCode.ValueMember = "CostCode";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void LoadUOM()
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTRUnitMeasurement]", con);
                    adapter.Fill(dt);
                    cboUOM.DataSource = dt;
                    cboUOM.DisplayMember = "UOM_Description";
                    cboUOM.ValueMember = "UOM_CODE";
                }
                catch (Exception ex)
                {
                }
            }
        }

        private string GenerateItemCode(string Category1, string Category2, string Category3)
        {

            if (Category1 == "")
            {

                Category1 = "0";

            }
            if (Category2 == "")
            {

                Category2 = "0";

            }
            if (Category3 == "")
            {

                Category3 = "0";

            }
            //  string connetionString = null;
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string SRet = "";

            //  connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "[dbo].[TRAN_GenItemCode] " + Convert.ToString(Category1) + ',' + Convert.ToString(Category2) + ',' + Convert.ToString(Category3);


            try
            {

                //  cnn = new SqlConnection(conStr);
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


        private void cboCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cboCategory1.SelectedValue.ToString() == "0")
            {
                cboCategory2.Text = "";
                cboCategory3.Text = "";
            }

            LoadCategory2(cboCategory1.SelectedValue.ToString());
            string Cat1 = "0";
            string Cat2 = "0";
            string Cat3 = "0";
            if (cboCategory1.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1.SelectedValue == null)
            {
                Cat1 = "0";
            }
            else
            {
                Cat1 = cboCategory1.SelectedValue.ToString();
            }
            if (cboCategory2.ValueMember == "")
            {
                Cat2 = "0";
            }
            else if (cboCategory2.SelectedValue == null)
            {
                Cat2 = "0";
            }
            else
            {
                Cat2 = cboCategory2.SelectedValue.ToString();
            }
            if (cboCategory3.ValueMember == "")
            {
                Cat3 = "0";
            }
            else if (cboCategory3.SelectedValue == null)
            {
                Cat3 = "0";
            }
            else
            {
                Cat3 = cboCategory3.SelectedValue.ToString();
            }


            if (txtret.Text == "0")
            {

                TXTITEMCODE.Text = GenerateItemCode(Cat1, Cat2, Cat3);
            }



        }

        private void cboCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory3(cboCategory2.SelectedValue.ToString());
            string Cat1 = "0";
            string Cat2 = "0";
            string Cat3 = "0";
            if (cboCategory1.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1.SelectedValue == null)
            {
                Cat1 = "0";

            }
            else
            {

                Cat1 = cboCategory1.SelectedValue.ToString();

            }
            if (cboCategory2.ValueMember == "")
            {
                Cat2 = "0";

            }
            else if (cboCategory2.SelectedValue == null)
            {
                Cat2 = "0";

            }
            else
            {

                Cat2 = cboCategory2.SelectedValue.ToString();

            }
            if (cboCategory3.ValueMember == "")
            {
                Cat3 = "0";

            }
            else if (cboCategory3.SelectedValue == null)
            {
                Cat3 = "0";

            }
            else
            {

                Cat3 = cboCategory3.SelectedValue.ToString();

            }



            if (txtret.Text == "0")
            {

                TXTITEMCODE.Text = GenerateItemCode(Cat1, Cat2, Cat3);
            }
        }

        private void cboCategory3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Cat1 = "";
            string Cat2 = "";
            string Cat3 = "";
            if (cboCategory1.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1.SelectedValue == null)
            {
                Cat1 = "0";

            }
            else
            {

                Cat1 = cboCategory1.SelectedValue.ToString();

            }
            if (cboCategory2.ValueMember == "")
            {
                Cat2 = "0";

            }
            else if (cboCategory2.SelectedValue == null)
            {
                Cat2 = "0";

            }
            else
            {

                Cat2 = cboCategory2.SelectedValue.ToString();

            }
            if (cboCategory3.ValueMember == "")
            {
                Cat3 = "0";

            }
            else if (cboCategory3.SelectedValue == null)
            {
                Cat3 = "0";

            }
            else
            {

                Cat3 = cboCategory3.SelectedValue.ToString();

            }




            if (txtret.Text == "0")
            {

                TXTITEMCODE.Text = GenerateItemCode(Cat1, Cat2, Cat3);
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            bool isactive = false;
             bool forinventory = false;


            if (cbostatus.Text == "Active")
            {
                isactive = true;

            }
            if (cbostatus.Text == "InActive")
            {
                isactive = false;

            }

            if (cboisinventory.Text == "Yes")
            {
                forinventory = true;

            }
            if (cboisinventory.Text == "No")
            {
                forinventory = false;

            }
            
       




            bool isValidNumeric = ValidateNumber(txtMaximumQty.Text);

            
            bool isValidNumeric2 = ValidateNumber(txtMinimumQty.Text);
            bool isValidNumeric3 = ValidateNumber(txtdefaultPrice.Text);
            if (cboCategory1.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboCategory1.Focus();
            }
            else   if (cboCategory2.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboCategory2.Focus();
            }

            else if (cboCategory3.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboCategory3.Focus();
            }


            else if (cboUOM.SelectedIndex == 0)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboUOM.Focus();
            }



            else if (cboCurrency.SelectedIndex == 0)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboCurrency.Focus();
            }


            else if (cboCostCode.SelectedIndex == 0)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboCostCode.Focus();
            }

            else if (txtSpecs1.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtSpecs1.Focus();
            }
            else if (txtSpecs2.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtSpecs2.Focus();
            }
            else if (txtSpecs3.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtSpecs3.Focus();
            }


           
            else if (isValidNumeric == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtMaximumQty.Focus();
            }

            else if (isValidNumeric2 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtMinimumQty.Focus();
            }
            else if (isValidNumeric3 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                txtdefaultPrice.Focus();
            }


            else
            {

                if (txtMaximumQty.Text == "" || txtMaximumQty.Text == " ")
                {

                    txtMaximumQty.Text = "0";
                }


                if (txtMinimumQty.Text == "" || txtMinimumQty.Text == " ")
                {

                    txtMinimumQty.Text = "0";
                }
                if (txtdefaultPrice.Text == "" || txtdefaultPrice.Text == " ")
                {

                    txtdefaultPrice.Text = "0";
                }



                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_SAVEITEMMASTER";
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = Convert.ToInt32(cboCategory1.SelectedValue);
                cmd.Parameters.Add("@CategoryID2", SqlDbType.Int).Value = Convert.ToInt32(cboCategory2.SelectedValue);
                cmd.Parameters.Add("@CategoryID3", SqlDbType.Int).Value = Convert.ToInt32(cboCategory3.SelectedValue);
                cmd.Parameters.Add("@ITEMDESCRIPTION", SqlDbType.VarChar, 50).Value = txtDescription1.Text;
                cmd.Parameters.Add("@ITEMDESCRIPTION2", SqlDbType.VarChar, 50).Value = txtDescription2.Text;
                cmd.Parameters.Add("@ITEMSPEC1", SqlDbType.VarChar, 50).Value = txtSpecs1.Text;
                cmd.Parameters.Add("@ITEMSPEC2", SqlDbType.VarChar, 50).Value = txtSpecs2.Text;
                cmd.Parameters.Add("@ITEMSPEC3", SqlDbType.VarChar, 50).Value = txtSpecs3.Text;
                cmd.Parameters.Add("@UOMID", SqlDbType.VarChar, 30).Value = cboUOM.SelectedValue;
                cmd.Parameters.Add("@MAXQTY", SqlDbType.Float).Value = txtMaximumQty.Text;
                cmd.Parameters.Add("@MINQTY", SqlDbType.Float).Value = txtMinimumQty.Text;
                cmd.Parameters.Add("@CurrencyID", SqlDbType.VarChar, 3).Value = cboCurrency.SelectedValue;
                cmd.Parameters.Add("@CostID", SqlDbType.VarChar, 3).Value = cboCostCode.SelectedValue;
                cmd.Parameters.Add("@StatusID", SqlDbType.Bit).Value = isactive;
                cmd.Parameters.Add("@IsInventory", SqlDbType.Bit).Value = forinventory;
                cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text.ToString());
                cmd.Parameters.Add("@DefaultPrice", SqlDbType.Float).Value = txtdefaultPrice.Text;
                cmd.Parameters.Add("@IsFixedAsset", SqlDbType.Bit).Value = Convert.ToBoolean(chkIsFixedAsset.Checked);


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


            }



            ResetForm();

            FormMode("INITAIL");
            MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);

            PopulateList("0", "0", "0", "0", "3");



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
        private void txtMaximumQty_KeyPress(object sender, KeyPressEventArgs e)
        {
         //   if (e.KeyChar == '.' && (txtMaximumQty.Text.IndexOf('.') > 0 || txtMaximumQty.Text.Length == 0)) ;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
            BtnNew.Enabled = true;
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
           // BtnDelete.Enabled = false;
            grpCategory.Enabled = false;
            grpSepecs.Enabled = false;
            grpDesc.Enabled = false;
            grpCostInfo.Enabled = false;
            grpItemParameters.Enabled = false;


            cbostatus.Enabled = false;
            cboisinventory.Enabled = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {


            string Cat1 = "0";
            string Cat2 = "0";
            string Cat3 = "0";
            if (cboCategory1.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1.SelectedValue == null)
            {
                Cat1 = "0";

            }
            else
            {

                Cat1 = cboCategory1.SelectedValue.ToString();

            }
            if (cboCategory2.ValueMember == "")
            {
                Cat2 = "0";

            }
            else if (cboCategory2.SelectedValue == null)
            {
                Cat2 = "0";

            }
            else
            {

                Cat2 = cboCategory2.SelectedValue.ToString();

            }
            if (cboCategory3.ValueMember == "")
            {
                Cat3 = "0";

            }
            else if (cboCategory3.SelectedValue == null)
            {
                Cat3 = "0";

            }
            else
            {

                Cat3 = cboCategory3.SelectedValue.ToString();

            }






            PopulateList(txtSpecificItemCode.Text, Cat1, Cat2, Cat3, "1");
        
        }

        private void cboCategory1Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory2Search(cboCategory1Search.SelectedValue.ToString());
        }

        private void cboCategory2Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory3Search(cboCategory2Search.SelectedValue.ToString());
        }

        private void cboCategory3Search_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Cat1 = "0";
            string Cat2 = "0";
            string Cat3 = "0";
            if (cboCategory1Search.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1Search.SelectedValue == null)
            {
                Cat1 = "0";

            }
            else
            {

                Cat1 = cboCategory1Search.SelectedValue.ToString();

            }
            if (cboCategory2Search.ValueMember == "")
            {
                Cat2 = "0";

            }
            else if (cboCategory2Search.SelectedValue == null)
            {
                Cat2 = "0";

            }
            else
            {

                Cat2 = cboCategory2Search.SelectedValue.ToString();

            }
            if (cboCategory3Search.ValueMember == "")
            {
                Cat3 = "0";

            }
            else if (cboCategory3Search.SelectedValue == null)
            {
                Cat3 = "0";

            }
            else
            {

                Cat3 = cboCategory3Search.SelectedValue.ToString();

            }






            PopulateList(txtSpecificItemCode.Text, Cat1, Cat2, Cat3, "2");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           //
            txtret.Text = "1";
            var index =  dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            ReadItemMaster(index);
            

        }
        private void ReadItemMaster(string ItemCode)
        {

 
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
 
            sql = "[dbo].[READ_ITEMCODEDETAILS] '" + ItemCode + "'";

            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    TXTITEMCODE.Text = sqlReader.GetValue(0).ToString();


                    cboCategory1.SelectedValue = sqlReader.GetValue(1).ToString();
                    cboCategory2.SelectedValue = sqlReader.GetValue(2).ToString();
                  
                    LoadCategory3(sqlReader.GetValue(2).ToString());
                    
                    LoadCategory3(cboCategory2.SelectedValue.ToString());
                    cboCategory3.SelectedValue = sqlReader.GetValue(3).ToString();
                    txtDescription1.Text = sqlReader.GetValue(4).ToString();

                    txtDescription2.Text = sqlReader.GetValue(5).ToString();

                    txtSpecs1.Text = sqlReader.GetValue(6).ToString();
                    txtSpecs2.Text = sqlReader.GetValue(7).ToString();
                    txtSpecs3.Text = sqlReader.GetValue(8).ToString();


                    cboUOM.SelectedValue = sqlReader.GetValue(9).ToString();

 

                    cboisinventory.Text = sqlReader.GetValue(10).ToString();
                    cbostatus.Text = sqlReader.GetValue(11).ToString();

                    txtMaximumQty.Text = sqlReader.GetValue(12).ToString();

                    txtMinimumQty.Text = sqlReader.GetValue(13).ToString();

                    cboCurrency.SelectedValue = sqlReader.GetValue(14).ToString();

                    cboCostCode.SelectedValue = sqlReader.GetValue(15).ToString();
                    txtdefaultPrice.Text = sqlReader.GetValue(16).ToString(); 
                    chkIsFixedAsset.Checked = Convert.ToBoolean(sqlReader.GetValue(22));
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();


                FormMode("EDIT");
                txtret.Text = "1";
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            bool isactive = false;
            bool forinventory = false;


            if (cbostatus.Text == "Active")
            {
                isactive = true;

            }
            if (cbostatus.Text == "InActive")
            {
                isactive = false;

            }

            if (cboisinventory.Text == "Yes")
            {
                forinventory = true;

            }
            if (cboisinventory.Text == "No")
            {
                forinventory = false;

            }






            bool isValidNumeric = ValidateNumber(txtMaximumQty.Text);


            bool isValidNumeric2 = ValidateNumber(txtMinimumQty.Text);
            bool isValidNumeric3 = ValidateNumber(txtdefaultPrice.Text);
            if (cboCategory1.SelectedIndex == -1)//Nothing selected
            {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    cboCategory1.Focus();



            }
            else if (cboCategory2.SelectedIndex == -1)//Nothing selected
            {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    cboCategory2.Focus();
            }

            else if (cboCategory3.SelectedIndex == -1)//Nothing selected
            {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    cboCategory3.Focus();
            }


            else if (cboUOM.SelectedIndex == 0)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button1);
                cboUOM.Focus();
            }



            else if (cboCurrency.SelectedIndex == 0)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button1);
                cboCurrency.Focus();
            }


            else if (cboCostCode.SelectedIndex == 0)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button1);
                cboCostCode.Focus();
            }




            else if (isValidNumeric == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button1);

                txtMaximumQty.Focus();
            }

            else if (isValidNumeric2 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button1);

                txtMinimumQty.Focus();
            }
            else if (isValidNumeric3 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button1);
                txtdefaultPrice.Focus();
            }


            else
            {

                if (txtMaximumQty.Text == "" || txtMaximumQty.Text == " ")
                {

                    txtMaximumQty.Text = "0";
                }


                if (txtMinimumQty.Text == "" || txtMinimumQty.Text == " ")
                {

                    txtMinimumQty.Text = "0";
                }
                if (txtdefaultPrice.Text == "" || txtdefaultPrice.Text == " ")
                {

                    txtdefaultPrice.Text = "0";
                }



                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TRAN_UPDATEITEMMASTER";





                cmd.Parameters.Add("@ITEMCODE", SqlDbType.VarChar , 50).Value = TXTITEMCODE.Text;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = Convert.ToInt32(cboCategory1.SelectedValue);
                cmd.Parameters.Add("@CategoryID2", SqlDbType.Int).Value = Convert.ToInt32(cboCategory2.SelectedValue);
                cmd.Parameters.Add("@CategoryID3", SqlDbType.Int).Value = Convert.ToInt32(cboCategory3.SelectedValue);
                cmd.Parameters.Add("@ITEMDESCRIPTION", SqlDbType.VarChar, 50).Value = txtDescription1.Text;
                cmd.Parameters.Add("@ITEMDESCRIPTION2", SqlDbType.VarChar, 50).Value = txtDescription2.Text;
                cmd.Parameters.Add("@ITEMSPEC1", SqlDbType.VarChar, 50).Value = txtSpecs1.Text;
                cmd.Parameters.Add("@ITEMSPEC2", SqlDbType.VarChar, 50).Value = txtSpecs2.Text;
                cmd.Parameters.Add("@ITEMSPEC3", SqlDbType.VarChar, 50).Value = txtSpecs3.Text;
                cmd.Parameters.Add("@UOMID", SqlDbType.VarChar, 30).Value = cboUOM.SelectedValue;
                cmd.Parameters.Add("@MAXQTY", SqlDbType.Float).Value = txtMaximumQty.Text;
                cmd.Parameters.Add("@MINQTY", SqlDbType.Float).Value = txtMinimumQty.Text;
                cmd.Parameters.Add("@CurrencyID", SqlDbType.VarChar, 3).Value = cboCurrency.SelectedValue;
                cmd.Parameters.Add("@CostID", SqlDbType.VarChar, 3).Value = cboCostCode.SelectedValue;
                cmd.Parameters.Add("@StatusID", SqlDbType.Bit).Value = isactive;
                cmd.Parameters.Add("@IsInventory", SqlDbType.Bit).Value = forinventory;
                cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text.ToString());
                cmd.Parameters.Add("@DefaultPrice", SqlDbType.Float).Value = txtdefaultPrice.Text;
                cmd.Parameters.Add("@IsFixedAsset", SqlDbType.Bit).Value = Convert.ToBoolean(chkIsFixedAsset.Checked);




                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();



                            MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);




                    ResetForm();
                    FormMode("INITIAL");

                    PopulateList("0", "0", "0", "0", "3");

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

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            string Cat1 = "0";
            string Cat2 = "0";
            string Cat3 = "0";
            if (cboCategory1.ValueMember == "")
            {
                Cat1 = "0";
            }
            else if (cboCategory1.SelectedValue == null)
            {
                Cat1 = "0";

            }
            else
            {

                Cat1 = cboCategory1.SelectedValue.ToString();

            }
            if (cboCategory2.ValueMember == "")
            {
                Cat2 = "0";

            }
            else if (cboCategory2.SelectedValue == null)
            {
                Cat2 = "0";

            }
            else
            {

                Cat2 = cboCategory2.SelectedValue.ToString();

            }
            if (cboCategory3.ValueMember == "")
            {
                Cat3 = "0";

            }
            else if (cboCategory3.SelectedValue == null)
            {
                Cat3 = "0";

            }
            else
            {

                Cat3 = cboCategory3.SelectedValue.ToString();

            }

            PopulateList(txtSpecificItemCode.Text, Cat1, Cat2, Cat3, "3");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PopulateList(txtAdvanceSearch.Text, "", "", "", "99");
        }
    }
}
