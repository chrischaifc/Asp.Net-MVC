using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models.Program;
using MyFirstMVC.Models.Rotation;

namespace MyFirstMVC.Controllers.Admin
{
    public class RotationController : Controller
    {
        // GET: Rotation
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                RotationContext context = new RotationContext();
                List<Rotation> rotations = context.Rotations.ToList();
                return View(rotations);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    RotationContext context = new RotationContext();
                    Rotation rotation = context.Rotations.Single(pro => pro.Id == id);
                    return View(rotation);
                }
                else
                {
                    return RedirectToAction("Index", "Rotation");
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
                    RotationContext context = new RotationContext();
                    Rotation rotation = context.Rotations.Single(pro => pro.Id == id);

                    rotation.Rotations = formCollection["Rotations"];
                    rotation.Supervisor = formCollection["Supervisor"];

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Rotation");
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
                Rotation rotations = new Rotation();
                RotationContext context = new RotationContext();

                rotations.Rotations = formCollection["Rotations"];
                rotations.Supervisor = formCollection["Supervisor"];

                context.Rotations.Add(rotations);
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
                    RotationContext context = new RotationContext();
                    Rotation rotation = context.Rotations.Single(pro => pro.Id == id);

                    return View(rotation);
                }
                else
                {
                    return RedirectToAction("Index", "Rotation");
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
                    RotationContext context = new RotationContext();
                    Rotation rotations = context.Rotations.Single(pro => pro.Id == id);

                    context.Rotations.Remove(rotations);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Rotation");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}