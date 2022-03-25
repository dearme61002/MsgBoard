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
            this.guestFunc.Visible = true;
            bool isAuth = HttpContext.Current.Request.IsAuthenticated;//要否讀取COOKie 有給true
            var user = HttpContext.Current.User;//判讀User            

            if (isAuth && user != null)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity; //判讀Identity
                if (identity != null)
                {
                    // show login functions list 
                    this.LoginList.Visible = true;
                    this.loginFunc.Visible = true;
                    this.guestFunc.Visible = false;

                    // check is admin and show back side website
                    if(AuthManager.UserLevelAuthentication(identity.Ticket.UserData))
                        this.backSide.Visible = true;
                }
            }
        }
    }
}