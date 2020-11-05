namespace FinalAssignment2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FIT5032Model : DbContext
    {
        public FIT5032Model()
            : base("name=FIT5032Model")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.School)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tutor>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Tutor)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<FinalAssignment2.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<FinalAssignment2.Models.Enrolment> Enrolments { get; set; }
    }
}
