using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.UnitTests.Common.Helpers;
using DevFreela.UnitTests.Common.TestDataSources;
using FluentAssertions;

namespace DevFreela.UnitTests.Core.Entities
{
    public class SkillTests
    {
        [Fact]
        public void InputDataAreOk_NewObject_Success()
        {
            // Arrange
            var description = FakeDataHelper.Faker.Name.JobTitle();

            // Act
            var skill = new Skill(description);

            // Assert
            //Assert.Equal(description, skill.Description);
            //Assert.NotNull(skill.UserSkills);

            skill.Description.Should().Be(description);
            skill.UserSkills.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(InvalidStringsTestDataSource))]
        public void InvalidDescription_NewObject_ThrowsArgumentException(string description)
        {
            // Arrange + Act + Assert
            var createSkill = () =>
            {
                _ = new Skill(description);
            };

            //var exception = Assert.Throws<ArgumentException>(createSkill);
            //Assert.Contains(ValidationRules.RequiredSkillDescriptionValidationMessage, exception.Message);

            createSkill.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.RequiredSkillDescriptionValidationMessage}*");
        }
    }
}
