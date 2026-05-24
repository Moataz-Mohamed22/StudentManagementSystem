using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static public string UserName;
        static public string UserRole;
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Student Management System [ "+UserName+":"+ UserRole+" ]";
            if (!(UserRole == "admin"))
            {
                masterToolStripMenuItem.Visible = false;
            
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void roleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Role role = new Role();
            role.MdiParent = this;
            role.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.MdiParent = this;
            user.Show();
        }

        private void changeUserPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.MdiParent = this;
            cp.Show();
        }

        private void facultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Faculty_Management faculty = new Faculty_Management();
            faculty.MdiParent = this;
            faculty.Show();
        }
    }
}
