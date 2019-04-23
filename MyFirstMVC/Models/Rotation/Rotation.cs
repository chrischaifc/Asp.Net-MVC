using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyFirstMVC.Models.Rotation
{
    [Table("dbo.Rotations")]
    public class Rotation
    {
        public int Id { get; set; }
        public string Rotations { get; set; }
        public string Supervisor { get; set; }

    }
}