using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers.Admin
{
    public class StudentListController : Controller
    {
        // GET: StudentList
        //For Student
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                StudentContext context = new StudentContext();
                List<Student> students = context.Students.ToList();
                return View(students);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    StudentContext context = new StudentContext();
                    Student student = context.Students.Single(pro => pro.ID == id);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "StudentList");
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
                    StudentContext context = new StudentContext();
                    Student student = context.Students.Single(pro => pro.ID == id);

                    student.FirstName = formCollection["FirstName"];
                    student.LastName = formCollection["LastName"];
                    student.Email = formCollection["Email"];
                    student.Address = formCollection["Address"];
                    student.DOB = formCollection["DOB"];
                    student.InstitutionName = formCollection["InstitutionName"];
                    student.Phone = formCollection["Phone"];
                    student.PostalCode = formCollection["PostalCode"];
                    student.ProgramName = formCollection["ProgramName"];
                    student.ProgramType = formCollection["ProgramType"];
                    student.SecurityCode = formCollection["SecurityCode"];
                    student.Password = formCollection["Password"];

                    context.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return RedirectToAction("Index", "StudentList");
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
                Student student = new Student();
                StudentContext context = new StudentContext();


                student.FirstName = formCollection["FirstName"];
                student.LastName = formCollection["LastName"];
                student.Email = formCollection["Email"];
                student.Address = formCollection["Address"];
                student.DOB = formCollection["DOB"];
                student.InstitutionName = formCollection["InstitutionName"];
                student.Phone = formCollection["Phone"];
                student.PostalCode = formCollection["PostalCode"];
                student.ProgramName = formCollection["ProgramName"];
                student.ProgramType = formCollection["ProgramType"];
                student.SecurityCode = formCollection["SecurityCode"];
                student.Password = formCollection["Password"];

                context.Students.Add(student);
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
                    StudentContext context = new StudentContext();
                    Student student = context.Students.Single(pro => pro.ID == id);

                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "StudentList");
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
                    StudentContext context = new StudentContext();
                    Student student = context.Students.Single(pro => pro.ID == id);

                    context.Students.Remove(student);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "StudentList");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}