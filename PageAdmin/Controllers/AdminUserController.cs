using PageAdmin.DAL;
using PageAdmin.Helpers;
using PageAdmin.Models;
using PageAdmin.Models.Enum;
using PageAdmin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageAdmin.Controllers
{
    [CustomAuthorizeAttribute("Admin", "Owner")]
    public class AdminUserController : Controller
    {
        AdminUserContext _db = new AdminUserContext();

        public ActionResult Index()
        {
            Guid userID = Guid.Parse(UserService.GetUserData("id"));
            
            var user = _db.AdminUser.Where(x => x.ID == userID).SingleOrDefault();
            if (user == null)
                return RedirectToAction("Login", "Start");


            var sites = user.Sites.ToList();
            var model = new UserViewModel(sites[0].Users.ToList());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser()
        {
            return View();
        }
    }
}