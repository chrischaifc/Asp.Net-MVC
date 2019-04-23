using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyFirstMVC.Models.Institution
{
    [Table("dbo.Institutional")]
    public class Institutional
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }
        

    }
}