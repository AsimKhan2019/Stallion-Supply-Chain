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
using System.Web.UI.WebControls;

namespace StallionSuppyChain.Admin_Reset_Password
{
    public partial class Reset_Password : Form
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Reset_Password()
        {
            InitializeComponent();
        }


        private void LoadUsers(string UserID)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_ADMIN_USER]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[0].Visible = false;
                }
            }

        }

        private void Reset_Password_Load(object sender, EventArgs e)
        {
            LoadUsers("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UserID = textBox1.Text;
            LoadUsers(UserID);
        }

     

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Admin_Update updateForm = new Admin_Update();

            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

            string val1 = Convert.ToString(selectedRow.Cells[0].Value);
            string val2 = Convert.ToString(selectedRow.Cells[1].Value);


            updateForm.GetUserID(TxtUserID.Text);
            updateForm.GetDetails(val1, val2);
            updateForm.Show();
            this.Hide();
        }


        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadUsers("");
            textBox1.Text = "";
        }
    }
}
