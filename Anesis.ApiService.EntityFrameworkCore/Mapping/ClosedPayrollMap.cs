using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class ClosedPayrollMap : IEntityTypeConfiguration<ClosedPayroll>
    {
        public void Configure(EntityTypeBuilder<ClosedPayroll> builder)
        {
            builder.ToTable("ClosedPayrolls");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.GrossHours).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DeductedHours).HasColumnType("decimal(18,2)");
            builder.Property(x => x.UnpaidHours).HasColumnType("decimal(18,2)");
            builder.Property(x => x.NetHours).HasColumnType("decimal(18,2)");
        }
    }
}
