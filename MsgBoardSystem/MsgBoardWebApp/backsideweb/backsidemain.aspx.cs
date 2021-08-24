using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace MsgBoardWebApp.backsideweb
{ 
    public partial class backsidemain : System.Web.UI.Page
    {
        public string qq;
        public string ee;
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL.token token = new token();
           qq=   token.encode(new Dictionary<string, object>
            {
                {
                    "id",123
                }
            });
            ee = token.decode(qq);
                
                }
    }
}