using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class ReconciliationMap : IEntityTypeConfiguration<Reconciliation>
    {
        public void Configure(EntityTypeBuilder<Reconciliation> builder)
        {
            builder.ToTable("Reconciliations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DepositBalance).HasColumnType("money");
        }
    }
}
