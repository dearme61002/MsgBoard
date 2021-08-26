using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WebAuth;

namespace MsgBoardWebApp.Handler
{
    /// <summary>
    /// SystemHandler 的摘要描述
    /// </summary>
    public class SystemHandler : IHttpHandler, IRequiresSessionState
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

                List<UserInfoModel> userInfo = AuthManager.GetInfo(acc);

                if(userInfo != null)
                {
                    if (string.Compare(pwd, userInfo[0].Password, false) == 0)
                    {
                        // 登入驗證
                        AuthManager.LoginAuthentication(userInfo[0]);
                        context.Session["UID"] = userInfo[0].UserID;
                        //string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userInfo);
                        //context.Response.ContentType = "application/json";
                        //context.Response.Write(jsonText);
                    }
                    else
                    {
                        context.Response.Write("密碼錯誤");
                        context.Response.End();
                    }
                }
                else
                {
                    context.Response.Write("用戶不存在");
                    context.Response.End();
                }
            }
            else if (actionName == "GetSession")
            {
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(context.Session["UID"]);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            else if(actionName == "GetAllPost")
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}