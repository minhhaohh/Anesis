using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class TimetableMap : IEntityTypeConfiguration<Timetable>
    {
        public void Configure(EntityTypeBuilder<Timetable> builder)
        {
            var timeSpanConverter = new TimeSpanToIntConverter();

            builder.ToTable("Timetables");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartTime).HasColumnType("int").HasConversion(timeSpanConverter);
            builder.Property(x => x.EndTime).HasColumnType("int").HasConversion(timeSpanConverter);

            builder.HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
