using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstMVC.Models.RotationList;
using MyFirstMVC.Models;

namespace MyFirstMVC.Controllers.Admin
{
    public class RotationListController : Controller
    {
        public ActionResult Index(string option, string search)
        {

            if (Session["Email"] != null)
            {
                RotationListContext context = new RotationListContext();
                //if a user choose the radio button option as Subject  
                if (option == "Name")
                {
                    //Index action method will return a view with a student records based on what a user specify the value in textbox  
                    return View(context.RotationList.Where(x => x.StudentName == search || search == null).ToList());
                }
                else if (option == "Type")
                {
                    return View(context.RotationList.Where(x => x.Type == search || search == null).ToList());
                }
                else if (option == "Start")
                {
                    return View(context.RotationList.Where(x => x.Start == search || search == null).ToList());
                }
                else
                {
                    return View(context.RotationList.Where(x => x.End.StartsWith(search) || search == null).ToList());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    RotationListContext context = new RotationListContext();
                    RotationList rotationList = context.RotationList.Single(pro => pro.Id == id);

                    return View(rotationList);
                }
                else
                {
                    return RedirectToAction("Index", "RotationList");
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
                    RotationListContext context = new RotationListContext();
                    RotationList rotationLists = context.RotationList.Single(pro => pro.Id == id);

                    rotationLists.StudentName = formCollection["StudentName"];
                    rotationLists.Type = formCollection["Type"];
                    rotationLists.Start = formCollection["Start"];
                    rotationLists.End = formCollection["End"];
                    rotationLists.Supervisor = formCollection["Supervisor"];

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "RotationList");
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
                RotationList rotationLists = new RotationList();
                RotationListContext context = new RotationListContext();

                rotationLists.StudentName = formCollection["StudentName"];
                rotationLists.Type = formCollection["Type"];
                rotationLists.Start = formCollection["Start"];
                rotationLists.End = formCollection["End"];
                rotationLists.Supervisor = formCollection["Supervisor"];

                context.RotationList.Add(rotationLists);
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
                    RotationListContext context = new RotationListContext();
                    RotationList rotationLists = context.RotationList.Single(pro => pro.Id == id);

                    return View(rotationLists);
                }
                else
                {
                    return RedirectToAction("Index", "RotationList");
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
                    RotationListContext context = new RotationListContext();
                    RotationList rotationLists = context.RotationList.Single(pro => pro.Id == id);

                    context.RotationList.Remove(rotationLists);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "RotationList");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int? id)
        {
            if (Session["Email"] != null)
            {
                if (id != null)
                {
                    StudentContext context = new StudentContext();
                    Student student = context.Students.Single(stu => stu.ID == id);

                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "RotationList");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExportView()
        {
            if (Session["Email"] != null)
            {
                RotationListContext context = new RotationListContext();
                List<RotationList> rotationLists = context.RotationList.ToList();
                Response.AddHeader("content-disposition", "attachment;filename=Report1.xls");
                Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                return View(rotationLists);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}