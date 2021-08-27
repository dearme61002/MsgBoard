using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using databaseORM;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

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
        public List<databaseORM.data.Accounting> GetEditMember()
        {
            #region 從資料庫取出EditAccounting紀錄
            using (databaseEF context = new databaseEF())
            {
                List<databaseORM.data.Accounting> cc = context.Accountings.ToList();
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
        [HttpPost]
        public Model.ApiResult DelMember([FromBody] string dataID)
        {

            #region 從資料庫刪除ErrorLog紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {
                    Accounting log = new Accounting() { ID = Convert.ToInt32(dataID) };
                    context.Accountings.Attach(log);
                    context.Accountings.Remove(log);
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

        [HttpPost]
        public Model.ApiResult editMember([FromBody] string data)
        {
           
            JObject myjsonData = JObject.Parse(data);
           string name = myjsonData["name"].ToString();
            string account = myjsonData["account"].ToString();
            string password = myjsonData["password"].ToString();
            string email = myjsonData["email"].ToString();
            string date = myjsonData["date"].ToString();
            string dataID = myjsonData["dataID"].ToString();

            Model.ApiResult apiResult = new Model.ApiResult();
            Regex rgxemail = new Regex(@"^([a-zA-Z0-9_\-?\.?]){3,}@([a-zA-Z]){3,}\.([a-zA-Z]){2,5}$");
            Regex rgxeaccount = new Regex(@"^[\w\S]+$");
            Regex replace = new Regex(@"^\S+$");
            Regex regxpassworld = new Regex(@"^\w+$");
            Regex regexdate = new Regex(@"^((19|20)?[0-9]{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))*$");
            Regex regexdata = new Regex(@"^\d{4}-\d{2}-\d{2}$");

            var dddate = regexdata.IsMatch(date);

            if (!regexdata.IsMatch(date))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }
            
            var cc = rgxemail.IsMatch(email);
            if (!rgxemail.IsMatch(email))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }
            var tt = rgxeaccount.IsMatch(account);
            if (!rgxeaccount.IsMatch(account))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }
 var ba = replace.IsMatch(name);
            if (!replace.IsMatch(name))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }
var gg = regexdate.IsMatch(date);
            if (!regexdate.IsMatch(date))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;

            }
            
            var ttc = regxpassworld.IsMatch(password);


            if (!regexdate.IsMatch(date))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }


            //if (rgxemail.IsMatch(email)  & rgxeaccount.IsMatch(account) &replace.IsMatch(name)&regexdate.IsMatch(date)&regxpassworld.IsMatch(password))
            //{
            //    apiResult.state = 404;
            //    apiResult.msg = "資料格式錯誤";
            //    return apiResult;
              
            //}



            #region 從資料庫紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                
                try
                {
                    int id = Convert.ToInt32(dataID);
                    Accounting accounting = context.Accountings.Where(x => x.ID == id).FirstOrDefault();
                    accounting.Name = name;
                    accounting.Account = account;
                    accounting.Password = password;
                    accounting.Email = email;
                    accounting.BirthDay = Convert.ToDateTime(date);
                    context.SaveChanges();
                    apiResult.state = 200;
                    apiResult.msg = "更新成功";
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