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
        private int plannerId;

        public int ProductId { get; set; }

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
                    dgvProductAttributes.Rows.Add("Remove", 0, pa.ItemCode, pa.Quantity, pa.Category1, pa.Category2, pa.Category3, pa.ItemDescription1, pa.ItemDescription2, pa.ItemSpecs1, pa.ItemSpecs2, pa.UOM, pa.CostCode);
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
            LoadProductAttributes(ProductId);
            LoadProductPlanner();
        }

        private void LoadProductDetails()
        {
            var product = new Product().GetProduct(ProductId);
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
                SqlString = "SELECT null DeptCode, '--Select--' DepartmentName UNION SELECT DeptCode, DepartmentName FROM MSTR_DEPARTMENTS",
                DisplayMember = "DepartmentName",
                ValueMember = "DeptCode",
                ComboBox = cbDepartment
            });

            ListSqlString.Add(new SqlComboBox()
            {
                SqlString = "SELECT null Id, '--Select--' ProcessCode UNION SELECT Id, ProcessCode FROM REF_ProcessCodes",
                DisplayMember = "ProcessCode",
                ValueMember = "Id",
                ComboBox = cbProcessCode
            });

            var sql = "SELECT null UserId, '--Select--' User_Name UNION SELECT UserId, User_Name FROM ADMIN_Users";

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
            //DisplayProductDetails();
            LoadProductAttributes(ProductId);
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

        private void LoadProductPlanner()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isEdit = plannerId > 0 ? true : false;

            string sql = "INSERT INTO [dbo].[TRAN_ProductPlanner] (ProductId,BatchNo,StartDate,EndDate,ProcessCode,DeptCode,DateCreated,CreatedBy,ApprovedBy,TargetOutput,TargetOutputIn,ActualOutput,ActualOutputIn,WithDiscrepancy,Reason) " +
                         " output INSERTED.Id VALUES(@ProductId,@BatchNo,@StartDate,@EndDate,@ProcessCode,@DeptCode,@DateCreated,@CreatedBy,@ApprovedBy,@TargetOutput,@TargetOutputIn,@ActualOutput,@ActualOutputIn,@WithDiscrepancy,@Reason)";

            if (isEdit)
                sql = "UPDATE [dbo].[TRAN_ProductPlanner] SET ProductId=@ProductId,BatchNo=@BatchNo,StartDate=@StartDate,EndDate=@EndDate,ProcessCode=@ProcessCode," +
                        "DeptCode=@DeptCode,DateCreated=@DateCreated,CreatedBy=@CreatedBy,ApprovedBy=@ApprovedBy,TargetOutput=@TargetOutput,TargetOutputIn=@TargetOutputIn,ActualOutput=@ActualOutput,ActualOutputIn=@ActualOutputIn,WithDiscrepancy=@WithDiscrepancy,Reason=@Reason " +
                        "WHERE Id=@Id";

            var planner = new ProductPlannerModel()
            {
                ProductId = int.Parse(txtProductCode.Text),
                BatchNo = int.Parse(txtBatchNo.Text),
                StartDate = DateTime.Parse(dtpStartDate.Value.ToString()),
                EndDate = DateTime.Parse(dtpEndDate.Value.ToString()),
                ProcessCode = int.Parse(cbProcessCode.SelectedValue.ToString()),
                DeptCode = cbDepartment.SelectedValue.ToString(),
                DateCreated = DateTime.Parse(dtpDateCreated.Value.ToString()),
                CreatedBy = int.Parse(cbCreatedBy.SelectedValue.ToString()),
                ApprovedBy = int.Parse(cbApprovedBy.SelectedValue.ToString()),
                TargetOutput = int.Parse(txtTargetOutput.Text),
                TargetOutputIn = int.Parse(txtTargetOutputIn.Text),
                ActualOutput = int.Parse(txtActualOutput.Text),
                ActualOutputIn = int.Parse(txtActualOutputIn.Text),
                WithDiscrepancy = chkWithDiscrepancy.Checked,
                Reason = txtReason.Text
            };

            if (!isEdit)
                plannerId = new SC.ProductPlanner(sql, planner).Save();
            else
                new SC.ProductPlanner(sql, planner).Update();
        }
    }
}
