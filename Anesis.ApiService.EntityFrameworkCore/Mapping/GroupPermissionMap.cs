using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class GroupPermissionMap : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {
            builder.ToTable("GroupPermissions");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Permission)
                .WithMany()
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.UserGroup)
                .WithMany()
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
