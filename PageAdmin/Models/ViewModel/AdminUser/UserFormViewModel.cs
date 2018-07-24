using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageAdmin.Models.ViewModel
{
    public class UserFormViewModel
    {
        [Required]
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Lösenords konfirmation")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Roll")]
        public string Role { get;set; }

        [Display(Name = "Förnamn")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }


        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}