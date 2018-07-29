using PageAdmin.Models;
using PageAdmin.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PageAdmin.DAL
{
    public class AdminUserIntialize : DropCreateDatabaseAlways<AdminUserContext>
    {
        protected override void Seed(AdminUserContext context)
        { 
            var salt = UserService.GetSalt();
            var adminUser = new List<AdminUser>
            {
                new AdminUser{ID = Guid.Parse("b1969de6-9beb-4933-a765-f8234303268c"), UserName = "Mimer", Password = UserService.PasswordHash(salt, "1234"),PasswordSalt = salt, CreateDate = DateTime.Now, LastLogin = DateTime.Now, Role = Models.Enum.RoleEnum.Owner },

                new AdminUser{ID = Guid.Parse("939e1a53-0799-4c50-9b1a-28287fae5d2b"), UserName = "Grifling", Password = UserService.PasswordHash(salt, "1234"),PasswordSalt = salt, CreateDate = DateTime.Now, LastLogin = DateTime.Now, Role = Models.Enum.RoleEnum.Admin }
            };

            adminUser.ForEach(s => context.AdminUser.Add(s));
            context.SaveChanges();

            var site = new List<Site>
            {
                new Site { ID = Guid.NewGuid(), Name = "Grifling" }
            };

            site.ForEach(s => context.Site.Add(s));
            context.SaveChanges();

            adminUser[0].Site = site[0];
            adminUser[1].Site = site[0];

            List<AdminUser> users = new List<AdminUser>();
            users.Add(adminUser[0]);
            users.Add(adminUser[1]);
            site[0].Users = users;
            //adminUser.ForEach(s => context.AdminUser.Add(s));
            context.SaveChanges();
        }
    }
}