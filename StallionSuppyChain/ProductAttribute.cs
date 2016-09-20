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

        public ProductAttribute() { }

        public ProductAttribute(string sqlString) {
            sql = sqlString;
        }

        public ProductAttribute(string sqlString, List<ProductAttributeModel> pa)
        {
            sql = sqlString;
            productAttr = pa;
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

                    if (edit)
                        Delete(con, productAttr[0].ProductId);

                    foreach (ProductAttributeModel pa in productAttr)
                    {
                        cmd.Parameters.AddWithValue("@ProductId", pa.ProductId);
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

        public int Delete(SqlConnection con, int productId)
        {
            using (var cmd = new SqlCommand("DELETE FROM [dbo].[TRAN_ProductAttributes] WHERE ProductId=@ProductId", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductId", productId);

                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public DataTable GetProductAttributes(int productId)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId",  productId);

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
