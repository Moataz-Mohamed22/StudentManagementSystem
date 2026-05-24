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
    public partial class Faculty_Management : Form
    {
        public Faculty_Management()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFacultyName.Text.Length == 0)
            {
                errorProvider1.SetError(txtFacultyName, "Please Enter Faculty Name");
            }
            else if (txtEmailID.Text.Length == 0)
            {
                errorProvider1.SetError(txtEmailID, "Please Enter Email");
            }
            else if (txtMobileNumber.Text.Length == 0)
            {
                errorProvider1.SetError(txtMobileNumber, "Please Enter Mobile Number");
            }
            else if (txtAddress.Text.Length == 0)
            {
                errorProvider1.SetError(txtAddress, "Please Enter Address");
            }
            else
            {
                errorProvider1.Clear();

                try
                {
                    using (SqlConnection con = Helper.GetConnection())
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO Faculty
                VALUES
                (@id,@userName,@dob,@gender,
                 @mobile,@email,@address,
                 @cby,@con,@uby,@uon)", con);

                        cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@userName", txtFacultyName.Text);
                        cmd.Parameters.AddWithValue("@dob", dtDob.Value);
                        cmd.Parameters.AddWithValue("@gender", cbGender.Text);
                        cmd.Parameters.AddWithValue("@mobile", txtMobileNumber.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmailID.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@cby",MainForm.UserName);
                        cmd.Parameters.AddWithValue("@con", DateTime.Now);
                        cmd.Parameters.AddWithValue("@uby", MainForm.UserName);
                        cmd.Parameters.AddWithValue("@uon", DateTime.Now);

                        int res = cmd.ExecuteNonQuery();

                        if (res > 0)
                        {
                            MessageBox.Show(
                                "Faculty Added Successfully",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            txtFacultyName.Clear();
                            txtEmailID.Clear();
                            txtMobileNumber.Clear();
                            txtAddress.Clear();

                            dtDob.Value = DateTime.Now;
                            cbGender.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Unable To Add Faculty");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Faculty_Management_Load(object sender, EventArgs e)
        {
            cbGender.SelectedIndex = 0;
        }
    }
}
