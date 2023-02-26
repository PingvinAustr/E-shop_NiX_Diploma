using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Mapping
{
    public class CarPictureResolver : IMemberValueResolver<Car, CarDto, string, object>
    {
        private readonly CatalogConfig _config;

        public CarPictureResolver(IOptionsSnapshot<CatalogConfig> config)
        {
            _config = config.Value;
        }

        public object Resolve(Car source, CarDto destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.Host}/{_config.ImgUrl}/{sourceMember}";
        }
    }
}
