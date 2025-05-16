using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class LocalityAllowedAmountMap : IEntityTypeConfiguration<LocalityAllowedAmount>
    {
        public void Configure(EntityTypeBuilder<LocalityAllowedAmount> builder)
        {
            builder.ToTable("LocalityAllowedAmounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AllowedAmount)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.County)
                .WithMany()
                .HasForeignKey(x => x.CountyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.BillingCode)
                .WithMany()
                .HasForeignKey(x => x.BillingCodeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
