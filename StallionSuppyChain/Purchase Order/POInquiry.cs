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
    public partial class POInquiry : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public POInquiry()
        {
            InitializeComponent();
        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }
        private void POInquiry_Load(object sender, EventArgs e)
        {
            GetProjectCode();
            GetProcurementCategory();
            GetForApprovalList();
            GetOPENMRM();
            GetForDraft();
            GetApprovedPO();
            GetRejected();
        }

        private void GetProjectCode()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cboProjectCode.DataSource = dt;
                    cboProjectCode.DisplayMember = "ProjectName";
                    cboProjectCode.ValueMember = "ProjectID";

                }
                catch (Exception ex)
                {

                }
            }
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

        private void GetOPENMRM()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MRMOPENLIST]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;

                }
            }
        }
        private void GetForApprovalList()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_ForApproval]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView3.DataSource = dt;
                    dataGridView3.Columns[0].Visible = false;
                    dataGridView3.Columns[2].Visible = false;
                }
            }
        }

        private void GetApprovedPO()
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
                    dataGridView4.DataSource = dt;
                    dataGridView4.Columns[0].Visible = false;
                   
                }
            }
        }

        private void GetRejected()
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
                    dataGridView5.DataSource = dt;
                    dataGridView5.Columns[0].Visible = false;
                     
                }
            }
        }
        

        private void GetForDraft() 
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_PODraft]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView2.DataSource = dt;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[2].Visible = false;
                }
            }
        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //// var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            //PurchaseOrder formTask = new PurchaseOrder();

            //formTask.RetrievePOForApproval(index.ToString(), "");
            //formTask.Show();
            //this.Hide();
        }




        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
           // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(),"");
            formTask.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetProjectCode();
            GetProcurementCategory();
            GetForApprovalList();
            GetOPENMRM();
            GetForDraft();
            GetApprovedPO();
            GetRejected();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(), "");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            RequestBatch formTask = new RequestBatch();

            formTask.RetrievePO(index.ToString());
            formTask.Show();
            this.Hide();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(), "");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView3_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(), "");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView4_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(), "");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            // var index2 = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            PurchaseOrder formTask = new PurchaseOrder();

            formTask.RetrievePOForApproval(index.ToString(), "");
            formTask.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

            if (a.ToString() == "0")
            {
                SearchMRMParam(TxtPONO.Text.ToString(), cboProjectCode.Text, cboCategoryType.Text, from, TO, a);
            }
            else 
            {

                SearchMRMParamDrftSubmitted(TxtPONO.Text.ToString(), cboProjectCode.Text, cboCategoryType.Text, from, TO, a);

            }
        }


        private void SearchMRMParam(string MRMNO, string ProjectCode, string PurchaseType, string DateNeededFROM, string DateNeededTo, string ProcessNO)
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
                SqlCommand cmd = new SqlCommand("[dbo].[Search_MRMOPENLIST]", con);


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
                  
                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Visible = false;
                     


                }
            }

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
                SqlCommand cmd = new SqlCommand("[dbo].[Search_PODraft]", con);


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
                    
                    if(ProcessNO == "1")
                    {
                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[0].Visible = false;
                    }
                    else if (ProcessNO == "2")
                    {
                        dataGridView3.DataSource = dt;
                        dataGridView3.Columns[0].Visible = false;
                    }
                    else if (ProcessNO == "3")
                    {
                        dataGridView4.DataSource = dt;
                        dataGridView4.Columns[0].Visible = false;
                    }
                    else if (ProcessNO == "4")
                    {
                        dataGridView5.DataSource = dt;
                        dataGridView5.Columns[0].Visible = false;
                    }

                }
            }

        }
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {

                label5.Text = "MRM # :";
            }
            else
            {

                label5.Text = "PO # :";
            }

        }

        
    }
}
