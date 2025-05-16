using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class EncounterTypeMap : IEntityTypeConfiguration<EncounterType>
    {
        public void Configure(EntityTypeBuilder<EncounterType> builder)
        {
            builder.ToTable("EncounterTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AverageAmount).HasColumnType("money");
            builder.Property(x => x.LabAmount).HasColumnType("money");
            builder.Property(x => x.SascAverageAmount).HasColumnType("money");
        }
    }
}
