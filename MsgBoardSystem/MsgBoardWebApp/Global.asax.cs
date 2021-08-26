using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI;
using MsgBoardWebApp.filter;
using DAL;
using System.Data.SqlClient;

namespace MsgBoardWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // 註冊API路由
            RouteTable.Routes.MapHttpRoute(
                name: "DefautApi",
                routeTemplate: "backsideweb/api/{controller}/{action}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
             );
            // 註冊filters
            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //獲得錯誤代碼
            string Message = "";
            Exception ex = Server.GetLastError();
            Message = "發生錯誤的網頁:{0}錯誤訊息:{1}堆疊內容:{2}";
            Message = String.Format(Message, Request.Path + Environment.NewLine, ex.GetBaseException().Message + Environment.NewLine, Environment.NewLine + ex.StackTrace);
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

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        //驗證方法
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {   
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            string path = request.Url.PathAndQuery;

            if (path.StartsWith("/Page06", StringComparison.InvariantCultureIgnoreCase)) //網址前面驗證
            {
                bool isAuth = HttpContext.Current.Request.IsAuthenticated;
                var user = HttpContext.Current.User;

                if (!isAuth || user == null)
                {
                    response.StatusCode = 403;
                    response.Redirect("Page02Login.aspx");//驗證不過調轉我要的頁面
                    response.End();
                    return;
                }

                var identity = HttpContext.Current.User.Identity as FormsIdentity;

                if (identity == null)
                {
                    response.StatusCode = 403;
                    response.Redirect("~/Page02Login.aspx");//驗證不過調轉我要的頁面
                    response.Write("Please Login");
                    response.End();
                    return;
                }
            }
        }
        
        // filters
        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new BacksideFilter());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}