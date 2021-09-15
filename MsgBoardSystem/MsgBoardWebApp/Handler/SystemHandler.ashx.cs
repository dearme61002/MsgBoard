using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                    string ip = Convert.ToString(context.Request.Form["IP"]);
                    string[] statusMsg = new string[2];

                    Accounting userInfo = AuthManager.GetAccountInfo(acc);

                    // check account exist
                    if (userInfo == null)
                    {
                        statusMsg[0] = "用戶不存在";
                        SendDataByJSON(context, statusMsg);
                        return; 
                    }

                    // Check account is bucket and bucket is expire or not
                    if (userInfo.Bucket != null && userInfo.Bucket > DateTime.Now)
                    {
                        statusMsg[0] = $"錯誤 : 此帳號被封鎖 ， 即日起至 {userInfo.Bucket.Value.ToString("yyyy-MM-dd")} 後解除";
                        SendDataByJSON(context, statusMsg);
                        return;
                    }

                    // Check Password
                    if (AuthManager.AccountPasswordAuthentication(pwd, userInfo.Password))
                    {
                        // 登入驗證
                        DateTime loginDate = DateTime.Now;

                        AuthManager.LoginAuthentication(userInfo);
                        AuthManager.RecordUserLogin(ip, userInfo.UserID, loginDate);

                        context.Session["LoginDate"] = loginDate;
                        context.Session["UID"] = userInfo.UserID;
                        statusMsg[0] = "Success";
                        statusMsg[1] = userInfo.Name;
                    }
                    else
                    {
                        statusMsg[0] = "密碼錯誤";
                    }

                    SendDataByJSON(context, statusMsg);
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
                SendDataByJSON(context, allPostInfo);
            }
            // 取得貼文內容
            else if (actionName == "GetPostInfo")
            {
                try
                {
                    // 從ajax取得PID
                    var ajaxPID = context.Request.Form["PID"];

                    // 取得貼文資料
                    PostInfoModel postInfo = PostManager.GetOnePostInfo(ConverStringToGuid(ajaxPID));

                    // 寫入Response
                    SendDataByJSON(context, postInfo);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                }
            }
            // 取得貼文的全部留言
            else if (actionName == "GetAllMsg")
            {
                try
                {
                    // 從ajax取得PID
                    var ajaxPID = context.Request.Form["PID"];

                    // 取得貼文的全部留言
                    List<MsgInfoModel> allMsg = PostManager.GetAllPostMsg(ConverStringToGuid(ajaxPID));

                    // 寫入Response
                    SendDataByJSON(context, allMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
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
                        SendDataByJSON(context, responseMsg);
                        return;
                    }

                    // Check email
                    if (AccountFunction.CheckEmailExist(accountInfo.Email))
                        responseMsg = "Email已被註冊";
                    else
                        // write account info into DB
                        responseMsg = AccountFunction.CreateAccount(accountInfo);

                    SendDataByJSON(context, responseMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                    SendDataByJSON(context, "警告! 發生預期外錯誤，請重新再試");
                }
            }
            // 建立貼文
            else if (actionName == "NewPost")
            {
                try
                {
                    // Get value from ajax
                    string title = context.Request.Form["Title"];
                    string body = context.Request.Form["Body"].Replace("\n", "<br>");
                    string strUID = CheckSession(context, "UID");
                    string responseMsg = string.Empty;

                    // Set img src
                    body = ShowImageAtPost(body);

                    // Check body and title string is no swear
                    string checkedTitle = DAL.tools.myTextCheck(title, DAL.tools.getSwear());
                    string checkedBody = DAL.tools.myTextCheck(body, DAL.tools.getSwear());

                    // set value to object and write into DB
                    Posting postInfo = new Posting()
                    {
                        PostID = Guid.NewGuid(),
                        UserID = ConverStringToGuid(strUID),
                        CreateDate = DateTime.Now,
                        Title = checkedTitle,
                        Body = checkedBody,
                        ismaincontent = false
                    };

                    // check UID is correct and user is exist
                    if (PostManager.CheckUserExist(ConverStringToGuid(strUID)))
                    {
                        // write into DB
                        responseMsg = PostManager.CreateNewPost(postInfo);
                    }

                    SendDataByJSON(context, responseMsg);
                }
                catch (Exception ex)
                {
                    SendDataByJSON(context, "發生預期外的錯誤，請重新輸入");
                    DAL.tools.summitError(ex);
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
                    string strUID = CheckSession(context, "UID");
                    string responseMsg = string.Empty;

                    // set value to object and write into DB
                    Message msgInfo = new Message()
                    {
                        MsgID = Guid.NewGuid(),
                        PostID = ConverStringToGuid(strPID),
                        UserID = ConverStringToGuid(strUID),
                        CreateDate = DateTime.Now,
                        Body = DAL.tools.myTextCheck(body, DAL.tools.getSwear())
                    };

                    // check UID and PID is correct and user is exist
                    if (PostManager.CheckUserExist(ConverStringToGuid(strUID)) && PostManager.CheckPostExist(ConverStringToGuid(strPID)))
                    {
                        // write into DB
                        responseMsg = PostManager.CreateNewMsg(msgInfo);
                    }
                    else
                    {
                        // Have error
                        responseMsg = "Exception Error";
                    }

                    SendDataByJSON(context, responseMsg);
                }
                catch (Exception ex)
                {
                    SendDataByJSON(context, "發生預期外的錯誤，請重新登入");
                    DAL.tools.summitError(ex);
                }
            }
            // 取得會員資料
            else if (actionName == "GetEditInfo")
            {
                try
                {
                    string strUID = CheckSession(context, "UID");

                    // get user infomation
                    EditInfoModel editInfo = AccountFunction.GetUserEditInfo(ConverStringToGuid(strUID));
                    SendDataByJSON(context, editInfo);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                }
            }
            // 更新會員資料
            else if (actionName == "UpdateInfo")
            {
                try
                {
                    string strUID = CheckSession(context, "UID");

                    EditInfoModel editSource = new EditInfoModel()
                    {
                        Name = context.Request.Form["Name"],
                        Email = context.Request.Form["Email"],
                        Birthday = context.Request.Form["Birthday"],
                        Account = context.Request.Form["Account"]
                    };

                    string resultMsg = AccountFunction.UpdateUserInfo(editSource, ConverStringToGuid(strUID));

                    // send to ajax
                    SendDataByJSON(context, resultMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                    SendDataByJSON(context, "警告! 發生預期外錯誤，請重新再試");
                }
            }
            // 更改會員密碼
            else if (actionName == "UpdatePwd")
            {
                try
                {
                    string strUID = CheckSession(context, "UID");
                    string oldPwd = context.Request.Form["OldPwd"];
                    string newPwd = context.Request.Form["NewPwd"];
                    string newPwdAgain = context.Request.Form["NewPwdAgain"];
                    string resultMsg = string.Empty;

                    // get password from DB
                    PwdInfoModel dbPwdInfo = AccountFunction.GetUserPwd(ConverStringToGuid(strUID));

                    // Check new password
                    if (string.Compare(newPwd, newPwdAgain, false) == 0)
                    {
                        // Check input password and DB password
                        if (AuthManager.AccountPasswordAuthentication(oldPwd, dbPwdInfo.Password))
                        {
                            // Update password
                            resultMsg = AccountFunction.UpdateUserPwd(ConverStringToGuid(strUID), dbPwdInfo.Account, newPwd);
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

                    // send to ajax
                    SendDataByJSON(context, resultMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                    SendDataByJSON(context, "警告! 發生預期外錯誤，請重新再試");
                }
            }
            // 獲取會員貼文
            else if (actionName == "GetUserPost")
            {
                try
                {
                    // get uid from session
                    string strUID = CheckSession(context, "UID");

                    // 取得貼文資料
                    List<PostInfoModel> userPostInfo = PostManager.GetAllUserPostInfo(ConverStringToGuid(strUID));

                    // 寫入Response
                    SendDataByJSON(context, userPostInfo);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                }
            }
            // 獲取會員留言
            else if (actionName == "GetUserMsg")
            {
                try
                {
                    // 從Session取得UID並轉型
                    string strUID = CheckSession(context, "UID");

                    // 取得留言資料
                    List<UserMsgInfo> userMsgInfo = PostManager.GetUserAllMsgInfo(ConverStringToGuid(strUID));

                    // 寫入Response
                    SendDataByJSON(context, userMsgInfo);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                }
            }
            // 會員自己刪除貼文
            else if (actionName == "UserDeletePost")
            {
                try
                {
                    string strUID = CheckSession(context, "UID");
                    string strPID = context.Request.Form["PID"];
                    string resultMsg = string.Empty;

                    // check guid
                    resultMsg = PostManager.UserDeletePost(ConverStringToGuid(strUID), ConverStringToGuid(strPID));

                    // send to ajax
                    SendDataByJSON(context, resultMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                    SendDataByJSON(context, "警告!發生預期外錯誤");
                }
            }
            // 會員自己刪除留言
            else if (actionName == "UserDeleteMsg")
            {
                try
                {
                    string strUID = CheckSession(context, "UID");
                    string strMID = context.Request.Form["MID"];
                    string resultMsg = string.Empty;

                    // check guid
                    resultMsg = PostManager.UserDeleteMsg(ConverStringToGuid(strUID), ConverStringToGuid(strMID));

                    // send to ajax
                    SendDataByJSON(context, resultMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                    SendDataByJSON(context, "警告!發生預期外錯誤");
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
                    if (!AccountFunction.CheckAccountExist(account))
                    {
                        resultMsg[0] = "此帳號不存在，請重新輸入";
                        SendDataByJSON(context, resultMsg);
                        return;
                    }

                    // check info is correct
                    Accounting userInfo = AuthManager.GetAccountInfo(account);
                    int checkEmail = string.Compare(email, userInfo.Email, false);
                    int checkDate = string.Compare(birthday, userInfo.BirthDay.ToString("yyyy-MM-dd"), false);

                    if (checkEmail + checkDate != 0)
                    {
                        resultMsg[0] = "Email 或 生日日期不符，請重新輸入";
                        SendDataByJSON(context, resultMsg);
                        return;
                    }

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

                    // send to ajax
                    SendDataByJSON(context, resultMsg);
                }
                catch (Exception ex)
                {
                    DAL.tools.summitError(ex);
                    SendDataByJSON(context, "警告!發生預期外錯誤");
                }
            }
            // 會員登出
            else if (actionName == "Logout")
            {
                try
                {
                    Guid uid = ConverStringToGuid(context.Session["UID"].ToString());
                    DateTime loginDate = Convert.ToDateTime(context.Session["LoginDate"]);
                    string ip = Convert.ToString(context.Request.Form["IP"]);
                    AuthManager.RecordUserLogout(ip, uid, loginDate);
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

        /// <summary>傳送JSON資料</summary>
        /// <param name="context"></param>
        /// <param name="statusMsg"></param>
        private void SendDataByJSON(HttpContext context, object statusMsg)
        {
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(statusMsg);
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);            
        }


        /// <summary>從String轉型成Guid </summary>
        /// <param name="sourceGuid"></param>
        /// <returns></returns>
        private Guid ConverStringToGuid(string sourceGuid)
        {
            if(!Guid.TryParse(sourceGuid, out Guid outputGuid))
            {
                throw new Exception("Guid 轉型錯誤");
            }

            return outputGuid;
        }

        /// <summary>檢查Session是否有數值</summary>
        /// <param name="context"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        private string CheckSession(HttpContext context, string dataName)
        {
            if (context.Session[dataName] == null)
            {
                context.Response.End();
                return null;
            }
            else
                return context.Session[dataName].ToString();
        }

        /// <summary>貼文顯示圖片功能</summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private string ShowImageAtPost(string body)
        {
            Regex regex = new Regex(@"(?<first>imgsrc+).*(imgur|giphy|truth.bahamut).*(jpg|jpeg|png|webp|gif|JPG|PNG).(\k<first>)");
            if (regex.IsMatch(body))
            {
                body = body.Replace("/imgsrc:", "<img class=\"img-fluid\" src=\"");
                body = body.Replace(":imgsrc/", "\" />");

                return body;
            }
            else
            {
                return body;
            }
        }

    }
}