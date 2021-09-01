using databaseORM.data;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDBFunction
{
    public class PostManager
    {
        #region Check Guid Data Exist Functions

        /// <summary>用UID檢查使用者是否存在 </summary>
        /// <param name="uid"></param>
        /// <returns>true : 存在, false : 不存在</returns>
        public static bool CheckUserExist(Guid uid)
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

                    if (list.Count() != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                return false;
            }
        }

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
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                return false;
            }
        }
        #endregion

        #region Posting Hall Page Functions

        /// <summary> 從DB取得全部貼文資料後，轉換成Model回傳Handler </summary>
        /// <returns>List PostInfoModel</returns>
        public static List<PostInfoModel> GetAllPostInfo()
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    // Get Post From DB View Table
                    var query =
                        (from item in context.vwDisplayPosts
                         orderby item.CreateDate descending
                         select item);

                    List<vwDisplayPost> sourceList = query.ToList();

                    // Check Data Exist
                    if (sourceList != null)
                    {
                        // Write into Model
                        List<PostInfoModel> postSource =
                            sourceList.Select(obj => new PostInfoModel()
                            {
                                PostID = obj.PostID,
                                Title = obj.Title,
                                Name = obj.Name,
                                CreateDate = obj.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                Level = obj.Level,
                                ismaincontent = obj.ismaincontent
                            }).ToList();

                        return postSource;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                return null;
            }
        }
        #endregion

        #region Post Info Functions

        /// <summary> 從資料庫取得貼文資訊 </summary>
        /// <returns></returns>
        public static List<PostInfoModel> GetOnePostInfo(Guid pid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    // get info from DB
                    var query =
                        (from item in context.Postings
                         where item.PostID == pid
                         select item);

                    List<Posting> sourceList = query.ToList();

                    // Check Data exist
                    if (sourceList != null)
                    {
                        // write into model
                        List<PostInfoModel> postInfo =
                            sourceList.Select(obj => new PostInfoModel()
                            {
                                CreateDate = obj.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
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
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
                return null;
            }
        }

        /// <summary> 從DB取得全部貼文中的留言後，轉換成Model回傳Handler </summary>
        /// <returns>List MsgInfoModel</returns>
        public static List<MsgInfoModel> GetAllPostMsg(Guid pid)
        {
            try
            {
                // get all msg by post id
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        (from item in context.vwDisplayMsg
                         where item.PostID == pid
                         orderby item.CreateDate descending
                         select item);

                    List<vwDisplayMsg> sourceList = query.ToList();

                    // Msg Data Exist
                    if (sourceList != null)
                    {
                        List<MsgInfoModel> MsgSource =
                            sourceList.Select(obj => new MsgInfoModel()
                            {
                                Body = obj.Body,
                                Name = obj.Name,
                                CreateDate = obj.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            }).ToList();

                        return MsgSource;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.tools.summitError(ex);
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
        #endregion

        #region Create Post Message Functions

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

        #region Show User's Post Functions

        /// <summary> 回傳會員貼文資料給Handler </summary>
        /// <param name="uid"></param>
        /// <returns> List PostInfoModel </returns>
        public static List<PostInfoModel> GetAllUserPostInfo(Guid uid)
        {
            using (databaseEF context = new databaseEF())
            {
                var query =
                (
                    from postInfo in context.Postings
                    where postInfo.UserID == uid
                    join account in context.Accountings
                    on postInfo.UserID equals account.UserID
                    select new
                    {
                        PostID = postInfo.PostID,
                        Title = postInfo.Title,
                        Name = account.Name,
                        CreateDate = postInfo.CreateDate,
                        UserID = postInfo.UserID
                    }
                );

                var sourceList = query.ToList();

                if (sourceList != null)
                {
                    List<PostInfoModel> postSource =
                        sourceList.Select(obj => new PostInfoModel()
                        {
                            PostID = obj.PostID,
                            UserID = obj.UserID,
                            Title = obj.Title,
                            Name = obj.Name,
                            CreateDate = obj.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")
                        }).ToList();

                    /*
                    // 用UID比對查詢，並寫入User Name
                    foreach (var item in postSource)
                    {
                        item.Name = GetUserName(item.UserID);
                    }
                    */

                    return postSource;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Show User's Message Functions

        /// <summary> 回傳會員留言資料給Handler </summary>
        /// <param name="uid"></param>
        /// <returns> List UserMsgInfo </returns>
        public static List<UserMsgInfo> GetUserAllMsgInfo(Guid uid)
        {
            try
            {                
                using (databaseEF context = new databaseEF())
                {
                    var query =
                    (
                        from msg in context.Messages
                        where msg.UserID == uid
                        join acc in context.Accountings on msg.UserID equals acc.UserID
                        join post in context.Postings on msg.PostID equals post.PostID
                        select new
                        {
                            MsgID = msg.MsgID,
                            PostID = msg.PostID,
                            Title = post.Title,
                            Body = msg.Body,
                            Name = acc.Name,
                            CreateDate = msg.CreateDate,
                            UserID = msg.UserID
                        }
                    );

                    var sourceList = query.ToList();

                    // Check exist in DB
                    if (sourceList != null)
                    {
                        List<UserMsgInfo> msgSource =
                            sourceList.Select(obj => new UserMsgInfo()
                            {
                                MsgID = obj.MsgID,
                                PostID = obj.PostID,
                                PostTile = obj.Title,
                                Body = obj.Body,
                                Name = obj.Name,
                                CreateDate = obj.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                UserID = obj.UserID,
                            }).ToList();

                        // 檢查Post是否存在
                        foreach (var item in msgSource)
                        {
                            if (item.PostID == null)
                                item.PostTile = "**貼文已被刪除**";
                        }

                        return msgSource;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete User Post And User Message Functions

        /// <summary> 從資料庫刪除會員貼文 </summary>
        /// <param name="uid"> 使用者Guid </param>
        /// <param name="pid"> 貼文Guid </param>
        /// <returns> Success : 成功, Other : 錯誤訊息</returns>
        public static string UserDeletePost(Guid uid, Guid pid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var deletePost =
                        $@"
                            DELETE FROM [dbo].[Posting]
                            WHERE [PostID] = '{pid}' and [UserID] = '{uid}'
                        ";
                    context.Database.ExecuteSqlCommand(deletePost);

                    var deletePostMsg =
                        $@"
                            DELETE FROM [dbo].[Message]
                            WHERE [PostID] = '{pid}'
                        ";
                    context.Database.ExecuteSqlCommand(deletePostMsg);

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary> 從資料庫刪除會員留言 </summary>
        /// <param name="uid"> 使用者Guid </param>
        /// <param name="mid"> 留言Guid </param>
        /// <returns> Success : 成功, Other : 錯誤訊息</returns>
        public static string UserDeleteMsg(Guid uid, Guid mid)
        {
            try
            {
                using (databaseEF context = new databaseEF())
                {
                    var query =
                        $@"
                            DELETE FROM [dbo].[Message]
                            WHERE [MsgID] = '{mid}' and [UserID] = '{uid}'
                        ";
                    context.Database.ExecuteSqlCommand(query);
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
    }
}
