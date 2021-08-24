namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        public int ID { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(100)]
        public string Body { get; set; }
    }
}
