using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;

namespace DevFreela.UnitTests.Core.Entities
{
    public class UserTests
    {
        public static IEnumerable<object[]> GetDateOnly()
        {
            yield return new object[] { ValidationRules.UserBirthDateMinimumValue.AddDays(-FakeDataHelper.Faker.Random.Int(min:1, max: 1000)) };
            yield return new object[] { ValidationRules.UserBirthDateMaximumValue.AddDays(FakeDataHelper.Faker.Random.Int(min: 1, max: 1000)) };
        }

        [Fact]
        public void InputDataAreOk_NewObject_Success()
        {
            // Arrange
            var fullName = FakeDataHelper.Faker.Person.FullName;
            var email = FakeDataHelper.Faker.Person.Email;
            var birthDate = FakeDataHelper.Faker.Date.BetweenDateOnly(
                ValidationRules.UserBirthDateMinimumValue,
                ValidationRules.UserBirthDateMaximumValue);
            var password = FakeDataHelper.Faker.Internet.PasswordCustom(
                minLength: ValidationRules.UserPasswordMinimumLength,
                maxLength: ValidationRules.UserPasswordMaximumLength);
            var role = FakeDataHelper.Faker.PickRandom<UserRole>();

            // Act
            var user = new User(
                fullName,
                email,
                birthDate,
                password,
                role);

            // Assert
            //Assert.Equal(fullName, user.FullName);
            //Assert.Equal(email, user.Email);
            //Assert.Equal(birthDate, user.BirthDate);
            //Assert.NotNull(user.OwnedProjects);
            //Assert.NotNull(user.FreelanceProjects);
            //Assert.NotNull(user.Comments);
            //Assert.NotNull(user.Skills);

            user.FullName.Should().Be(fullName);
            user.Email.Should().Be(email);
            user.BirthDate.Should().Be(birthDate);
            user.OwnedProjects.Should().NotBeNull();
            user.FreelanceProjects.Should().NotBeNull();
            user.Comments.Should().NotBeNull();
            user.Skills.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void InvalidFullName_NewObject_ThrowsArgumentException(string fullName)
        {
            // Arrange
            var birthDate = FakeDataHelper.Faker.Date.BetweenDateOnly(
                ValidationRules.UserBirthDateMinimumValue,
                ValidationRules.UserBirthDateMaximumValue);
            var email = FakeDataHelper.Faker.Person.Email;
            var password = FakeDataHelper.Faker.Internet.PasswordCustom(
                minLength: ValidationRules.UserPasswordMinimumLength,
                maxLength: ValidationRules.UserPasswordMaximumLength);
            var role = FakeDataHelper.Faker.PickRandom<UserRole>();

            // Act + Assert
            var createUser = () =>
            {
                _ = new User(
                fullName,
                email,
                birthDate,
                password,
                role);
            };

            //var exception = Assert.Throws<ArgumentException>(createUser);
            //Assert.Contains(ValidationRules.RequiredUserFullNameValidationMessage, exception.Message);

            createUser.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.RequiredUserFullNameValidationMessage}*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("mail_mail.com")]
        [InlineData("")]
        [InlineData("   ")]
        public void InvalidEmail_NewObject_ThrowsArgumentException(string email)
        {
            // Arrange
            var fullName = FakeDataHelper.Faker.Person.FullName;
            var birthDate = FakeDataHelper.Faker.Date.BetweenDateOnly(ValidationRules.UserBirthDateMinimumValue, ValidationRules.UserBirthDateMaximumValue);
            var password = FakeDataHelper.Faker.Internet.PasswordCustom(
                minLength: ValidationRules.UserPasswordMinimumLength,
                maxLength: ValidationRules.UserPasswordMaximumLength);
            var role = FakeDataHelper.Faker.PickRandom<UserRole>();

            // Act + Assert
            var createNullEmailUser = () =>
            {
                _ = new User(
                fullName,
                email,
                birthDate,
                password,
                role);
            };

            //var exception = Assert.Throws<ArgumentException>(createNullEmailUser);
            //Assert.Contains(ValidationRules.InvalidUserEmailValidationMessage, exception.Message);

            createNullEmailUser.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.InvalidUserEmailValidationMessage}*");
        }

        [Theory]
        [MemberData(nameof(GetDateOnly))]
        public void InvalidBirthDate_NewObject_ThrowsArgumentException(DateOnly birthDate)
        {
            // Arrange
            var fullName = FakeDataHelper.Faker.Person.FullName;
            var email = FakeDataHelper.Faker.Person.Email;
            var password = FakeDataHelper.Faker.Internet.PasswordCustom(
                minLength: ValidationRules.UserPasswordMinimumLength,
                maxLength: ValidationRules.UserPasswordMaximumLength);
            var role = FakeDataHelper.Faker.PickRandom<UserRole>();

            // Act + Assert
            var createUser = () =>
            {
                _ = new User(
                fullName,
                email,
                birthDate,
                password,
                role);
            };

            //var exception = Assert.Throws<ArgumentException>(createUser);
            //Assert.Contains(ValidationRules.InvalidUserBirthDateValidationMessage, exception.Message);

            createUser.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.InvalidUserBirthDateValidationMessage}*");
        }

        [Fact]
        public void InputDataAreOk_UpdatePasswordHash_Success()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();
            var newPasswordHash = FakeDataHelper.Faker.Internet.PasswordCustom(
                minLength: ValidationRules.UserPasswordMinimumLength,
                maxLength: ValidationRules.UserPasswordMaximumLength);

            // Act
            user.UpdatePasswordHash(newPasswordHash);

            // Assert
            //Assert.Equal(newPasswordHash, user.PasswordHash);

            user.PasswordHash.Should().Be(newPasswordHash);
        }
    }
}
