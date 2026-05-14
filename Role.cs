using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Role : Form
    {
        public Role()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear old error
                errorProvider1.Clear();

                // Get Role Name
                string roleName = tb_roleName.Text.Trim();

                // Validation
                if (roleName.Length == 0)
                {
                    errorProvider1.SetError(
                        tb_roleName,
                        "Please Enter Role Name"
                    );

                    return;
                }

                // Get Connection
                SqlConnection con = Helper.GetConnection();

                // Open Connection
                con.Open();

                // Check if role already exists
                SqlCommand com = new SqlCommand(
                    "SELECT * FROM Role WHERE roleName=@rname",
                    con
                );

                com.Parameters.AddWithValue(
                    "@rname",
                    roleName
                );

                SqlDataReader reader = com.ExecuteReader();

                // Role Exists
                if (reader.Read())
                {
                    MessageBox.Show(
                        "Role Already Exists",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    reader.Close();
                }
                else
                {
                    // Close Reader before another command
                    reader.Close();

                    // Insert Query
                    SqlCommand cmd = new SqlCommand(
                        "Insert Into Role " +
                        "(roleName,createdBy,createdOn,updateBy,updateOn) " +
                        "Values " +
                        "(@rname,@cby,@con,@uby,@uon)",
                        con
                    );

                    // Parameters
                    cmd.Parameters.AddWithValue("@rname", roleName);
                    cmd.Parameters.AddWithValue("@cby", "Admin");
                    cmd.Parameters.AddWithValue("@con", DateTime.Now);
                    cmd.Parameters.AddWithValue("@uby", DBNull.Value);
                    cmd.Parameters.AddWithValue("@uon", DBNull.Value);

                    // Execute Insert
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show(
                            "Role Created Successfully",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // Clear TextBox
                        tb_roleName.Clear();

                        // Focus again
                        tb_roleName.Focus();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed To Create Role",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }

                // Close Connection
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string roleName = tb_roleName.Text.Trim();
                if (roleName.Length == 0)
                {
                    MessageBox.Show("Please Enter Role Name");
                    return;
                }

                SqlConnection con = Helper.GetConnection();

                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Role WHERE roleName=@rname",
                    con
                );

                cmd.Parameters.AddWithValue(
                    "@rname",
                   roleName
                );

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show(
                        "Role Deleted Successfully","Success",MessageBoxButtons.OK, MessageBoxIcon.Information
                    );

                    tb_roleName.Clear();
                }
                else
                {
                    MessageBox.Show(
                        "Role Not Found" ,
                        "Warning",
                        MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                    );
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message
                );
            }
        }
    }
}