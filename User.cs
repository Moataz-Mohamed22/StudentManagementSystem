using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {
            tbPassword.Text = Helper.GetPassword();
            SqlConnection con =Helper.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Role",con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read()) 
            {
                cbRloe.Items.Add(dr.GetString(1));
            }
            dr.Close();
            con.Close();
            if (cbRloe.Items.Count > 0) {
            cbRloe.SelectedIndex = 0;
            }
        }
    }
}
