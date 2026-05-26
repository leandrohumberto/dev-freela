using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTests.Common.Helpers;
using DevFreela.UnitTests.Common.TestDataSources;
using FluentAssertions;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void InputDataAreOk_NewObject_Success()
        {
            // Arrange
            var projectTitle = FakeDataHelper.Faker.Commerce.ProductName();
            var projectDescription = FakeDataHelper.Faker.Lorem.Sentence();
            var clientId = FakeDataHelper.Faker.Random.Guid();
            var freelancerId = FakeDataHelper.Faker.Random.Guid();
            var totalCost = FakeDataHelper.GetProjectTotalCostValue();

            // Act
            var project = new Project(
                projectTitle,
                projectDescription,
                clientId,
                freelancerId,
                totalCost);

            // Assert
            Assert.Equal(projectTitle, project.Title);
            Assert.Equal(projectDescription, project.Description);
            Assert.Equal(clientId, project.ClientId);
            Assert.Equal(freelancerId, project.FreelancerId);
            Assert.Equal(totalCost, project.TotalCost);

            project.Title.Should().Be(projectTitle);
            project.Description.Should().Be(projectDescription);
            project.ClientId.Should().Be(clientId);
            project.FreelancerId.Should().Be(freelancerId);
            project.TotalCost.Should().Be(totalCost);
        }

        [Theory]
        [ClassData(typeof(InvalidStringsTestDataSource))]
        public void InvalidTitle_NewObject_ThrowsArgumentException(string title)
        {
            // Arrange
            var projectDescription = FakeDataHelper.Faker.Lorem.Sentence();
            var clientId = FakeDataHelper.Faker.Random.Guid();
            var freelancerId = FakeDataHelper.Faker.Random.Guid();
            var totalCost = FakeDataHelper.GetProjectTotalCostValue();

            // Act + Assert
            var createProject = () =>
            {
                _ = new Project(
                title,
                projectDescription,
                clientId,
                freelancerId,
                totalCost);
            };

            //var exception = Assert.Throws<ArgumentException>(createProject);
            //Assert.Contains(ValidationRules.RequiredProjectTitleValidationMessage, exception.Message);
            createProject.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.RequiredProjectTitleValidationMessage}*");
        }

        [Theory]
        [ClassData(typeof(InvalidStringsTestDataSource))]
        public void InvalidDescription_NewObject_ThrowsArgumentException(string description)
        {
            // Arrange
            var projectTitle = FakeDataHelper.Faker.Commerce.ProductName();
            var clientId = FakeDataHelper.Faker.Random.Guid();
            var freelancerId = FakeDataHelper.Faker.Random.Guid();
            var totalCost = FakeDataHelper.GetProjectTotalCostValue();

            // Act + Assert
            var createProject = () =>
            {
                _ = new Project(
                projectTitle,
                description,
                clientId,
                freelancerId,
                totalCost);
            };

            var exception = Assert.Throws<ArgumentException>(createProject);
            Assert.Contains(ValidationRules.RequiredProjectTitleValidationMessage, exception.Message);
        }

        [Fact]
        public void InvalidTotalCost_NewObject_ThrowsArgumentException()
        {
            // Arrange
            var projectTitle = FakeDataHelper.Faker.Commerce.ProductName();
            var projectDescription = FakeDataHelper.Faker.Lorem.Sentence();
            var clientId = FakeDataHelper.Faker.Random.Guid();
            var freelancerId = FakeDataHelper.Faker.Random.Guid();
            const decimal lessThanMinimumTotalCost = decimal.MinValue;

            // Act + Assert
            var createInvalidTotalCostProject = () =>
            {
                _ = new Project(
                projectTitle,
                projectDescription,
                clientId,
                freelancerId,
                lessThanMinimumTotalCost);
            };

            //Assert.True(lessThanMinimumTotalCost < ValidationRules.ProjectTotalCostMinimumValue);

            //var exception = Assert.Throws<ArgumentException>(createInvalidTotalCostProject);
            //Assert.Contains(ValidationRules.InvalidProjectTotalCostMinimumValueValidationMessage, exception.Message);

            lessThanMinimumTotalCost.Should().BeLessThan(ValidationRules.ProjectTotalCostMinimumValue);

            createInvalidTotalCostProject.Should().Throw<ArgumentException>().WithMessage($"*{ValidationRules.InvalidProjectTotalCostMinimumValueValidationMessage}*");
        }

        [Fact]
        public void ProjectCreated_Starts_Success()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            // Act
            project.Start();

            // Assert
            //Assert.Equal(ProjectStatus.InProgress, project.Status);
            //Assert.NotNull(project.StartedAt);

            project.Status.Should().Be(ProjectStatus.InProgress);
            project.StartedAt.Should().NotBeNull();
        }

        [Fact]
        public void ProjectIsInInvalidState_Starts_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();
            project.Start(); // Status "In Progress"

            // Act & Assert
            var start = project.Start;
            //Assert.NotEqual(ProjectStatus.Created, project.Status);
            //var exception = Assert.Throws<InvalidOperationException>(start);
            //Assert.Equal(ValidationRules.InvalidProjectStateValidationMessage, exception.Message);

            project.Status.Should().NotBe(ProjectStatus.Created);
            start.Should().Throw<InvalidOperationException>().WithMessage($"*{ValidationRules.InvalidProjectStateValidationMessage}*");
        }

        [Fact]
        public void ProjectIsPendingPaymentOrInProgress_Completed_Success()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();
            project.Start();

            // Act
            project.Complete();

            // Assert
            //Assert.Equal(ProjectStatus.Completed, project.Status);
            //Assert.NotNull(project.CompletedAt);

            project.Status.Should().Be(ProjectStatus.Completed);
            project.CompletedAt.Should().NotBeNull();
        }

        [Fact]
        public void ProjectIsInInvalidState_Completed_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            // Act & Assert
            var complete = project.Complete;
            //Assert.NotEqual(ProjectStatus.PendingPayment, project.Status);
            //Assert.NotEqual(ProjectStatus.InProgress, project.Status);
            //var exception = Assert.Throws<InvalidOperationException>(complete);
            //Assert.Equal(ValidationRules.InvalidProjectStateValidationMessage, exception.Message);

            project.Status.Should().NotBe(ProjectStatus.PendingPayment).And
                .NotBe(ProjectStatus.InProgress);
            complete.Should().Throw<InvalidOperationException>().WithMessage($"*{ValidationRules.InvalidProjectStateValidationMessage}*");
        }

        [Fact]
        public void ProjectIsInProgressOrSuspended_Cancelled_Success()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();
            project.Start();

            // Act
            project.Cancel();

            // Assert
            //Assert.Equal(ProjectStatus.Cancelled, project.Status);

            project.Status.Should().Be(ProjectStatus.Cancelled);
        }

        [Fact]
        public void ProjectIsInInvalidState_Cancelled_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            // Act & Assert
            var cancel = project.Cancel;
            //Assert.NotEqual(ProjectStatus.InProgress, project.Status);
            //Assert.NotEqual(ProjectStatus.Suspended, project.Status);
            //var exception = Assert.Throws<InvalidOperationException>(cancel);
            //Assert.Equal(ValidationRules.InvalidProjectStateValidationMessage, exception.Message);

            project.Status.Should().NotBe(ProjectStatus.InProgress);
            project.Status.Should().NotBe(ProjectStatus.Suspended);
            cancel.Should().Throw<InvalidOperationException>().WithMessage($"{ValidationRules.InvalidProjectStateValidationMessage}");
        }

        [Fact]
        public void ProjectIsInProgress_SetPaymentPending_Success()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();
            project.Start();

            // Act
            project.SetPaymentPending();

            // Assert
            //Assert.Equal(ProjectStatus.PendingPayment, project.Status);

            project.Status.Should().Be(ProjectStatus.PendingPayment);
        }

        [Fact]
        public void ProjectIsInInvalidState_SetPaymentPending_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            // Act & Assert
            var setPaymentPending = project.SetPaymentPending;
            //Assert.NotEqual(ProjectStatus.InProgress, project.Status);
            //var exception = Assert.Throws<InvalidOperationException>(setPaymentPending);
            //Assert.Equal(ValidationRules.InvalidProjectStateValidationMessage, exception.Message);

            project.Status.Should().NotBe(ProjectStatus.InProgress);
            setPaymentPending.Should().Throw<InvalidOperationException>().WithMessage($"*{ValidationRules.InvalidProjectStateValidationMessage}*");
        }

        [Fact]
        public void ProjectIsInstantiated_Updated_Success()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();
            var newTitle = FakeDataHelper.Faker.Commerce.ProductName();
            var newDescription = FakeDataHelper.Faker.Lorem.Sentence();
            const decimal newTotalCost = 100m;

            // Act
            project.Update(newTitle, newDescription, newTotalCost);

            // Assert
            //Assert.Equal(newTitle, project.Title);
            //Assert.Equal(newDescription, project.Description);
            //Assert.Equal(newTotalCost, project.TotalCost);

            project.Title.Should().Be(newTitle);
            project.Description.Should().Be(newDescription);
            project.TotalCost.Should().Be(newTotalCost);
        }

        [Theory]
        [ClassData(typeof(ValidProjectCommentsTestDataSource))]
        public void ProjectIsInstantiated_Deleted_Success(List<ProjectComment> comments)
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();
            project.Comments.AddRange(comments);

            // Act
            project.Delete();

            // Assert
            //Assert.True(project.Deleted);
            //Assert.All(project.Comments, c => Assert.True(c.Deleted));

            project.Deleted.Should().BeTrue();
            project.Comments.Should().AllSatisfy(c => c.Deleted.Should().BeTrue());
        }
    }
}
