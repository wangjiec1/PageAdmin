using PageAdmin.Models.Enum;
using PageAdmin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageAdmin.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private new List<string> _roles;
        public CustomAuthorizeAttribute(params string[] roles) : base()
        {
            _roles = roles.ToList();           
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
            if(_roles == null)
            {
                HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var userRole = UserService.GetUserData("role");
                bool isAuthorize = _roles.Any(role => role == userRole);

                if (!isAuthorize)
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
        }
    }

}