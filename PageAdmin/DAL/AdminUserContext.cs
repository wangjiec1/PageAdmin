using PageAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PageAdmin.DAL
{
    public class AdminUserContext : DbContext
    {
        public AdminUserContext() : base("AdminUserContext")
        {

        }

        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<Site> Site { get; set; }
    }
}