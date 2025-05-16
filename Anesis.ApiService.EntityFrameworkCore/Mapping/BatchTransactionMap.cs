using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class BatchTransactionMap : IEntityTypeConfiguration<BatchTransaction>
    {
        public void Configure(EntityTypeBuilder<BatchTransaction> builder)
        {
            builder.ToTable("BatchTransactions");

            builder.HasKey(x => x.Id);


            builder.HasOne(t => t.BankTransaction)
                .WithMany()
                .HasForeignKey(d => d.BankTransactionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Reconciliation)
                .WithMany()
                .HasForeignKey(d => d.ReconciliationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.PaymentAmount).HasColumnType("money");
        }
    }
}
