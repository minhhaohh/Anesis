using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.RiskLevel)
                .WithMany()
                .HasForeignKey(d => d.RiskLevelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
