namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwDisplayMsg")]
    public partial class vwDisplayMsg
    {
        [Key]
        [Column(Order = 0)]
        public Guid PostID { get; set; }

        [StringLength(4000)]
        public string Body { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreateDate { get; set; }
    }
}
