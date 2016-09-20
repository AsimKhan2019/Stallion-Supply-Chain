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
namespace StallionSuppyChain.Procurement
{
    public partial class MaterialRequestForApproval : Form
    {
        public MaterialRequestForApproval()
        {
            InitializeComponent();
        }


        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        private void MaterialRequestForApproval_Load(object sender, EventArgs e)
        {
           // GetWorker1();
            GetProcurementType();
            GetProjectCode();
            LoadForApproval();
            LoadRejected();
            LoadApproved();

        }
        //private void GetWorker1()
        //{
        //    DataTable dt = new DataTable();

        //    using (SqlConnection con = new SqlConnection(conStr))
        //    {
        //        try
        //        {
        //            SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_WORKER_DETAILS]", con);
        //            adapter.Fill(dt);
        //            comboBox1.DataSource = dt;

        //            comboBox1.DisplayMember = "FULL_NAME";
        //            comboBox1.ValueMember = "WorkerID";

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //}
        private void GetProcurementType()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROCUREMENT_TYPE]", con);
                    adapter.Fill(dt);
                    cboCategoryType.DataSource = dt;


                    cboCategoryType.ValueMember = "ProcurementID";
                    cboCategoryType.DisplayMember = "Description";

                }
                catch (Exception ex)
                {

                }
            }
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
          
        private void LoadForApproval()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MRMRequestListForApproval]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@MRMID", MRMNO);
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
    
        private void LoadApproved()
        {

          


            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MRMRequestListApproved]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@MRMID", MRMNO);
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

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                row.Height = 100;
            }



        }
        private void LoadRejected()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MRMRequestListRejected]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@MRMID", MRMNO);
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
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
         
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadForApproval();
            LoadRejected();
            LoadApproved();
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            MaterialRequestMain formTask = new MaterialRequestMain();
            formTask.GetMRMID(index.ToString(), "true");
            formTask.GetUserID(TxtUserID.Text);


            formTask.Show();
            this.Dispose();

        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            MaterialRequestMain formTask = new MaterialRequestMain();
            formTask.GetMRMID(index.ToString(), "false");
            formTask.GetUserID(TxtUserID.Text);


            formTask.Show();
            this.Dispose();
        }

        private void dataGridView4_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            MaterialRequestMain formTask = new MaterialRequestMain();
            formTask.GetMRMID(index.ToString(), "false");
            formTask.GetUserID(TxtUserID.Text);


            formTask.Show();
            this.Dispose();
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

            SearchMRMParam(txtMRMNo.Text.ToString(), cboProjectCode.Text, cboCategoryType.Text, from, TO, a);
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
                SqlCommand cmd = new SqlCommand("[dbo].[Search_MRMRequestListForApproval]", con);


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
                    if (ProcessNO == "1")
                    {
                        dataGridView3.DataSource = dt;
                        dataGridView3.Columns[0].Visible = false;
                    }
                    if (ProcessNO == "2")
                    {
                        dataGridView4.DataSource = dt;
                        dataGridView4.Columns[0].Visible = false;
                    }

                }
            }

        }
    }
}
