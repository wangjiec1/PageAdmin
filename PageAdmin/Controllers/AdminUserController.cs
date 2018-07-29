using PageAdmin.DAL;
using PageAdmin.Helpers;
using PageAdmin.Models;
using PageAdmin.Models.Enum;
using PageAdmin.Models.ViewModel;
using PageAdmin.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageAdmin.Controllers
{
    [CustomAuthorizeAttribute("Admin", "Owner")]
    public class AdminUserController : BaseController
    {
        AdminUserContext _db = new AdminUserContext();

        public ActionResult Index()
        {
            var userID = Guid.Parse(UserService.GetUserData("id"));
            var information = new AdminUserIndexViewModel();
            var user = _db.AdminUser.Find(userID);
            information.Name = user.FirstName + " " + user.LastName;
            information.SiteName = user.Site.Name;
            return View(information);
        }
        
        [HttpPost]
        public ActionResult CreateUser(UserFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                EditUser(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateUser(UserFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                EditUser(model);
            }
            return RedirectToAction("Index");
        }

        public JsonResult DeleteUser(string id)
        {
            Guid delUserID = Guid.Parse(id);
            var delUser = _db.AdminUser.Find(delUserID);
            if(delUser == null)
            {
                return Json("Användare kunde inte tas bort");
            }
            delUser.IsDeleted = true;
            _db.SaveChanges();

            Guid userID = Guid.Parse(UserService.GetUserData("id"));

            var user = _db.AdminUser.Where(x => x.ID == userID).SingleOrDefault();
            if (user == null)
            {
                return Json("Test");
            }


            var sites = user.Site;
            var model = new UserViewModel(sites.Users.Where(x => x.IsDeleted == false).ToList());
            string view = RenderRazorViewToString("_UserGrid", model);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        public void EditUser(UserFormViewModel model)
        {
            if(model.EditType == EditType.Create)
            {
                var user = new AdminUser(model);
                Guid siteId = Guid.Parse(UserService.GetUserData("siteid"));
                user.Site = _db.Site.Where(x => x.ID == siteId).SingleOrDefault();
                _db.AdminUser.Add(user);
                _db.SaveChanges();
            }
            else if(model.EditType == EditType.Edit)
            {
                var user = _db.AdminUser.Where(x => x.UserName == model.Username).SingleOrDefault();
                if(user == null)
                {
                    ModelState.AddModelError("", "Ingen Användare");
                }
                user.ID = model.ID;

                user.UserName = model.Username;
                if (model.EditType == EditType.Edit && model.Password != null)
                {
                    user.PasswordSalt = UserService.GetSalt();
                    user.Password = UserService.PasswordHash(user.PasswordSalt, model.Password);
                }
                user.CreateDate = DateTime.Now;
                user.Role = model.Role;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Address = model.Address;
                user.ZipCode = model.ZipCode;
                user.City = model.City;
                user.IsDeleted = false;

                _db.SaveChanges();
            }
            else if(model.EditType == EditType.Delete)
            {
                var user = _db.AdminUser.Where(x => x.UserName == model.Username && x.Email == model.Email).SingleOrDefault();
                user.IsDeleted = true;
                _db.AdminUser.Attach(user);
                DbEntityEntry<AdminUser> entry = _db.Entry<AdminUser>(user);
                entry.Property(m => m.IsDeleted).IsModified = true;
                _db.SaveChanges();
            }
        }

        public JsonResult LoadForm(string id)
        {
            UserFormViewModel user = new UserFormViewModel();
            if (id == null)
            {
                user.EditType = EditType.Create;
            }
            else
            {
                Guid UserId = Guid.Parse(id);
                var updateUser = new UserFormViewModel(_db.AdminUser.Where(x => x.ID == UserId).SingleOrDefault());
                updateUser.EditType = EditType.Edit;
                user = updateUser;
            }
            string view = RenderRazorViewToString("_UserForm", user);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadUserGrid()
        {
            Guid userID = Guid.Parse(UserService.GetUserData("id"));

            var user = _db.AdminUser.Where(x => x.ID == userID).SingleOrDefault();
            if (user == null)
            {
                return Json("Test");
            }


            var sites = user.Site;
            var model = new UserViewModel(sites.Users.Where(x => x.IsDeleted == false).ToList());
            string view = RenderRazorViewToString("_UserGrid", model);
            return Json(view, JsonRequestBehavior.AllowGet);
        }
    }
}