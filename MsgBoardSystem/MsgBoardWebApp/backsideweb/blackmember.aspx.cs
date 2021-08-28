using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MsgBoardWebApp.backsideweb
{
    public partial class blackmember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UID"] != null)
            {
            }
            else
            {
                Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddDays(-1);
                Response.Redirect(@"~/Page02Login.aspx");
            }

        }
    }
}