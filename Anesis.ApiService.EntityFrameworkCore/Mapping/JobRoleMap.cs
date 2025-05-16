using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class JobRoleMap : IEntityTypeConfiguration<JobRole>
    {
        public void Configure(EntityTypeBuilder<JobRole> builder)
        {
            builder.ToTable("JobRoles");

            builder.HasKey(x => x.Id);
        }
    }
}
