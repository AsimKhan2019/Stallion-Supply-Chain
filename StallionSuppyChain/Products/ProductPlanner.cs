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
using SC = StallionSuppyChain;

namespace StallionSuppyChain.Products
{
    public partial class ProductPlanner : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private List<ProductAttributeModel> productAttributes = new List<ProductAttributeModel>();
        private string action = "Draft";

        public int ProductId { get; set; }
        public int BatchNo { get; set; }
        public string ActionButton { get; set; }

        public ProductPlanner()
        {
            InitializeComponent();
        }

        private void btnAddNewMaterial_Click(object sender, EventArgs e)
        {
            var productAttr = new List<ProductAttributeModel>();
            using (ItemMasterLookUp formItemMaster = new ItemMasterLookUp())
            {
                string productMasterFile = "PMF";
                formItemMaster.Test("", productMasterFile);
                formItemMaster.GetUserID("1");
                var result = formItemMaster.ShowDialog();

                if (result == DialogResult.OK)
                {
                    productAttr = formItemMaster.ProductAttributes;
                    productAttributes.AddRange(productAttr);
                }
                foreach (ProductAttributeModel pa in productAttr)
                {
                    dgvProductAttributes.Rows.Add("Remove", 0, pa.ItemCode, pa.Quantity, pa.Category1, pa.Category2, pa.Category3, pa.ItemDescription1, pa.ItemDescription2, pa.ItemSpecs1, pa.ItemSpecs2, pa.UOM, pa.CostCode, pa.ItemMasterId);
                }
            }
        }

        private void ProductPlanner_Load(object sender, EventArgs e)
        {
            txtProductCode.Text = ProductId.ToString();
            using (var con = new SqlConnection(conStr))
            {
                foreach (SqlComboBox sql in GetComboBoxSqlString())
                {
                    LoadDropdownListReference(con, sql.ComboBox, sql.SqlString, sql.DisplayMember, sql.ValueMember);
                }
            }
            LoadProductDetails();
            LoadProductAttributes();
            LoadProductPlanner();
            LoadProductPlannerHistory();
            SetControlAccessibility();

            if (ActionButton == "View")
            {
                tabControl1.SelectedTab = tabPage2;
                txtProductCode.ReadOnly = true;
                btnSearch.Visible = false;
            }
        }

        private void LoadProductDetails()
        {
            var product = new Product().GetProduct(ProductId);
            txtDescription.Text = "";
            txtProductType.Text = "";
            txtDivision.Text = "";
            txtBrand.Text = "";
            txtStyle.Text = "";
            txtSize.Text = "";
            txtColor.Text = "";
            if (product.Rows.Count == 0)
            {
                MessageBox.Show("Product code doesn't exist.", "No Record");
                return;
            }

            foreach (DataRow row in product.Rows)
            {
                txtDescription.Text = row["Description"].ToString();
                txtProductType.Text = row["Product Type"].ToString();
                txtDivision.Text = row["Division"].ToString();
                txtBrand.Text = row["Brand"].ToString();
                txtStyle.Text = row["Style"].ToString();
                txtSize.Text = row["Size"].ToString();
                txtColor.Text = row["Color"].ToString();
            }
        }

