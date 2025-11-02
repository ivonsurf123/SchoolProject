using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Models;

namespace SchoolProject.Data.Configurations
{
    public class ClassScheduleConfiguration : IEntityTypeConfiguration<ClassSchedule>
    {
        public void Configure(EntityTypeBuilder<ClassSchedule> builder)
        {
            builder.HasKey(h => h.ClassScheduleId);

            // Propiedades
            builder.Property(h => h.Day)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(h => h.StartTime)
                .IsRequired();

            builder.Property(h => h.EndTime)
                .IsRequired();

            builder.Property(h => h.Classroom)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(h => h.Period)
                .HasMaxLength(20);

            builder.Property(h => h.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}

