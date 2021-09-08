using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace MsgBoardWebApp.backsideweb
{
    public partial class badlanguage : System.Web.UI.Page
    {
        public string dataID;
        public string enCodedataID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                double exp = (DateTime.UtcNow.AddHours(1) - new DateTime(1970, 1, 1)).TotalSeconds;//設定過期時間一小時
                if (Session["UID"] != null)
                {
                    dataID = Session["UID"].ToString();
                    token token = new token();
                    Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                    keyValuePairs.Add("UserID", dataID);
                    keyValuePairs.Add("exp", exp);
                    if (!token.isAdmin(dataID))
                    {
                        Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect(@"~/Page02Login.aspx");
                    }
                    enCodedataID = token.encode(keyValuePairs);
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