using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyFirstMVC.Models.Institution
{
    public class InstitutionalContext:DbContext
    {
        public DbSet<Institutional> Institutionals { get; set; }
    }
}