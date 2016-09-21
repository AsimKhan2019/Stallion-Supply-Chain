using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace StallionSuppyChain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public int StyleId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public int DivisionId { get; set; }
        public int? PriceId { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal StandardPrice { get; set; }
        public int? PrimaryBuyerId { get; set; }
        public decimal Weight { get; set; }
        public int? DefaultWhseId { get; set; }
        public int? SeasonId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateTo { get; set; }
        public bool IncludeCreatedDate { get; set; }

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private string sql;

        public Product()
        {
        }

        public Product(string sqlString)
        {
            sql = sqlString;
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
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@ProductTypeId", ProductTypeId);
                    cmd.Parameters.AddWithValue("@StyleId", StyleId);
                    cmd.Parameters.AddWithValue("@SizeId", SizeId);
                    cmd.Parameters.AddWithValue("@ColorId", ColorId);
                    cmd.Parameters.AddWithValue("@BrandId", BrandId);
                    cmd.Parameters.AddWithValue("@DivisionId", DivisionId);
                    cmd.Parameters.AddWithValue("@PriceId", (object)PriceId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RetailPrice", RetailPrice);
                    cmd.Parameters.AddWithValue("@StandardPrice", StandardPrice);
                    cmd.Parameters.AddWithValue("@PrimaryBuyerId", (object)PrimaryBuyerId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Weight", Weight);
                    cmd.Parameters.AddWithValue("@DefaultWhseId", (object)DefaultWhseId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SeasonId", (object)SeasonId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.Date).Value = DateTime.Now;

                    if (edit) cmd.Parameters.AddWithValue("@ProductId", ProductId);

                    con.Open();

                    if (!edit)
                        return (int)cmd.ExecuteScalar();
                    else
                        return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetProduct()
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DateFrom", CreatedDate);
                    cmd.Parameters.AddWithValue("@DateTo", CreatedDateTo);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@IncludeCreatedDate", IncludeCreatedDate);
                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public bool Delete(int Id)
        {
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    new ProductAttribute().Delete(con, Id);

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductId", Id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
