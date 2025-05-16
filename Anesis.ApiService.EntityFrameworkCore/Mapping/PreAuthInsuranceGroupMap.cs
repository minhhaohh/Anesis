using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class PreAuthInsuranceGroupMap : IEntityTypeConfiguration<PreAuthInsuranceGroup>
    {
        public void Configure(EntityTypeBuilder<PreAuthInsuranceGroup> builder)
        {
            builder.ToTable("PreauthInsuranceGroups");

            builder.HasKey(x => x.Id);
        }
    }
}
