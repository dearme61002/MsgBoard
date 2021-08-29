using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PostInfoModel
    {
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string CreateDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }      
        public string Level { get; set; }
    }
}
