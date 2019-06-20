using System.Data.Entity.Infrastructure.Annotations;
using System.Security.Cryptography.X509Certificates;

namespace CFMigration
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KTStoreModel : DbContext
    {
        public KTStoreModel()
            : base("name=KTStoreConnectionString")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => new { p.Id, p.Name });//½Æ¦X¯Á¤Þ
            modelBuilder.Entity<Product>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Product>().Property(p => p.Name)
            //    .IsRequired()
            //    .HasMaxLength(450)
            //    .HasColumnAnnotation(
            //        "Index",
            //        new IndexAnnotation(
            //            new IndexAttribute("IX_Name") { IsUnique = true })
            //             );


        }

        public DbSet<Product> Product { get; set; }
    }
}
