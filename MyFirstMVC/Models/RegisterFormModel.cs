using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class RegisterFormModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be more than 2 characters long.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be more than 2 characters long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Provide valid email.")]
        public string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Address must be more than 2 characters long.")]
        public string Address { get; set; }

        [Required]
        [RegularExpression("[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy][0-9]" +
                           "[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvwxyz] ?[0-9][ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvwxyz][0-9]"
            , ErrorMessage = "Provide a valid zip code")]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(10)]
        public string DOB { get; set; }

        [Required]
        [StringLength(10)]
        public string StartDate { get; set; }

        [Required]
        [StringLength(10)]
        public string EndDate { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [MinLength(4)]
        [Compare("Password", ErrorMessage = "Passwords don't match, try again.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string ProgramType { get; set; }

        [Required]
        public string ProgramName { get; set; }

        [Required]
        public string InstitutionName { get; set; }

        [Required]
        public string Rotations { get; set; }

        [Required]
        public string RotationSupervisors { get; set; }

    }
}