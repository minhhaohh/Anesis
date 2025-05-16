using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class BillingCodeMap : IEntityTypeConfiguration<BillingCode>
    {
        public void Configure(EntityTypeBuilder<BillingCode> builder)
        {
            builder.ToTable("BillingCodes");

            builder.HasKey(x => x.Id);
        }
    }
}
