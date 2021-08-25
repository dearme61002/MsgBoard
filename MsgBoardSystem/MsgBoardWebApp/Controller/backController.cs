using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using databaseORM;


namespace MsgBoardWebApp
{
    public class backController : ApiController
    {
        // GET api/<controller>[TypeFilter(type)]
      
        [HttpPost]
        public List<databaseORM.data.ErrorLog> GetErrorLogs()
        {
            #region 從資料庫取出ErrorLog紀錄
            using (databaseEF context = new databaseEF())
            {
                List<databaseORM.data.ErrorLog> cc =  context.ErrorLogs.ToList();
                return cc;
            }
            #endregion

        }
        [HttpPost]
        public Model.ApiResult DelErrorLogs([FromBody] string dataID)
        {
           
            #region 從資料庫刪除ErrorLog紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {           
                ErrorLog log = new ErrorLog() { ID = Convert.ToInt32(dataID)};
                context.ErrorLogs.Attach(log);
                context.ErrorLogs.Remove(log);
                context.SaveChanges();
                    apiResult.state = 200;
                    apiResult.msg = "刪除成功";
                    return apiResult;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    apiResult.state = 404;
                    apiResult.msg = "刪除失敗";
                    return apiResult;

                }
                
            }
            #endregion

        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}