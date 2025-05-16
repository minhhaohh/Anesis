using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class ProcedureMap : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.ToTable("Procedures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BonusPoint)
                .HasColumnType("decimal(5, 2)");
        }
    }
}
