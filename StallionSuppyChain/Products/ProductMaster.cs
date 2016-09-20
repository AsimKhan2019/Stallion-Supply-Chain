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

namespace StallionSuppyChain.Products
{
    public partial class ProductMaster : Form
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private bool fromProductListing = false;
        private List<ProductAttributeModel> productAttributes = new List<ProductAttributeModel>();

        public ProductMaster()
        {
            InitializeComponent();
        }

        private void ProductMaster_Load(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            btnDelete.Enabled = false;
            using (var con = new SqlConnection(conStr))
            {
                try
                {
                    foreach (SqlComboBox sql in GetSqlCommand())
                    {
                        LoadDropdownListReference(con, sql.ComboBox, sql.SqlString, sql.DisplayMember, sql.ValueMember);
                    }
                    cbStatus.SelectedIndex = 0;

                    dgvProducts.DataSource = new Product("LIST_Products").GetProduct();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private List<SqlComboBox> GetSqlCommand()
        {
            List<SqlComboBox> ListSqlString = new List<SqlComboBox>();

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' ProductTypeName UNION SELECT Id, ProductTypeName FROM REF_ProductTypes",
                DisplayMember = "ProductTypeName",
                ValueMember = "Id",
                ComboBox = cbProductType
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' StyleName UNION SELECT Id, StyleName FROM REF_Styles",
                DisplayMember = "StyleName",
                ValueMember = "Id",
                ComboBox = cbStyle
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' SizeName UNION SELECT Id, SizeName FROM REF_Sizes",
                DisplayMember = "SizeName",
                ValueMember = "Id",
                ComboBox = cbSize
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' ColorName UNION SELECT Id, ColorName FROM REF_Colors",
                DisplayMember = "ColorName",
                ValueMember = "Id",
                ComboBox = cbColor
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' BrandName UNION SELECT Id, BrandName FROM REF_Brand",
                DisplayMember = "BrandName",
                ValueMember = "Id",
                ComboBox = cbBrand
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' DivisionName UNION SELECT Id, DivisionName FROM REF_Divisions",
                DisplayMember = "DivisionName",
                ValueMember = "Id",
                ComboBox = cbDivision
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' PriceName UNION SELECT Id, PriceName FROM REF_Prices",
                DisplayMember = "PriceName",
                ValueMember = "Id",
                ComboBox = cbPriceCode
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' PrimaryBuyerName UNION SELECT Id, PrimaryBuyerName FROM REF_PrimaryBuyers",
                DisplayMember = "PrimaryBuyerName",
                ValueMember = "Id",
                ComboBox = cbPrimaryBuyer
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' SeasonName UNION SELECT Id, SeasonName FROM REF_Seasons",
                DisplayMember = "SeasonName",
                ValueMember = "Id",
                ComboBox = cbSeason
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null ProjectId, '--Select--' ProjectName UNION SELECT ProjectId, ProjectName FROM MSTR_Projects",
                DisplayMember = "ProjectName",
                ValueMember = "ProjectId",
                ComboBox = cbDefaultWarehouse
            });

            return ListSqlString;

        }

        private void LoadDropdownListReference(SqlConnection con, ComboBox cb, string sql, string displayMember, string valueMember)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                    cb.DataSource = dt;
                    cb.DisplayMember = displayMember;
                    cb.ValueMember = valueMember;
                }
            }
        }

        private void btnProductAttr_Click(object sender, EventArgs e)
        {
            var productAttr = new List<ProductAttributeModel>();
            using (ItemMasterLookUp formTask = new ItemMasterLookUp())
            {
                string productMasterFile = "PMF";
                formTask.Test("", productMasterFile);
                formTask.GetUserID("1");
                var result = formTask.ShowDialog();

                if (result == DialogResult.OK)
                {
                    productAttr = formTask.ProductAttributes;
                    productAttributes.AddRange(productAttr);
                }
                foreach (ProductAttributeModel pa in productAttr)
                {
                    dgvProductAttributes.Rows.Add("Remove", 0, pa.ItemCode, pa.Quantity, pa.Category1, pa.Category2, pa.Category3, pa.ItemDescription1, pa.ItemDescription2, pa.ItemSpecs1, pa.ItemSpecs2, pa.UOM, pa.CostCode);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtProductCode.Text = "";
            cbStatus.SelectedIndex = 0;
            txtBarcode.Text = "";
            txtDescription.Text = "";
            cbProductType.SelectedIndex = 0;
            cbDivision.SelectedIndex = 0;
            cbBrand.SelectedIndex = 0;
            cbColor.SelectedIndex = 0;
            cbSize.SelectedIndex = 0;
            cbStyle.SelectedIndex = 0;
            cbPriceCode.SelectedIndex = 0;
            txtRetailPrice.Text = "";
            txtStandardPrice.Text = "";
            cbPrimaryBuyer.SelectedIndex = 0;
            txtWeight.Text = "";
            cbDefaultWarehouse.SelectedIndex = 0;
            cbSeason.SelectedIndex = 0;
            btnNew.Enabled = false;
            btnDelete.Enabled = false;
            productAttributes = new List<ProductAttributeModel>();
            dgvProductAttributes.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isEdit = txtProductCode.Text != "" ? true : false;

            if (IsValidInput())
            {
                string sql = "";

                if (!isEdit)
                {
                    sql = "INSERT INTO [dbo].[MSTR_Products] (Status, Description,ProductTypeId,StyleId, " +
                            "SizeId,ColorId,BrandId,DivisionId,PriceId,RetailPrice,StandardPrice, " +
                            "PrimaryBuyerId,Weight,DefaultWhseId,SeasonId,CreatedBy,CreatedDate) " +
                            "output INSERTED.ProductId VALUES(@Status, @Description,@ProductTypeId,@StyleId, " +
                            "@SizeId,@ColorId,@BrandId,@DivisionId,@PriceId,@RetailPrice,@StandardPrice, " +
                            "@PrimaryBuyerId,@Weight,@DefaultWhseId,@SeasonId,@CreatedBy,@CreatedDate)";
                }
                else
                {
                    sql = "UPDATE [dbo].[MSTR_Products] SET Status=@Status, Description=@Description,ProductTypeId=@ProductTypeId,StyleId=@StyleId, " +
                            "SizeId=@SizeId,ColorId=@ColorId,BrandId=@BrandId,DivisionId=@DivisionId,PriceId=@PriceId,RetailPrice=@RetailPrice,StandardPrice=@StandardPrice, " +
                            "PrimaryBuyerId=@PrimaryBuyerId,Weight=@Weight,DefaultWhseId=@DefaultWhseId,SeasonId=@SeasonId,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate " +
                            "WHERE ProductId=@ProductId";
                }

                try
                {
                    var product = new Product(sql)
                    {
                        BrandId = (int)cbBrand.SelectedValue,
                        ColorId = (int)cbColor.SelectedValue,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                        DefaultWhseId = string.IsNullOrEmpty(cbDefaultWarehouse.SelectedValue.ToString()) ? (int?)null : int.Parse(cbDefaultWarehouse.SelectedValue.ToString()),
                        Description = txtDescription.Text,
                        DivisionId = (int)cbDivision.SelectedValue,
                        PriceId = string.IsNullOrEmpty(cbPriceCode.SelectedValue.ToString()) ? (int?)null : int.Parse(cbPriceCode.SelectedValue.ToString()),
                        PrimaryBuyerId = string.IsNullOrEmpty(cbPrimaryBuyer.SelectedValue.ToString()) ? (int?)null : int.Parse(cbPrimaryBuyer.SelectedValue.ToString()),
                        ProductTypeId = (int)cbProductType.SelectedValue,
                        RetailPrice = Decimal.Parse(txtRetailPrice.Text),
                        SeasonId = string.IsNullOrEmpty(cbSeason.SelectedValue.ToString()) ? (int?)null : int.Parse(cbSeason.SelectedValue.ToString()),
                        SizeId = (int)cbSize.SelectedValue,
                        StandardPrice = Decimal.Parse(txtStandardPrice.Text),
                        Status = cbStatus.Text,
                        StyleId = (int)cbStyle.SelectedValue,
                        Weight = Decimal.Parse(txtWeight.Text)
                    };

                    if (!isEdit)
                    {
                        txtProductCode.Text = product.Save().ToString();
                        txtBarcode.Text = txtProductCode.Text;
                    }
                    else
                    {
                        product.ProductId = int.Parse(txtProductCode.Text);
                        product.Update();
                    }
                    if (txtProductCode.Text != "" && productAttributes.Count() > 0)
                    {
                        sql = "INSERT INTO [dbo].[TRAN_ProductAttributes] (ProductId,ItemMasterId,Quantity,Categoryid1,CategoryId2,CategoryId3,ItemDescription1,ItemDescription2,ItemSpecs1,ItemSpecs2,UOM,CostCode) " +
                            "output INSERTED.Id VALUES(@ProductId,@ItemMasterId,@Quantity,@Categoryid1,@CategoryId2,@CategoryId3,@ItemDescription1,@ItemDescription2,@ItemSpecs1,@ItemSpecs2,@UOM,@CostCode)";
                        foreach (var pa in productAttributes)
                            pa.ProductId = int.Parse(txtProductCode.Text);

                        var prodAttr = new ProductAttribute(sql, productAttributes);
                        if (!isEdit)
                            prodAttr.Save();
                        else
                            prodAttr.Update();
                    }

                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    dgvProducts.DataSource = new Product("LIST_Products").GetProduct();

                    MessageBox.Show("Details has been saved successfully.", "Products");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool IsValidInput()
        {
            string requiredFieldMsg = "This field is required.";
            int totalInvalid = 0;

            if (cbProductType.SelectedIndex == 0)
            {
                errorProvider.SetError(cbProductType, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbProductType, "");

            if (cbDivision.SelectedIndex == 0)
            {
                errorProvider.SetError(cbDivision, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbDivision, "");

            if (cbBrand.SelectedIndex == 0)
            {
                errorProvider.SetError(cbBrand, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbBrand, "");

            if (cbDivision.SelectedIndex == 0)
            {
                errorProvider.SetError(cbDivision, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbDivision, "");

            if (cbColor.SelectedIndex == 0)
            {
                errorProvider.SetError(cbColor, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbColor, "");

            if (cbSize.SelectedIndex == 0)
            {
                errorProvider.SetError(cbSize, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbSize, "");

            if (cbStyle.SelectedIndex == 0)
            {
                errorProvider.SetError(cbStyle, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbStyle, "");

            if (txtWeight.Text == "")
                txtWeight.Text = "0.00";

            return totalInvalid > 0 ? false : true;
        }

        private void cbPriceCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!fromProductListing)
            {
                try
                {
                    if (cbPriceCode.SelectedValue.ToString() != "")
                    {
                        using (SqlConnection con = new SqlConnection(conStr))
                        {
                            string sql = "SELECT RetailPrice, StandardPrice FROM [dbo].[REF_Prices] WHERE Id=@PriceId";
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@PriceId", (int)cbPriceCode.SelectedValue);
                                con.Open();
                                SqlDataReader reader = null;
                                reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    txtStandardPrice.Text = reader["StandardPrice"].ToString();
                                    txtRetailPrice.Text = reader["RetailPrice"].ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        txtStandardPrice.Text = "0.00";
                        txtRetailPrice.Text = "0.00";
                    }
                }
                catch (Exception)
                {

                }
            }
            fromProductListing = false;
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void dgvProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int Id = (int)dgvProducts.CurrentRow.Cells[0].Value;
                LoadProductById(Id);
                LoadProductAttributes(Id);
                if (Id > 0)
                    btnDelete.Enabled = true;
            }
            catch (Exception)
            {
            }
        }

        private void LoadProductById(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = "SELECT * FROM [dbo].[MSTR_Products] WHERE ProductId=@ProductId";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ProductId", Id);
                        con.Open();
                        SqlDataReader reader = null;
                        reader = cmd.ExecuteReader();
                        fromProductListing = true;
                        while (reader.Read())
                        {
                            txtProductCode.Text = reader["ProductId"].ToString();
                            txtBarcode.Text = reader["ProductId"].ToString();
                            cbStatus.Text = reader["Status"].ToString();
                            txtDescription.Text = reader["Description"].ToString();
                            cbProductType.SelectedValue = reader["ProductTypeId"].ToString();
                            cbDivision.SelectedValue = reader["DivisionId"].ToString();
                            cbBrand.SelectedValue = reader["BrandId"].ToString();
                            cbColor.SelectedValue = reader["ColorId"].ToString();
                            cbSize.SelectedValue = reader["SizeId"].ToString();
                            cbStyle.SelectedValue = reader["StyleId"].ToString();

                            if (reader["PriceId"].ToString() != "")
                                cbPriceCode.SelectedValue = reader["PriceId"].ToString();
                            else
                                cbPriceCode.SelectedValue = DBNull.Value;

                            if (reader["PrimaryBuyerId"].ToString() != "")
                                cbPrimaryBuyer.SelectedValue = reader["PrimaryBuyerId"].ToString();
                            else
                                cbPrimaryBuyer.SelectedValue = DBNull.Value;

                            if (reader["DefaultWhseId"].ToString() != "")
                                cbDefaultWarehouse.SelectedValue = reader["DefaultWhseId"].ToString();
                            else
                                cbDefaultWarehouse.SelectedValue = DBNull.Value;

                            if (reader["SeasonId"].ToString() != "")
                                cbSeason.SelectedValue = reader["SeasonId"].ToString();
                            else
                                cbSeason.SelectedValue = DBNull.Value;

                            txtStandardPrice.Text = reader["StandardPrice"].ToString();
                            txtRetailPrice.Text = reader["RetailPrice"].ToString();
                            txtWeight.Text = reader["Weight"].ToString();
                        }
                    }
                }
                tabControl1.SelectedTab = tabPage1;
            }
            catch (Exception)
            {

            }
        }

        private void LoadProductAttributes(int Id)
        {
            DataTable productAttr = new ProductAttribute("[dbo].[LIST_ProductAttributes]").GetProductAttributes(Id);
            dgvProductAttributes.Rows.Clear();
            dgvProductAttributes.Refresh();
            productAttributes = new List<ProductAttributeModel>();
            foreach (DataRow pa in productAttr.Rows)
            {
                dgvProductAttributes.Rows.Add("Remove", pa["Id"], pa["ItemCode"], pa["Quantity"], pa["Category1"], pa["Category2"], pa["Category3"],
                    pa["ItemDescription1"], pa["ItemDescription2"], pa["ItemSpecs1"], pa["ItemSpecs2"], pa["UOM"], pa["CostCode"]);

                var model = new ProductAttributeModel()
                {
                    Category1 = pa["Category1"].ToString(),
                    Category2 = pa["Category2"].ToString(),
                    Category3 = pa["Category3"].ToString(),
                    CategoryId1 = (int)pa["CategoryId1"],
                    CategoryId2 = (int)pa["CategoryId2"],
                    CategoryId3 = (int)pa["CategoryId3"],
                    CostCode = pa["CostCode"].ToString(),
                    Id = (int)pa["Id"],
                    ItemCode = pa["ItemCode"].ToString(),
                    ItemDescription1 = pa["ItemDescription1"].ToString(),
                    ItemDescription2 = pa["ItemDescription2"].ToString(),
                    ItemMasterId = (int)pa["ItemMasterId"],
                    ItemSpecs1 = pa["ItemSpecs1"].ToString(),
                    ItemSpecs2 = pa["ItemSpecs2"].ToString(),
                    ProductId = int.Parse(txtProductCode.Text),
                    Quantity = (decimal)pa["Quantity"],
                    UOM = pa["UOM"].ToString()
                };

                productAttributes.Add(model);
            }
        }

        private void dgvProductAttributes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProductAttributes.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete selected product attributes?", "Attributes deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.dgvProductAttributes.Rows.RemoveAt(e.RowIndex);
                    productAttributes.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product details?", "Product deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                new Product("DELETE FROM [dbo].[MSTR_Products] WHERE ProductId=@ProductId").Delete(int.Parse(txtProductCode.Text));
                dgvProducts.DataSource = new Product("LIST_Products").GetProduct();
                btnNew_Click(sender, e);
            }
        }

        private void txtWeight_Leave(object sender, EventArgs e)
        {
            if (txtWeight.Text == "")
                txtWeight.Text = "0.00";
        }


    }
}

