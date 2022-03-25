using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   
    public class writefile
    {
        #region write log
        /// <summary>
        /// 輸入字串內容並導出到指定的url並加入時間訊息
        /// </summary>
        /// <param name="msg">字串內容</param>
        /// <param name="url">指定的url</param>
        public static void writelog(string msg,string url)
        {
           
               FileStream fs = new FileStream(url, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString()+":"+ msg);
            sw.Close();
            fs.Close();
        }
        #endregion
    }
}
