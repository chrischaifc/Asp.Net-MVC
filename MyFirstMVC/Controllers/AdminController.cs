﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminPage()
        {
            return View();
        }
    }
}