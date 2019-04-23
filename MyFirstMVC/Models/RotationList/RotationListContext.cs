using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyFirstMVC.Models.RotationList
{
    public class RotationListContext:DbContext
    {
        public DbSet<RotationList> RotationList { get; set; }

        public System.Data.Entity.DbSet<MyFirstMVC.Models.Rotation.Rotation> Rotations { get; set; }
    }
}