namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Posting")]
    public partial class Posting
    {
        public int ID { get; set; }

        public Guid PostID { get; set; }

        public Guid UserID { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Body { get; set; }

        public bool ismaincontent { get; set; }
    }
}
