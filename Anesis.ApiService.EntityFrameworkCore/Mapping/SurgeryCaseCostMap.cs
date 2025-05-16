using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class SurgeryCaseCostMap : IEntityTypeConfiguration<SurgeryCaseCost>
    {
        public void Configure(EntityTypeBuilder<SurgeryCaseCost> builder)
        {
            builder.ToTable("SurgeryCaseCosts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AppliedCost)
                .HasColumnType("money");

            builder.HasOne(x => x.SurgeryCase)
                .WithMany(x => x.DeviceCosts)
                .HasForeignKey(x => x.SurgeryCaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Device)
                .WithMany()
                .HasForeignKey(x => x.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.DeviceCost)
                .WithMany()
                .HasForeignKey(x => x.CostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
