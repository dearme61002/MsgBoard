using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace MsgBoardWebApp.backsideweb
{
    public partial class editarticles : System.Web.UI.Page
    {
        public string dataID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UID"] != null)
                {
                    dataID = Session["UID"].ToString();
                    token token = new token();
                    if (!token.isAdmin(dataID))
                    {
                        Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect(@"~/Page02Login.aspx");
                    }
                }
                else
                {
                    Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddDays(-1);
                    Response.Redirect(@"~/Page02Login.aspx");
                }

            }
            catch (Exception ex)
            {
                tools.summitError(ex);
            }
        }
    }
}