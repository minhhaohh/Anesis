using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class WeekMap : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            builder.ToTable("Weeks");

            builder.HasKey(x => x.Id);
        }
    }
}
