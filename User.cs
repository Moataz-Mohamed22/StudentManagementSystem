using System;
using System.Data.SqlClient;
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
            // Generate Random Password
            tbPassword.Text = Helper.GetPassword();

            // Load Roles
            SqlConnection con =
                Helper.GetConnection();

            con.Open();

            SqlCommand cmd =
                new SqlCommand(
                    "SELECT * FROM Role",
                    con
                );

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                cbRloe.Items.Add(
                    dr["roleName"].ToString()
                );
            }

            dr.Close();

            con.Close();

            // Select First Role
            if (cbRloe.Items.Count > 0)
            {
                cbRloe.SelectedIndex = 0;
            }
        }

        private void cbRloe_SelectedIndexChanged
        (
            object sender,
            EventArgs e
        )
        {

        }

        private void btnAddUser_Click
        (
            object sender,
            EventArgs e
        )
        {
            // Get Values
            string userName =
                tbUserName.Text.Trim();

            string password =
                tbPassword.Text.Trim();

            string email =
                tbEmailId.Text.Trim();

            string roleId =
                cbRloe.Text;

            // Clear Old Errors
            errorProvider1.Clear();

            // Validation
            if (userName.Length <= 4)
            {
                errorProvider1.SetError(
                    tbUserName,
                    "User Name must be greater than 4 characters"
                );

                return;
            }

            if (password.Length < 8)
            {
                errorProvider1.SetError(
                    tbPassword,
                    "Password must be at least 8 characters"
                );

                return;
            }

            if (email.Length == 0)
            {
                errorProvider1.SetError(
                    tbEmailId,
                    "Email cannot be blank"
                );

                return;
            }

            try
            {
                SqlConnection con =
                    Helper.GetConnection();

                con.Open();

                // Check User Exists
                SqlCommand checkCmd =
                    new SqlCommand(
                        "SELECT * FROM Users WHERE userName=@uname",
                        con
                    );

                checkCmd.Parameters.AddWithValue(
                    "@uname",
                    userName
                );

                SqlDataReader reader =
                    checkCmd.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show(
                        "User Already Exists",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    reader.Close();

                    con.Close();

                    return;
                }

                reader.Close();

                // Hash Password
                string hashPassword =
                    Helper.HashPassword(password);

                // Insert Query
                string query =
                @"INSERT INTO Users
                (
                    userId,
                    userName,
                    roleId,
                    hashPassword,
                    emailId,
                    createdBy,
                    createdOn
                )

                VALUES
                (
                    @userId,
                    @userName,
                    @roleId,
                    @hashPassword,
                    @emailId,
                    @createdBy,
                    @createdOn
                )";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                // Parameters
                cmd.Parameters.AddWithValue(
                    "@userId",
                    Guid.NewGuid().ToString()
                );

                cmd.Parameters.AddWithValue(
                    "@userName",
                    userName
                );

                cmd.Parameters.AddWithValue(
                    "@roleId",
                    roleId
                );

                cmd.Parameters.AddWithValue(
                    "@hashPassword",
                    hashPassword
                );

                cmd.Parameters.AddWithValue(
                    "@emailId",
                    email
                );

                cmd.Parameters.AddWithValue(
                    "@createdBy",
                    "Admin"
                );

                cmd.Parameters.AddWithValue(
                    "@createdOn",
                    DateTime.Now
                );

                // Execute Insert
                int res =
                    cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    // Send Email
                    bool flag =
                        Helper.GetMail(
                            email,
                            "Credential Information",

                            "Dear "+userName+",<br/><br/>" +

                            "You Have Successfully Registered In Our ERP Application.<br/><br/>" +

                            "User Name : " + userName + "<br/>" +

                            "Password : " + password + "<br/><br/>" +

                            "Regards,<br/>" +

                            "Admin<br/>" +

                            "Student Management System"
                        );

                    if (flag == true)
                    {
                        MessageBox.Show(
                            "User Registered Successfully",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "User Added But Email Failed",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }

                    // Clear Fields
                    tbUserName.Clear();

                    tbEmailId.Clear();

                    // Generate New Password
                    tbPassword.Text =
                        Helper.GetPassword();

                    // Select First Role Again
                    if (cbRloe.Items.Count > 0)
                    {
                        cbRloe.SelectedIndex = 0;
                    }

                    // Focus
                    tbUserName.Focus();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error : " + ex.Message,
                    "Exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void tbEmailId_TextChanged
        (
            object sender,
            EventArgs e
        )
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}