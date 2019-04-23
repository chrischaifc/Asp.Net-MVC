using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models.Institution;

namespace MyFirstMVC.Controllers.Admin
{
    public class InstitutionalController : Controller
    {

        string CS = ConfigurationManager.ConnectionStrings["HealthSchool"].ConnectionString;
        // GET: Institutional
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                InstitutionalContext context = new InstitutionalContext();
                List<Institutional> institutionals = context.Institutionals.ToList();
                return View(institutionals);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    InstitutionalContext context = new InstitutionalContext();
                    Institutional institutional = context.Institutionals.Single(pro => pro.Id == id);

                    return View(institutional);
                }
                else
                {
                    return RedirectToAction("Index", "Institutional");
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
                    InstitutionalContext context = new InstitutionalContext();
                    Institutional institutional = context.Institutionals.Single(pro => pro.Id == id);

                    institutional.Name = formCollection["Name"];

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Institutional");
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
                Institutional institutional = new Institutional();
                InstitutionalContext context = new InstitutionalContext();

                institutional.Name = formCollection["Name"];

                context.Institutionals.Add(institutional);
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
                    InstitutionalContext context = new InstitutionalContext();
                    Institutional institutional = context.Institutionals.Single(pro => pro.Id == id);

                    return View(institutional);
                }
                else
                {
                    return RedirectToAction("Index", "Institutional");
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
                    InstitutionalContext context = new InstitutionalContext();
                    Institutional institutional = context.Institutionals.Single(pro => pro.Id == id);

                    context.Institutionals.Remove(institutional);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Institutional");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}