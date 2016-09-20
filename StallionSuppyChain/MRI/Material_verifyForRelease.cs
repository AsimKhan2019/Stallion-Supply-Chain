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
using StallionSuppyChain.Material_Releasing;
namespace StallionSuppyChain.MRI
{
    public partial class Material_verifyForRelease : Form
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();
        public Material_verifyForRelease()
        {
            InitializeComponent();
        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }
        public void GetTranType(string parameter1)
        {
            txttrantype.Text = parameter1;
            label1.Text = parameter1 + " #:";
        }
        private void Material_verifyForRelease_Load(object sender, EventArgs e)
        {
            LoadAllMRMForApproval();
            LoadAllMRMApproved();
            LoadAllMRMRejected();
        }

        private void LoadAllMRMForApproval()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[List_MRI_FORAPPROVAL]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Trantype", txttrantype.Text);
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

        private void LoadAllMRMApproved()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[List_MRI_APPROVED]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Trantype", txttrantype.Text);
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
        private void LoadAllMRMRejected()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[List_MRI_REJECTED]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Trantype", txttrantype.Text);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Material_Releasing_Main formTask = new Material_Releasing_Main();
            formTask.GetUserID(TxtUserID.Text);
            formTask.Retrieverequest(index.ToString());
            formTask.GetTranType(txttrantype.Text.ToString());


          formTask.GetAction("ForApproval");
            formTask.Show();
            this.Dispose();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            Material_Releasing_Main formTask = new Material_Releasing_Main();
            formTask.GetUserID(TxtUserID.Text);
            formTask.Retrieverequest(index.ToString());
            formTask.GetTranType(txttrantype.Text.ToString());


            formTask.GetAction("Approved");
            formTask.Show();
            this.Dispose();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {

            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            Material_Releasing_Main formTask = new Material_Releasing_Main();
            formTask.GetUserID(TxtUserID.Text);
            formTask.Retrieverequest(index.ToString());
            formTask.GetTranType(txttrantype.Text.ToString());


            formTask.GetAction("Rejected");
            formTask.Show();
            this.Dispose();
        }

    }
}
