namespace EmployeesManagement.RestApi.Validation;

public static class ValidationRules
{
    public static bool ContainsOnlyLetters(string str) => str.All(char.IsLetter);

    public static bool ContainsLetter(string str) => str.Any(char.IsLetter);

    public static bool IsValidDateOfBirth(DateOnly? dateOfBirth)
    {
        if (dateOfBirth == default)
        {
            return false;
        }

        const int minAllowedAge = 16;
        const int maxAllowedAge = 100;

        var today = DateOnly.FromDateTime(DateTime.Now);
        var maxDateOfBirth = today.AddYears(-minAllowedAge);
        var minDateOfBirth = today.AddYears(-maxAllowedAge);

        return dateOfBirth >= minDateOfBirth && dateOfBirth <= maxDateOfBirth;
    }
}
