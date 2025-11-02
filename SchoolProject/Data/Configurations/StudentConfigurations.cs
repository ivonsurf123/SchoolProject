using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Models;

namespace SchoolProject.Data.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(e => e.StudentId);

            // Propiedades
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Lastname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.GuardianName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.GuardianPhone)
                .HasMaxLength(20);

            builder.Property(e => e.EnrollmentDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices
            //builder.HasIndex(e => e.Code)
            //    .IsUnique()
            //    .HasDatabaseName("IX_Student_Code");

            //builder.HasIndex(e => e.Email)
            //    .IsUnique()
            //    .HasDatabaseName("IX_Estudiante_Email");

            //builder.HasIndex(e => e.Lastname)
            //    .HasDatabaseName("IX_Student_Lastname");
        }
    }
}

