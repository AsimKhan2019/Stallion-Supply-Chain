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

namespace StallionSuppyChain.Material_Releasing
{
    public partial class Material_Releasing_Project_Sel : Form
    {

        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public Material_Releasing_Project_Sel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
            // retrievedata(Convert.ToInt32(parameter1.ToString()));
        }

        #region generating dropDown Value

        private void GetProjectCode( string Trantype)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODERELEASING] '" + Trantype.ToString() + "'", con);
                    adapter.Fill(dt);
                    cmbIssueFrom.DataSource = dt;
                    cmbIssueFrom.DisplayMember = "ProjectName";
                    cmbIssueFrom.ValueMember = "ProjectID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void POApprovedBy(string Trantype)
        {

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[LIST_MSTR_PROJECT_CODERELEASINGTO] '" + Trantype.ToString() + "'", con);
                    adapter.Fill(dt);

                    cmbIssueTo.DataSource = dt;
                    cmbIssueTo.DisplayMember = "ProjectName";
                    cmbIssueTo.ValueMember = "ProjectID";

                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        private void Material_Releasing_Project_Sel_Load(object sender, EventArgs e)
        {
            GetProjectCode( txttrantype.Text.ToString().Trim());
            POApprovedBy(txttrantype.Text.ToString().Trim());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Material_Releasing_Inquiry main = new Material_Releasing_Inquiry();
            this.Hide();
            main.Show();
        }
        public void GetTranType(string parameter1)
        {
            txttrantype.Text = parameter1;
            label4.Text = parameter1;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (cmbIssueFrom.Text == " -- Select-- " || cmbIssueTo.Text == " -- Select-- ")
            {
                //MessageBox.Show("Please Fill Required Field(s)");
                //return;
                MessageBox.Show("Please Fill Required Field(s).", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                return;
            }
            else if (cmbIssueFrom.Text == cmbIssueTo.Text)
            {
                //  MessageBox.Show("Invalid Project Codes");

                MessageBox.Show("Invalid Project Codes", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);

                return;
            }
            else
            {
                Material_Releasing_Main main = new Material_Releasing_Main();
                main.GetUserID(TxtUserID.Text);
                main.GetTranType(txttrantype.Text);
                main.GetParameters(cmbIssueFrom.SelectedValue.ToString(), cmbIssueTo.SelectedValue.ToString());
                this.Hide();
                main.Show();
            }
        }
    }
}