        private void LoadProductAttributes()
        {
            DataTable productAttr = new ProductAttribute("[dbo].[LIST_ProductAttributes]").GetProductAttributes(ProductId, BatchNo);
            dgvProductAttributes.Rows.Clear();
            dgvProductAttributes.Refresh();
            productAttributes = new List<ProductAttributeModel>();
            foreach (DataRow pa in productAttr.Rows)
            {
                dgvProductAttributes.Rows.Add("Remove", pa["Id"], pa["ItemCode"], pa["Quantity"], pa["Category1"], pa["Category2"], pa["Category3"],
                    pa["ItemDescription1"], pa["ItemDescription2"], pa["ItemSpecs1"], pa["ItemSpecs2"], pa["UOM"], pa["CostCode"], pa["ItemMasterId"]);

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

        private void LoadProductPlanner()
        {
            if (ActionButton == "New")
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string sql = "SELECT * FROM [dbo].[TRAN_ProductPlanner] WHERE BatchNo=@BatchNo";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@BatchNo", BatchNo);
                        con.Open();
                        SqlDataReader reader = null;
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            txtProductCode.Text = reader["ProductId"].ToString();
                            txtBatchNo.Text = reader["BatchNo"].ToString();

                            if (reader["Status"].ToString() != "")
                                cbStatus.SelectedValue = reader["Status"].ToString();
                            else
                                cbStatus.SelectedValue = DBNull.Value;

                            if (reader["ProcessCode"].ToString() != "")
                                cbProcessCode.SelectedValue = reader["ProcessCode"].ToString();
                            else
                                cbProcessCode.SelectedValue = DBNull.Value;

                            if (reader["DeptCode"].ToString() != "")
                                cbDepartment.SelectedValue = reader["DeptCode"].ToString();
                            else
                                cbDepartment.SelectedValue = DBNull.Value;

                            if (reader["CreatedBy"].ToString() != "")
                                cbCreatedBy.SelectedValue = reader["CreatedBy"].ToString();
                            else
                                cbCreatedBy.SelectedValue = DBNull.Value;

                            if (reader["ResponsiblePerson"].ToString() != "")
                                cbApprovedBy.SelectedValue = reader["ResponsiblePerson"].ToString();
                            else
                                cbApprovedBy.SelectedValue = DBNull.Value;

                            if (reader["TargetOutputIn"].ToString() != "")
                                cbTargetOutput.SelectedValue = reader["TargetOutputIn"].ToString();
                            else
                                cbTargetOutput.SelectedValue = DBNull.Value;

                            if (reader["ActualOutputIn"].ToString() != "")
                                cbActualOutput.SelectedValue = reader["ActualOutputIn"].ToString();
                            else
                                cbActualOutput.SelectedValue = DBNull.Value;

                            txtTargetOutput.Text = reader["TargetOutput"].ToString();
                            txtActualOutput.Text = reader["ActualOutput"].ToString();
                            txtDiscrepancy.Text = reader["Discrepancy"].ToString();
                            txtClipping.Text = reader["Clipping"].ToString();
                            txtReason.Text = reader["Reason"].ToString();

                            dtpDateCreated.Value = (DateTime.Parse(reader["DateCreated"].ToString()));
                            dtpEndDate.Value = (DateTime.Parse(reader["EndDate"].ToString()));
                            dtpStartDate.Value = (DateTime.Parse(reader["StartDate"].ToString()));
                            action = reader["Action"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadProductPlannerHistory()
        {
            if (ActionButton == "View")
            {
                DataTable history = new StallionSuppyChain.ProductPlanner("[dbo].[LIST_ProductPlannerHistory]", null).GetHistory(BatchNo);
                dgvHistory.DataSource = history;
            }
        }

        private void SetControlAccessibility()
        {
            switch (action)
            {
                case "Draft":
                    btnSave.Enabled = true;
                    txtProductCode.ReadOnly = false;
                    cbDepartment.Enabled = true;
                    cbCreatedBy.Enabled = true;
                    dtpDateCreated.Enabled = true;
                    cbApprovedBy.Enabled = true;
                    txtTargetOutput.ReadOnly = false;
                    cbTargetOutput.Enabled = true;
                    cbActualOutput.Enabled = true;
                    txtClipping.ReadOnly = false;
                    txtReason.ReadOnly = false;
                    break;
                case "Submitted":
                    btnSave.Enabled = false;
                    txtProductCode.ReadOnly = true;
                    cbDepartment.Enabled = false;
                    cbCreatedBy.Enabled = false;
                    dtpDateCreated.Enabled = false;
                    cbApprovedBy.Enabled = false;
                    txtTargetOutput.ReadOnly = true;
                    cbTargetOutput.Enabled = false;
                    cbActualOutput.Enabled = false;
                    txtClipping.ReadOnly = true;
                    txtReason.ReadOnly = true;
                    dgvProductAttributes.ReadOnly = true;
                    break;
            };
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

        private List<SqlComboBox> GetComboBoxSqlString()
        {
            List<SqlComboBox> ListSqlString = new List<SqlComboBox>();

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null DeptCode, ' --Select--' DepartmentName UNION SELECT DeptCode, DepartmentName FROM MSTR_DEPARTMENTS ORDER BY DepartmentName ASC",
                DisplayMember = "DepartmentName",
                ValueMember = "DeptCode",
                ComboBox = cbDepartment
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT Id, ProcessCode FROM REF_ProcessCodes ORDER BY ProcessCode ASC",
                DisplayMember = "ProcessCode",
                ValueMember = "Id",
                ComboBox = cbProcessCode
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT Id, Status FROM REF_Status",
                DisplayMember = "Status",
                ValueMember = "Id",
                ComboBox = cbStatus
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null UOMID, ' --Select--' UOM_CODE UNION SELECT UOMID, UOM_CODE + ' - ' + UOM_Description AS UOM_CODE FROM MSTR_UnitMeasurement ORDER BY UOM_CODE ASC",
                DisplayMember = "UOM_CODE",
                ValueMember = "UOMID",
                ComboBox = cbTargetOutput
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null UOMID, ' --Select--' UOM_CODE UNION SELECT UOMID, UOM_CODE + ' - ' + UOM_Description AS UOM_CODE FROM MSTR_UnitMeasurement  ORDER BY UOM_CODE ASC",
                DisplayMember = "UOM_CODE",
                ValueMember = "UOMID",
                ComboBox = cbActualOutput
            });

            var sql = "SELECT null UserId, ' --Select--' User_Name UNION SELECT UserId, User_Name FROM ADMIN_Users  ORDER BY User_Name ASC";

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = sql,
                DisplayMember = "User_Name",
                ValueMember = "UserId",
                ComboBox = cbCreatedBy
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = sql,
                DisplayMember = "User_Name",
                ValueMember = "UserId",
                ComboBox = cbApprovedBy
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProductId = int.Parse(txtProductCode.Text);
            LoadProductDetails();
            LoadProductAttributes();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetIntValues();
            SavePlanner("SAVE");
            ActionButton = "View";
            LoadProductPlannerHistory();
        }

        private void txtTargetOutput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtActualOutput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDiscrepancy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtClipping_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidInput("SUBMIT"))
            {
                SetIntValues();
                SavePlanner("SUBMIT");
                ActionButton = "View";
                LoadProductPlannerHistory();
                SetControlAccessibility();
            }
        }

        private bool IsValidInput(string transaction)
        {
            string requiredFieldMsg = "This field is required.";
            int totalInvalid = 0;

            if (txtProductCode.Text == "")
            {
                errorProvider.SetError(txtProductCode, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(txtProductCode, "");

            if (cbDepartment.SelectedIndex == 0)
            {
                errorProvider.SetError(cbDepartment, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbDepartment, "");

            if (cbApprovedBy.SelectedIndex == 0)
            {
                errorProvider.SetError(cbApprovedBy, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbApprovedBy, "");

            if (cbTargetOutput.SelectedIndex == 0)
            {
                errorProvider.SetError(cbTargetOutput, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbTargetOutput, "");

            if (txtTargetOutput.Text == "")
            {
                errorProvider.SetError(txtTargetOutput, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(txtTargetOutput, "");

            if (cbActualOutput.SelectedIndex == 0)
            {
                errorProvider.SetError(cbActualOutput, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(cbActualOutput, "");

            if (txtActualOutput.Text == "")
            {
                errorProvider.SetError(txtActualOutput, requiredFieldMsg);
                totalInvalid++;
            }
            else errorProvider.SetError(txtActualOutput, "");

            string message;
            if (cbStatus.SelectedText.ToUpper() == "OPEN" && transaction == "SUBMIT")
            {
                message = "Status value should not equal to Open.";
                errorProvider.SetError(cbStatus, message);
                totalInvalid++;

                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else errorProvider.SetError(cbStatus, "");

            if (int.Parse(txtDiscrepancy.Text) < 0)
            {
                message = "Invalid Target or Actual Output value. Discrepancy should not be equal to negative value.";
                errorProvider.SetError(txtTargetOutput, message);
                errorProvider.SetError(txtActualOutput, message);
                totalInvalid++;
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (totalInvalid == 0)
                if (cbProcessCode.Text.ToUpper() == "TRANSFER TO STORAGE")
                    cbStatus.Text = "Closed";

            return totalInvalid > 0 ? false : true;
        }

        private void SetIntValues()
        {
            if (txtTargetOutput.Text == "")
                txtTargetOutput.Text = "0";

            if (txtActualOutput.Text == "")
                txtActualOutput.Text = "0";

            if (txtDiscrepancy.Text == "")
                txtDiscrepancy.Text = "0";

            if (txtClipping.Text == "")
                txtClipping.Text = "0";
        }

        private void SavePlanner(string transaction)
        {
            if (!new Product().Exist(int.Parse(txtProductCode.Text)))
            {
                MessageBox.Show("Product code doesn't exist.", "Error");
                return;
            }

            bool isEdit = ActionButton == "View" ? true : false;

            string sql = "INSERT INTO [dbo].[TRAN_ProductPlanner] (ProductId,StartDate,EndDate,ProcessCode,Status,DeptCode,DateCreated,CreatedBy,ResponsiblePerson,TargetOutput,TargetOutputIn,ActualOutput,ActualOutputIn,Discrepancy,Clipping,Reason,Action) " +
                         " output INSERTED.BatchNo VALUES(@ProductId,@StartDate,@EndDate,@ProcessCode,@Status,@DeptCode,@DateCreated,@CreatedBy,@ResponsiblePerson,@TargetOutput,@TargetOutputIn,@ActualOutput,@ActualOutputIn,@Discrepancy,@Clipping,@Reason,@Action)";

            if (isEdit)
                sql = "UPDATE [dbo].[TRAN_ProductPlanner] SET ProductId=@ProductId,StartDate=@StartDate,EndDate=@EndDate,ProcessCode=@ProcessCode,Status=@Status,DeptCode=@DeptCode,DateCreated=@DateCreated,CreatedBy=@CreatedBy," +
                        "ResponsiblePerson=@ResponsiblePerson,TargetOutput=@TargetOutput,TargetOutputIn=@TargetOutputIn,ActualOutput=@ActualOutput,ActualOutputIn=@ActualOutputIn,Discrepancy=@Discrepancy,Clipping=@Clipping,Reason=@Reason, Action=@Action " +
                        "WHERE BatchNo=@BatchNo";

            var planner = new ProductPlannerModel()
            {
                ProductId = int.Parse(txtProductCode.Text),
                StartDate = dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
                ActualEndDate = dtpActualEndDate.Value,
                ProcessCode = int.Parse(cbProcessCode.SelectedValue.ToString()),
                Status = int.Parse(cbStatus.SelectedValue.ToString()),
                DeptCode = cbDepartment.SelectedValue.ToString(),
                DateCreated = dtpDateCreated.Value,
                CreatedBy = int.Parse(cbCreatedBy.SelectedValue.ToString() == "" ? "0" : cbCreatedBy.SelectedValue.ToString()),
                ResponsiblePerson = int.Parse(cbApprovedBy.SelectedValue.ToString() == "" ? "0" : cbApprovedBy.SelectedValue.ToString()),
                TargetOutput = int.Parse(txtTargetOutput.Text),
                TargetOutputIn = int.Parse(cbTargetOutput.SelectedValue.ToString() == "" ? "0" : cbTargetOutput.SelectedValue.ToString()),
                ActualOutput = int.Parse(txtActualOutput.Text),
                ActualOutputIn = int.Parse(cbActualOutput.SelectedValue.ToString() == "" ? "0" : cbActualOutput.SelectedValue.ToString()),
                Discrepancy = int.Parse(txtDiscrepancy.Text),
                Clipping = int.Parse(txtClipping.Text),
                Reason = txtReason.Text,
                Action = (transaction == "SUBMIT" ? (action = "Submitted") : (action = "Draft"))
            };

            if (!isEdit)
            {
                BatchNo = new SC.ProductPlanner(sql, planner).Save();
                txtBatchNo.Text = BatchNo.ToString();
            }
            else
            {
                planner.BatchNo = int.Parse(txtBatchNo.Text);
                new SC.ProductPlanner(sql, planner).Update();
            }

            if (txtBatchNo.Text != "" && productAttributes.Count() > 0)
            {

                foreach (var pa in productAttributes)
                {
                    pa.BatchNo = int.Parse(txtBatchNo.Text);
                    pa.ProductId = 0;
                }

                sql = "SELECT Quantity FROM [dbo].[MSTR_INVENTORY_MASTER] WHERE ITEMID=@ITEMID";

                bool isValidQuantity = true;
                foreach (DataGridViewRow row in dgvProductAttributes.Rows)
                {
                    int productAttrId = (int.Parse(row.Cells[1].Value.ToString()));
                    decimal quantity = (decimal.Parse(row.Cells[3].Value.ToString()));
                    int itemId = (int.Parse(row.Cells[13].Value.ToString()));

                    if (new StallionSuppyChain.ProductPlanner(sql).GetInventoryItemQuantity(itemId) <= quantity)
                    {
                        row.Cells[3].Style.BackColor = System.Drawing.Color.Red;
                        row.Cells[3].Style.ForeColor = System.Drawing.Color.White;
                        isValidQuantity = false;
                    }
                    else
                    {
                        row.Cells[3].Style.BackColor = System.Drawing.Color.White;
                        row.Cells[3].Style.ForeColor = System.Drawing.Color.Black;
                        productAttributes.Where(x => x.Id == productAttrId).FirstOrDefault().Quantity = quantity;
                    }
                }

                if (!isValidQuantity)
                {
                    MessageBox.Show("Inadequate materials.", "Error");
                    return;
                }

                sql = "INSERT INTO [dbo].[TRAN_ProductPlannerMaterials] (BatchNo,ItemMasterId,Quantity,Categoryid1,CategoryId2,CategoryId3,ItemDescription1,ItemDescription2,ItemSpecs1,ItemSpecs2,UOM,CostCode) " +
                   "output INSERTED.Id VALUES(@BatchNo,@ItemMasterId,@Quantity,@Categoryid1,@CategoryId2,@CategoryId3,@ItemDescription1,@ItemDescription2,@ItemSpecs1,@ItemSpecs2,@UOM,@CostCode)";

                var prodAttr = new ProductAttribute(sql, productAttributes, transaction);

                if (!isEdit)
                    prodAttr.Save();
                else
                    prodAttr.Update();

                if (transaction == "SUBMIT")
                {
                    foreach (var attr in productAttributes)
                    {
                        new SC.ProductPlanner().UpdateInventoryItemQuantity(attr.ItemMasterId, attr.Quantity, "minus");
                    }
                }
            }

            if (cbProcessCode.SelectedText.ToUpper() == "TRANSFER TO STORAGE" && cbStatus.SelectedText == "CLOSED")
            {
                new Product().SaveInventory(1, ProductId, (decimal.Parse(txtActualOutput.Text)));
            }

            MessageBox.Show("Successfully saved.", "Success");
        }

        private void txtActualOutput_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtActualOutput.Text == "")
                txtActualOutput.Text = "0";

            try
            {
                txtDiscrepancy.Text = (int.Parse(txtTargetOutput.Text) - int.Parse(txtActualOutput.Text)).ToString();
            }
            catch (Exception) { };
        }

        private void txtTargetOutput_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTargetOutput.Text == "")
                txtTargetOutput.Text = "0";

            try
            {
                txtDiscrepancy.Text = (int.Parse(txtTargetOutput.Text) - int.Parse(txtActualOutput.Text)).ToString();
            }
            catch (Exception) { };
        }
    }
}
