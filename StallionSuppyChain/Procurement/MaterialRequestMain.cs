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
using StallionSuppyChain.Material_Releasing;
namespace StallionSuppyChain.Procurement
{
     
    public partial class MaterialRequestMain : Form
    {


        private string conStr = ConfigurationManager.ConnectionStrings["SCM_STALLIONLIVE"].ToString();

        public MaterialRequestMain()
        {
            InitializeComponent();

            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "MM/dd/yyyy";

        }

        public void Test(string parameter1)
        {

            cboProjectCode.Enabled = false;
            GetProjectCode();
            GetProcurementType();
            GetWorker1();
            GetWorker2();
            GetWorker3();
            GetProjectCodeDelivedTo();
            txtMRMID.Text = parameter1;
            retrievedata(Convert.ToInt32(txtMRMID.Text));
            LoadItemMaster(Convert.ToInt32(txtMRMID.Text));
        }
        public void GetUserID(string parameter1)
        {
            TxtUserID.Text = parameter1;
           // retrievedata(Convert.ToInt32(parameter1.ToString()));
        }
        private void retrievedata(int MRMNO)
        {
            cboProjectCode.Enabled = false;
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "MM/dd/yyyy";

            
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            sql = "[dbo].[LIST_RetrieveMRMDetails] '" + MRMNO.ToString() + "'";
            sqlCnn = new SqlConnection(conStr);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    cboProjectCode.SelectedValue = sqlReader.GetValue(16).ToString();
                    TXTMRMNO.Text = sqlReader.GetValue(1).ToString();
                    cboCategoryType.SelectedValue = sqlReader.GetValue(3).ToString();
                    ckpurchase.Checked = Convert.ToBoolean(sqlReader.GetValue(12));
                    ckpurchase.Checked = Convert.ToBoolean(sqlReader.GetValue(12));
                    txtDate.Text = sqlReader.GetValue(14).ToString();

                    dateTimePicker4.Format = DateTimePickerFormat.Custom;
                    dateTimePicker4.CustomFormat = "MM/dd/yyyy";

                    dateTimePicker4.Text  =  sqlReader.GetValue(13).ToString();
                    cboStatus.Text = sqlReader.GetValue(11).ToString();

                     textBox1.Text= sqlReader.GetValue(20).ToString();

                  if (sqlReader.GetValue(20).ToString() != "")
                    {
                        groupBox4.Visible  = true;
                        groupBox4.Enabled  = false;

                    }

                        FormMode(cboStatus.Text);


                    cborquestedby.SelectedValue = sqlReader.GetValue(5).ToString();
                    cboRecomendedby.SelectedValue = sqlReader.GetValue(7).ToString();
                    cboApproveby.SelectedValue = sqlReader.GetValue(17).ToString();
                    cboDeleveredto.SelectedValue = sqlReader.GetValue(10).ToString();



                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();

                btnAdd.Text = "Add More Item(s)";
                btnSave.Enabled = true;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }


            LoadItemMaster(MRMNO);
            txtMRMID.Text = MRMNO.ToString();



        }


