using PageAdmin.Models.Enum;
using PageAdmin.Models.ViewModel;
using PageAdmin.Service;
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
        public DateTime? LastLogin { get; set; }

        public RoleEnum Role { get; set; }
        //public virtual List<Site> SiteList { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Site Site { get; set; }

        public AdminUser()
        {

        }

        public AdminUser(UserFormViewModel model)
        {
            if (model.ID == Guid.Empty)
            {
                ID = Guid.NewGuid();
            }
            else
            {
                ID = model.ID;
            }
            UserName = model.Username;
            if (model.EditType == EditType.Create || model.EditType == EditType.Edit && model.Password != string.Empty)
            {
                PasswordSalt = UserService.GetSalt();
                Password = UserService.PasswordHash(PasswordSalt, model.Password);
            }
            CreateDate = DateTime.Now;
            Role = model.Role;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Address = model.Address;
            ZipCode = model.ZipCode;
            City = model.City;
            IsDeleted = false;
        }

        public AdminUser(AdminUser model)
        {
            ID = model.ID;
            UserName = model.UserName;
            PasswordSalt = UserService.GetSalt();
            Password = UserService.PasswordHash(PasswordSalt, model.Password);
            CreateDate = DateTime.Now;
            Role = model.Role;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Address = model.Address;
            ZipCode = model.ZipCode;
            City = model.City;
            IsDeleted = false;
        }
    }
}