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
        private string action;

        public ProductPlanner()
        {

        }

        public ProductPlanner(string sqlString)
        {
            sql = sqlString;
        }

        public ProductPlanner(string sqlString, ProductPlannerModel model)
        {
            sql = sqlString;
            productPlanner = model;
        }

        public int Save()
        {
            action = "SAVE";
            return Save(false);
        }

        public int Update()
        {
            action = "SUBMIT";
            return Save(true);
        }

        private int Save(bool edit)
        {
            int batchNo = 0;
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@StartDate", SqlDbType.DateTime).Value = productPlanner.StartDate;
                    cmd.Parameters.AddWithValue("@EndDate", SqlDbType.DateTime).Value = productPlanner.EndDate;
                    cmd.Parameters.AddWithValue("@ProcessCode", productPlanner.ProcessCode);
                    cmd.Parameters.AddWithValue("@Status", productPlanner.Status);
                    cmd.Parameters.AddWithValue("@ActualOutput", (object)productPlanner.ActualOutput ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Discrepancy", productPlanner.Discrepancy);
                    cmd.Parameters.AddWithValue("@ProductId", productPlanner.ProductId);
                    cmd.Parameters.AddWithValue("@DeptCode", productPlanner.DeptCode == "" ? (object)DBNull.Value : productPlanner.DeptCode);
                    cmd.Parameters.AddWithValue("@DateCreated", SqlDbType.DateTime).Value = productPlanner.DateCreated;
                    cmd.Parameters.AddWithValue("@CreatedBy", productPlanner.CreatedBy == 0 ? (object)DBNull.Value : productPlanner.CreatedBy);
                    cmd.Parameters.AddWithValue("@ResponsiblePerson", productPlanner.ResponsiblePerson == 0 ? (object)DBNull.Value : productPlanner.ResponsiblePerson);
                    cmd.Parameters.AddWithValue("@TargetOutput", (object)productPlanner.TargetOutput ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TargetOutputIn", productPlanner.TargetOutputIn == 0 ? (object)DBNull.Value : productPlanner.TargetOutputIn);
                    cmd.Parameters.AddWithValue("@ActualOutputIn", productPlanner.ActualOutputIn == 0 ? (object)DBNull.Value : productPlanner.ActualOutputIn);
                    cmd.Parameters.AddWithValue("@Clipping", productPlanner.Clipping);
                    cmd.Parameters.AddWithValue("@Reason", productPlanner.Reason);

                    if (action == "SUBMIT")
                        cmd.Parameters.AddWithValue("@Action", "Submitted");
                    else if (action == "SAVE")
                        cmd.Parameters.AddWithValue("@Action", "Draft");

                    con.Open();

                    if (edit) cmd.Parameters.AddWithValue("@BatchNo", productPlanner.BatchNo);

                    if (!edit)
                    {
                        batchNo = (int)cmd.ExecuteScalar();
                        productPlanner.BatchNo = batchNo;
                    }
                    else
                    {
                        batchNo = productPlanner.BatchNo;
                        cmd.ExecuteNonQuery();
                    }

                    SaveHistory(edit);
                }
            }
            return batchNo;
        }

        private int SaveHistory(bool edit)
        {
            if (!edit || action == "SUBMIT")
            {
                sql = "INSERT INTO TRAN_ProductPlannerHistory (BatchNo,StartDateTime,EndDateTime,ActualEndDateTime,Status,ProcessCode,DateCreated,CreatedBy,TargetOutput,ActualOutput)" +
                      " OUTPUT INSERTED.Id VALUES (@BatchNo,@StartDateTime,@EndDateTime,@ActualEndDateTime,@Status,@ProcessCode,@DateCreated,@CreatedBy,@TargetOutput,@ActualOutput)";
            }
            else
            {
                sql = "UPDATE TRAN_ProductPlannerHistory SET StartDateTime=@StartDateTime,EndDateTime=@EndDateTime,ActualEndDateTime=@ActualEndDateTime,Status=@Status,ProcessCode=@ProcessCode,ActualOutput=@ActualOutput " +
                      " WHERE BatchNo=@BatchNo ";
            }

            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    cmd.Parameters.AddWithValue("@BatchNo", productPlanner.BatchNo);
                    cmd.Parameters.AddWithValue("@StartDateTime", SqlDbType.DateTime).Value = productPlanner.StartDate;
                    cmd.Parameters.AddWithValue("@EndDateTime", SqlDbType.DateTime).Value = productPlanner.EndDate;
                    cmd.Parameters.AddWithValue("@ActualEndDateTime", SqlDbType.DateTime).Value = productPlanner.ActualEndDate;
                    cmd.Parameters.AddWithValue("@Status", productPlanner.Status);
                    cmd.Parameters.AddWithValue("@ProcessCode", productPlanner.ProcessCode);
                    cmd.Parameters.AddWithValue("@ActualOutput", (object)productPlanner.ActualOutput ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DateCreated", SqlDbType.DateTime).Value = productPlanner.DateCreated;
                    cmd.Parameters.AddWithValue("@CreatedBy", productPlanner.CreatedBy);
                    cmd.Parameters.AddWithValue("@TargetOutput", (object)productPlanner.TargetOutput ?? DBNull.Value);

                    if (!edit)
                        return (int)cmd.ExecuteScalar();
                    else
                        return (int)cmd.ExecuteNonQuery(); ;
                }
            }
        }

        public DataTable GetHistory(int batchNo)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BatchNo", batchNo);
                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        return dt;
                    }
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

        public decimal GetInventoryItemQuantity(int itemMasterId)
        {
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ITEMID", itemMasterId);
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        return decimal.Parse(result.ToString());
                    else return 0;
                }
            }
        }

        public bool UpdateInventoryItemQuantity(int itemMasterId, decimal value, string action)
        {
            if (action == "add")
                sql = "UPDATE [dbo].[MSTR_INVENTORY_MASTER] SET Quantity = (Quantity + @value) WHERE ITEMID=@ItemId";
            else if (action == "minus")
                sql = "UPDATE [dbo].[MSTR_INVENTORY_MASTER] SET Quantity = (Quantity - @value) WHERE ITEMID=@ItemId";

            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ItemId", itemMasterId);
                    cmd.Parameters.AddWithValue("@value", value);
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }

        public bool UpdateInventoryItemQuantity(int itemMasterId, int batchNo)
        {
            using (var sqlcon = new SqlConnection(conStr))
            {
                using (var command = new SqlCommand("SELECT * FROM TRAN_ProductPlannerMaterials WHERE BatchNo=@BatchNo AND ItemMasterID=@ItemMasterID", sqlcon))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@BatchNo", batchNo);
                    command.Parameters.AddWithValue("@ItemMasterID", itemMasterId);
                    sqlcon.Open();
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            UpdateInventoryItemQuantity(itemMasterId, (decimal.Parse(rdr["Quantity"].ToString())), "add");
                        }
                    }

                    return true;
                }
            }
        }
    }

    public class ProductPlannerModel
    {
        public int BatchNo { get; set; }
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public int ProcessCode { get; set; }
        public int Status { get; set; }
        public string DeptCode { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CreatedBy { get; set; }
        public int? ResponsiblePerson { get; set; }
        public int TargetOutput { get; set; }
        public int? TargetOutputIn { get; set; }
        public int ActualOutput { get; set; }
        public int? ActualOutputIn { get; set; }
        public int Discrepancy { get; set; }
        public int Clipping { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
    }
}
