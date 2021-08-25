using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace databaseORM.data
{
    public partial class databaseEF : DbContext
    {
        public databaseEF()
            : base("name=databaseEF")
        {
        }

        public virtual DbSet<Accounting> Accountings { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Info> Infoes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Posting> Postings { get; set; }
        public virtual DbSet<Swear> Swears { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounting>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<Accounting>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Accounting>()
                .Property(e => e.Level)
                .IsUnicode(false);
        }
    }
}
