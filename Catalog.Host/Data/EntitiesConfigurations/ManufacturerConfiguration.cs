using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfigurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturer");

            builder.HasKey(x => x.ManufacturerId);

            builder.Property(x => x.ManufacturerId)
                .UseHiLo("car_manufaturer_hilo")
                .IsRequired();

            builder.Property(x => x.ManufacturerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ManufacturerCountry)
                .IsRequired(true)
                .HasMaxLength(100);
        }
    }
}
