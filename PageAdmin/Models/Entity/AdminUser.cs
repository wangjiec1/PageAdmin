using PageAdmin.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageAdmin.Models
{
    public class AdminUser
    {
        public Guid ID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime LastLogin { get; set; }

        public RoleEnum Role { get; set; }
        //public virtual List<Site> SiteList { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}