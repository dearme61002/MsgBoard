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
        protected void Page_Load(object sender, EventArgs e)
        {
         

            #region 從資料庫取出紀錄
            using (databaseEF context = new databaseEF())
            {
              
                memberCount = context.Accountings.Where(x => x.Level == "Member").Count();
                //memberCount = context.Infoes.Select(x => x.RegisteredPeople).Sum();
            }
            #endregion

         


        }
    }
}