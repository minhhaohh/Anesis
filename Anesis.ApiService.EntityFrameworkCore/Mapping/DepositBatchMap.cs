using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class DepositBatchMap : IEntityTypeConfiguration<DepositBatch>
    {
        public void Configure(EntityTypeBuilder<DepositBatch> builder)
        {
            builder.ToTable("DepositBatch");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DepositAmount).HasColumnType("money");

            builder.HasOne(t => t.Reconciliation)
                .WithMany()
                .HasForeignKey(d => d.ReconciliationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.BankTransaction)
                .WithMany()
                .HasForeignKey(d => d.BankTransactionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
