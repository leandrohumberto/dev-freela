using DevFreela.Core.Enums;

namespace DevFreela.Core.Common
{
    public static class ValidationRules
    {
        // Default Values
        public const string EmailRegexPattern = @"^(?!\.)(?!.*\.\.)([a-z0-9_'+\-\.]*)[a-z0-9_+-]@([a-z0-9][a-z0-9\-]*\.)+[a-z]{2,}$";
        public const string PasswordRegexPattern = @"^.*(?=.{8,72})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        public const int UserPasswordMinimumLength = 8;
        public const int UserPasswordMaximumLength = 72;
        public const int ProjectDescriptionMaximumLength = 255;
        public const int ProjectTitleMaximumLength = 30;
        public static DateOnly UserBirthDateMinimumValue { get; } = new(1900, 1, 1);
        public static DateOnly UserBirthDateMaximumValue { get; } = DateOnly.FromDateTime(DateTime.Now.AddYears(-18)); // At least 18yo
        public static decimal ProjectTotalCostMinimumValue { get; } = 10000m;

        // Validation Messages
        public static string InvalidProjectStateValidationMessage { get; } = "Project is in invalid state.";
        public static string InvalidUserEmailValidationMessage { get; } = "Email is not in a valid format.";
        public static string RequiredProjectTitleValidationMessage { get; } = "Project title is required.";
        public static string RequiredProjectDescriptionValidationMessage { get; } = "Project title is required.";
        public static string RequiredUserFullNameValidationMessage { get; } = "Full name is required.";
        public static string RequiredSkillDescriptionValidationMessage { get; } = "Description is required.";
        public static string RequiredProjectCommentContentDescriptionValidationMessage { get; } = "Content is required.";
        public static string RequiredSkillIdsValidationMessage { get; } = "List of Skill Ids is required.";
        public static string ProjectNotFoundValidationMessage { get; } = "Project not found.";
        public static string UserNotFoundValidationMessage { get; } = "User not found.";
        public static string SkillNotFoundValidationMessage { get; } = "Skill not found.";
        public static string ClientOrFreelancerNotFoundValidationMessage { get; } = "Client or Freelancer not found.";

        public static string InvalidProjectTotalCostMinimumValueValidationMessage { get; } = $"Total Cost must be greater than {ProjectTotalCostMinimumValue}.";
        public static string InvalidUserBirthDateValidationMessage { get; } = $"Birth date must be between {UserBirthDateMinimumValue} and {UserBirthDateMaximumValue}.";
        public static string InvalidUserPasswordFormatValidationMessage { get; } = $"Password must be between {UserPasswordMinimumLength} and {UserPasswordMaximumLength} characters long, and contain a number, a lower case letter, uppercase letter, and a special character.";
        public static string InvalidUserPasswordMaximumByteLengthValidationMessage { get; } = $"Password exceeds the maximum allowed secure size. Please reduce the number of special characters or symbols used.";
        public static string InvalidUserRoleValidationMessage { get; } = $"Invalid user role. Must be one of the following options: {string.Join(", ", Enum.GetNames(typeof(UserRole)))}";
        public static string InvalidResetPasswordCodeValidationMessage { get; } = "Invalid Reset Code.";
        public static string UserAlreadyExistsValidationMessage { get; } = "User already exists.";
        public static string SkillAlreadyExistsValidationMessage { get; } = "Skill already exists.";
    }
}
