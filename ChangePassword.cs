using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            txtPassword.Clear();

            txtPassword.Enabled = false;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string userName =
                txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show(
                    "Please Enter User Name",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtUserName.Focus();

                return;
            }

            try
            {
                using (
                    SqlConnection con =
                    Helper.GetConnection()
                )
                {
                    con.Open();

                    string email = "";

                    SqlCommand checkUser =
                        new SqlCommand(
                            "SELECT emailId FROM Users WHERE UserName=@userName",
                            con
                        );

                    checkUser.Parameters.AddWithValue(
                        "@userName",
                        userName
                    );

                    using (
                        SqlDataReader reader =
                        checkUser.ExecuteReader()
                    )
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show(
                                "User Does Not Exist",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );

                            return;
                        }

                        email =
                            reader["emailId"]
                            .ToString();
                    }

                    string newPassword =
                        Helper.GetPassword();

                    txtPassword.Text =
                        newPassword;

                    SqlCommand update =
                        new SqlCommand(
                            @"UPDATE Users
                              SET
                              hashPassword=@password,
                              updatedBy=@updatedby,
                              updatedOn=@updatedon
                              WHERE UserName=@userName",
                            con
                        );

                    update.Parameters.AddWithValue(
                        "@userName",
                        userName
                    );

                    update.Parameters.AddWithValue(
                        "@password",
                        Helper.HashPassword(
                            newPassword
                        )
                    );

                    update.Parameters.AddWithValue(
                        "@updatedby",
                        userName
                    );

                    update.Parameters.AddWithValue(
                        "@updatedon",
                        DateTime.Now
                    );

                    int result =
                        update.ExecuteNonQuery();

                    if (result > 0)
                    {
                        bool flag =
                            Helper.GetMail(
                                email,

                                "Credential Information",

                                "Dear "
                                + userName +

                                "<br/><br/>"

                                +

                                "Your Password Has Been Changed Successfully."

                                +

                                "<br/><br/>"

                                +

                                "User Name : "
                                + userName

                                +

                                "<br/>"

                                +

                                "Password : "
                                + newPassword

                                +

                                "<br/><br/>Regards,<br/>Admin<br/>Student Management System"
                            );

                        MessageBox.Show(
                            "Password Changed Successfully",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        txtUserName.Clear();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Password Change Failed",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}