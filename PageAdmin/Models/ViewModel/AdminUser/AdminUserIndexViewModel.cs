using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageAdmin.Models.ViewModel
{
    public class AdminUserIndexViewModel
    {
        [Display(Name = "Inloggad")]
        public string Name { get; set; }

        [Display(Name = "Sida")]
        public string SiteName { get; set; }
    }
}