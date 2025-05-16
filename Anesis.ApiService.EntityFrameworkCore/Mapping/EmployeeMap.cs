using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LunchAllowance).HasColumnType("decimal(18,2)");
            builder.Property(x => x.HoursPerDay).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DaysPerWeek).HasColumnType("decimal(18,2)");

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
        }
    }
}
