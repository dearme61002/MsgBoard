using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// <summary>
        /// 寫入錯誤訊息到DB內
        /// </summary>
        /// <param name="ex">要寫入的錯誤訊息</param>
        public static void summitError(Exception ex)
        {
            //獲得錯誤代碼
            string Message = "";

            Message = "{0}錯誤訊息:{1}堆疊內容:{2}";
            Message = String.Format(Message, Environment.NewLine, ex.GetBaseException().Message + Environment.NewLine, Environment.NewLine + ex.StackTrace);
            //以下要寫出錯誤代碼並導入置資料庫
            DAL.sqlhelper sqlhelper = new sqlhelper();
            string sql = @"insert into ErrorLog(Body) values (@Body)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Body",Message)
            };
            try
            {
                sqlhelper.executeNonQuerysql(sql, sqlParameters, false);
            }
            catch (Exception)
            {
                Console.WriteLine("LOG寫入問題");
            }
        }


    }


}

