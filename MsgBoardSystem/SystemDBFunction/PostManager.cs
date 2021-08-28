using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDBFunction
{
    public class PostManager
    {
        #region Posting Hall and Post Message Page Functions

        /// <summary> 從資料庫取得所有DB </summary>
        /// <returns></returns>
        public static List<Posting> GetAllPostingFromDB()
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Postings
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception)
            {
                throw null;
            }
        }

        /// <summary> 從資料庫取得所有DB </summary>
        /// <returns></returns>
        public static List<Message> GetAllMsgFromDB(Guid pid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Messages
                         where item.PostID == pid
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception)
            {
                throw null;
            }
        }

        /// <summary> 從資料庫取得特定Post資料 </summary>
        /// <returns></returns>
        public static List<Posting> GetOnePostInfoFromDB(Guid pid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Postings
                         where item.PostID == pid
                         select item);

                    var list = query.ToList();

                    if (list.Count != 0)
                        return list;
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                throw null;
            }
        }

        /// <summary> 從UserID尋找使用者名稱: Name </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static List<Accounting> GetUserName(Guid uid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == uid
                         select item);

                    var list = query.ToList();

                    if (list.Count != 0)
                        return list;
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 全部貼文資料轉換Model後送回Handler </summary>
        /// <returns></returns>
        public static List<PostInfoModel> GetAllPostInfo()
        {
            List<Posting> sourceList = GetAllPostingFromDB();

            if (sourceList != null)
            {
                List<PostInfoModel> postSource =
                    sourceList.Select(obj => new PostInfoModel()
                    {
                        PostID = obj.PostID,
                        UserID = obj.UserID,
                        CreateDate = obj.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"),
                        Title = obj.Title,
                        Body = obj.Body
                    }).ToList();

                // 用UID比對查詢，並寫入User Name
                foreach (var item in postSource)
                {
                    List<Accounting> posterName = GetUserName(item.UserID);
                    item.Name = posterName[0].Name;
                }

                return postSource;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Create Post Functions

        /// <summary> 建立貼文寫入DB </summary>
        /// <param name="postInfo"></param>
        /// <returns></returns>
        public static string CreateNewPost(Posting postInfo)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    context.Postings.Add(postInfo);
                    context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Create Exception Error";
            }
        }

        /// <summary> 取得貼文資訊 </summary>
        /// <returns></returns>
        public static List<PostInfoModel> GetOnePostInfo(Guid pid)
        {
            // get info by post guid
            List<Posting> sourceList = GetOnePostInfoFromDB(pid);

            if (sourceList != null)
            {
                // write into model
                List<PostInfoModel> postInfo =
                    sourceList.Select(obj => new PostInfoModel()
                    {
                        CreateDate = obj.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"),
                        Title = obj.Title,
                        Body = obj.Body
                    }).ToList();

                return postInfo;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 全部貼文資料轉換Model後送回Handler </summary>
        /// <returns></returns>
        public static List<MsgInfoModel> GetAllPostMsg(Guid pid)
        {
            // get all msg by post id
            List<Message> sourceList = GetAllMsgFromDB(pid);

            if (sourceList != null)
            {
                List<MsgInfoModel> MsgSource =
                    sourceList.Select(obj => new MsgInfoModel()
                    {
                        UserID = obj.UserID,
                        CreateDate = obj.CreateDate.ToString("yyyy-MM-dd hh:mm:ss"),
                        Body = obj.Body
                    }).ToList();

                // 用UID比對查詢，並寫入User Name
                foreach (var obj in MsgSource)
                {
                    List<Accounting> posterName = GetUserName(obj.UserID);
                    obj.Name = posterName[0].Name;
                }

                return MsgSource;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Create Post Message Functions

        /// <summary> 用PID檢查貼文是否存在 </summary>
        /// <param name="pid"></param>
        /// <returns>true : 存在, false : 不存在</returns>
        public static bool CheckPostExist(Guid pid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.Postings
                         where item.PostID == pid
                         select item);

                    var list = query.ToList();

                    if (list.Count() != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 建立留言寫入DB </summary>
        /// <param name="msgInfo"></param>
        /// <returns></returns>
        public static string CreateNewMsg(Message msgInfo)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    context.Messages.Add(msgInfo);
                    context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Create Exception Error";
            }
        }
        #endregion

        #region Delete User Own Post Functions


        #endregion
    }
}
