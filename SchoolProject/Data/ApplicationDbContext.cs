using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Configurations;
using SchoolProject.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SchoolProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Professor> Profesors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClassSchedule> ClassSchedules { get; set; }
        public DbSet<Score> Scores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
         
    }

    }
}