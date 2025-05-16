using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class GeneralCategoryMap : IEntityTypeConfiguration<GeneralCategory>
    {
        public void Configure(EntityTypeBuilder<GeneralCategory> builder)
        {
            builder.ToTable("GeneralCategories");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.ParentCategory)
                .WithMany()
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
