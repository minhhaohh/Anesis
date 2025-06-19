using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class MenuTabMap : IEntityTypeConfiguration<MenuTab>
    {
        public void Configure(EntityTypeBuilder<MenuTab> builder)
        {
            builder.ToTable("MenuTabs");

            builder.HasKey(x => x.Id);
        }
    }
}
