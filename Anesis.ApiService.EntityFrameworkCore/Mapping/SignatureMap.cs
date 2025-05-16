using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class SignatureMap : IEntityTypeConfiguration<Signature>
    {
        public void Configure(EntityTypeBuilder<Signature> builder)
        {
            builder.ToTable("Signatures");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.MediaFile)
                .WithMany(x => x.Signatures)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
