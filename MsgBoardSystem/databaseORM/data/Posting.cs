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
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid PostID { get; set; }

        public Guid UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(4000)]
        public string Body { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool ismaincontent { get; set; }
    }
}
