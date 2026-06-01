using DevFreela.Core.Common;
using DevFreela.Core.Enums;
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
             .WithMessage(ValidationRules.InvalidUserEmailValidationMessage);

            RuleFor(u => u.FullName)
                .NotEmpty();

            RuleFor(u => u.BirthDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(ValidationRules.UserBirthDateMinimumValue)
                .LessThanOrEqualTo(ValidationRules.UserBirthDateMaximumValue);

            RuleFor(u => u.Password)
                .Must(ValidPassword)
                .WithMessage(ValidationRules.InvalidUserPasswordFormatValidationMessage)
                .DependentRules(() =>
                {
                    RuleFor(x => x.Password)
                        .Must(pass => System.Text.Encoding.UTF8.GetByteCount(pass) <= 72)
                        .WithMessage(ValidationRules.InvalidUserPasswordMaximumByteLengthValidationMessage);
                });

            RuleFor(u => u.Role)
                .IsInEnum()
                .WithMessage(ValidationRules.InvalidUserRoleValidationMessage);
        }

        private static bool ValidEmail(string email)
        {
            var regex = EmailRegex();
            return regex.IsMatch(email);
        }

        [GeneratedRegex(ValidationRules.EmailRegexPattern, RegexOptions.IgnoreCase)]
        private static partial Regex EmailRegex();

        private static bool ValidPassword(string password)
        {
            var regex = PasswordRegex();
            return regex.IsMatch(password);
        }

        [GeneratedRegex(ValidationRules.PasswordRegexPattern)]
        private static partial Regex PasswordRegex();
    }
}
