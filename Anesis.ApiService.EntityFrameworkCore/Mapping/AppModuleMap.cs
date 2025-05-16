using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class AppModuleMap : IEntityTypeConfiguration<AppModule>
    {
        public void Configure(EntityTypeBuilder<AppModule> builder)
        {
            builder.ToTable("AppModules");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.DisplayOrder).HasColumnName("SortOrder");
        }
    }
}
