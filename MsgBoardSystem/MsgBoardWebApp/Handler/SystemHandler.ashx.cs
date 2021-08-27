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

                string statusMsg = string.Empty;

                List<UserInfoModel> userInfo = AuthManager.GetInfo(acc);

                if (userInfo != null)
                {
                    if (string.Compare(pwd, userInfo[0].Password, false) == 0)
                    {
                        // 登入驗證
                        AuthManager.LoginAuthentication(userInfo[0]);
                        context.Session["UID"] = userInfo[0].UserID;
                        statusMsg = "Success";
                    }
                    else
                    {
                        statusMsg = "密碼錯誤";
                    }
                }
                else
                {
                    statusMsg = "用戶不存在";
                }

                // throw statusMsg
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(statusMsg);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            // ajax呼叫後傳送Session UID
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
            // 取得貼文內容
            else if (actionName == "GetPostInfo")
            {
                // 從ajax取得PID
                var ajaxPID = context.Request.Form["PID"];
                if (!Guid.TryParse(ajaxPID, out Guid pid))
                {
                    context.Response.Write("Pid Error");
                    context.Response.End();
                    return;
                }

                try
                {
                    // 取得貼文資料
                    List<PostInfoModel> postInfo = PostManager.GetOnePostInfo(pid);

                    // 寫入Response
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(postInfo);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // 取得貼文的全部留言
            else if (actionName == "GetAllMsg")
            {
                // 從ajax取得PID
                var ajaxPID = context.Request.Form["PID"];
                if (!Guid.TryParse(ajaxPID, out Guid pid))
                {
                    context.Response.Write("Pid Error");
                    context.Response.End();
                    return;
                }

                try
                {
                    // 取得貼文的全部留言
                    List<MsgInfoModel> allMsg = PostManager.GetAllPostMsg(pid);

                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(allMsg);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
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
                    string responseMsg = AccountFunction.CheckAccountExist(accountInfo.Account);

                    if (responseMsg == string.Empty)
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
            // 建立貼文
            else if (actionName == "NewPost")
            {
                // Get value from ajax
                string title = context.Request.Form["Title"];
                string body = context.Request.Form["Body"];
                string strUID = context.Session["UID"].ToString();
                string responseMsg = string.Empty;

                if (!Guid.TryParse(strUID, out Guid UID))
                {
                    responseMsg = "Session UID Error";
                }

                // set value to object and write into DB
                try
                {
                    Posting postInfo = new Posting()
                    {
                        PostID = Guid.NewGuid(),
                        UserID = UID,
                        CreateDate = DateTime.Now,
                        Title = title,
                        Body = body,
                        ismaincontent = false
                    };

                    // check UID is correct and user is exist
                    var checkUID = PostManager.GetUserName(UID);

                    if (checkUID != null)
                    {
                        // write into DB
                        responseMsg = PostManager.CreateNewPost(postInfo);
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
            // 建立留言
            else if (actionName == "NewMsg")
            {
                // Get value from ajax
                string body = context.Request.Form["Body"];
                string strPID = context.Request.Form["PID"];
                string strUID = context.Session["UID"].ToString();
                string responseMsg = string.Empty;

                // check guid
                if (!Guid.TryParse(strUID, out Guid uid))
                {
                    responseMsg = "Param UID Error";
                }

                if (!Guid.TryParse(strPID, out Guid pid))
                {
                    responseMsg = "Param PID Error";
                }

                // set value to object and write into DB
                try
                {
                    Message msgInfo = new Message()
                    {
                        MsgID = Guid.NewGuid(),
                        PostID = pid,
                        UserID = uid,
                        CreateDate = DateTime.Now,
                        Body = body,
                    };

                    // check UID and PID is correct and user is exist
                    var checkUID = PostManager.GetUserName(uid);
                    bool checkPID = PostManager.CheckPostExist(pid);

                    if (checkUID != null && checkPID)
                    {
                        // write into DB
                        responseMsg = PostManager.CreateNewMsg(msgInfo);
                    }
                    else
                    {
                        // Have error
                        responseMsg = "Exception Error";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(responseMsg);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
            // 取得會員資料
            else if(actionName == "GetEditInfo")
            {
                string strUID = context.Session["UID"].ToString();

                // check guid
                if (!Guid.TryParse(strUID, out Guid uid))
                {
                    context.Response.Write("Param UID Error");
                    context.Response.End();
                }

                try
                {
                    // get user infomation
                    List<EditInfoModel> editInfo = AccountFunction.GetEditInfo(uid);

                    // send to ajax
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(editInfo[0]);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // 更新會員資料
            else if (actionName == "UpdateInfo")
            {
                string strUID = context.Session["UID"].ToString();

                // check guid
                if (!Guid.TryParse(strUID, out Guid uid))
                {
                    context.Response.Write("Param UID Error");
                    context.Response.End();
                }

                try
                {
                    EditInfoModel editSource = new EditInfoModel()
                    {
                        Name = context.Request.Form["Name"],
                        Email = context.Request.Form["Email"],
                        Birthday = context.Request.Form["Birthday"],
                        Account = context.Request.Form["Account"]
                    };

                    string resultMsg = AccountFunction.UpdateUserInfo(editSource, uid);

                    // send to ajax
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(resultMsg);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    throw ex;
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
    }
}