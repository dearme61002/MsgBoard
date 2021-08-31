namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwDisplayPost")]
    public partial class vwDisplayPost
    {
        [Key]
        [Column(Order = 0)]
        public Guid PostID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string Level { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool ismaincontent { get; set; }
    }
}
