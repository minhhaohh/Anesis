using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class GenericInvoiceItemMap : IEntityTypeConfiguration<GenericInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<GenericInvoiceItem> builder)
        {
            builder.ToTable("GenericInvoiceItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitPrice).HasColumnType("money");

            builder.HasOne(t => t.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
