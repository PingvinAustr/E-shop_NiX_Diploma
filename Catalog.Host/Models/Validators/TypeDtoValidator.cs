using Catalog.Host.Models.Dtos;
using FluentValidation;

public class TypeDtoValidator : AbstractValidator<TypeDto>
{
    public TypeDtoValidator()
    {
        RuleFor(x => x.TypeId)
            .GreaterThan(0)
            .WithMessage("Type ID must be greater than 0");

        RuleFor(x => x.TypeName)
            .NotEmpty()
            .WithMessage("Type name is required")
            .MaximumLength(100)
            .WithMessage("Type name must not exceed 100 characters");

        RuleFor(x => x.TypeDescription)
            .MaximumLength(850)
            .WithMessage("Type description must not exceed 850 characters");
    }
}
