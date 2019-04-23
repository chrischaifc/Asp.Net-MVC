using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstMVC.Models
{
    [Table("dbo.Programs")]
    public class Programs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }
}