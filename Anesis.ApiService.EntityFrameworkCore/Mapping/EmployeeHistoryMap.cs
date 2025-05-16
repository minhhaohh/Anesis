using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class EmployeeHistoryMap : IEntityTypeConfiguration<EmployeeHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeHistory> builder)
        {
            builder.ToTable("EmployeeHistory");

            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.JobRole)
                .WithMany()
                .HasForeignKey(d => d.JobRoleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.LunchAllowance).HasColumnType("decimal(18,2)");
            builder.Property(x => x.HoursPerDay).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DaysPerWeek).HasColumnType("decimal(18,2)");
        }
    }
}
