using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Faculty_Management : Form
    {
        public string facId = null;

        public Faculty_Management()
        {
            InitializeComponent();
        }

        private void Faculty_Management_Load(object sender, EventArgs e)
        {
            cbGender.SelectedIndex = 0;
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFacultyName.Text.Length == 0)
            {
                errorProvider1.SetError(
                    txtFacultyName,
                    "Please Enter Faculty Name");
            }
            else if (txtEmailID.Text.Length == 0)
            {
                errorProvider1.SetError(
                    txtEmailID,
                    "Please Enter Email");
            }
            else if (txtMobileNumber.Text.Length == 0)
            {
                errorProvider1.SetError(
                    txtMobileNumber,
                    "Please Enter Mobile Number");
            }
            else if (txtAddress.Text.Length == 0)
            {
                errorProvider1.SetError(
                    txtAddress,
                    "Please Enter Address");
            }
            else
            {
                errorProvider1.Clear();

                try
                {
                    using (SqlConnection con = Helper.GetConnection())
                    {
                        con.Open();

                        string query = @"INSERT INTO Faculty
                                        VALUES
                                        (
                                            @id,
                                            @userName,
                                            @dob,
                                            @gender,
                                            @mobile,
                                            @email,
                                            @address,
                                            @cby,
                                            @con,
                                            @uby,
                                            @uon
                                        )";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(
                            "@id",
                            Guid.NewGuid().ToString());

                        cmd.Parameters.AddWithValue(
                            "@userName",
                            txtFacultyName.Text);

                        cmd.Parameters.AddWithValue(
                            "@dob",
                            dtDob.Value);

                        cmd.Parameters.AddWithValue(
                            "@gender",
                            cbGender.Text);

                        cmd.Parameters.AddWithValue(
                            "@mobile",
                            txtMobileNumber.Text);

                        cmd.Parameters.AddWithValue(
                            "@email",
                            txtEmailID.Text);

                        cmd.Parameters.AddWithValue(
                            "@address",
                            txtAddress.Text);

                        cmd.Parameters.AddWithValue(
                            "@cby",
                            MainForm.UserName);

                        cmd.Parameters.AddWithValue(
                            "@con",
                            DateTime.Now);

                        cmd.Parameters.AddWithValue(
                            "@uby",
                            MainForm.UserName);

                        cmd.Parameters.AddWithValue(
                            "@uon",
                            DateTime.Now);

                        int res = cmd.ExecuteNonQuery();

                        if (res > 0)
                        {
                            MessageBox.Show(
                                "Faculty Added Successfully",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            ClearControls();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Unable To Add Faculty",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            FacultyInfo facultyInfo = new FacultyInfo();

            facultyInfo.ShowDialog();

            DataRow dr = FacultyInfo.dr;

            if (dr != null)
            {
                facId = dr[0].ToString();

                txtFacultyName.Text = dr[1].ToString();

                dtDob.Value = Convert.ToDateTime(dr[2]);

                cbGender.Text = dr[3].ToString();

                txtMobileNumber.Text = dr[4].ToString();

                txtEmailID.Text = dr[5].ToString();

                txtAddress.Text = dr[6].ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (facId != null)
            {
                try
                {
                    using (SqlConnection con = Helper.GetConnection())
                    {
                        con.Open();

                        string query = @"UPDATE Faculty
                                         SET
                                             facName = @nm,
                                             facDOB = @dob,
                                             facGender = @fGender,
                                             facMob = @mob,
                                             facEmail = @mail,
                                             facAddress = @address,
                                             updatedBy = @uby,
                                             updatedOn = @uon
                                         WHERE facId = @id";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@id", facId);

                        cmd.Parameters.AddWithValue(
                            "@nm",
                            txtFacultyName.Text);

                        cmd.Parameters.AddWithValue(
                            "@dob",
                            dtDob.Value);

                        cmd.Parameters.AddWithValue(
                            "@fGender",
                            cbGender.Text);

                        cmd.Parameters.AddWithValue(
                            "@mob",
                            txtMobileNumber.Text);

                        cmd.Parameters.AddWithValue(
                            "@mail",
                            txtEmailID.Text);

                        cmd.Parameters.AddWithValue(
                            "@address",
                            txtAddress.Text);

                        cmd.Parameters.AddWithValue(
                            "@uby",
                            MainForm.UserName);

                        cmd.Parameters.AddWithValue(
                            "@uon",
                            DateTime.Now);

                        int res = cmd.ExecuteNonQuery();

                        if (res > 0)
                        {
                            MessageBox.Show(
                                "Faculty Updated Successfully",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            ClearControls();

                            facId = null;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Unable To Update Faculty",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
            else
            {
                MessageBox.Show(
                    "Please show the record first to update details",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (facId != null)
            {
                try
                {
                    using (SqlConnection con = Helper.GetConnection())
                    {
                        con.Open();

                        string query =
                            "DELETE FROM Faculty WHERE facId = @id";

                        SqlCommand cmd =
                            new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(
                            "@id",
                            facId);

                        int res = cmd.ExecuteNonQuery();

                        if (res > 0)
                        {
                            MessageBox.Show(
                                "Faculty Deleted Successfully",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            ClearControls();

                            facId = null;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Unable To Delete Faculty",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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
            else
            {
                MessageBox.Show(
                    "Please show the record first to delete details",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void ClearControls()
        {
            txtFacultyName.Clear();

            txtMobileNumber.Clear();

            txtEmailID.Clear();

            txtAddress.Clear();

            dtDob.Value = DateTime.Now;

            cbGender.SelectedIndex = 0;
        }
    }
}