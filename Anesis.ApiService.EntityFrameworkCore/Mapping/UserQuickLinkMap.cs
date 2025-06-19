using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class UserQuickLinkMap : IEntityTypeConfiguration<UserQuickLink>
    {
        public void Configure(EntityTypeBuilder<UserQuickLink> builder)
        {
            builder.ToTable("UserQuickLinks");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.MenuItem)
                .WithMany()
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
