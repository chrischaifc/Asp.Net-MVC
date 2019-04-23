using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyFirstMVC.Models.RotationList
{
    [Table("dbo.RotationList")]
    public class RotationList
    {
        public int Id { get; set; }
        
        public string StudentName { get; set; }

        public string Type { get; set; }
        
        public string Start { get; set; }

        public string End { get; set; }

        public string Supervisor { get; set; }

        public int StudentId { get; set; }
    }
}