using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class CustomMediaFileMap : IEntityTypeConfiguration<CustomMediaFile>
    {
        public void Configure(EntityTypeBuilder<CustomMediaFile> builder)
        {
            builder.ToTable("CustomMediaFiles");

            builder.HasKey(x => x.Id);
        }
    }
}
