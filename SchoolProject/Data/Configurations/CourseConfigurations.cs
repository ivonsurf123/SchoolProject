using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Models;

namespace SchoolProject.Data.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            // Propiedades
            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.Credits)
                .IsRequired();

            builder.Property(c => c.Level)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Grade)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices
            builder.HasIndex(c => c.Code)
                .IsUnique()
                .HasDatabaseName("IX_Course_Code");

            builder.HasIndex(c => c.ProfessorId)
                .HasDatabaseName("IX_Course_ProfessorId");

            // Relación con Profesor (muchos a uno)
            builder.HasOne(c => c.Professor)
                .WithMany(p => p.Courses)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}

