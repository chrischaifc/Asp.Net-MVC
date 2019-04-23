using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyFirstMVC.Models.Rotation;

namespace MyFirstMVC.Models.Rotation
{
    public class RotationContext:DbContext
    {
        public DbSet<Rotation> Rotations { get; set; }
    }
}