using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databaseORM;
using databaseORM.data;

namespace DAL
{
    class LogInDatabase
    {
        public void Logwrite()
        {
            //string Message = "";
           
            //Message = "發生錯誤的網頁:{0}錯誤訊息:{1}堆疊內容:{2}";
            //Message = String.Format(Message, Request.Path + Environment.NewLine, ex.GetBaseException().Message + Environment.NewLine, Environment.NewLine + ex.StackTrace);
            ////以下要寫出錯誤代碼並導入置資料庫
            //try
            //{
            //    using (databaseEF context = new databaseEF())
            //    {
            //        databaseORM.data.ErrorLog errorLog = new ErrorLog()
            //        {
            //            Body = Message
            //        };
            //        context.ErrorLogs.Add(errorLog);
            //     int result= context.SaveChanges();
            //        if (result < 0)
            //        {
            //            Console.WriteLine("寫入LOG出異常");
            //        }

            //    }
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("class Global出異常");
                
            //}
        }
    }
}
