using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using System.Data;
using System.Drawing;


using System.Data.SqlClient;
using StallionSuppyChain.Procurement;
using StallionSuppyChain.Material_Releasing;

namespace StallionSuppyChain
{
    public partial class ItemMasterLookUp : Form
    {
        public List<ProductAttributeModel> ProductAttributes { get; set; }

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private ProductAttributeModel productAttr = new ProductAttributeModel();

        public ItemMasterLookUp()
        {
            InitializeComponent();
            ProductAttributes = new List<ProductAttributeModel>();
        }

        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

        }

        private void ItemMasterLookUp_Load(object sender, EventArgs e)
        {
            //PopulateList("0", "0", "0", "0", "3");
            LoadCategory();
            LoadUOM();



        }

        public void Test(string parameter1, string module)
        {
            txtMRMNO.Text = parameter1;
            TxtModule.Text = module;






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

        private void PopulateList(string ItemCOde, string Category1, string Category2, string Category3, string proceessID, string ProjectID, string TranType)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MSTRGETITEMMASTER]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ITEMCODE", ItemCOde);
                cmd.Parameters.AddWithValue("@CategoryID", Category1);
                cmd.Parameters.AddWithValue("@CategoryID2", Category2);
                cmd.Parameters.AddWithValue("@CategoryID3", Category3);
                cmd.Parameters.AddWithValue("@ProcessID", proceessID);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
                cmd.Parameters.AddWithValue("@TranType", TranType);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView2.DataSource = dt;

                }
            }
            dataGridView2.Columns[17].Width = 0;

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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory3(cboCategory2.SelectedValue.ToString());
        }

        private void cboCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory2Search(cboCategory1.SelectedValue.ToString());
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
                    cboCategory2.DataSource = dt;
                    cboCategory2.DisplayMember = "CategoryCode2";
                    cboCategory2.ValueMember = "CategoryID2";
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

        private void btnSearch_Click(object sender, EventArgs e)
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

                Cat2 = cboCategory1.SelectedValue.ToString();

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

            if (txttrantype.Text != "")
            {







                PopulateList(txtSpecificItemCode.Text, Cat1, Cat2, Cat3, "4", lblProjectCode.Text.ToString().Trim(), txttrantype.Text.ToString().Trim());
                tabControl1.SelectedIndex = 1;
            }

            else
            {
                PopulateList(txtSpecificItemCode.Text, Cat1, Cat2, Cat3, "2", lblProjectCode.Text.ToString().Trim(), txttrantype.Text.ToString().Trim());
                tabControl1.SelectedIndex = 1;
            }




        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {

            // var results = dataGridView1.SelectedRows
            //         .Cast<DataGridViewRow>()
            //         .Select(x => Convert.ToString(x.Cells[0].Value));
            // X1 = dataGridView1.SelectedCells[0].Value.ToString();
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            // MessageBox.Show(index.ToString());

            ReadItemMaster(index);
            tabControl1.SelectedIndex = 0;
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
                    lblItemCode.Text = sqlReader.GetValue(0).ToString();
                    lblCategory1.Text = sqlReader.GetValue(17).ToString();
                    lblCategory2.Text = sqlReader.GetValue(18).ToString();
                    lblCategory3.Text = sqlReader.GetValue(19).ToString();
                    LBLSpecs1.Text = sqlReader.GetValue(6).ToString();
                    lblSpecs2.Text = sqlReader.GetValue(7).ToString();
                    lblSpecs3.Text = sqlReader.GetValue(8).ToString();

                    lblDescription1.Text = sqlReader.GetValue(4).ToString();
                    lblDescription2.Text = sqlReader.GetValue(5).ToString();
                    lblUOM.Text = sqlReader.GetValue(9).ToString();
                    cboUOM.SelectedValue = sqlReader.GetValue(9).ToString();


                    lblCurrency.Text = sqlReader.GetValue(14).ToString();
                    lblCostCode.Text = sqlReader.GetValue(20).ToString();
                    lblDefaultPrice.Text = sqlReader.GetValue(16).ToString();
                    txtItemMasterID.Text = sqlReader.GetValue(21).ToString();

                    productAttr = new ProductAttributeModel()
                    {
                        ItemMasterId = (int)sqlReader.GetValue(21),
                        CategoryId1 = (int)sqlReader.GetValue(1),
                        CategoryId2 = (int)sqlReader.GetValue(2),
                        CategoryId3 = (int)sqlReader.GetValue(3),
                        ItemDescription1 = sqlReader.GetValue(4).ToString(),
                        ItemDescription2 = sqlReader.GetValue(5).ToString(),
                        ItemSpecs1 = sqlReader.GetValue(6).ToString(),
                        ItemSpecs2 = sqlReader.GetValue(7).ToString(),
                        UOM = sqlReader.GetValue(9).ToString(),
                        CostCode = sqlReader.GetValue(15).ToString(),
                        Category1 = sqlReader.GetValue(17).ToString(),
                        Category2 = sqlReader.GetValue(18).ToString(),
                        Category3 = sqlReader.GetValue(19).ToString(),
                        ItemCode = sqlReader.GetValue(1).ToString()
                    };

                    lblStock.Text = sqlReader.GetValue(23).ToString();
                    if (txttrantype.Text.ToString() != "")
                    {
                        readstock(txtItemMasterID.Text, lblProjectCode.Text.ToString().Trim(), lblUOM.Text);
                    }
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

        private void readstock(string itemcode, string Projectcode, string UOM)
        {

            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[READ_StockPerProjectUOM] '" + itemcode + "','" + Projectcode + "','" + UOM + "';";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    lblStock.Text = sqlReader.GetValue(0).ToString();
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
        private void button1_Click_1(object sender, EventArgs e)
        {
            bool isValidNumeric3 = ValidateNumber(TxtQuantity.Text);
            if (txttrantype.Text != "")
            {
                if (Convert.ToInt32(TxtQuantity.Text) > Convert.ToInt32(lblStock.Text))
                {

                    MessageBox.Show("Invalid Quantity", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    TxtQuantity.Focus();
                    return;
                }
            }
            if (isValidNumeric3 == false)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtQuantity.Focus();
            }
            else if (lblItemCode.Text == "-----")
            {

                MessageBox.Show("Please select Item.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboCategory1.Focus();
            }
            else if (TxtQuantity.Text == "")
            {

                MessageBox.Show("Input Quantity", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                TxtQuantity.Focus();
            }
            else if (cboUOM.SelectedValue == "" || cboUOM.SelectedValue == null)
            {

                MessageBox.Show("Input UOM.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboUOM.Focus();
            }
            else
            {
                this.dataGridView1.Rows.Add("Remove", TxtQuantity.Text, cboUOM.SelectedValue.ToString(), lblItemCode.Text, lblCategory1.Text, lblCategory2.Text, lblCategory3.Text, LBLSpecs1.Text, lblSpecs2.Text, lblSpecs3.Text, lblCurrency.Text, lblCostCode.Text, lblDefaultPrice.Text, txtItemMasterID.Text);
                productAttr.Quantity = decimal.Parse(TxtQuantity.Text);
                ProductAttributes.Add(productAttr);
                ClearFields();
            }
        }
        private void ClearFields()
        {
            TxtQuantity.Text = "";
            cboUOM.SelectedValue = 0;
            lblItemCode.Text = "-----";
            lblCategory1.Text = "-----";
            lblCategory2.Text = "-----";
            lblCategory3.Text = "-----";
            LBLSpecs1.Text = "-----";
            lblSpecs2.Text = "-----";
            lblSpecs3.Text = "-----";
            lblCurrency.Text = "-----";
            lblCostCode.Text = "-----";
            lblDefaultPrice.Text = "-----";
            lblDescription1.Text = "-----";
            lblDescription2.Text = "-----";
            lblUOM.Text = "-----";
            txtItemMasterID.Text = "";
            lblStock.Text = "-----";
        }


        private int rowIndex = 0;

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;

                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
            {

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["BtnRemove"].Index && e.RowIndex >= 0)
            {

                this.dataGridView1.Rows.RemoveAt(this.rowIndex);
                //StateMents you Want to execute to Get Data 

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }



        //   public delegate void IdentityUpdateHandler(object sender, IdentityUpdateEventArgs e);



        //  public event StallionSuppyChain.Procurement.MaterialRequestMain.AddressUpdateHandler AddressUpdated;

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                // var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string Value1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string Value2 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string ID = dataGridView1.Rows[i].Cells[13].Value.ToString();
                string DefaultPrice = dataGridView1.Rows[i].Cells[12].Value.ToString();

                if (TxtModule.Text == "MRM")
                {
                    SaveMateriealRequest(Convert.ToInt32(txtMRMNO.Text.ToString()), Convert.ToInt32(ID), Value1, Convert.ToInt32(TxtUserID.Text), Value2);
                }
                if (TxtModule.Text == "MRI" || TxtModule.Text == "TRP" || TxtModule.Text == "TRM")
                {
                    SaveMRIRequest(Convert.ToInt32(txtMRMNO.Text.ToString()), Convert.ToInt32(ID), Value1, Convert.ToInt32(TxtUserID.Text), Value2, DefaultPrice);
                }



            }

            if (TxtModule.Text == "PMF") //Product Master File
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            if (TxtModule.Text == "MRM")
            {
                MaterialRequestMain formTask = new MaterialRequestMain();
                formTask.GetUserID(TxtUserID.Text);
                this.Dispose();
                formTask.Show();


                formTask.Test(txtMRMNO.Text.ToString());

            }

            if (TxtModule.Text == "MRI" || TxtModule.Text == "TRP" || TxtModule.Text == "TRM")
            {
                Material_Releasing_Main formTask = new Material_Releasing_Main();
                formTask.GetUserID(TxtUserID.Text);
                this.Dispose();
                formTask.Show();

                formTask.GetTranType(txttrantype.Text.ToString());

                formTask.Retrieverequest(txtMRMNO.Text.ToString());

            }

        }
        public void GetTranType(string parameter1, string parameter2)
        {
            txttrantype.Text = parameter1;
            lblProjectCode.Text = parameter2;
        }

        private void SaveMateriealRequest(int MRMNO, int ItemMasterID, string Quantity, int userid, string UOMID)
        {
            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TRAN_SAVEMateriealRequestDetails";
            cmd.Parameters.Add("@MRMID", SqlDbType.Int).Value = MRMNO;
            cmd.Parameters.Add("@ItemMasterID", SqlDbType.Int).Value = ItemMasterID;
            cmd.Parameters.Add("@Quantity", SqlDbType.Float).Value = Quantity;
            cmd.Parameters.Add("@UOMID", SqlDbType.VarChar).Value = UOMID;



            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userid;
            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();

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
        private void SaveMRIRequest(int MRMNO, int ItemMasterID, string Quantity, int userid, string UOMID, string Amount)
        {
            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TRAN_SAVE_MRI_DETAILS";
            cmd.Parameters.Add("@MRIID", SqlDbType.Int).Value = MRMNO;
            cmd.Parameters.Add("@ItemMasterID", SqlDbType.Int).Value = ItemMasterID;
            cmd.Parameters.Add("@MRIQuantity", SqlDbType.Float).Value = Quantity;
            cmd.Parameters.Add("@UOM", SqlDbType.VarChar).Value = UOMID;
            cmd.Parameters.Add("@Amount", SqlDbType.Float).Value = Amount;
            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();

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

        public class IdentityUpdateEventArgs : System.EventArgs
        {
            // add local member variable to hold text
            private string mFirstName;
            private string mMiddleName;
            private string mLastName;

            // class constructor
            public IdentityUpdateEventArgs(string sFirstName, string sMiddleName, string sLastName)
            {
                this.mFirstName = sFirstName;
                this.mMiddleName = sMiddleName;
                this.mLastName = sLastName;
            }

            // Properties - Accessible by the listener

            public string FirstName
            {
                get
                {
                    return mFirstName;
                }
            }

            public string MiddleName
            {
                get
                {
                    return mMiddleName;
                }
            }


            public string LastName
            {
                get
                {
                    return mLastName;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (TxtModule.Text == "MRM")
            {
                MaterialRequestMain formTask = new MaterialRequestMain();

                this.Dispose();
                formTask.Show();


                formTask.Test(txtMRMNO.Text.ToString());
                formTask.GetUserID(TxtUserID.Text);

            }
            else
            {
                Material_Releasing_Main formTask = new Material_Releasing_Main();
                formTask.GetUserID(TxtUserID.Text);
                formTask.Retrieverequest(txtMRMNO.Text.ToString());

                formTask.GetTranType(txttrantype.Text.ToString());


                this.Dispose();
                formTask.Show();

            }



        }

        private void ItemMasterLookUp_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (TxtModule.Text != "PMF")
            {
                MaterialRequestMain formTask = new MaterialRequestMain();
                formTask.GetUserID(TxtUserID.Text);
                this.Dispose();
                formTask.Show();


                formTask.Test(txtMRMNO.Text.ToString());
            }
        }

        private void TxtQuantity_TextChanged(object sender, EventArgs e)
        {

            if (TxtQuantity != null)
            {
                TxtQuantity.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
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

        private void grpSepecs_Enter(object sender, EventArgs e)
        {

        }

    }
}
