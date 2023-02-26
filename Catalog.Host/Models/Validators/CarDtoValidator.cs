using Catalog.Host.Models.Dtos;
using FluentValidation;

namespace Catalog.Host.Models.Validators
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator()
        {
            RuleFor(x => x.CarName).NotEmpty();
            RuleFor(x => x.CarPromo).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.ImageFileName).NotEmpty();
            RuleFor(x => x.CarType.TypeId).GreaterThan(0);
            RuleFor(x => x.Manufacturer.ManufacturerId).GreaterThan(0);
        }
    }
}
