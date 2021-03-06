﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyFirstMVC.Models
{
    [Table("dbo.Students")] 
    public class Student
    {
       
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string ProgramType { get; set; }
        public string ProgramName{ get; set; }
        public string InstitutionName { get; set; }
        public string  SecurityCode{ get; set; }
        
    }
}