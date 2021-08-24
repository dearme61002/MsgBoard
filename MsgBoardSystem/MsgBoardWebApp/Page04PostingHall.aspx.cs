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
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuth = HttpContext.Current.Request.IsAuthenticated;
            var user = HttpContext.Current.User;

            if (isAuth && user != null)
            {
                var identity = HttpContext.Current.User.Identity as FormsIdentity;
                if (identity == null)
                {
                    this.ltlMsg.Text = "Not Login.";
                    return;
                }

                var userdata = identity.Ticket;
                this.ltlMsg.Text = $"User : {user.Identity.Name}, ID : {identity.Ticket.UserData}";
                this.btnLogout.Visible = true;
            }
            else
            {
                this.ltlMsg.Text = "Not Login";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //FormsAuthentication.SignOut();
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now.AddHours(-5);
            Response.Cookies.Add(cookie);
            Response.Redirect(Request.RawUrl);
        }
    }
}