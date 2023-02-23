using EmployeesManagement.RestApi.Contracts.Requests.Position;

using FluentValidation;

namespace EmployeesManagement.RestApi.Validation;

public class SavePositionRequestValidator : AbstractValidator<SavePositionRequest>
{
    public SavePositionRequestValidator()
    {
        RuleFor(x => x.PositionId).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Level).InclusiveBetween(1, 15);

        RuleFor(x => x.Title)
            .MinimumLength(2)
            .MaximumLength(256)
            .Must(ValidationRules.ContainsLetter).WithMessage("'{PropertyName}' must contain at least one letter.");
    }
}
