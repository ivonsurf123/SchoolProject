using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Models;

namespace SchoolProject.Data.Configurations
{
    public class EnrollmentConfigurations : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(i => i.EnrollmentId);

            // Propiedades
            builder.Property(i => i.EnrollmentDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(i => i.Period)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(i => i.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
