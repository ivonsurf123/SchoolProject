using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Models;

namespace SchoolProject.Data.Configurations
{
    public class ScoreConfigurations : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.HasKey(c => c.ScoreId);

            // Propiedades
            builder.Property(c => c.EvaluationType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Value)
                .IsRequired()
                .HasPrecision(5, 2);  // Precisión: 5 dígitos totales, 2 decimales (Ej: 100.00)

            builder.Property(c => c.Date)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Period)
                .HasMaxLength(20);

            builder.Property(c => c.Observations)
                .HasMaxLength(200);
        }
    }
}
