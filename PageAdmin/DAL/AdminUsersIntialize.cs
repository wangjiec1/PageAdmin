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
                new AdminUser{ID = Guid.NewGuid(), UserName = "Mimer", Password = UserService.PasswordHash(salt, "1234"),PasswordSalt = salt, CreateDate = DateTime.Now, LastLogin = DateTime.Now, Role = Models.Enum.RoleEnum.Owner },

                new AdminUser{ID = Guid.NewGuid(), UserName = "Grifling", Password = UserService.PasswordHash(salt, "1234"),PasswordSalt = salt, CreateDate = DateTime.Now, LastLogin = DateTime.Now, Role = Models.Enum.RoleEnum.Admin }
            };

            adminUser.ForEach(s => context.AdminUser.Add(s));
            context.SaveChanges();

            var site = new List<Site>
            {
                new Site { ID = Guid.NewGuid(), Name = "Grifling" }
            };

            site.ForEach(s => context.Site.Add(s));
            context.SaveChanges();

            List<Site> sites = new List<Site>();
            sites.Add(site[0]);
            adminUser[0].Sites = sites;
            adminUser[1].Sites = sites;

            List<AdminUser> users = new List<AdminUser>();
            users.Add(adminUser[0]);
            users.Add(adminUser[1]);
            site[0].Users = users;
            //adminUser.ForEach(s => context.AdminUser.Add(s));
            context.SaveChanges();
        }
    }
}