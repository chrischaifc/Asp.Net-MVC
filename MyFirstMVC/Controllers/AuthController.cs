using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginForm(Models.FormData formData)
        {
            if (formData.IsValid())
            {
                Session["Email"] = formData.Email;
                Session["FirstName"] = formData.FirstName;
                return RedirectToAction("StudentPage", "Student");
            }
            else
            {
                TempData["LoginMessage"] = formData.Message;
                return RedirectToAction("Index", "Auth");
            }
        }

        [HttpPost]
        public ActionResult RegisterForm(Models.FormData formData)
        {
            if (formData.isNew())
            {
                Session["Email"] = formData.Email;
                return RedirectToAction("Index", "Registration");
            }
            else
            {
                TempData["RegisterMessage"] = formData.Message;
                return RedirectToAction("Index", "Auth");
            }
        }
    }
}