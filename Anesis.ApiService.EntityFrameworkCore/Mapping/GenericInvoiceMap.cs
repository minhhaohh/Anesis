using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class GenericInvoiceMap : IEntityTypeConfiguration<GenericInvoice>
    {
        public void Configure(EntityTypeBuilder<GenericInvoice> builder)
        {
            builder.ToTable("GenericInvoices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Subtotal).HasColumnType("money");
            builder.Property(x => x.TaxAmount).HasColumnType("money");
            builder.Property(x => x.ShippingFee).HasColumnType("money");
            builder.Property(x => x.AdditionalCharges).HasColumnType("money");
            builder.Property(x => x.DiscountAmount).HasColumnType("money");
            builder.Property(x => x.TotalAmount).HasColumnType("money");
            builder.Property(x => x.AmountToPay).HasColumnType("money");
            builder.Property(x => x.AmountPaid).HasColumnType("money");

            builder.HasOne(t => t.Vendor)
                .WithMany()
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
