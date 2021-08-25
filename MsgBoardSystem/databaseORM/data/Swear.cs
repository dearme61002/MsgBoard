namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Swear")]
    public partial class Swear
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Body { get; set; }
    }
}
