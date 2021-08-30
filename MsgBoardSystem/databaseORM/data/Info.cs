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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RegisteredPeople { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int PeopleOnline { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
    }
}
