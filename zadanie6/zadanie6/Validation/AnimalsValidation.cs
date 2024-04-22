using zadanie6.DTOs;
using FluentValidation;
namespace zadanie6.Validation;

public class AnimalsValidation: AbstractValidator<AnimalDTO.CreateAnimalsRequest>
{
    public AnimalsValidation()
    {
        RuleFor(e => e.Name).MaximumLength(50).NotNull();
        RuleFor(e => e.Description).MaximumLength(200);
        RuleFor(e => e.Category).MaximumLength(200).NotNull();
        RuleFor(e => e.Areae).MaximumLength(200);
    }
}