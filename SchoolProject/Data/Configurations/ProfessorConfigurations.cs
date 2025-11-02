using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Models;

namespace SchoolProject.Data.Configurations
{
    public class ProfessorConfigurations : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.HasKey(p => p.ProfessorId);

            // Propiedades
            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Phone)
                .HasMaxLength(20);

            builder.Property(p => p.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.HiringDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices
            builder.HasIndex(p => p.Code)
                .IsUnique()
                .HasDatabaseName("IX_Professor_Code");

            builder.HasIndex(p => p.Email)
                .IsUnique()
                .HasDatabaseName("IX_Professor_Email");

            // Relación con Usuario (1 a 1 opcional)
            builder.HasOne(p => p.User)
                .WithOne(u => u.Professor)
                .HasForeignKey<Professor>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

