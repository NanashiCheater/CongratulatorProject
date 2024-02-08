using System;
using System.Collections.Generic;
using CongratulatorProject.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions;

namespace CongratulatorProject.db
{
    public partial class Congratulator : DbContext
    {
       public Congratulator()
        {
        }

        public Congratulator(DbContextOptions<Congratulator> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<PersonImage> PersonsImages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Congratulator;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonImage>(entity =>
            {
                entity.HasOne(d => d.PersonModel)
                    .WithMany(p => p.PersonsImages)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonsImages_Persons");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
   
}
