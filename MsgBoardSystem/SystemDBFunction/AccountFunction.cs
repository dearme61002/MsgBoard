using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDBFunction
{
    public class AccountFunction
    {
        public static string CreateAccount(Accounting accounting)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    accounting.CreateDate = DateTime.Now;
                    context.Accountings.Add(accounting);
                    context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Excrption Error";
            }
        }
    }
}
