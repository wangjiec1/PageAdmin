using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageAdmin.Models.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Användar namn")]
        public string Username { get; set; }

        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}