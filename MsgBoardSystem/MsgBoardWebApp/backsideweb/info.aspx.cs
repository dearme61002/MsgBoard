using DAL;
using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MsgBoardWebApp.backsideweb
{
    public partial class info : System.Web.UI.Page
    {
        public int memberCount = 0;
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

            try
            {
                #region 從資料庫取出紀錄
                using (databaseEF context = new databaseEF())
                {

                    memberCount = context.Accountings.Where(x => x.Level == "Member").Count();
                    //memberCount = context.Infoes.Select(x => x.RegisteredPeople).Sum();
                }
                #endregion
            }
            catch (Exception ex)
            {

                tools.summitError(ex);
            }






        }
    }
}