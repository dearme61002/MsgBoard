using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using SystemAuth;

namespace WebAuth
{
    public class AuthManager
    {
        #region User Authentication Functions

        /// <summary> 從資料庫抓取使用者資料 </summary>
        /// <param name="account"></param>
        /// <returns>List Accounting 資料</returns>
        public static Accounting GetAccountInfo(string account)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Accountings
                         where item.Account == account
                         select item);

                    var list = query.ToList();

                    if (list.Count == 1)
                        return list[0];
                    else
                        return null;
                }
            }
            catch (Exception)
            {               
                return null;
            }
        }

        /// <summary>比對輸入密碼是否相同</summary>
        /// <param name="inputPwd">比對的密碼</param>
        /// <param name="dbPwd">資料庫的密碼</param>
        /// <returns>ture: 相同, false: 不相同</returns>
        public static bool AccountPasswordAuthentication(string inputPwd, string dbPwd)
        {
            try
            {
                // 轉成加密類型
                string enInputPwd = PasswordAESCryptography.Encrypt(inputPwd);

                if (string.Compare(enInputPwd, dbPwd, false) == 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                DAL.tools.summitError(ex);
                return false;
            }
        }

        /// <summary> 建立驗證Cookie </summary>
        /// <param name="userInfo"></param>
        public static void LoginAuthentication(Accounting userInfo)
        {
            string userID = userInfo.UserID.ToString();
            string[] roles = { userInfo.Level };
            bool isPersistance = false;

            FormsAuthentication.SetAuthCookie(userInfo.Account, isPersistance);

            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(
                    1,
                    userInfo.Account,
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    isPersistance,
                    userID
                );

            FormsIdentity identity = new FormsIdentity(ticket);
            HttpCookie cookie =
                new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket)
                );

            // Set false for page read .ASPXAUTH cookie
            cookie.HttpOnly = false;

            // Current.User roles has bug, wait for fix
            GenericPrincipal gp = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = gp; 
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary> 檢查是否為管理者 </summary>
        /// <param name="uid"></param>
        /// <returns>True, False </returns>
        public static bool UserLevelAuthentication(string uid)
        {
            try
            {
                if(!Guid.TryParse(uid, out Guid userID))
                    return false;

                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID
                         select item.Level);

                    if (query.ToList()[0] == "Admin")
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>紀錄登入IP與時間</summary>
        /// <param name="ip"></param>
        public static void RecordUserLogin(string ip, Guid uid, DateTime loginDate)
        {
            try
            {
                UserLogin userLogin = new UserLogin()
                {
                    UserID = uid,
                    LoginDate = loginDate,
                    LogoutDate = new DateTime(1911, 10, 10),
                    IP = ip
                };

                using (databaseEF context = new databaseEF())
                {
                    context.UserLogins.Add(userLogin);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                throw;
            }
        }

        /// <summary>紀錄IP的登出與時間</summary>
        /// <param name="ip"></param>
        public static void RecordUserLogout(string ip, Guid uid, DateTime loginDate)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        $@"
                            UPDATE [dbo].[UserLogin]
                            SET [LogoutDate] = '{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")}'
                            WHERE [IP] = '{ip}' and [UserID] = '{uid}' and [LoginDate] BETWEEN '{loginDate.ToString("yyyy/MM/dd HH:mm")}' AND '{loginDate.AddMinutes(1).ToString("yyyy/MM/dd HH:mm")}'
                        ";

                    context.Database.ExecuteSqlCommand(query);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                throw;
            }
        }
        #endregion
    }
}
