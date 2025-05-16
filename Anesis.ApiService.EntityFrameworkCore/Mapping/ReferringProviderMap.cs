using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class ReferringProviderMap : IEntityTypeConfiguration<ReferringProvider>
    {
        public void Configure(EntityTypeBuilder<ReferringProvider> builder)
        {
            builder.ToTable("ReferringProviders");

            builder.HasKey(x => x.Id);
        }
    }
}
