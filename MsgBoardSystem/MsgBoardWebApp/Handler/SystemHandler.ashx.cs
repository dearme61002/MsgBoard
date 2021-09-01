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

            #region Function List
            // 登入驗證
            if (actionName == "Login")
            {
                try
                {
                    string acc = Convert.ToString(context.Request.Form["Account"]);
                    string pwd = Convert.ToString(context.Request.Form["Password"]);
                    string[] statusMsg = new string[2];

                    Accounting userInfo = AuthManager.GetAccountInfo(acc);

                    // check account exist
                    if (userInfo != null)
                    {
                        // Check account is bucket and bucket is expire or not
                        if (userInfo.Bucket != null && userInfo.Bucket > DateTime.Now)
                        {
                            statusMsg[0] = $"錯誤 : 此帳號被封鎖 ， 即日起至 {userInfo.Bucket.Value.ToString("yyyy-MM-dd")} 後解除";
                        }
                        else
                        {
                            // Check Password
                            if (AuthManager.AccountPasswordAuthentication(pwd, userInfo.Password))
                            {
                                // 登入驗證
                                AuthManager.LoginAuthentication(userInfo);
                                context.Session["UID"] = userInfo.UserID;
                                statusMsg[0] = "Success";
                                statusMsg[1] = userInfo.Name;
                            }
                            else
                            {
                                statusMsg[0] = "密碼錯誤";
                            }
                        }
                    }
                    else
                    {
                        statusMsg[0] = "用戶不存在";
                    }

                    // throw statusMsg
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(statusMsg);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                }
            }
            // 首頁載入時寫入資料
            else if (actionName == "DefaultPageLoad")
            {
                OtherFunctions.DefaultPageRecord();
            }
            // 從DB取得全部貼文資料
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
                try
                {
                    // 從ajax取得PID
                    var ajaxPID = context.Request.Form["PID"];
                    if (!Guid.TryParse(ajaxPID, out Guid pid))
                    {
                        context.Response.Write("Pid Error");
                        context.Response.End();
                        return;
                    }

                    // 取得貼文資料
                    List<PostInfoModel> postInfo = PostManager.GetOnePostInfo(pid);

                    // 寫入Response
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(postInfo[0]);
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
                string responseMsg;
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

                    // check account
                    if (AccountFunction.CheckAccountExist(accountInfo.Account))
                    {
                        responseMsg = "帳號已被註冊";
                    }
                    else
                    {
                        // Check email
                        if (AccountFunction.CheckEmailExist(accountInfo.Email))
                            responseMsg = "Email已被註冊";
                        else
                            // write account info into DB
                            responseMsg = AccountFunction.CreateAccount(accountInfo);
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
                try
                {
                    // Get value from ajax
                    string title = context.Request.Form["Title"];
                    string body = context.Request.Form["Body"];
                    string strUID = context.Session["UID"].ToString();
                    string responseMsg = string.Empty;

                    // Check Guid
                    if (!Guid.TryParse(strUID, out Guid UID))
                        responseMsg = "Session UID Error";

                    // Check body and title string is no swear
                    string checkedTitle = DAL.tools.myTextCheck(title, DAL.tools.getSwear());
                    string checkedBody = DAL.tools.myTextCheck(body, DAL.tools.getSwear());

                    // set value to object and write into DB
                    Posting postInfo = new Posting()
                    {
                        PostID = Guid.NewGuid(),
                        UserID = UID,
                        CreateDate = DateTime.Now,
                        Title = checkedTitle,
                        Body = checkedBody,
                        ismaincontent = false
                    };

                    // check UID is correct and user is exist
                    if (PostManager.CheckUserExist(UID))
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
                try
                {
                    // Get value from ajax
                    string body = context.Request.Form["Body"];
                    string strPID = context.Request.Form["PID"];
                    string strUID = context.Session["UID"].ToString();
                    string responseMsg = string.Empty;

                    // check Guid Values
                    if (!Guid.TryParse(strUID, out Guid uid))
                        responseMsg = "Param UID Error";

                    if (!Guid.TryParse(strPID, out Guid pid))
                        responseMsg = "Param PID Error";

                    // Check message string is no swear
                    string checkedBody = DAL.tools.myTextCheck(body, DAL.tools.getSwear());

                    // set value to object and write into DB
                    Message msgInfo = new Message()
                    {
                        MsgID = Guid.NewGuid(),
                        PostID = pid,
                        UserID = uid,
                        CreateDate = DateTime.Now,
                        Body = checkedBody,
                    };

                    // check UID and PID is correct and user is exist
                    if (PostManager.CheckUserExist(uid) && PostManager.CheckPostExist(pid))
                    {
                        // write into DB
                        responseMsg = PostManager.CreateNewMsg(msgInfo);
                    }
                    else
                    {
                        // Have error
                        responseMsg = "Exception Error";
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
            // 取得會員資料
            else if (actionName == "GetEditInfo")
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
            // 更改會員密碼
            else if (actionName == "UpdatePwd")
            {
                try
                {
                    string strUID = context.Session["UID"].ToString();
                    string oldPwd = context.Request.Form["OldPwd"];
                    string newPwd = context.Request.Form["NewPwd"];
                    string newPwdAgain = context.Request.Form["NewPwdAgain"];
                    string resultMsg = string.Empty;

                    // check guid
                    if (Guid.TryParse(strUID, out Guid uid))
                    {
                        // get password from DB
                        PwdInfoModel dbPwdInfo = AccountFunction.GetUserPwd(uid);

                        // Check new password
                        if (string.Compare(newPwd, newPwdAgain, false) == 0)
                        {
                            // Check input password and DB password
                            if (AuthManager.AccountPasswordAuthentication(oldPwd, dbPwdInfo.Password))
                            {
                                // Update password
                                resultMsg = AccountFunction.UpdateUserPwd(uid, dbPwdInfo.Account, newPwd);
                            }
                            else
                            {
                                resultMsg = "舊密碼輸入錯誤";
                            }
                        }
                        else
                        {
                            resultMsg = "輸入的兩次新密碼不相同";
                        }
                    }
                    else
                    {
                        resultMsg = "Param UID Error";
                    }

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
            // 獲取會員貼文
            else if (actionName == "GetUserPost")
            {
                try
                {
                    // 從Session取得UID並轉型
                    if (!Guid.TryParse(context.Session["UID"].ToString(), out Guid userID))
                    {
                        context.Response.Write("Session UID Error");
                        context.Response.End();
                        return;
                    }

                    // 取得貼文資料
                    List<PostInfoModel> userPostInfo = PostManager.GetAllUserPostInfo(userID);

                    // 寫入Response
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userPostInfo);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // 獲取會員留言
            else if (actionName == "GetUserMsg")
            {
                try
                {
                    // 從Session取得UID並轉型
                    if (!Guid.TryParse(context.Session["UID"].ToString(), out Guid userID))
                    {
                        context.Response.Write("Session UID Error");
                        context.Response.End();
                        return;
                    }

                    // 取得留言資料
                    List<UserMsgInfo> userPostInfo = PostManager.GetUserAllMsgInfo(userID);

                    // 寫入Response
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(userPostInfo);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // 會員自己刪除貼文
            else if (actionName == "UserDeletePost")
            {
                try
                {
                    string strUID = context.Session["UID"].ToString();
                    string strPID = context.Request.Form["PID"];
                    string resultMsg = string.Empty;

                    // check guid
                    if (Guid.TryParse(strUID, out Guid uid) && Guid.TryParse(strPID, out Guid pid))
                        resultMsg = PostManager.UserDeletePost(uid, pid);
                    else
                        resultMsg = "Param UID or Ajax PID Error";

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
            // 會員自己刪除留言
            else if (actionName == "UserDeleteMsg")
            {
                try
                {
                    string strUID = context.Session["UID"].ToString();
                    string strMID = context.Request.Form["MID"];
                    string resultMsg = string.Empty;

                    // check guid
                    if (Guid.TryParse(strUID, out Guid uid) && Guid.TryParse(strMID, out Guid mid))
                        resultMsg = PostManager.UserDeleteMsg(uid, mid);
                    else
                        resultMsg = "Param UID or Ajax MID Error";

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
            // 忘記密碼
            else if (actionName == "ForgetPW")
            {
                try
                {
                    // set values
                    string account = context.Request.Form["Account"];
                    string email = context.Request.Form["Email"];
                    string birthday = context.Request.Form["Birthday"];
                    string newPwd = AccountFunction.CreateRandomCode(5, 5, 10);
                    string[] resultMsg = new string[2];

                    // check account exist
                    if (AccountFunction.CheckAccountExist(account))
                    {
                        // check info is correct
                        Accounting userInfo = AuthManager.GetAccountInfo(account);
                        int checkEmail = string.Compare(email, userInfo.Email, false);
                        int checkDate = string.Compare(birthday, userInfo.BirthDay.ToString("yyyy-MM-dd"), false);
                        int checkResult = checkEmail + checkDate;

                        if (checkResult != 0)
                            resultMsg[0] = "Email 或 生日日期不符，請重新輸入";

                        // write random string into password
                        string updateResult = AccountFunction.UpdateUserPwd(userInfo.UserID, account, newPwd);
                        if (string.Compare(updateResult, "Success", false) == 0)
                        {
                            resultMsg[0] = "Success";
                            resultMsg[1] = "<p>新密碼 :  " + newPwd + "</p><p>請登入後盡快修改會員密碼</p>";
                        }
                        else
                        {
                            resultMsg[0] = updateResult;
                        }
                    }
                    else
                    {
                        resultMsg[0] = "此帳號不存在，請重新輸入";
                    }


                    // send to ajax
                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(resultMsg);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                }
            }
            #endregion
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