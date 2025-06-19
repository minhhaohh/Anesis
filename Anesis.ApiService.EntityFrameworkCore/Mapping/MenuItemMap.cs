using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class MenuItemMap : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.MenuTab)
                .WithMany(x => x.MenuItems)
                .HasForeignKey(d => d.MenuTabId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
