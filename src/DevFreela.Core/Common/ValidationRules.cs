namespace DevFreela.Core.Common
{
    public static class ValidationRules
    {
        public const string EmailRegexPattern = @"^(?!\.)(?!.*\.\.)([a-z0-9_'+\-\.]*)[a-z0-9_+-]@([a-z0-9][a-z0-9\-]*\.)+[a-z]{2,}$";
        public const string PasswordRegexPattern = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        public const int ProjectDescriptionMaximumLength = 255;
        public const int ProjectTitleMaximumLength = 30;

        public static DateOnly UserBirthDateMinimumValue { get; } = new DateOnly(1900, 1, 1);
        public static DateOnly UserBirthDateMaximumValue { get; } = DateOnly.FromDateTime(DateTime.Now.AddYears(-18)); // At least 18yo
    }
}
