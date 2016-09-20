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

namespace StallionSuppyChain.Admin_User_Role
{
    public partial class Admin_Role_Inquiry : Form
    {
        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Admin_Role_Inquiry()
        {
            InitializeComponent();
        }

        public void LoadUserRole(string UserID)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("LIST_USER_ROLE", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                cmd.ExecuteNonQuery();

                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[7].Visible = false;

                }
            }
        }

        private void Admin_Role_Inquiry_Load(object sender, EventArgs e)
        {
            LoadUserRole("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UserID = textBox1.Text;
            LoadUserRole(UserID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_Role_Save addForm = new Admin_Role_Save();
            addForm.GetUserID(TxtUserID.Text);
            addForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please Select Row in GridView");
            }
            else
            {

                //int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                //DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                //int val1 = Convert.ToInt32(selectedRow.Cells[0].Value);

                try
                {
                    SqlConnection con = new SqlConnection(conStr);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TRAN_DELETE_ADMIN_ROLE";

                    cmd.Parameters.Add("@UserRoleID", SqlDbType.Int).Value = Convert.ToInt32( textBox2.Text);
                    

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    LoadUserRole("");

                }
                catch (Exception ex)
                {
                    string errMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    MessageBox.Show(errMessage);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 
              textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select Row in GridView");
            }
            else
            {
                Admin_Role_Update UpdateForm = new Admin_Role_Update();

                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                string val1 = Convert.ToString(selectedRow.Cells[0].Value);
                string val2 = Convert.ToString(selectedRow.Cells[1].Value);


                UpdateForm.LoadVal(val1, val2);
                UpdateForm.Show();
                this.Hide();
            }
        }

        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadUserRole("");
            textBox1.Text = "";
        }

    }
}
