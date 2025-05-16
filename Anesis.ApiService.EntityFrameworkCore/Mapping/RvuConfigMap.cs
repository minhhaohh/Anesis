using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class RvuConfigMap : IEntityTypeConfiguration<RvuConfig>
    {
        public void Configure(EntityTypeBuilder<RvuConfig> builder)
        {
            builder.ToTable("RvuConfigs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.WorkRvu)
                .HasColumnType("decimal(8, 2)");

            builder.Property(x => x.MalpracticeRvu)
                .HasColumnType("decimal(8, 2)");

            builder.Property(x => x.NonFacilityPeRvu)
                .HasColumnType("decimal(8, 2)");

            builder.Property(x => x.NonFacilityRvuTotal)
                .HasColumnType("decimal(8, 2)");

            builder.Property(x => x.FacilityPeRvu)
                .HasColumnType("decimal(8, 2)");

            builder.Property(x => x.FacilityRvuTotal)
                .HasColumnType("decimal(8, 2)");

            builder.Property(x => x.ConversionFactor)
                .HasColumnType("decimal(8, 2)");

            builder.HasOne(x => x.BillingCode)
                .WithMany()
                .HasForeignKey(x => x.BillingCodeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
