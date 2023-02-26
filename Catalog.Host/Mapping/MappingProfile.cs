using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember("ImageFileName", opt
                    => opt.MapFrom<CarPictureResolver, string>(c => c.ImageFileName))
                .ReverseMap();
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
            CreateMap<Data.Entities.Type, TypeDto>().ReverseMap();
        }
    }
}
