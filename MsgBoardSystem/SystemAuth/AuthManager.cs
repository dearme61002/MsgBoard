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

namespace WebAuth
{
    public class AuthManager
    {
        #region User Authentication Functions

        /// <summary> 從資料庫抓取使用者資料 </summary>
        /// <param name="account"></param>
        /// <returns>List Accounting 資料</returns>
        public static List<Accounting> GetAccountInfo(string account)
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
                        return list;
                    else
                        return null;
                }
            }
            catch (Exception)
            {               
                return null;
            }
        }

        /// <summary> 取得使用者資料 </summary>
        /// <param name="account"></param>
        /// <returns>UserInfoModel 資料</returns>
        public static UserInfoModel GetInfo(string account)
        {
            DateTime defaultDateTime = new DateTime(1911, 10, 10);
            List<Accounting> sourceList = GetAccountInfo(account);

            // Check account exist
            if (sourceList != null)
            {
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

                return userSource[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary> 建立驗證Cookie </summary>
        /// <param name="userInfo"></param>
        public static void LoginAuthentication(UserInfoModel userInfo)
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
        #endregion
    }
}
