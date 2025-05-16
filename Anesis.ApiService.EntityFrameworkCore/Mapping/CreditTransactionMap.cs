using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class CreditTransactionMap : IEntityTypeConfiguration<CreditTransaction>
    {
        public void Configure(EntityTypeBuilder<CreditTransaction> builder)
        {
            builder.ToTable("CreditTransactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).HasColumnType("money");
        }
    }
}
