using DevFreela.Core.Common;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Features.Users.CreateUser
{
    public partial class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email)
             .Must(ValidEmail)
             .WithMessage($"'{nameof(CreateUserCommand.Email)}' is not a valid email address.");

            RuleFor(u => u.FullName)
                .NotEmpty();

            RuleFor(u => u.BirthDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(ValidationRules.UserBirthDateMinimumValue)
                .LessThanOrEqualTo(ValidationRules.UserBirthDateMaximumValue);

            //RuleFor(u => u.Password)
            //    .Must(ValidPassword)
            //    .WithMessage("Password must be at least 8-character long, and contain a number, " +
            //        "a lower case letter, uppercase letter, and a special character.");
        }

        private static bool ValidEmail(string email)
        {
            var regex = EmailRegex();
            return regex.IsMatch(email);
        }

        [GeneratedRegex(ValidationRules.EmailRegexPattern, RegexOptions.IgnoreCase)]
        private static partial Regex EmailRegex();

        //private static bool ValidPassword(string password)
        //{
        //    var regex = PasswordRegex();
        //    return regex.IsMatch(password);
        //}

        //[GeneratedRegex(ValidationRules.PasswordRegexPattern)]
        //private static partial Regex PasswordRegex();
    }
}
