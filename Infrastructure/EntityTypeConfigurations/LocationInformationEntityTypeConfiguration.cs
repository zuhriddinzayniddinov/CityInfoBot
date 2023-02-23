using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

internal class LocationInformationEntityTypeConfiguration : IEntityTypeConfiguration<LocationInformation>
{
    public void Configure(EntityTypeBuilder<LocationInformation> builder)
    {
        builder.HasKey(x => x.LocationName);

        builder.Property(x => x.LocationName)
            .IsRequired();
    }
}
