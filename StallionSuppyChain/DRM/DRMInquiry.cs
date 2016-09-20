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
namespace StallionSuppyChain.DRM
{
    
    public partial class DRMInquiry : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public DRMInquiry()
        {
            InitializeComponent();
        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }
        private void DRMInquiry_Load(object sender, EventArgs e)
        {
            GetApprovedPO();
            GetDRAFT();
            GetClosed();
        }
        private void GetApprovedPO()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_OPENPODetails]", con);
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
        private void GetDRAFT()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_DRMDRAFT]", con);
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
        private void GetClosed()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_DRMClosed]", con);
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
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            DRM formTask = new DRM();
            formTask.GetPOID(index.ToString(),"ApprovedPO");
            formTask.GetUserID(TxtUserID.Text);

            formTask.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            DRM formTask = new DRM();
            formTask.GetPOID(index.ToString(), "DRAFTDRM");
            formTask.GetUserID(TxtUserID.Text);

            formTask.Show();
            this.Hide();
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            DRM formTask = new DRM();
            formTask.GetPOID(index.ToString(), "SUBMITTEDTAB");
            formTask.GetUserID(TxtUserID.Text);

            formTask.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetApprovedPO();
            GetDRAFT();
            GetClosed();
        }
    }
}
