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
                    return list;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 轉換Model後送回Handler </summary>
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
                        CreateDate = obj.CreateDate,
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
    }
}
