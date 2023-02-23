using EmployeesManagement.RestApi.Contracts.Requests.Employee;

using FastEndpoints;

using FluentValidation;

namespace EmployeesManagement.RestApi.Validation;

public class SaveEmployeeRequestValidator : Validator<SaveEmployeeRequest>
{
    public SaveEmployeeRequestValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThanOrEqualTo(0);

        RuleFor(x => x.FirstName)
            .Length(2, 40)
            .Must(ValidationRules.ContainsOnlyLetters).WithMessage("'{PropertyName}' must contain only letters.");

        RuleFor(x => x.SecondName)
            .Length(2, 40)
            .Must(ValidationRules.ContainsOnlyLetters).WithMessage("'{PropertyName}' must contain only letters.");

        RuleFor(x => x.Patronymic)
            .Length(2, 40).When(x => x is not null)
            .Must(ValidationRules.ContainsOnlyLetters).WithMessage("'{PropertyName}' must contain only letters.");

        RuleFor(x => x.DateOfBirth)
            .Must(ValidationRules.IsValidDateOfBirth)
            .WithMessage("'{PropertyName}' value '{PropertyValue}' is invalid. Employee can be from 16 to 100 years old.")
            .When(x => x is not null);

        RuleFor(x => x.PositionsIds)
            .Must(x => x!.All(i => i > 0))
            .WithMessage("{PropertyName} contains invalid values. Positions ids must be non-negative integers.")
            .When(x => x is not null);
    }
}
