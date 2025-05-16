using Anesis.ApiService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anesis.ApiService.EntityFrameworkCore.Mapping
{
	public class DeviceAndSupplyMap : IEntityTypeConfiguration<DeviceAndSupply>
	{
		public void Configure(EntityTypeBuilder<DeviceAndSupply> builder)
		{
			builder.ToTable("DeviceAndSupplies");

			builder.HasKey(x => x.Id);
		}
	}
}
