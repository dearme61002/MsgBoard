namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLogin")]
    public partial class UserLogin
    {
        public int ID { get; set; }

        public Guid UserID { get; set; }

        public DateTime LoginDate { get; set; }

        public DateTime LogoutDate { get; set; }

        [Required]
        [StringLength(50)]
        public string IP { get; set; }
    }
}