        private void LoadItemMaster(int MRMNO)
        {

            
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[LIST_LoadMaterialRequestDetailMRM]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MRMID", MRMNO);
                con.Open();
                cmd.ExecuteNonQuery();
                using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[13].Width = 0; 
                }
            }
          //  dataGridView2.Columns[17].Width = 0; 

         

        }
        public void GetMRMID(string parameter1,string ForAction)
        {
        
            GetProjectCode();
            GetProcurementType();
            GetWorker1();
            GetWorker2();
            GetWorker3();
            GetProjectCodeDelivedTo();

            txtMRMID.Text = parameter1;

            retrievedata(Convert.ToInt32(txtMRMID.Text));

            LoadItemMaster(Convert.ToInt32(txtMRMID.Text));


            if (ForAction == "true")
            {
                txtForApproval.Text = "true";
                
                btnSave.Visible = false;
                btnEdit.Visible = false;
             
                button1.Visible = true;

                groupBox4.Visible = true;

                groupBox4.Enabled = true;

                button2.Visible = true;
                groupBox1.Enabled = true;
            }
            if (ForAction == "false")
            {
                txtForApproval.Text = "true"; 
                btnSave.Enabled = false;
                btnEdit.Enabled = false;
              
                button1.Visible = false;
                button2.Visible = false;
                groupBox1.Enabled = true;
            }
            if (ForAction == "MRI")
            {
                txtForApproval.Text = "MRI";
                btnSave.Enabled = false;
                btnEdit.Enabled = false;

                button1.Visible = false;
                button2.Visible = false;
                groupBox1.Enabled = true;
            }

        }
        private void MaterialRequestMain_Load(object sender, EventArgs e)
        {
            if (txtMRMID.Text == "")
            {
                GetProjectCode();
                GetProcurementType();
                GetWorker1();
                GetWorker2();
                GetWorker3();
                GetProjectCodeDelivedTo();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy").ToString();

            }
        }

        private void FormMode(string Status)
        {
            if (Status == "Submitted")
            {

                groupBox5.Enabled = false;
                groupBox2.Enabled = false;
                dataGridView1.Columns[0].Visible = false;
                groupBox1.Enabled = false;
              //  dataGridView1.Enabled = false;
            }
            if (Status == "Approved")
            {

                groupBox5.Enabled = false;
                groupBox2.Enabled = false;
                dataGridView1.Columns[0].Visible = false;
                groupBox1.Enabled = false;
                button1.Visible = false;
                button2.Visible = false;
                //  dataGridView1.Enabled = false;
            }
            if (Status == "Rejected")
            {

                groupBox5.Enabled = false;
                groupBox2.Enabled = false;
                dataGridView1.Columns[0].Visible = false;
                groupBox1.Enabled = false;
                button1.Visible = false;
                button2.Visible = false;
                //  dataGridView1.Enabled = false;
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

        private void GetProjectCodeDelivedTo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_PROJECT_CODE]", con);
                    adapter.Fill(dt);
                    cboDeleveredto.DataSource = dt;
                    cboDeleveredto.DisplayMember = "ProjectName";
                    cboDeleveredto.ValueMember = "ProjectID";
                }
                catch (Exception ex)
                {

                }
            }
        }


        private void GetWorker1()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);
                    cborquestedby.DataSource = dt;

                    cborquestedby.DisplayMember = "FULL_NAME";
                    cborquestedby.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {

                }
            }
        }
        private void GetWorker2()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_MSTR_WORKER_DETAILS]", con);
                    adapter.Fill(dt);
                    cboRecomendedby.DataSource = dt;

                    cboRecomendedby.DisplayMember = "FULL_NAME";
                    cboRecomendedby.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {

                }
            }
        }
        private void GetWorker3()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("[dbo].[LIST_LoadApprovers]", con);
                    adapter.Fill(dt);
                    cboApproveby.DataSource = dt;

                    cboApproveby.DisplayMember = "FULL_NAME";
                    cboApproveby.ValueMember = "WorkerID";

                }
                catch (Exception ex)
                {

                }
            }
        }


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






             private void button8_Click(object sender, EventArgs e)
             {

                 if (cboProjectCode.SelectedValue.ToString() == "0")//Nothing selected
                 {
                     MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                     cboProjectCode.Focus();
                 }
                 else if (cborquestedby.SelectedValue.ToString() == "0")//Nothing selected
                 {
                     MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                     cborquestedby.Focus();



                 }
                 else if (cboRecomendedby.SelectedValue.ToString() == "0")//Nothing selected
                 {
                     MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                     cboRecomendedby.Focus();



                 }
                 else if (cboApproveby.SelectedValue.ToString() == "0")//Nothing selected
                 {
                     MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                     cboApproveby.Focus();



                 }

                 else if (cboDeleveredto.SelectedValue.ToString() == "0")//Nothing selected
                 {
                     MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                     cboDeleveredto.Focus();



                 }



                 else if (cboCategoryType.SelectedValue.ToString() == "0")//Nothing selected
                 {
                     MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                     cboDeleveredto.Focus();



                 }
                     

                 else
                 {
                     if (btnAdd.Text == "Proceed")
                     {
                         TranSaveHeader(1, "ADDNEW");
                     }
                     else if (btnAdd.Text == "Add More Item(s)")
                     {

                         TranSaveHeader(2, "ADDNEW");

                     }

                 }
             }
         


       private void TranSaveHeader(int processID,string Action)
       {

             string StringReturn = "";
             string status = "";
             if (Action == "SAVE" || Action == "ADDNEW")
             {
                   status = "Open";
             }
             if (Action == "SUBMIT")
             {
                   status = "Submitted";
             }

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
             
            cmd.CommandText = "TRAN_SAVEMaterialRequest";
 
            cmd.Parameters.Add("@ProcurementID", SqlDbType.VarChar).Value =  cboCategoryType.SelectedValue.ToString() ;
            cmd.Parameters.Add("@ProjectID", SqlDbType.Int).Value = Convert.ToInt32(cboProjectCode.SelectedValue.ToString());
            cmd.Parameters.Add("@status", SqlDbType.VarChar, 15).Value = status;
            cmd.Parameters.Add("@ForPurchase", SqlDbType.Bit).Value = ckpurchase.CheckState;
            cmd.Parameters.Add("@DateNeeded", SqlDbType.DateTime).Value = Convert.ToDateTime( dateTimePicker4.Value.ToString());
            cmd.Parameters.Add("@Requestedby", SqlDbType.Int).Value = Convert.ToInt32(cborquestedby.SelectedValue.ToString());
            cmd.Parameters.Add("@Recomendedby", SqlDbType.Int).Value = Convert.ToInt32(cboRecomendedby.SelectedValue.ToString());
            cmd.Parameters.Add("@DelivedTo", SqlDbType.Int).Value = Convert.ToInt32(cboDeleveredto.SelectedValue.ToString());
            cmd.Parameters.Add("@UserCreated", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@Approvedby", SqlDbType.Int).Value = Convert.ToInt32(cboApproveby.SelectedValue.ToString());
            cmd.Parameters.Add("@MRMNo", SqlDbType.VarChar, 50).Value = TXTMRMNO.Text;
            cmd.Parameters.Add("@ProcessID", SqlDbType.Int).Value = processID;
         
            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                if (Action == "SAVE" || Action == "SUBMIT")
                {
                    MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    btnSave.Enabled = false;
                    MaterialRequestIquiry formTask2 = new MaterialRequestIquiry();
                    formTask2.GetUserID(TxtUserID.Text);
                    this.Dispose();
                    formTask2.Show();
                }

                ItemMasterLookUp formTask = new ItemMasterLookUp();

                if (Action == "ADDNEW")
                {
                    formTask.Test(StringReturn,"MRM");
                    formTask.GetUserID(TxtUserID.Text);
                    this.Dispose();
                    formTask.Show();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            } 
       }
       private void TranDeleteItemInMRM(int TranLineNo)
       {
            
           SqlConnection con = new SqlConnection(conStr);
           SqlCommand cmd = new SqlCommand();
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.CommandText = "TRAN_DELMRMITEMCODE";
           cmd.Parameters.Add("@TranLineNo", SqlDbType.Int).Value = TranLineNo;
          

           cmd.Connection = con;
           try
           {
               con.Open();

               cmd.ExecuteNonQuery();

             
            

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               con.Close();
               con.Dispose();
           }

           LoadItemMaster(Convert.ToInt32(txtMRMID.Text));
       }
        private void cboProjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (cboCategory1.SelectedValue.ToString() == "0")
            //{
            //    cboCategory2.Text = "";
            //    cboCategory3.Text = "";
            //}

            //LoadCategory2(cboCategory1.SelectedValue.ToString());


            try
            {
                TXTMRMNO.Text = GenerateMRMCode(cboProjectCode.SelectedValue.ToString());
            }
            catch (Exception ex)
            {


            }



        }

        private string GenerateMRMCode(string ProjectCode )
        {

            if (ProjectCode == "0")
            {

                ProjectCode = "0";

            }
            
            //  string connetionString = null;
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string SRet = "";

            //  connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "[dbo].[TRAN_GenMRMNO] " + Convert.ToString(ProjectCode);


            try
            {

                //  cnn = new SqlConnection(conStr);
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    SRet = cmd.ExecuteScalar().ToString();
                    con.Close();
                    cmd.Dispose();



                }


            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            return SRet;

        }

        private void cboProjectCode_SelectedValueChanged(object sender, EventArgs e)
        {
            //int ProjectID = 0;

            //ProjectID = Convert.ToInt32(cboProjectCode.SelectedValue.ToString());


          //  object sSelectedClient = cboProjectCode.SelectedValue;
            string sSelectedClient = (string)cboProjectCode.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            retrievedata(1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            
            if (cboProjectCode.SelectedIndex == -1)//Nothing selected
            {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    cboProjectCode.Focus();
            }
            else if (cborquestedby.SelectedIndex == -1)//Nothing selected
            {
                    MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    cborquestedby.Focus();



            }
            else if (cboRecomendedby.SelectedIndex == -1)//Nothing selected
            {
                        MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                        cboRecomendedby.Focus();



            }
            else if (cboApproveby.SelectedIndex == -1)//Nothing selected
            {
                        MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                        cboApproveby.Focus();



            }

            else if (cboDeleveredto.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboDeleveredto.Focus();



            }


            else
            {
                if (btnAdd.Text == "Proceed")
                {
                    TranSaveHeader(1, "SAVE");
                }
                else if (btnAdd.Text == "Add More Item(s)")
                {

                    TranSaveHeader(2, "SAVE");

                }

            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                TXTMRMNO.Text = "";
                GetProjectCode();
                GetProcurementType();
                GetWorker1();
                GetWorker2();
                GetWorker3();
                GetProjectCodeDelivedTo();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy").ToString();
                groupBox2.Enabled = false;
                groupBox5.Enabled = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                GetProjectCode();
                GetProcurementType();
                GetWorker1();
                GetWorker2();
                GetWorker3();
                GetProjectCodeDelivedTo();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy").ToString();
                groupBox2.Enabled = true;
                groupBox5.Enabled = true;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
             
                TXTMRMNO.Text = "";

            }
            catch (Exception ex)
            {

            }
        }

        private void MaterialRequestMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (txtForApproval.Text == "")
            {
                MaterialRequestIquiry formTask = new MaterialRequestIquiry();
                formTask.GetUserID(TxtUserID.Text);
                this.Dispose();
                formTask.Show();
            }
            if (txtForApproval.Text == "true")
            {
                MaterialRequestForApproval formTask = new MaterialRequestForApproval();
                formTask.GetUserID(TxtUserID.Text);
                this.Dispose();
                formTask.Show();
            }
            if (txtForApproval.Text == "MRI")
            {
                Material_Releasing_Inquiry formTask = new Material_Releasing_Inquiry();
                formTask.GetUserID(TxtUserID.Text);
                this.Dispose();
                formTask.Show();
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["BtnRemove"].Index && e.RowIndex >= 0)
            {


                var index = dataGridView1.CurrentRow.Cells[13].Value.ToString();


                TranDeleteItemInMRM(Convert.ToInt32(index));

            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {


            if (cboProjectCode.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboProjectCode.Focus();
            }
            else if (cborquestedby.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cborquestedby.Focus();



            }
            else if (cboRecomendedby.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboRecomendedby.Focus();



            }
            else if (cboApproveby.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboApproveby.Focus();



            }

            else if (cboDeleveredto.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboDeleveredto.Focus();
            }
            else if (dataGridView1.RowCount <= 0)//Nothing selected
            {
                MessageBox.Show("Please select atleast one Item.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                cboDeleveredto.Focus();
            }

            else
            {
                if (btnAdd.Text == "Proceed")
                {
                    TranSaveHeader(1, "SUBMIT");
                }
                else if (btnAdd.Text == "Add More Item(s)")
                {

                    TranSaveHeader(2, "SUBMIT");

                }

            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            if (textBox1.Text ==  "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                textBox1.Focus();
                return;


            }


            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_ApproveMaterialRequest";
            cmd.Parameters.Add("@MRMID", SqlDbType.Int).Value = Convert.ToInt32(txtMRMID.Text);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = textBox1.Text;

            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                if (StringReturn == "Rejected")
                {
                    MessageBox.Show("This Request is already rejected!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
                else
                {

                   MessageBox.Show("Request has been approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                   MaterialRequestForApproval formTask = new MaterialRequestForApproval();
                        formTask.GetUserID(TxtUserID.Text);
                        this.Dispose();
                        formTask.Show();
                   

                
                }
            

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            } 
 
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")//Nothing selected
            {
                MessageBox.Show("This field is required!.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                textBox1.Focus();

                return;

            }



            string StringReturn = "";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "TRAN_RejectMaterialRequest";
            cmd.Parameters.Add("@MRMID", SqlDbType.Int).Value = Convert.ToInt32(txtMRMID.Text);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(TxtUserID.Text);
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = textBox1.Text;

            cmd.Connection = con;
            try
            {
                con.Open();
                StringReturn = cmd.ExecuteScalar().ToString();
                if (StringReturn == "Approved")
                {
                    MessageBox.Show("This Request is already Approved!.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("Request has been Rejected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    MaterialRequestForApproval formTask = new MaterialRequestForApproval();
                    formTask.GetUserID(TxtUserID.Text);
                    this.Dispose();
                    formTask.Show();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            } 
        }
    }
}
