using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class BankTransactionMap : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("BankTransactions");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Reconciliation)
                .WithMany()
                .HasForeignKey(d => d.ReconciliationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.Debit).HasColumnType("money");
            builder.Property(x => x.Credit).HasColumnType("money");
        }
    }
}
