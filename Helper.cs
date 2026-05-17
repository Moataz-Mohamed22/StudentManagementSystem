using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    internal class Helper
    {
        // Database Connection
        public static SqlConnection GetConnection()
        {
            string constr =
                ConfigurationManager
                .ConnectionStrings["constr"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(constr);

            return con;
        }

        // Generate Random Password
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
                int index =
                    random.Next(chars.Length);

                password[i] = chars[index];
            }

            return new string(password);
        }

        // Send Email
        public static bool GetMail
        (
            string email,
            string subject,
            string message
        )
        {
            MailMessage msg =
                new MailMessage();

            SmtpClient smtp =
                new SmtpClient();

            msg.From =
                new MailAddress(
                    "moataz.wmm@gmail.com"
                );

            msg.To.Add(email);

            msg.Subject = subject;

            msg.Body = message;

            msg.IsBodyHtml = true;

            smtp.Port = 587;

            smtp.Host = "smtp.gmail.com";

            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;

            smtp.Credentials =
                new NetworkCredential(
                    "moataz.wmm@gmail.com",
                    "wxto hcry mzta pbmp"
                );

            smtp.DeliveryMethod =
                SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        // Hash Password
        public static string HashPassword
        (
            string password
        )
        {
            SHA256 sha =
                SHA256.Create();

            byte[] bytes =
                sha.ComputeHash(
                    Encoding.UTF8.GetBytes(password)
                );

            StringBuilder pass =
                new StringBuilder();

            foreach (byte b in bytes)
            {
                pass.Append(
                    b.ToString("x2")
                );
            }

            return pass.ToString();
        }
    }
}