using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserMsgInfo
    {
        public Guid MsgID { get; set; }
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
        public string PostTile { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }
        public string CreateDate { get; set; }
    }
}
