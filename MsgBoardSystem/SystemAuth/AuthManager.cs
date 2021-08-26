using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemAuth
{
    public class AuthManager
    {
        /// <summary>
        /// 從資料庫抓取使用者資料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
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
                    return list;
                }
            }
            catch (Exception ex)
            {               
                //Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
