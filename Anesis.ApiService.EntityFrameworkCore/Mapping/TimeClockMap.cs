using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class TimeClockMap : IEntityTypeConfiguration<TimeClock>
    {
        public void Configure(EntityTypeBuilder<TimeClock> builder)
        {
            builder.ToTable("TimeClocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalHours).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Deduction).HasColumnType("decimal(18,2)");
            builder.Property(x => x.UserAgent).HasMaxLength(1000);


            builder.HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Approver)
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.ClosedPayroll)
                .WithMany()
                .HasForeignKey(d => d.ClosedPayrollId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.EmployeeHistory)
                .WithMany()
                .HasForeignKey(d => d.EmployeeHistoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
