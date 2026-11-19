using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Data
{
    public class OnlineLearningContext : DbContext
    {
        public OnlineLearningContext()
        {
        }

        public OnlineLearningContext(
            DbContextOptions<OnlineLearningContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(local);Database=OnlineLearningPlatformDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;");
            }
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OnlineLearningContext).Assembly);
        }
    }
}