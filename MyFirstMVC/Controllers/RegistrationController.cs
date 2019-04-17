using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace MyFirstMVC.Controllers
{
    public class RegistrationController : Controller
    {
        string CS = ConfigurationManager.ConnectionStrings["HealthSchool"].ConnectionString;

        // GET: Registration
        public ActionResult Index()
        {
            // Get data from DB to fill dropdown options
            List<string> instutionsList = new List<string>();
            List<string> rotationsList = new List<string>();
            
            using (var connection = new SqlConnection(CS))
            {
                string commandText = "SELECT Name FROM [Institutional]";
                using (var command = new SqlCommand(commandText, connection))
                { 
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            instutionsList.Add(Convert.ToString(sdr[0]));
                        }
                    }
                    connection.Close();
                }
            }

            using (var connection = new SqlConnection(CS))
            {
                string commandText = "SELECT Rotations FROM [Rotations]";
                using (var command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            rotationsList.Add(Convert.ToString(sdr[0]));
                        }
                    }
                    connection.Close();
                }
            }

            SelectList selectIntitutions = new SelectList(instutionsList, "id");
            SelectList selectRotations = new SelectList(rotationsList, "id");

            ViewData["Institutions"] = selectIntitutions;
            ViewData["Rotations"] = selectRotations;

            ViewBag.Institutions = selectIntitutions;
            ViewBag.Rotations = selectRotations;

            return View();
        }

        [HttpPost]
        public ActionResult RegistrationForm(Models.RegisterFormModel formData)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    string secCode = generateSecurityCode();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user (FirstName, LastName, Address, PostalCode, DOB, " +
                                                    "Email, Password, Phone, ProgramName, InstitutionName, SecurityCode) " +
                                                    "VALUES (" 
                                                    + "'" + formData.FirstName + "', "
                                                    + "'" + formData.LastName + "', "
                                                    + "'" + formData.Address + "', "
                                                    + "'" + formData.PostalCode + "', "
                                                    + "'" + formData.DOB + "', "
                                                    + "'" + formData.Email + "', "
                                                    + "'" + formData.Password + "', "
                                                    + "'" + formData.Phone + "', "
                                                    + "'" + formData.ProgramName + "', "
                                                    + "'" + formData.InstitutionName + "', "
                                                    + "'" + secCode + "'", con);
                }
                return RedirectToAction("StudentPage", "Student");
            }
            else
            {
                return View("Index");
            }
        }

        private string generateSecurityCode()
        {
            Random random = new Random();
            int length = 10;
            string generatedCode;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            using (var connection = new SqlConnection(CS))
            {
                string commandText = "SELECT User FROM [User] WHERE SecurityCode=@SecurityCode";
                using (var command = new SqlCommand(commandText, connection))
                {
                    while (true)
                    {
                        generatedCode = new string(Enumerable.Repeat(chars, length)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                        command.Parameters.AddWithValue("@SecurityCode", generatedCode);
                        connection.Open();

                        string user = (string)command.ExecuteScalar();

                        if (String.IsNullOrEmpty(user))
                        {
                            break;
                        }

                    }
                    connection.Close();
                }
            }
            return generatedCode;
        }
    }
}
