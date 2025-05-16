using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
	public class DeviceCostMap : IEntityTypeConfiguration<DeviceCost>
	{
		public void Configure(EntityTypeBuilder<DeviceCost> builder)
		{
			builder.ToTable("DeviceCosts");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.VendorCost)
				.HasColumnType("money");

			builder.Property(x => x.AppliedCost)
				.HasColumnType("money");

			builder.HasOne(x => x.Device)
				.WithMany()
				.HasForeignKey(x => x.DeviceId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
