using DevFreela.Core.Common;
using System.Text.RegularExpressions;

namespace DevFreela.Core.Entities
{
    public partial class User : BaseEntity
    {
        public User(string fullName, string email, DateOnly birthDate)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException(ValidationRules.RequiredUserFullNameValidationMessage, nameof(fullName));

            if (birthDate > ValidationRules.UserBirthDateMaximumValue || birthDate < ValidationRules.UserBirthDateMinimumValue)
                throw new ArgumentException(ValidationRules.InvalidUserBirthDateValidationMessage, nameof(birthDate));

            // Stricter email validation including TLD check
            if (string.IsNullOrWhiteSpace(email) || !EmailRegex().IsMatch(email))
                throw new ArgumentException(ValidationRules.InvalidUserEmailValidationMessage, nameof(email));

            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            OwnedProjects = [];
            FreelanceProjects = [];
            Comments = [];
            Skills = [];
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public bool Active { get; private set; } = true;
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
        public List<UserSkill> Skills { get; private set; }

        [GeneratedRegex(ValidationRules.EmailRegexPattern, RegexOptions.IgnoreCase, "pt-BR")]
        private static partial Regex EmailRegex();
    }
}
