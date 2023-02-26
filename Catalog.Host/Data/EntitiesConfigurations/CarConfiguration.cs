using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.Property(x => x.CarId)
                .UseHiLo("cars_hilo")
                .IsRequired();

            builder.Property(x => x.CarName)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .IsRequired(true);

            builder.Property(x => x.ImageFileName)
                .IsRequired(false);

            builder.Property(x => x.CarPromo)
                .IsRequired(false);

            builder.HasOne(x => x.Manufacturer)
                .WithMany()
                .HasForeignKey(x => x.ManufacturerId);

            builder.HasOne(x => x.CarType)
                .WithMany()
                .HasForeignKey(x => x.TypeId);
        }
    }
}
