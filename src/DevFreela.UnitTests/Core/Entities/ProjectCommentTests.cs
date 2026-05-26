using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.UnitTests.Common.Helpers;
using DevFreela.UnitTests.Common.TestDataSources;
using FluentAssertions;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectCommentTests
    {
        [Fact]
        public void InputDataAreOk_NewObject_Success()
        {
            // Arrange
            var content = FakeDataHelper.Faker.Lorem.Paragraph();
            var projectId = FakeDataHelper.Faker.Random.Guid();
            var userId = FakeDataHelper.Faker.Random.Guid();

            // Act
            var comment = new ProjectComment(content, projectId, userId);

            // Assert
            //Assert.Equal(content, comment.Content);
            //Assert.Equal(projectId, comment.ProjectId);
            //Assert.Equal(userId, comment.UserId);
            
            comment.Content.Should().Be(content);
            comment.ProjectId.Should().Be(projectId);
            comment.UserId.Should().Be(userId);
        }

        [Theory]
        [ClassData(typeof(InvalidStringsTestDataSource))]
        public void InvalidContent_NewObject_ThrowsArgumentException(string description)
        {
            // Arrange
            var projectId = FakeDataHelper.Faker.Random.Guid();
            var userId = FakeDataHelper.Faker.Random.Guid();

            // Act + Assert
            var createComment = () =>
            {
                _ = new ProjectComment(description, projectId, userId);
            };

            //var exception = Assert.Throws<ArgumentException>(createComment);
            //Assert.Contains(ValidationRules.RequiredProjectCommentContentDescriptionValidationMessage, exception.Message);
            createComment.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.RequiredProjectCommentContentDescriptionValidationMessage}*");
        }
    }
}
