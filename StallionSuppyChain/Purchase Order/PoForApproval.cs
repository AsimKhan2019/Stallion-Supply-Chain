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
using StallionSuppyChain.Procurement;
namespace StallionSuppyChain.Purchase_Order
{
    public partial class PoForApproval : Form
    {
        public PoForApproval()
        {
            InitializeComponent();
        }
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;

        }
        private void PoForApproval_Load(object sender, EventArgs e)
        {
            LoadForApproval();
            LoadApproved();
            LoadRejected();
            GetProcurementCategory();
        }

        private void GetProcurementCategory()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_GET_PROCUREMENT_CATEGORY]", con);
                    adapter.Fill(dt);

                    cboCategoryType.DataSource = dt;
                    cboCategoryType.DisplayMember = "Category";
                    cboCategoryType.ValueMember = "ProcurementCategoryID";
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void LoadForApproval()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_PODetailsForApproval]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView2.DataSource = dt;
                    dataGridView2.Columns[0].Visible = false;
                }
            }

        }


        private void LoadRejected()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_PODetailsRejected]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView4.DataSource = dt;
                    dataGridView4.Columns[0].Visible = false;
                }
            }

        }




        private void LoadApproved()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_PODetailsApproved]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView3.DataSource = dt;
                    dataGridView3.Columns[0].Visible = false;
                }
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();
            formTask.GetUserID(TxtUserID.Text);
            formTask.RetrievePOForApproval(index.ToString(), "Approval");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView3_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(), "Approved");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView4_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();
            formTask.GetUserID(TxtUserID.Text);
            formTask.RetrievePOForApproval(index.ToString(), "Rejected");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a;
            a = tabControl2.SelectedIndex.ToString();


            string from = "";
            string TO = "";

            if (txtNeededfrom.Checked == true)
            {

                from = txtNeededfrom.Text;

            }
            if (txtNeededTo.Checked == true)
            {


                TO = txtNeededTo.Text;
            }



            SearchMRMParamDrftSubmitted(TxtPONO.Text.ToString(), "", cboCategoryType.Text, from, TO, a);

            
        }

        private void SearchMRMParamDrftSubmitted(string MRMNO, string ProjectCode, string PurchaseType, string DateNeededFROM, string DateNeededTo, string ProcessNO)
        {

            object DateNeededFROM1;
            object DateNeededTo1;
            if (DateNeededFROM == "")
            {

                DateNeededFROM1 = System.DBNull.Value;

            }
            else
            {

                DateNeededFROM1 = DateNeededFROM;

            }


            if (DateNeededTo == "")
            {

                DateNeededTo1 = System.DBNull.Value;

            }
            else
            {

                DateNeededTo1 = DateNeededTo;

            }



            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Search_POForApproval]", con);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@MRMNo", SqlDbType.VarChar).Value = MRMNO;
                cmd.Parameters.Add("@ProjectCode", SqlDbType.VarChar).Value = ProjectCode;
                cmd.Parameters.Add("@PurchaseType", SqlDbType.VarChar).Value = PurchaseType;
                cmd.Parameters.Add("@DateNeededFROM", SqlDbType.DateTime).Value = DateNeededFROM1;
                cmd.Parameters.Add("@DateNeededTo", SqlDbType.DateTime).Value = DateNeededTo1;

                cmd.Parameters.Add("@processID", SqlDbType.Int).Value = ProcessNO;



                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);

                    if (ProcessNO == "0")
                    {
                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[0].Visible = false;
                    }
                    else if (ProcessNO == "1")
                    {
                        dataGridView3.DataSource = dt;
                        dataGridView3.Columns[0].Visible = false;
                    }
                    else if (ProcessNO == "2")
                    {
                        dataGridView4.DataSource = dt;
                        dataGridView4.Columns[0].Visible = false;
                    }
                 

                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadForApproval();
            LoadApproved();
            LoadRejected();
            GetProcurementCategory();
        }
    }
}
