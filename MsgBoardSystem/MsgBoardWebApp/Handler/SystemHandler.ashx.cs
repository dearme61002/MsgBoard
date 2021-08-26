using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SystemDBFunction;
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

            // 登入驗證
            if (actionName == "Login")
            {
                var get_acc = context.Request.Form["Account"];
                var get_pwd = context.Request.Form["Password"];
                string acc = Convert.ToString(get_acc);
                string pwd = Convert.ToString(get_pwd);

                List<UserInfoModel> userInfo = AuthManager.GetInfo(acc);

                if (userInfo != null)
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
            // 用Session傳送UID
            else if (actionName == "GetSession")
            {
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(context.Session["UID"]);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            // 從DB取得貼文資料
            else if (actionName == "GetAllPost")
            {
                List<PostInfoModel> allPostInfo = PostManager.GetAllPostInfo();

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(allPostInfo);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            // 註冊會員功能
            else if (actionName == "Register")
            {
                // Get value from ajax
                string name = context.Request.Form["Name"];
                string account = context.Request.Form["Account"];
                string password = context.Request.Form["Password"];
                string email = context.Request.Form["Email"];
                DateTime birthday = Convert.ToDateTime(context.Request.Form["BirthDay"]);

                // return status message
                string responseMsg = string.Empty;

                // set value to object and write into DB
                try
                {
                    Accounting accountInfo = new Accounting()
                    {
                        UserID = Guid.NewGuid(),
                        Name = name,
                        CreateDate = DateTime.Now,
                        Account = account,
                        Password = password,
                        Level = "Member",
                        Email = email,
                        BirthDay = birthday
                    };

                    // check account is already exist
                    responseMsg = AccountFunction.CheckAccountExist(accountInfo.Account);
                    
                    if(responseMsg == string.Empty)
                    {
                        // check email is already exist
                        responseMsg = AccountFunction.CheckEmailExist(accountInfo.Email);
                        if (responseMsg == string.Empty)
                        {
                            // write account info into DB
                            responseMsg = AccountFunction.CreateAccount(accountInfo);
                        }
                    }

                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(responseMsg);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // 確認註冊成功
            else if (actionName == "Register_re")
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