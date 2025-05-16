using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class GeneralChangeLogMap : IEntityTypeConfiguration<GeneralChangeLog>
    {
        public void Configure(EntityTypeBuilder<GeneralChangeLog> builder)
        {
            builder.ToTable("GeneralChangeLogs");

            builder.HasKey(x => x.Id);
        }
    }
}
