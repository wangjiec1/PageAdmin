using PageAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace PageAdmin.Service
{
    public static class UserService
    {
        private static string _cookieName = "cookieAuthAdmin";
        private static string _siteCookieID = "siteCookieID";

        public static string GetSalt()
        {
            byte[] buffer = new byte[128 / 8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }

        public static string PasswordHash(string salt, string Password)
        {
            string passwordHash;
            using (var sha256 = SHA256.Create())
            {
                var hashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                passwordHash = BitConverter.ToString(hashbytes).Replace("-", "").ToLower();
                passwordHash = salt + passwordHash;              
            }
            return passwordHash;
        }

        public static bool ComparePassword(string saltedPassword, string passwordHash)
        {
            bool match = saltedPassword.Equals(passwordHash);
            return match;
        }

        public static HttpCookie AuthCookie(AdminUser user)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.UserName,
                DateTime.Now,
                DateTime.Now.AddDays(7),
                true,
                user.Role + "|" + user.ID,
                _cookieName
                );
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(_cookieName, encryptedTicket);
            cookie.HttpOnly = true;
            return cookie;
        }
        public static HttpCookie GetAuthCookie()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[_cookieName];
            return authCookie;
        }
        public static string GetUserData(string data)
        {
            HttpCookie cookie = GetAuthCookie();
            if (cookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                string[] userData = authTicket.UserData.Split(new char[] { '|' });
                switch (data.ToLower())
                {
                    case "id":
                        Guid UserID = Guid.Parse(userData[1]);
                        return UserID.ToString();

                    case "role":
                        return userData[0];

                    case "sites":
                        return userData[2];

                    default:
                        return userData.ToString();
                }
            }
            return string.Empty;
        }

        public static HttpCookie SiteCookie(AdminUser user)
        {
            string siteIDs = string.Empty;
            foreach(var site in user.Sites)
            {
                if(siteIDs == string.Empty)
                {
                    siteIDs = site.ID.ToString();
                }
                else
                {
                    siteIDs = siteIDs + "|" + site.ID.ToString();
                }
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.UserName,
                DateTime.Now,
                DateTime.Now.AddDays(7),
                true,
                siteIDs,
                _siteCookieID
                );
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(_siteCookieID, encryptedTicket);
            cookie.HttpOnly = true;
            return cookie;
        }

    }
}