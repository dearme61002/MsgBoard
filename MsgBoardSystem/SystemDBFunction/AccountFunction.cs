using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDBFunction
{
    public class AccountFunction
    {
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return "Excrption Error";
            }
        }
    }
}
