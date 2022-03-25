namespace databaseORM.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Info")]
    public partial class Info
    {
        public int ID { get; set; }

        public int RegisteredPeople { get; set; }

        public int PeopleOnline { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
    }
}
