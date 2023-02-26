using Catalog.Host.Models.Dtos;
using FluentValidation;

namespace Catalog.Host.Models.Validators
{
    public class ManufacturerDtoValidator : AbstractValidator<ManufacturerDto>
    {
        public ManufacturerDtoValidator()
        {
            RuleFor(x => x.ManufacturerId)
            .GreaterThan(0)
            .WithMessage("Type ID must be greater than 0");

            RuleFor(x => x.ManufacturerName)
                .NotEmpty()
                .WithMessage("Manufacturer name is required")
                .MaximumLength(100)
                .WithMessage("Manufacturer name should not be longer than 100 characters");

            RuleFor(x => x.ManufacturerCountry)
                .NotEmpty()
                .WithMessage("Manufacturer country is required")
                .MaximumLength(100)
                .WithMessage("Manufacturer country should not be longer than 100 characters");
        }
    }
}
