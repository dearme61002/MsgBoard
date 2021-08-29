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
                try
                {
                    var get_acc = context.Request.Form["Account"];
                    var get_pwd = context.Request.Form["Password"];
                    string acc = Convert.ToString(get_acc);
                    string pwd = Convert.ToString(get_pwd);

                    string[] statusMsg = new string[2];

                    UserInfoModel userInfo = AuthManager.GetInfo(acc);

                    if (userInfo != null)
                    {
                        if (string.Compare(pwd, userInfo.Password, false) == 0)
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
                    throw ex;
                }
            }
            // ajax呼叫後傳送Session UID
            else if (actionName == "GetSession")
            {
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(context.Session["UID"]);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
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
                        List<PwdInfoModel> pwdInfo = AccountFunction.GetUserPwd(uid);

                        // Check new password
                        if (string.Compare(newPwd, newPwdAgain, false) == 0)
                        {
                            // Check input password and DB password
                            if (string.Compare(oldPwd, pwdInfo[0].Password, false) == 0)
                            {
                                // Update password
                                resultMsg = AccountFunction.UpdateUserPwd(uid, pwdInfo[0].Account, newPwd);
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
                    if (AccountFunction.CheckUserByAccount(account) != null)
                    {
                        // check info is correct
                        UserInfoModel userInfo = AuthManager.GetInfo(account);
                        int checkEmail = string.Compare(email, userInfo.Email, false);
                        int checkDate = string.Compare(birthday, userInfo.Birthday.ToString("yyyy-MM-dd"), false);
                        int checkResult = checkEmail + checkDate;

                        if (checkResult == 0)
                        {
                            // write random string into password
                            string updateResult = AccountFunction.UpdateUserPwd(userInfo.UserID, account, newPwd);
                            if (string.Compare(updateResult, "Success", false) == 0)
                            {
                                resultMsg[0] = "Success";
                                resultMsg[1] = "<p>新密碼 :  " + newPwd + "</p><p>請登入後盡快修改會員密碼</p>";
                            }
                            else
                                resultMsg[0] = updateResult;
                        }
                        else
                            resultMsg[0] = "Email 或 生日日期不符，請重新輸入";
                    }
                    else
                        resultMsg[0] = "此帳號不存在，請重新輸入";

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