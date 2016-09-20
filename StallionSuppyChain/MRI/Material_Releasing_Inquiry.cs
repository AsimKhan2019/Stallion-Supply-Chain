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

namespace StallionSuppyChain.Material_Releasing
{
    

    public partial class Material_Releasing_Inquiry : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Material_Releasing_Inquiry()
        {
            InitializeComponent();
        }

        #region
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNeededfrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        #endregion 

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

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            Material_Releasing_Project_Sel main = new Material_Releasing_Project_Sel();
            main.GetUserID(TxtUserID.Text);
            main.GetTranType( txttrantype.Text);

            this.Hide();
            main.Show();
        }

        private void GridDraft_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Material_Releasing_Inquiry_Load(object sender, EventArgs e)
        {
            LoadAllMRM();
            LoadAllSubmittedd();
            MaterialForRelease();
        }
        private void LoadAllMRM()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[List_MRI_DRAFT]", con);
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
        private void MaterialForRelease()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_MRMForRelease]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
              //  cmd.Parameters.AddWithValue("@MRMID", txttrantype.Text);
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
        private void LoadAllSubmittedd()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[List_MRI_ForVerification]", con);
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
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }
        public void GetTranType(string parameter1)
        {
            txttrantype.Text = parameter1;
            label1.Text = parameter1 + " #:";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Material_Releasing_Main formTask = new Material_Releasing_Main();
            formTask.GetUserID(TxtUserID.Text);
            formTask.Retrieverequest(index.ToString() );
            formTask.GetTranType(txttrantype.Text.ToString());
            formTask.Show();
            this.Dispose();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            Material_Releasing_Main formTask = new Material_Releasing_Main();
            formTask.GetUserID(TxtUserID.Text);
            formTask.Retrieverequest(index.ToString());

            formTask.Show();
            this.Dispose();
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            var index = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            MaterialRequestMain formTask = new MaterialRequestMain();
            formTask.GetMRMID(index.ToString(), "MRI");
            formTask.GetUserID(TxtUserID.Text);
            formTask.Show();
            this.Dispose();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
