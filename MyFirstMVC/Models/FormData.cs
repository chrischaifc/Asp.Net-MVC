using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace MyFirstMVC.Models
{
    public class FormData
    {
        string CS = ConfigurationManager.ConnectionStrings["HealthSchool"].ConnectionString;
         
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Message { get; set; }
        public string FirstName { get; set; }

        public bool IsValid()
        {
            using (var connection = new SqlConnection(CS))
            {
                string commandText = "SELECT FirstName FROM [User] WHERE Email=@Username AND Password = @Password";
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Username", this.Email);
                    command.Parameters.AddWithValue("@Password", this.Password);
                    connection.Open();

                    FirstName = (string)command.ExecuteScalar();

                    if (!String.IsNullOrEmpty(FirstName))
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie(Email, false);
                        return true;
                    }

                    connection.Close();
                    Message = "Email/Password don't match.";
                    return false;
                }
            } 
        }

        public bool isNew()
        {
            using (var connection = new SqlConnection(CS))
            {
                string commandText = "SELECT Email FROM [User] WHERE Email=@Email";
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Email", this.Email);
                    connection.Open();
                    
                    string EmailFromDB = (string)command.ExecuteScalar();

                    if (String.IsNullOrEmpty(EmailFromDB))
                    {
                        return true;
                    }
                    
                    connection.Close();
                    Message = "Email already registered.";
                    return false;
                }
            }
        }
    }
}