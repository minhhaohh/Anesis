using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class TimeClockHistoryMap : IEntityTypeConfiguration<TimeClockHistory>
    {
        public void Configure(EntityTypeBuilder<TimeClockHistory> builder)
        {
            builder.ToTable("TimeClockHistory");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.TimeClock)
                .WithMany()
                .HasForeignKey(d => d.TimeClockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
