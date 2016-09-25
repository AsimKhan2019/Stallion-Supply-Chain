using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace StallionSuppyChain
{
    public class ProductPlanner
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private string sql;
        private ProductPlannerModel productPlanner;



        public ProductPlanner(string sqlString, ProductPlannerModel model)
        {
            sql = sqlString;
            productPlanner = model;
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
                    cmd.Parameters.AddWithValue("@ProductId", productPlanner.ProductId);
                    cmd.Parameters.AddWithValue("@BatchNo", productPlanner.BatchNo);
                    cmd.Parameters.AddWithValue("@StartDate", SqlDbType.Date).Value = productPlanner.StartDate;
                    cmd.Parameters.AddWithValue("@EndDate", SqlDbType.Date).Value = productPlanner.EndDate;
                    cmd.Parameters.AddWithValue("@ProcessCode", productPlanner.ProcessCode);
                    cmd.Parameters.AddWithValue("@DeptCode", productPlanner.DeptCode);
                    cmd.Parameters.AddWithValue("@DateCreated", SqlDbType.Date).Value = productPlanner.DateCreated;
                    cmd.Parameters.AddWithValue("@CreatedBy", productPlanner.CreatedBy);
                    cmd.Parameters.AddWithValue("@ApprovedBy", productPlanner.ApprovedBy);
                    cmd.Parameters.AddWithValue("@TargetOutput", (object)productPlanner.TargetOutput ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TargetOutputIn", (object)productPlanner.TargetOutputIn ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ActualOutput", (object)productPlanner.ActualOutput ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ActualOutputIn", (object)productPlanner.ActualOutputIn ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@WithDiscrepancy", productPlanner.WithDiscrepancy);
                    cmd.Parameters.AddWithValue("@Reason", productPlanner.Reason);

                    con.Open();

                    if (edit) cmd.Parameters.AddWithValue("@Id", productPlanner.Id);

                    if (!edit)
                        return (int)cmd.ExecuteScalar();
                    else
                        return cmd.ExecuteNonQuery();
                }
            }
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
    }

    public class ProductPlannerModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BatchNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProcessCode { get; set; }
        public string DeptCode { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public int ApprovedBy { get; set; }
        public int TargetOutput { get; set; }
        public int TargetOutputIn { get; set; }
        public int ActualOutput { get; set; }
        public int ActualOutputIn { get; set; }
        public bool WithDiscrepancy { get; set; }
        public string Reason { get; set; }

    }
}
