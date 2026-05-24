using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            if (userName.Length == 0)
            {
                errorProvider1.SetError(
                    txtUserName,
                    "User Name Field Cannot Be Blank"
                );
            }
            else if (password.Length == 0)
            {
                errorProvider1.SetError(
                    txtPassword,
                    "Password Field Cannot Be Blank"
                );
            }
            else
            {
                try
                {
                    errorProvider1.Clear();

                    SqlConnection con =
                        Helper.GetConnection();

                    con.Open();

                    SqlCommand cmd =
                        new SqlCommand(
                            "SELECT * FROM Users WHERE userName=@userName AND hashPassword=@password",
                            con
                        );

                    cmd.Parameters.Add(
                        new SqlParameter(
                            "@userName",
                            userName
                        )
                    );

                    cmd.Parameters.Add(
                        new SqlParameter(
                            "@password",
                            Helper.HashPassword(password)
                        )
                    );

                    SqlDataReader reader =
                        cmd.ExecuteReader();

                    if (reader.Read())
                    {
                      this.Hide();
                        MainForm mf = new MainForm();
                        MainForm.UserName = reader.GetString(1);
                        MainForm.UserRole = reader.GetString(2);
                        mf.Show();

                    }
                    else
                    {
                        MessageBox.Show(
                            "Invalid User"
                        );
                    }

                    reader.Close();

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}