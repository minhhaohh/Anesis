using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class RiskLevelMap : IEntityTypeConfiguration<RiskLevel>
    {
        public void Configure(EntityTypeBuilder<RiskLevel> builder)
        {
            builder.ToTable("RiskLevels");

            builder.HasKey(x => x.Id);
        }
    }
}
