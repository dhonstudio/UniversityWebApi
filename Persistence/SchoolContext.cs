using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            SavingChanges += SavingChangeEvent;
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Enrollment> Enrollments{ get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("gufron");
            builder.Entity<Student>(e =>
            {
                e.HasKey(x => x.ID);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
                e.HasMany(x => x.Enrollments).WithOne(x => x.Student).HasForeignKey(x => x.StudentID);
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            builder.Entity<Enrollment>(e =>
            {
                e.HasKey(x => x.EnrollmentId);
                e.Property(x => x.StudentID).IsRequired();
                e.Property(x => x.CourseID).IsRequired();
                e.HasOne(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentID);
                e.HasOne(x => x.Course).WithMany(x => x.Enrollments).HasForeignKey(x => x.CourseID);                
                e.HasQueryFilter(x => x.DeletedAt == null);
            });

            builder.Entity<Course>(e =>
            {
                e.HasKey(x => x.CourseID);
                e.Property(x => x.Title).IsRequired().HasMaxLength(512);
                e.HasMany(x => x.Enrollments).WithOne(x => x.Course).HasForeignKey(x => x.CourseID);
                e.HasQueryFilter(x => x.DeletedAt == null);
            });
        }

        public void SavingChangeEvent(object? sender, SavingChangesEventArgs e)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if(entry.Entity is BaseEntity entity && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.DeletedAt = DateTime.Now;
                }
            }
        }
    }
}
