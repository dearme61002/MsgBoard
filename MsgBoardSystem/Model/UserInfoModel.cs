using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfoModel
    {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }
        public string Email { get; set; }
        public DateTime? Bucket { get; set; }
        public DateTime Birthday { get; set; }
    }
}
