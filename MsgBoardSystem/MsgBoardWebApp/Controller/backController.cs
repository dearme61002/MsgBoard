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
using Model;

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
                List<databaseORM.data.ErrorLog> cc = context.ErrorLogs.ToList();
                return cc;
            }
            #endregion

        }

        [HttpPost]
        public List<databaseORM.data.Swear> GetSwear()
        {
            #region 從資料庫取出ErrorLog紀錄
            using (databaseEF context = new databaseEF())
            {
                List<databaseORM.data.Swear> cc = context.Swears.ToList();
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
                List<databaseORM.data.Accounting> cc = context.Accountings.Where(x => x.Level == "Member").ToList();
                return cc;
            }
            #endregion

        }

        [HttpPost]
        public List<Model.EditArticles> GetEditArticles()
        {
            #region 從資料庫取出Articles
            using (databaseEF context = new databaseEF())
            {
                var cc = from message in context.Messages
                         join account in context.Accountings on message.UserID equals account.UserID
                         join posting in context.Postings on message.PostID equals posting.PostID
                         orderby message.CreateDate descending
                         select new EditArticles
                         {
                             Name = account.Name,
                             CreateDate = message.CreateDate,
                             Account = account.Account,
                             UserID = message.UserID,
                             Title = posting.Title,
                             Body = message.Body,
                             ID = message.ID,

                         };

                return cc.ToList();
            }
            #endregion

        }



        [HttpPost]
        public List<databaseORM.data.Posting> Getbord([FromBody] string data)
        {
            JObject myjsonData = JObject.Parse(data);
            string mydataID = myjsonData["dataID"].ToString();
            #region 從資料庫取出EditAccounting紀錄
            using (databaseEF context = new databaseEF())
            {
                Guid guiduserID = Guid.Parse(mydataID);
                List<databaseORM.data.Posting> cc = context.Postings.Where(x => x.UserID == guiduserID).ToList();
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
                    ErrorLog log = new ErrorLog() { ID = Convert.ToInt32(dataID) };
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
        public Model.ApiResult DelSwear([FromBody] string dataID)
        {

            #region 從資料庫刪除ErrorLog紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {
                    
                    Swear swear = new Swear() { ID = Convert.ToInt32(dataID) };
                    context.Swears.Attach(swear);
                    context.Swears.Remove(swear);
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
        public Model.ApiResult DelPosting([FromBody] string dataID)
        {

            #region 從資料庫刪除Posting紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {

                    Posting posting = new Posting() { ID = Convert.ToInt32(dataID) };
                    context.Postings.Attach(posting);
                    context.Postings.Remove(posting);
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

        public Model.ApiResult DelMessage([FromBody] string dataID)
        {

            #region 從資料庫刪除Posting紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {
                    Message message = new Message() { ID = Convert.ToInt32(dataID) };
                    context.Messages.Attach(message);
                    context.Messages.Remove(message);
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

        public Model.ApiResult DelEditMessage([FromBody] string dataID)
        {

            #region 從資料庫刪除Posting紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {
                    int id = Convert.ToInt32(dataID);
                   string today=  DateTime.Now.ToString("yyyy-MM-dd");
                     string timeNow=  DateTime.Now.ToShortTimeString().ToString();
                    Message message = context.Messages.Where(x => x.ID == id).FirstOrDefault();
                    message.Body = "已被管理者刪除於日期:" + today + "時間" + timeNow + "刪除";
                    
                    context.SaveChanges();
                    apiResult.state = 200;
                    apiResult.msg = "刪除內文成功";
                    return apiResult;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    apiResult.state = 404;
                    apiResult.msg = "刪除內文成功失敗";
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
        public Model.ApiResult CancelBucket([FromBody] string dataID)
        {

            //int id = Convert.ToInt32(dataID);
            //Accounting accounting = context.Accountings.Where(x => x.ID == id).FirstOrDefault();
            //accounting.Name = name;
            //accounting.Account = account;
            //accounting.Password = password;
            //accounting.Email = email;
            //accounting.BirthDay = Convert.ToDateTime(date);
            //context.SaveChanges();
            //apiResult.state = 200;
            //apiResult.msg = "更新成功";
            //return apiResult;



            #region 從資料庫刪除ErrorLog紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {
                Model.ApiResult apiResult = new Model.ApiResult();
                try
                {
                    int id = Convert.ToInt32(dataID);
                    Accounting accounting = context.Accountings.Where(x => x.ID == id).FirstOrDefault();
                    accounting.Bucket = null;
                    context.SaveChanges();
                    apiResult.state = 200;
                    apiResult.msg = "取消成功";
                    return apiResult;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    apiResult.state = 404;
                    apiResult.msg = "取消失敗";
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

            //檢查帳號是否重複
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query = context.Accountings.Where(x => x.Account == account);
                    var ddd = query.Count();
                    if (query.Count() > 0)
                    {
                        apiResult.state = 404;
                        apiResult.msg = "更新失敗,已存在帳號";
                        return apiResult;
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                apiResult.state = 404;
                apiResult.msg = "更新失敗";
                return apiResult;
            }
            //檢查帳號是否重複






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



        [HttpPost]
        public Model.ApiResult editPosting([FromBody] string data)
        {

            JObject myjsonData = JObject.Parse(data);
            string Title = myjsonData["Title"].ToString();
            string Textarea = myjsonData["Textarea"].ToString();
            string dataID = myjsonData["dataID"].ToString();

            Model.ApiResult apiResult = new Model.ApiResult();

            Regex replace = new Regex(@"^\S+$");


            if (!replace.IsMatch(Title))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }

            #region 從資料庫紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {

                try
                {
                    int id = Convert.ToInt32(dataID);
                    Posting posting = context.Postings.Where(x => x.ID == id).FirstOrDefault();
                    posting.Title = Title;
                    posting.Body = Textarea;
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




        [HttpPost]
        public Model.ApiResult editBucket([FromBody] string data)
        {

            JObject myjsonData = JObject.Parse(data);

            string MybucketDate = myjsonData["MybucketDate"].ToString();
            string dataID = myjsonData["dataID"].ToString();

            Model.ApiResult apiResult = new Model.ApiResult();

            Regex regexdata = new Regex(@"^\d{4}-\d{2}-\d{2}$");



            if (!regexdata.IsMatch(MybucketDate))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }


            #region 從資料庫紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {

                try
                {
                    int id = Convert.ToInt32(dataID);
                    Accounting accounting = context.Accountings.Where(x => x.ID == id).FirstOrDefault();

                    accounting.Bucket = Convert.ToDateTime(MybucketDate);
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


        [HttpPost]
        public Model.ApiResult editmydata([FromBody] string data)
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



            if (!regexdata.IsMatch(date))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }


            if (!rgxemail.IsMatch(email))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }

            if (!rgxeaccount.IsMatch(account))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }

            if (!replace.IsMatch(name))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;
            }

            if (!regexdate.IsMatch(date))
            {
                apiResult.state = 404;
                apiResult.msg = "資料格式錯誤";
                return apiResult;

            }




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

            //檢查帳號是否重複
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query = context.Accountings.Where(x => x.Account == account);
                    var ddd = query.Count();
                    if (query.Count() > 0)
                    {
                        apiResult.state = 404;
                        apiResult.msg = "更新失敗,已存在帳號";
                        return apiResult;
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                apiResult.state = 404;
                apiResult.msg = "更新失敗";
                return apiResult;
            }
            //檢查帳號是否重複




            #region 從資料庫紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {

                try
                {
                    // string id =    dataID;

                    Guid id = Guid.Parse(dataID);
                    Accounting accounting = context.Accountings.Where(x => x.UserID == id).FirstOrDefault();
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
                    apiResult.msg = "更新失敗";
                    return apiResult;

                }

            }
            #endregion

        }


        [HttpPost]
        public Model.ApiResult addBoard([FromBody] string data)
        {

            JObject myjsonData = JObject.Parse(data);
            string Title = myjsonData["Title"].ToString();
            string Textarea = myjsonData["Textarea"].ToString();
            string userID = myjsonData["dataID"].ToString();

            Model.ApiResult apiResult = new Model.ApiResult();

            Regex replace = new Regex(@"^\S+$");

            if (!replace.IsMatch(Title))
            {
                apiResult.state = 404;
                apiResult.msg = "標題資料格式錯誤";
                return apiResult;
            }

            #region 從資料庫紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {

                try
                {
                    Guid guidUserId = Guid.Parse(userID);
                    Posting posting = new Posting();
                    posting.Title = Title;
                    posting.Body = Textarea;
                    posting.UserID = guidUserId;

                    context.Postings.Add(posting);

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

        [HttpPost]
        public Model.ApiResult addswearing([FromBody] string data)
        {

            JObject myjsonData = JObject.Parse(data);
            string addData = myjsonData["addData"].ToString();
       

            Model.ApiResult apiResult = new Model.ApiResult();

            Regex replace = new Regex(@"^\S+$");

            if (!replace.IsMatch(addData))
            {
                apiResult.state = 404;
                apiResult.msg = "輸入框為空無法增加資料";
                return apiResult;
            }

            #region 從資料庫紀錄透過dataID
            using (databaseEF context = new databaseEF())
            {

                try
                {

                  
                    Swear swear = new Swear();
                    swear.Body = addData;

                    context.Swears.Add(swear);

                    context.SaveChanges();
                    apiResult.state = 200;
                    apiResult.msg = "增加成功";
                    return apiResult;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    apiResult.state = 404;
                    apiResult.msg = "增加失敗";
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