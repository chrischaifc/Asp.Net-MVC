using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using MyFirstMVC.Models;
using MyFirstMVC.Models.Program;
using MyFirstMVC.Models.RotationList;

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
            List<string> programTypesList = new List<string>();
            List<string> programsList = new List<string>();
            List<string> rotationsList = new List<string>();
            List<string> rotationSupervisorsList = new List<string>();

            using (var connection = new SqlConnection(CS))
            {
                string commandText = "SELECT DISTINCT Name FROM [Institutional]";
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
                string commandText = "SELECT DISTINCT Rotations FROM [Rotations]";
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

                commandText = "SELECT DISTINCT Supervisor FROM [Rotations]";
                using (var command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            rotationSupervisorsList.Add(Convert.ToString(sdr[0]));
                        }
                    }
                    connection.Close();
                }

                commandText = "SELECT DISTINCT Name FROM [Programs]";
                using (var command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            programsList.Add(Convert.ToString(sdr[0]));
                        }
                    }
                    connection.Close();
                }

                commandText = "SELECT DISTINCT Type FROM [Programs]";
                using (var command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            programTypesList.Add(Convert.ToString(sdr[0]));
                        }
                    }
                    connection.Close();
                }
            }

            SelectList selectIntitutions = new SelectList(instutionsList, "id");
            SelectList selectRotations = new SelectList(rotationsList, "id");
            SelectList selectPrograms = new SelectList(programsList, "id");
            SelectList selectProgramTypes = new SelectList(programTypesList, "id");
            SelectList selectRotationSupervisors = new SelectList(rotationSupervisorsList, "id");

            ViewData["Institutions"] = selectIntitutions;
            ViewData["Rotations"] = selectRotations;
            ViewData["RotationSupervisors"] = selectRotationSupervisors;
            ViewData["Programs"] = selectPrograms;
            ViewData["ProgramTypes"] = selectProgramTypes;

            ViewBag.Institutions = selectIntitutions;
            ViewBag.Rotations = selectRotations;
            ViewBag.RotationSupervisors = selectRotationSupervisors;
            ViewBag.Programs = selectPrograms;
            ViewBag.ProgramTypes = selectProgramTypes;

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

                    StudentContext studentContext = new StudentContext();
                    Student student = new Student();
                    try
                    {
                        student.FirstName = formData.FirstName;
                        student.LastName = formData.LastName;
                        student.Address = formData.Address;
                        student.PostalCode = formData.PostalCode;
                        student.DOB = formData.DOB;
                        student.Email = formData.Email;
                        student.Password = formData.Password;
                        student.Phone = formData.Phone;
                        student.ProgramName = formData.ProgramName;
                        student.InstitutionName = formData.InstitutionName;
                        student.ProgramType = formData.ProgramType;
                        student.SecurityCode = secCode;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    studentContext.Students.Add(student);
                    studentContext.SaveChanges();

                    Session["FirstName"] = student.FirstName;
                    Session["id"] = student.ID;


                    RotationListContext rotationListContext = new RotationListContext();
                    RotationList rotationList = new RotationList();
                    rotationList.StudentName = student.LastName + ", " + student.FirstName;
                    rotationList.StudentId = student.ID;
                    rotationList.Start = formData.StartDate;
                    rotationList.End = formData.EndDate;
                    rotationList.Supervisor = formData.RotationSupervisors;
                    rotationList.Type = formData.Rotations;

                    rotationListContext.RotationList.Add(rotationList);
                    rotationListContext.SaveChanges();

                }
                return RedirectToAction("StudentPage", "Student");
            }
            else
            {
                //return View("Index");
                return RedirectToAction("Index", "Registration");
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
                string commandText = "SELECT id FROM [Students] WHERE SecurityCode=@SecurityCode";
                using (var command = new SqlCommand(commandText, connection))
                {
                    while (true)
                    {
                        generatedCode = new string(Enumerable.Repeat(chars, length)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                        command.Parameters.AddWithValue("@SecurityCode", generatedCode);
                        connection.Open();

                        string id = (string)command.ExecuteScalar();

                        if (String.IsNullOrEmpty(id))
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
