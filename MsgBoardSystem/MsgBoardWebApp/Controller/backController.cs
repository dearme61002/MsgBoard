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
using DAL;
using System.Data.SqlClient;
using System.Data;

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
        public info Getinfo([FromBody] string data)
        {
            JObject myjsonData = JObject.Parse(data);
            string date = myjsonData["date"].ToString();
            string date2 = myjsonData["date2"].ToString();
            DateTime dateTime1 = Convert.ToDateTime(date);
            DateTime dateTime2 = Convert.ToDateTime(date2);

            Regex regexdate = new Regex(@"^((19|20)?[0-9]{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))*$");
            info info = new info();

            if (dateTime2 < dateTime1)
            {
                info.msg = "日期資料大小錯誤";
                return info;
            }
            long days = (dateTime2 - dateTime1).Days + 1;

            if (!regexdate.IsMatch(date))
            {

                info.msg = "資料格式錯誤";
                return info;
            }
            if (!regexdate.IsMatch(date2))
            {

                info.msg = "資料格式錯誤";
                return info;
            }

            try
            {
                //string ARP = "SELECT sum(RegisteredPeople) as AllRegisteredPeople FROM Info WHERE CreateDate BETWEEN @timeOne AND @timeTwo";
                //SqlParameter[] ARPsqls = new SqlParameter[]
                //{
                //new SqlParameter("@timeOne",date),
                //new SqlParameter("@timeTwo",date2)
                //};
                //info.AllRegisteredPeople = Convert.ToInt32(sqlhelper.executeScalarsql(ARP, ARPsqls, false));

                //DateTime sd = Convert.ToDateTime("2010/05/07");
                //4:      DateTime ed = Convert.ToDateTime("2010/05/10").AddDays(1);
                //5:      TESTDBEntities _db = new TESTDBEntities();
                //6:      var showToView = _db.TrackStatistic.Where(
                //7:          m => m.ClickDate >= sd && m.ClickDate <= ed);
                //8:      return View(showToView);
                long AllRegistered = 0;
                using (databaseEF context = new databaseEF())
                {
                    AllRegistered = context.Accountings.Where(x => x.CreateDate >= dateTime1 && x.CreateDate <= dateTime2 &&x.Level== "Member").Count();
                }
               



                string APO = "SELECT sum(PeopleOnline) as AllPeopleOnline FROM Info WHERE CreateDate BETWEEN @timeOne AND @timeTwo";
                SqlParameter[] APOsqls = new SqlParameter[]
                {
                new SqlParameter("@timeOne",date),
                new SqlParameter("@timeTwo",date2)
                };
                info.AllPeopleOnline = Convert.ToInt32(sqlhelper.executeScalarsql(APO, APOsqls, false));
                long a = info.AllPeopleOnline;

                info.AllRegisteredPeople = AllRegistered;
                info.avgPeopleOnline = Math.Round((decimal)a / days,3);
                info.avgRegisteredPeople = Math.Round((decimal)AllRegistered / days,3);
                info.msg = "獲取資料成功";
                return info;
            }
            catch (Exception)
            {

                info.msg = "沒有資料";
                return info;
            }


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
                foreach (var acc in cc)
                {
                    acc.Password = SystemAuth.PasswordAESCryptography.Decrypt(acc.Password);
                }
                return cc;
            }
            #endregion

        }

        [HttpPost]
        public List<EditArticles> GetEditArticles()
        {
            //#region 從資料庫取出Articles
            //using (databaseEF context = new databaseEF())
            //{
                //var cc = from message in context.Messages
                //         join account in context.Accountings on message.UserID equals account.UserID into ps
                //         from account in ps.DefaultIfEmpty()
                //         join posting in context.Postings on message.PostID equals posting.PostID into pse
                //         from posting in pse.DefaultIfEmpty()
                //         orderby message.CreateDate descending
                //         select new EditArticles
                //         {
                //             Name = account.Name,
                //             CreateDate = message.CreateDate,
                //             Account = account.Account,
                //             UserID = message.UserID,
                //             Title = posting.Title,
                //             Body = message.Body,
                //             ID = message.ID,

                //         };

    

              
                string sql = "select Name,Message.CreateDate,Account,Posting.UserID,Title,Message.Body,Message.ID FROM Message left JOIN Accounting ON Accounting.UserID = Message.UserID left join Posting on Message.PostID = Message.PostID";
                SqlDataReader sqlDataReader = sqlhelper.executeReadesql(sql);
                
                List<EditArticles> EditArticlesList = new List<EditArticles>();
                while (sqlDataReader.Read())
                {
                EditArticles editArticles = new EditArticles();

                    editArticles.Name = sqlDataReader["Name"].ToString();
                    editArticles.CreateDate = Convert.ToDateTime(sqlDataReader["CreateDate"]);
                    editArticles.Account = sqlDataReader["Account"].ToString();
                    editArticles.UserID =  Guid.Parse(sqlDataReader["UserID"].ToString());
                    editArticles.Title = sqlDataReader["Title"].ToString();
                    editArticles.Body = sqlDataReader["Body"].ToString();
                    editArticles.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    EditArticlesList.Add(editArticles);

                }

                return EditArticlesList;
            //}
            //#endregion

        }
        [HttpPost]
        public List<Model.GetIndexmy> GetIndex()
        {
            
          

                //List<databaseORM.data.Posting> cc = context.Postings.OrderByDescending(x => x.CreateDate).ToList();

                string sql = "SELECT Posting.CreateDate,Title,Body,ismaincontent,Posting.ID FROM Posting left JOIN Accounting ON Accounting.UserID = Posting.UserID where Accounting.Level != 'Admin'";
                SqlDataReader sqlDataReader = sqlhelper.executeReadesql(sql);
               
                List<GetIndexmy> getIndexmies = new List<GetIndexmy>();
            while (sqlDataReader.Read())
            {
                GetIndexmy getIndexmy = new GetIndexmy();
                getIndexmy.CreateDate = Convert.ToDateTime(sqlDataReader["CreateDate"]);
                getIndexmy.Title = sqlDataReader["Title"].ToString();
                getIndexmy.Body = sqlDataReader["Body"].ToString();
                getIndexmy.ismaincontent = Convert.ToBoolean(sqlDataReader["ismaincontent"]);
                getIndexmy.ID = sqlDataReader["ID"].ToString();
                getIndexmies.Add(getIndexmy);
            }

                return getIndexmies;
           

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
        public Model.ApiResult DelIndex([FromBody] string dataID)
        {

            #region 從資料庫刪除ErrorLog紀錄透過dataID
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
                    string today = DateTime.Now.ToString("yyyy-MM-dd");
                    string timeNow = DateTime.Now.ToShortTimeString().ToString();
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
            Regex regxpassworld = new Regex(@"^\w{6,15}$");
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
                    accounting.Password = SystemAuth.PasswordAESCryptography.Encrypt(password);
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
        public Model.ApiResult setIndex([FromBody] string dataID)
        {

            Model.ApiResult apiResult = new Model.ApiResult();
            string sql = "update  Posting set ismaincontent=1 where ID =@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@id", dataID)
            };

            try
            {
                int i = sqlhelper.executeNonQuerysql(sql, sqlParameters, false);
                apiResult.state = 200;
                apiResult.msg = "設定成功";

                return apiResult;
            }
            catch (Exception)
            {
                apiResult.state = 404;
                apiResult.msg = "設定失敗";

                return apiResult;
            }


        }

        [HttpPost]
        public Model.ApiResult CancelIndex([FromBody] string dataID)
        {

            Model.ApiResult apiResult = new Model.ApiResult();
            string sql = "update  Posting set ismaincontent=0 where ID =@id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@id", dataID)
            };

            try
            {
                int i = sqlhelper.executeNonQuerysql(sql, sqlParameters, false);
                apiResult.state = 200;
                apiResult.msg = "設定成功";

                return apiResult;
            }
            catch (Exception)
            {
                apiResult.state = 404;
                apiResult.msg = "設定失敗";

                return apiResult;
            }


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
            Regex regxpassworld = new Regex(@"^\w{6,15}$");
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

                    var emailquery = context.Accountings.Where(x => x.Email == email);
                    var eee = emailquery.Count();
                    if (emailquery.Count() > 0)
                    {
                        apiResult.state = 404;
                        apiResult.msg = "更新失敗,已存在E-mail";
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
                    accounting.Password = SystemAuth.PasswordAESCryptography.Encrypt(password);
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