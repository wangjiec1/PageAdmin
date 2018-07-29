using PageAdmin.DAL;
using PageAdmin.Helpers;
using PageAdmin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageAdmin.Controllers
{
    [CustomAuthorizeAttribute("Owner", "SuperAdmin")]
    public class SiteController : BaseController
    {

        AdminUserContext _db = new AdminUserContext();
        // GET: Site
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult SiteGrid()
        {
            var sites = _db.Site.ToList();
            var model = new SiteGridViewModel(sites);
            var view = RenderRazorViewToString("_siteGrid", model);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SiteForm(string id)
        {
            Guid siteID = Guid.Parse(id);
            var site = new SiteGridViewModel(_db.Site.Find(siteID));
            var view = RenderRazorViewToString("_siteForm", site);
            return Json(view);
        }
    }
}