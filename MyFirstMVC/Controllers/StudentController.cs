using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentPage()
        {
            if (!Session["FirstName"].Equals(null))
            {
                ViewBag.FirstName = Session["FirstName"];
            }
            return View();
        }
    }
}