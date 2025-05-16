using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("NormalizedUserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("NormalizedUserEmailIndex").IsUnique();

            builder.HasMany<UserInRole>().WithOne(x => x.Account).HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        }
    }
}
