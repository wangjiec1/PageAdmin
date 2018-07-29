﻿using PageAdmin.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageAdmin.Controllers
{
    public class BaseController : Controller
    {
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public Guid UserID()
        {
            return Guid.Parse(UserService.GetUserData("id"));
        }

        public Guid SiteId()
        {
            return Guid.Parse(UserService.GetUserData("siteid"));
        }
    }

}