using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MsgBoardWebApp.Handler
{
    /// <summary>
    /// SystemHandler 的摘要描述
    /// </summary>
    public class SystemHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string actionName = context.Request.QueryString["ActionName"];

            if (string.IsNullOrEmpty(actionName))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ActionName is required");
                context.Response.End();
            }

            if (actionName == "Login")
            {
                var get_acc = context.Request.Form["Account"];
                var get_pwd = context.Request.Form["Password"];
                string acc = Convert.ToString(get_acc);
                string pwd = Convert.ToString(get_pwd);
                
                if(string.Compare(acc, "Admin", false) == 0)
                {
                    if(string.Compare(pwd, "12345", false) == 0)
                    {
                        LoginAuthentication(context, acc);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void LoginAuthentication(HttpContext context, string account)
        {
            string userID = "";
            string[] roles = { "Admin" };
            bool isPersistance = false;

            FormsAuthentication.SetAuthCookie(account, isPersistance);
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(
                    1,
                    account,
                    DateTime.Now,
                    DateTime.Now.AddHours(12),
                    isPersistance,
                    userID
                );

            FormsIdentity identity = new FormsIdentity(ticket);
            HttpCookie cookie =
                new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket)
                );
            cookie.HttpOnly = true;

            GenericPrincipal gp = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = gp;
            HttpContext.Current.Response.Cookies.Add(cookie);
            context.Response.Redirect("~\\Page01Default.aspx");
        }
    }
}