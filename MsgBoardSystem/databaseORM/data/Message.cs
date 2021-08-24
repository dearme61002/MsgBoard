namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int ID { get; set; }

        public Guid MsgID { get; set; }

        public Guid PostID { get; set; }

        public Guid UserID { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(4000)]
        public string Body { get; set; }
    }
}
