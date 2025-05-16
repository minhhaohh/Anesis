using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class SurgeryCaseCptCodeMap : IEntityTypeConfiguration<SurgeryCaseCptCode>
    {
        public void Configure(EntityTypeBuilder<SurgeryCaseCptCode> builder)
        {
            builder.ToTable("SurgeryCaseCptCodes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AllowedAmountPct).HasColumnType("decimal(10,5)");
            builder.Property(x => x.CollectedAmount).HasColumnType("money");

            builder.HasOne(x => x.SurgeryCase)
                .WithMany(x => x.CptCodes)
                .HasForeignKey(x => x.SurgeryCaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.BillingCode)
                .WithMany()
                .HasForeignKey(x => x.BillingCodeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.RvuConfig)
                .WithMany()
                .HasForeignKey(x => x.RvuConfigId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AllowedAmount)
                .WithMany()
                .HasForeignKey(x => x.AllowedAmountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
