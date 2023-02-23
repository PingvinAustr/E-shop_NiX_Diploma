using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfigurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Entities.Type> builder)
        {
            builder.ToTable("Type");

            builder.HasKey(x => x.TypeId);

            builder.Property(x => x.TypeId)
                .UseHiLo("car_type_hilo")
                .IsRequired();

            builder.Property(x => x.TypeName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TypeDescription)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
