using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class FacultyInfo : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
      public  static DataRow dr;

        public FacultyInfo()
        {
            InitializeComponent();
        }

        private void FacultyInfo_Load(object sender, EventArgs e)
        {
            try
            {
                con = Helper.GetConnection();
                string query = @"SELECT 
                               facId as 'Faculty ID',
                               facName as 'Name',
                               facDOB as 'DOB',
                               facGender as 'Gender',
                               facMob as 'Mobile',
                               facEmail as 'Email',
                               facAddress as 'Address'
                               FROM Faculty";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Faculty");
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                dr = ds.Tables[0].Rows[index];
                this.Close();
            }

        }
    }
}
