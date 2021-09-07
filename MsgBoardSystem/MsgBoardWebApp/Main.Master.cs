using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuth;

namespace MsgBoardWebApp
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuth = HttpContext.Current.Request.IsAuthenticated;//要否讀取COOKie 有給true
            var user = HttpContext.Current.User;//判讀User

            if (isAuth && user != null)
            {
                loginFunc.Visible = true;
                var identity = HttpContext.Current.User.Identity as FormsIdentity; //判讀Identity
                if (identity != null)
                {
                    if(AuthManager.UserLevelAuthentication(identity.Ticket.UserData))
                        this.backSide.Visible = true;
                }
            }
        }
    }
}