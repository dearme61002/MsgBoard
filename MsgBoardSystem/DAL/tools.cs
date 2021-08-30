using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class tools
    {
        /// <summary>
        /// 從禁言資料庫(Swear)取出禁言字串
        /// </summary>
        /// <returns>
        /// 回傳List<string>類型的禁言字組
        /// </returns>
        public static List<string> getSwear()
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    List<string> getMySwears = new List<string>();

                    List<databaseORM.data.Swear> swears = context.Swears.ToList();

                    foreach (var item in swears)
                    {
                        getMySwears.Add(item.Body);
                    }
                    return getMySwears;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        ///  傳入要跟改的文章與 傳入禁言數組List類型的文字組
        /// </summary>
        /// <param name="msg">要被跟改的原文章</param>
        /// <param name="MySwears">要被替換的禁言數組List<string>類型的文字組</param>
        /// <returns>替換完成的新文章</returns>
        public static string myTextCheck(string msg, List<string> MySwears)
        {


            try
            {
                foreach (string item in MySwears)
                {
                    msg = msg.Replace(item, "*");
                }
                return msg;
            }
            catch (Exception)
            {

                throw;
            }



        }

    }


}

