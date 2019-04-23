using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyFirstMVC.Models.Admin
{
    public class AdminContext:DbContext
    {
        public DbSet<Admins> Admins { get; set; }
    }
}