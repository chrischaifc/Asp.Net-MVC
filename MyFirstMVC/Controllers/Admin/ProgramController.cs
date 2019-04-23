using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models;
using MyFirstMVC.Models.Program;

namespace MyFirstMVC.Controllers.Admin
{
    public class ProgramController : Controller
    {
        string CS = ConfigurationManager.ConnectionStrings["HealthSchool"].ConnectionString;

        // GET: Admin
        public ActionResult AdminPage()
        {
            if (Session["Email"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //For Program
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                ProgramContext context = new ProgramContext();
                List<Programs> programs = context.Programs.ToList();
                return View(programs);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    ProgramContext context = new ProgramContext();
                    Programs program = context.Programs.Single(pro => pro.Id == id);

                    List<string> programNameList = new List<string>();
                    List<string> programTypeList = new List<string>();


                    using (var connection = new SqlConnection(CS))
                    {
                        string commandText = "SELECT DISTINCT Name FROM [Programs]";
                        using (var command = new SqlCommand(commandText, connection))
                        {
                            connection.Open();
                            using (SqlDataReader sdr = command.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    programNameList.Add(Convert.ToString(sdr[0]));                                  
                                }
                            }

                            connection.Close();
                        }
                    }

                    using (var connection = new SqlConnection(CS))
                    {
                        string commandText = "SELECT DISTINCT Type FROM [Programs]";
                        using (var command = new SqlCommand(commandText, connection))
                        {
                            connection.Open();
                            using (SqlDataReader sdr = command.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    programTypeList.Add(Convert.ToString(sdr[0]));
                                }
                            }

                            connection.Close();
                        }
                    }


                    SelectList selectPrograms = new SelectList(programNameList, "id");
                    SelectList selectProgramTypes = new SelectList(programTypeList, "id");
                    ViewBag.ProgramNames = selectPrograms;
                    ViewBag.ProgramTypes = selectProgramTypes;
                    return View(program);
                }
                else
                {
                    return RedirectToAction("Index", "Program");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {

                    ProgramContext context = new ProgramContext();
                    Programs program = context.Programs.Single(pro => pro.Id == id);

                    program.Name = formCollection["Name"];
                    program.Type = formCollection["Type"];

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Program");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Email"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            if (Session["Email"] != null)
            {
                Programs programs = new Programs();
                ProgramContext context = new ProgramContext();

                programs.Name = formCollection["Name"];
                programs.Type = formCollection["Type"];

                context.Programs.Add(programs);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    ProgramContext context = new ProgramContext();
                    Programs program = context.Programs.Single(pro => pro.Id == id);

                    return View(program);
                }
                else
                {
                    return RedirectToAction("Index", "Program");
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public ActionResult Delete(int? id, FormCollection formCollection)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    ProgramContext context = new ProgramContext();
                    Programs program = context.Programs.Single(pro => pro.Id == id);

                    context.Programs.Remove(program);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Program");
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}