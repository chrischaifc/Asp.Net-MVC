using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MyFirstMVC.Models;
using MyFirstMVC.Models.RotationList;
using MyFirstMVC.Models.Rotation;

namespace MyFirstMVC.Controllers
{
    public class StudentController : Controller
    {
        string CS = ConfigurationManager.ConnectionStrings["HealthSchool"].ConnectionString;
        [HttpGet]
        public ActionResult StudentPage()
        {
            if (Session["Email"] != null)
            {
                ViewBag.FirstName = Session["FirstName"];

                StudentContext studentContext = new StudentContext();
                string email = (string)Session["Email"];
                Student student = studentContext.Students.Single(e => e.Email.Equals(email));

                RotationListContext rotationListContext = new RotationListContext();
                List<RotationList> rotationsList = rotationListContext.RotationList.Where(e => e.StudentId == student.ID).ToList();

                return View(rotationsList);


            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddNewRotationStudent()
        {
            if (Session["Email"] != null)
            {

                string email = (string) Session["Email"];
                Student student = getStudentInfo(email);


                List<String> rotationsList = new List<String>();
                List<String> rotationSupervisorsList = new List<String>();

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


                }


                SelectList selectRotations = new SelectList(rotationsList, "id");
                SelectList selectRotationSupervisors = new SelectList(rotationSupervisorsList, "id");

                ViewBag.Student = student.LastName + ", " + student.FirstName;
                ViewBag.Rotations = selectRotations;
                ViewBag.Supervisors = selectRotationSupervisors;



                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddNewRotationStudent(FormCollection formCollection)
        {
            if (Session["Email"] != null)
            {
                string email = (string)Session["Email"];
                StudentContext studentContext = new StudentContext();
                Student student = studentContext.Students.Single(e => e.Email.Equals(email));

                RotationListContext rotationListContext = new RotationListContext();
                RotationList rotationList = new RotationList();

                rotationList.StudentName = formCollection["StudentName"];
                rotationList.StudentId = student.ID;
                rotationList.Start = formCollection["Start"];
                rotationList.End = formCollection["End"];
                rotationList.Supervisor = formCollection["Supervisors"];
                rotationList.Type = formCollection["Rotations"];

                rotationListContext.RotationList.Add(rotationList);
                rotationListContext.SaveChanges();

                return RedirectToAction("StudentPage", "Student");

            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditRotationStudent(int? id)
        {
            if (Session["Email"] != null)
            {

                if (id != null)
                {

                    string email = (string)Session["Email"];
                    Student student = getStudentInfo(email);



                    RotationListContext rotationListContext = new RotationListContext();
                    RotationList rotationList = rotationListContext.RotationList.Single(e => e.Id == id);


                    List<String> rotationsList = new List<String>();
                    List<String> rotationSupervisorsList = new List<String>();

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


                    }


                    SelectList selectRotations = new SelectList(rotationsList, "id");
                    SelectList selectRotationSupervisors = new SelectList(rotationSupervisorsList, "id");

                    ViewBag.Student = student.LastName + ", " + student.FirstName;
                    ViewBag.Rotations = selectRotations;
                    ViewBag.Supervisors = selectRotationSupervisors;
               
                    return View(rotationList);
                }
                else
                {
                    return RedirectToAction("StudentPage", "Student");
                }

            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult EditRotationStudent(RotationList rotationList)
        {
            if (Session["Email"] != null)
            {
                RotationListContext rotationListContext = new RotationListContext();
                rotationListContext.Entry(rotationList).State = System.Data.Entity.EntityState.Modified;
                rotationListContext.SaveChanges();

                return RedirectToAction("StudentPage", "Student");


            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DetailsRotationStudent(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    RotationListContext rotationListContext = new RotationListContext();
                RotationList rotationList = rotationListContext.RotationList.Single(e => e.Id == id);

                return View(rotationList);

                }
                else
                {
                    return RedirectToAction("StudentPage", "Student");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult DeleteRotationStudent(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    RotationListContext rotationListContext = new RotationListContext();
                RotationList rotationList = rotationListContext.RotationList.Single(e => e.Id == id);

                return View(rotationList);

                }
                else
                {
                    return RedirectToAction("StudentPage", "Student");
                }
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult DeleteRotationStudent(int? id, FormCollection formCollection)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    RotationListContext rotationListContext = new RotationListContext();
                RotationList rotationList = rotationListContext.RotationList.Single(e => e.Id == id);

                rotationListContext.RotationList.Remove(rotationList);
                rotationListContext.SaveChanges();

                return RedirectToAction("StudentPage", "Student");

                }
                else
                {
                    return RedirectToAction("StudentPage", "Student");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        public Student getStudentInfo(string email)
        {
            StudentContext studentContext = new StudentContext();
            Student student = studentContext.Students.Single(e => e.Email.Equals(email));

            return student;
        }
    }

}