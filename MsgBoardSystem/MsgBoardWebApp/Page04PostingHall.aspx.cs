using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MsgBoardWebApp
{
    public partial class Page04PostingHall : System.Web.UI.Page
    {
        //這裡的方法是登陸驗證要判斷isAuth user identity
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuth = HttpContext.Current.Request.IsAuthenticated;//要否讀取COOKie 有給true
            var user = HttpContext.Current.User;//判讀User

            if (isAuth && user != null)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity; //判讀Identity
                if (identity == null)
                {
                    this.ltlMsg.Text = "Not Login.";
                    return;
                }

                var userdata = identity.Ticket;
                 this.ltlMsg.Text = $"User : {user.Identity.Name}, ID : {identity.Ticket.UserData}";
            }
            else
            {
                this.ltlMsg.Text = "Not Login";
            }
        }
    }
}