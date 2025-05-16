using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class PotentialProcedureMap : IEntityTypeConfiguration<PotentialProcedure>
    {
        public void Configure(EntityTypeBuilder<PotentialProcedure> builder)
        {
            var nullableTimeSpanConverter = new NullableTimeSpanToIntConverter();

            builder.ToTable("PotentialProcedures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SurgeryTime).HasColumnType("int").HasConversion(nullableTimeSpanConverter);

            builder.HasOne(x => x.Proposer)
                .WithMany()
                .HasForeignKey(x => x.ProposerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ApptProvider)
                .WithMany()
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Reviewer)
                .WithMany()
                .HasForeignKey(x => x.ReviewerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Surgeon)
                .WithMany()
                .HasForeignKey(x => x.SurgeonId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ApptLocation)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SurgeryLocation)
                .WithMany()
                .HasForeignKey(x => x.SurgeryLocationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Procedure)
                .WithMany()
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
