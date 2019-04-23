using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyFirstMVC.Models
{
    public class StudentContext : DbContext
    {
        // GET: StudentContext
        public DbSet<Student> Students { get; set; }
    }
}