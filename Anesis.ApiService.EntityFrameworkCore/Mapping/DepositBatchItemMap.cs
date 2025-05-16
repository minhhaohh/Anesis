using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class DepositBatchItemMap : IEntityTypeConfiguration<DepositBatchItem>
    {
        public void Configure(EntityTypeBuilder<DepositBatchItem> builder)
        {
            builder.ToTable("DepositBatchItems");

            builder.HasKey(x => new { x.DepositBatchId, x.ItemNumber });

            builder.HasOne(t => t.DepositBatch)
                .WithMany(x => x.DepositBatchItems)
                .HasForeignKey(d => d.DepositBatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ItemAmount).HasColumnType("money");
        }
    }
}
