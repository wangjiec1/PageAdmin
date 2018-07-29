using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PageAdmin.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Display(Name= "Email")]
        public string Email { get; set; }

        [Display(Name = "Roll")]
        public string Role { get; set; }

        [Display(Name = "Skapad")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Senast inloggad")]
        public DateTime? LastLogin { get; set; }

        public List<UserViewModel> Users { get; set; }

        public UserViewModel()
        {

        }

        public UserViewModel(AdminUser user)
        {
            Id = user.ID;
            UserName = user.UserName;
            Name = user.FirstName + " " + user.LastName;
            Email = user.Email;
            Role = user.Role.ToString();
            CreateDate = user.CreateDate;
            LastLogin = user.LastLogin;
        }

        public UserViewModel(List<AdminUser> users)
        {
            Users = new List<UserViewModel>();
            foreach(var user in users)
            {
                var m = new UserViewModel(user);
                Users.Add(m);
            }
        }
    }

}