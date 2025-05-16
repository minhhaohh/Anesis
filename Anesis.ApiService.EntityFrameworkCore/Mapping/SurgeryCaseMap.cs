using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class SurgeryCaseMap : IEntityTypeConfiguration<SurgeryCase>
    {
        public void Configure(EntityTypeBuilder<SurgeryCase> builder)
        {
            var timeSpanConverter = new TimeSpanToIntConverter();
            var nullableTimeSpanConverter = new NullableTimeSpanToIntConverter();

            builder.ToTable("SurgeryCases");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartTime).HasColumnType("int").HasConversion(timeSpanConverter);
            builder.Property(x => x.EndTime).HasColumnType("int").HasConversion(timeSpanConverter);

            builder.Property(x => x.AnesthesiaStartTime).HasColumnType("int").HasConversion(nullableTimeSpanConverter);
            builder.Property(x => x.AnesthesiaEndTime).HasColumnType("int").HasConversion(nullableTimeSpanConverter);

            builder.Property(x => x.InvoiceAmount).HasColumnType("money");


            builder.HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PrimaryProvider)
                .WithMany()
                .HasForeignKey(x => x.PrimaryProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AttendingProvider)
                .WithMany()
                .HasForeignKey(x => x.AttendingProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.EncounterType)
                .WithMany()
                .HasForeignKey(x => x.EncounterTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Procedure)
                .WithMany()
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Insurance)
                .WithMany()
                .HasForeignKey(x => x.InsuranceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ReferringProvider)
                .WithMany()
                .HasForeignKey(x => x.ReferringProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Week)
                .WithMany()
                .HasForeignKey(x => x.WeekId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PurchaseInvoice)
                .WithMany()
                .HasForeignKey(x => x.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
