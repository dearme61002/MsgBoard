using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using SystemAuth;

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

                if (string.Compare(acc, "Admin", false) == 0)
                {
                    if (string.Compare(pwd, "12345", false) == 0)
                    {
                        // 登入驗證
                        LoginAuthentication();
                        GetInfo(acc, context);
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

        public void LoginAuthentication()
        {
            string account = "Will";
            string userID = "S12345";
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
            cookie.HttpOnly = false;

            GenericPrincipal gp = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = gp;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private void GetInfo(string account, HttpContext context)
        {
            List<Accounting> sourceList = AuthManager.GetAccountInfo(account);
            List<UserInfoModel> userSource =
                sourceList.Select(obj => new UserInfoModel()
                {
                    ID = obj.ID,
                    UserID = obj.UserID,
                    Name = obj.Name,
                    CreateDate = obj.CreateDate,
                    Account = obj.Account,
                    Password = obj.Password,
                    Level = obj.Level,
                    Email = obj.Email,
                    Bucket = obj.Bucket,
                    Birthday = obj.BirthDay
                }).ToList();

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userSource);

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);
        }
    }
}