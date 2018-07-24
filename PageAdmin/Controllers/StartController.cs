using PageAdmin.DAL;
using PageAdmin.Models;
using PageAdmin.Models.ViewModel;
using PageAdmin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageAdmin.Controllers
{
    public class StartController : Controller
    {
        private AdminUserContext _db = new AdminUserContext();
        // GET: Start
        public ActionResult Index()
        {
            var userData = UserService.GetUserData(string.Empty);
            if(userData == string.Empty)
            {
                return RedirectToAction("Login");
            }
            else
            {
                 return RedirectToAction("Index", "AdminUser");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginValidate(LoginViewModel model)
        {
            var user = _db.AdminUser.Where(x => x.UserName.ToLower() == model.Username.ToLower()).SingleOrDefault();
            if(user != null)
            {
                var saltedPassword = UserService.PasswordHash(user.PasswordSalt, model.Password);
                bool passwordMatch = UserService.ComparePassword(user.Password, saltedPassword);
                if (passwordMatch)
                {
                    
                    HttpContext.Response.Cookies.Add(UserService.AuthCookie(user));
                    HttpContext.Response.Cookies.Add(UserService.SiteCookie(user));
                    return RedirectToAction("Index", "AdminUser");
                }
            }
            ModelState.AddModelError("", "Fel användarnamn eller lösenord.");
            return View("Login");
        }
    }
}