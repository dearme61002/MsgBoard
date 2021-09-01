using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDBFunction
{
    public class AccountFunction
    {
        #region RegisterFunctions

        /// <summary> 檢查帳號是否已經存在 </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool CheckAccountExist(string account)
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

                    if (list.Count() != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                throw ex;
            }
        }

        /// <summary> 檢查帳號是否已經存在 </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool CheckEmailExist(string email)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Accountings
                         where item.Email == email
                         select item);

                    var list = query.ToList();

                    if (list.Count() != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                throw ex;
            }
        }

        /// <summary> 傳入會員資訊後，在DB新增會員 </summary>
        /// <param name="accounting"></param>
        /// <returns></returns>
        public static string CreateAccount(Accounting accounting)
        {
            try
            {
                // Encrypt password before write into data base
                accounting.Password = SystemAuth.PasswordAESCryptography.Encrypt(accounting.Password);

                using (databaseEF context = new databaseEF())
                {
                    context.Accountings.Add(accounting);
                    context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                return "Exception Error";
            }
        }
        #endregion

        #region EditUserInfoFunctions

        /// <summary> 從資料庫抓取使用者資料 </summary>
        /// <param name="uid"></param>
        /// <returns> List Accounting 資料 </returns>
        public static List<Accounting> GetUserInfo(Guid uid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == uid
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
        /// <param name="uid"></param>
        /// <returns> EditInfoModel 格式資料 </returns>
        public static List<EditInfoModel> GetEditInfo(Guid uid)
        {
            List<Accounting> sourceList = GetUserInfo(uid);

            // Check exist
            if (sourceList != null)
            {
                List<EditInfoModel> editSource =
                    sourceList.Select(obj => new EditInfoModel()
                    {
                        Name = obj.Name,
                        CreateDate = obj.CreateDate.ToString("yyyy-MM-dd"),
                        Account = obj.Account,
                        Level = (obj.Level == "Admin") ? "管理者" : "一般會員",
                        Email = obj.Email,
                        Birthday = obj.BirthDay.ToString("yyyy-MM-dd")
                    }).ToList();

                return editSource;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 更新使用者資料 </summary>
        /// <param name="editSource"></param>
        /// <param name="userID"></param>
        /// <returns> Success : 成功, Others string : 失敗的錯誤訊息 </returns>
        public static string UpdateUserInfo(EditInfoModel editSource, Guid userID)
        {
            string name = editSource.Name;
            string email = editSource.Email;
            string birthday = editSource.Birthday;
            string account = editSource.Account;

            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        $@"
                            UPDATE [dbo].[Accounting]
                            SET 
                                [Name] = '{name}',
                                [Email] = '{email}',
                                [BirthDay] = '{birthday}'
                            WHERE [Account] = '{account}' and [UserID] = '{userID}'
                        ";

                    context.Database.ExecuteSqlCommand(query);

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region ChangePasswordFunctions

        /// <summary> 取得使用者密碼 </summary>
        /// <param name="uid"></param>
        /// <returns> PwdInfoModel 格式資料 </returns>
        public static PwdInfoModel GetUserPwd(Guid uid)
        {
            List<Accounting> sourceList = GetUserInfo(uid);

            // Check exist
            if (sourceList != null)
            {
                List<PwdInfoModel> pwdSource =
                    sourceList.Select(obj => new PwdInfoModel()
                    {
                        Account = obj.Account,
                        Password = obj.Password
                    }).ToList();

                return pwdSource[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary> 更新使用者密碼 </summary>
        /// <param name="userID"></param>
        /// <param name="account"></param>
        /// <param name="newPwd"></param>
        /// <returns> Success : 成功, Others string : 失敗的錯誤訊息 </returns>
        public static string UpdateUserPwd(Guid userID, string account, string newPwd)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    // encrpt new password
                    newPwd = SystemAuth.PasswordAESCryptography.Encrypt(newPwd);

                    var query =
                        $@"
                            UPDATE [dbo].[Accounting]
                            SET [Password] = '{newPwd}'
                            WHERE [Account] = '{account}' and [UserID] = '{userID}'
                        ";

                    context.Database.ExecuteSqlCommand(query);

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region Forget Password Function

        /// <summary>產生亂數字串</summary>
        /// <param name="charCount">英文字碼數目</param>
        /// <param name="codeCount">英數字碼數目</param>
        /// <param name="extCount">延展長度數目</param>
        public static string CreateRandomCode(int charCount, int codeCount, int extCount, int seed = 0)
        {
            string allChar = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";

            Random rand = new Random((int)DateTime.Now.Ticks + seed);

            // 產生英文字碼
            for (int i = 0; i < charCount; i++)
                randomCode += allCharArray[rand.Next() % 52];

            // 產生英數字碼
            for (int i = 0; i < codeCount + rand.Next() % (extCount + 1); i++)
                randomCode += allCharArray[rand.Next() % 62];

            // return
            return randomCode;
        }
        #endregion
    }
}
