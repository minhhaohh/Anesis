using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class UserInGroupMap : IEntityTypeConfiguration<UserInGroup>
    {
        public void Configure(EntityTypeBuilder<UserInGroup> builder)
        {
            builder.ToTable("UserInGroups");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.UserGroup)
                .WithMany()
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
