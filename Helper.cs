using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace StudentManagementSystem
{
    internal class Helper
    {
        public static SqlConnection GetConnection()
        {
            string constr =
                ConfigurationManager
                .ConnectionStrings["constr"]
                .ConnectionString;

            SqlConnection con = new SqlConnection(constr);

            return con;
        }
        public static string GetPassword()
        {
            string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789" +
                "@#$%&*";

            Random random = new Random();

            char[] password = new char[8];

            for (int i = 0; i < password.Length; i++)
            {
                int index = random.Next(chars.Length);

                password[i] = chars[index];
            }

            return new string(password);
        }
    }
}