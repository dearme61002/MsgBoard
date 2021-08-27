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
        public static string CheckAccountExist(string account)
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

                    if(list.Count() != 0)
                    {
                        return "帳號已存在!";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                return "Check account exception error";
            }
        }

        /// <summary> 檢查帳號是否已經存在 </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string CheckEmailExist(string email)
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
                    {
                        return "Email已存在!";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                return "Check email exception error";
            }
        }

        /// <summary> 傳入會員資訊後，在DB新增會員 </summary>
        /// <param name="accounting"></param>
        /// <returns></returns>
        public static string CreateAccount(Accounting accounting)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    context.Accountings.Add(accounting);
                    context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Excrption Error";
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
                        Password = obj.Password,
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

    }
}
