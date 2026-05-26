using DevFreela.Application.Features.Projects.CompleteProject;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Projects.CompleteProject
{
    public class CompleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Complete_Success_NSubstitute()
        {
            // Arrange
            var project = new Project(
                "Project Title",
                "Project Description",
                Guid.NewGuid(),
                Guid.NewGuid(),
                ValidationRules.ProjectTotalCostMinimumValue);
            project.Start();

            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(true);
            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<Project?>(project));
            repository.SaveChangesAsync().Returns(Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeCompleteProjectCommand();
            var handler = new CompleteProjectCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.Equal(ProjectStatus.Completed, project.Status);
            //Assert.NotNull(project.CompletedAt);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            project.Status.Should().Be(ProjectStatus.Completed);
            project.CompletedAt.Should().NotBeNull();
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.Received(1).GetByIdAsync(Arg.Any<Guid>());
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task ProjectDoesNotExist_Complete_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(false);

            var command = FakeDataHelper.CreateFakeCompleteProjectCommand();
            var handler = new CompleteProjectCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.ProjectNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.ProjectNotFoundValidationMessage);
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task ProjectExists_Complete_Success_Moq()
        {
            // Arrange
            var project = new Project(
                "Project Title",
                "Project Description",
                Guid.NewGuid(),
                Guid.NewGuid(),
                ValidationRules.ProjectTotalCostMinimumValue);
            project.Start();

            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(true) &&
                r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<Project?>(project) &&
                r.SaveChangesAsync(CancellationToken.None) == Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeCompleteProjectCommand();
            var handler = new CompleteProjectCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.Equal(ProjectStatus.Completed, project.Status);
            //Assert.NotNull(project.CompletedAt);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            project.Status.Should().Be(ProjectStatus.Completed);
            project.CompletedAt.Should().NotBeNull();
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Complete_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));

            var command = FakeDataHelper.CreateFakeCompleteProjectCommand();
            var handler = new CompleteProjectCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.ProjectNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.ProjectNotFoundValidationMessage);
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Never);
            Mock.Get(repository).Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
        }
    }
}
