using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace MsgBoardWebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            ////獲得錯誤代碼
            //string Message = "";
            //Exception ex = Server.GetLastError();
            //Message = "發生錯誤的網頁:{0}錯誤訊息:{1}堆疊內容:{2}";
            //Message = String.Format(Message, Request.Path + Environment.NewLine, ex.GetBaseException().Message + Environment.NewLine, Environment.NewLine + ex.StackTrace);
            ////以下要寫出錯誤代碼並導入置資料庫

        }
    }
}