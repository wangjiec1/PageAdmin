using Foolproof;
using PageAdmin.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageAdmin.Models.ViewModel
{
    public class UserFormViewModel
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }

        [RequiredIf("EditType", Operator.EqualTo, EditType.Create)]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RequiredIf("EditType", Operator.EqualTo, EditType.Create)]
        [Display(Name = "Bekräfta Lösenord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenorden stämmer inte överens.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Roll")]
        public RoleEnum Role { get;set; }

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

        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        public EditType EditType { get;set; }

        public UserFormViewModel()
        {

        }

        public UserFormViewModel(AdminUser user)
        {
            ID = user.ID;
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Address = user.Address;
            ZipCode = user.ZipCode;
            City = user.City;
            Role = user.Role;
        }
    }
}