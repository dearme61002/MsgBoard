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
    
        protected void Page_Load(object sender, EventArgs e)
        {

          List<string> dd=  DAL.tools.getSwear();
            if (dd.Count > 5)
            {

            }
                
        }
    }
}