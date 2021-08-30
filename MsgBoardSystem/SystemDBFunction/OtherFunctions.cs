using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDBFunction
{
    public class OtherFunctions
    {
        /// <summary> 首頁載入時，紀錄一筆資料 </summary>
        public static void DefaultPageRecord()
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    Info PageLoad = new Info()
                    {
                        PeopleOnline = 1,
                        CreateDate = DateTime.Now
                    };

                    context.Infoes.Add(PageLoad);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
            }
        }
    }
}
