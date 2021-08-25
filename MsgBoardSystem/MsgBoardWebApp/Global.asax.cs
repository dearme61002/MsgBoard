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

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {   
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            string path = request.Url.PathAndQuery;

            if (path.StartsWith("/PageMember", StringComparison.InvariantCultureIgnoreCase))
            {
                bool isAuth = HttpContext.Current.Request.IsAuthenticated;
                var user = HttpContext.Current.User;

                if (!isAuth || user == null)
                {
                    response.StatusCode = 403;
                    response.Redirect("~/Page02Login.aspx");
                    response.Write("Please Login");
                    response.End();
                    return;
                }

                var identity = HttpContext.Current.User.Identity as FormsIdentity;

                if (identity == null)
                {
                    response.StatusCode = 403;
                    response.Redirect("~/Page02Login.aspx");
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

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}