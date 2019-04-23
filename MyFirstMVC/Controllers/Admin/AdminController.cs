using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models.Admin;
using MyFirstMVC.Models.RotationList;

namespace MyFirstMVC.Controllers.Admin
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult Index()
        {
            RotationListContext context = new RotationListContext();
            List<RotationList> rotationLists = context.RotationList.ToList();
            

            return View();
        }

        public ActionResult Validate(FormCollection formCollection)
        {
            AdminContext adminContext = new AdminContext();
            List<Admins> admins = adminContext.Admins.ToList();
            {
                var _admin = admins.Where(s => s.Username == formCollection["Username"]);
                if (_admin.Any())
                {
                    if (_admin.Where(s => s.Password == formCollection["Password"]).Any())
                    {
                        Session["Email"] = formCollection["Username"];
                        return RedirectToAction("Index", "RotationList");
                    }
                    else
                    {
                        TempData["LoginMessage"] = "Invalid Password!";
                        return RedirectToAction("AdminLogin", "Admin");
                    }
                }
                else
                {
                    TempData["LoginMessage"] = "Invalid Email!";
                    return RedirectToAction("AdminLogin", "Admin");
                }
            }
        }
    }
}