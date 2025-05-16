using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class InsuranceMap : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {
            builder.ToTable("Insurances");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.PreauthInsuranceGroup)
                .WithMany()
                .HasForeignKey(d => d.PreAuthInsGroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
