using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("NormalizedRoleNameIndex").IsUnique();

            builder.HasMany<UserInRole>().WithOne(x => x.Role).HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
        }
    }
}
