using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyFirstMVC.Models.Program
{
    public class ProgramContext:DbContext
    {
        public DbSet<Programs> Programs { get; set; }
    }
}