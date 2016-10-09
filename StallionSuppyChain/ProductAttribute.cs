using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace StallionSuppyChain
{
    public class ProductAttribute
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private string sql;
        private List<ProductAttributeModel> productAttr = new List<ProductAttributeModel>();
        private string transaction;

        public ProductAttribute() { }

        public ProductAttribute(string sqlString)
        {
            sql = sqlString;
        }

        public ProductAttribute(string sqlString, List<ProductAttributeModel> pa, string trans)
        {
            sql = sqlString;
            productAttr = pa;
            transaction = trans;
        }

        public int Save()
        {
            return Save(false);
        }

        public int Update()
        {
            return Save(true);
        }

        private int Save(bool edit)
        {
            int totalInserted = 0;
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    Delete(con);// productAttr[0].ProductId, productAttr[0].BatchNo);

                    foreach (ProductAttributeModel pa in productAttr)
                    {
                        if (pa.ProductId > 0)
                            cmd.Parameters.AddWithValue("@ProductId", pa.ProductId);
                        else if (pa.BatchNo > 0)
                            cmd.Parameters.AddWithValue("@BatchNo", pa.BatchNo);

                        cmd.Parameters.AddWithValue("@ItemMasterId", pa.ItemMasterId);
                        cmd.Parameters.AddWithValue("@Quantity", pa.Quantity);
                        cmd.Parameters.AddWithValue("@CategoryId1", pa.CategoryId1);
                        cmd.Parameters.AddWithValue("@CategoryId2", pa.CategoryId2);
                        cmd.Parameters.AddWithValue("@CategoryId3", pa.CategoryId3);
                        cmd.Parameters.AddWithValue("@ItemDescription1", pa.ItemDescription1);
                        cmd.Parameters.AddWithValue("@ItemDescription2", pa.ItemDescription2);
                        cmd.Parameters.AddWithValue("@ItemSpecs1", pa.ItemSpecs1);
                        cmd.Parameters.AddWithValue("@ItemSpecs2", pa.ItemSpecs2);
                        cmd.Parameters.AddWithValue("@UOM", pa.UOM);
                        cmd.Parameters.AddWithValue("@CostCode", pa.CostCode);
                        totalInserted += (int)cmd.ExecuteScalar();

                        cmd.Parameters.Clear();
                    }
                }
            }
            return totalInserted;
        }

        public int Delete(SqlConnection con)
        {
            string sqlString = "";
            int productId = 0;
            int batchNo = 0;

            if (transaction == "SUBMIT")
            {
                foreach (ProductAttributeModel pa in productAttr)
                {
                    new ProductPlanner().UpdateInventoryItemQuantity(pa.ItemMasterId, pa.BatchNo);
                    productId = pa.ProductId;
                    batchNo = pa.BatchNo;
                }
            }

            sqlString = "DELETE FROM [dbo].[TRAN_ProductPlannerMaterials] WHERE BatchNo=@BatchNo";

            using (var cmd = new SqlCommand(sqlString, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@BatchNo", batchNo);

                return (int)cmd.ExecuteNonQuery();
            }
        }

        public int Delete(SqlConnection con, int productId)
        {
            string sqlString = "";

            sqlString = "DELETE FROM [dbo].[TRAN_ProductAttributes] WHERE ProductId=@ProductId";

            using (var cmd = new SqlCommand(sqlString, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductId", productId);

                return (int)cmd.ExecuteNonQuery();
            }
        }


        public DataTable GetProductAttributes(int productId, int batchNo)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@BatchNo", batchNo);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }



    }

    public class ProductAttributeModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BatchNo { get; set; }
        public int ItemMasterId { get; set; }
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
        public int CategoryId1 { get; set; }
        public string Category1 { get; set; }
        public int CategoryId2 { get; set; }
        public string Category2 { get; set; }
        public int CategoryId3 { get; set; }
        public string Category3 { get; set; }
        public string ItemDescription1 { get; set; }
        public string ItemDescription2 { get; set; }
        public string ItemSpecs1 { get; set; }
        public string ItemSpecs2 { get; set; }
        public string UOM { get; set; }
        public string CostCode { get; set; }
    }
}
