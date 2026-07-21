using DevFreela.Application.Features.Projects.DeleteProject;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Projects.DeleteProject
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Delete_Success_NSubstitute()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(true);
            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<Project?>(project));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeDeleteProjectCommand();
            var handler = new DeleteProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.True(project.Deleted);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            project.Deleted.Should().BeTrue();
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.Received(1).GetByIdAsync(Arg.Any<Guid>());
            await unitOfWork.Received(1).CompleteAsync();
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(false);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);

            var command = FakeDataHelper.CreateFakeDeleteProjectCommand();
            var handler = new DeleteProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsFailure);
            //Assert.False(result.IsSuccess);
            //Assert.Equal(ValidationRules.ProjectNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ValidationRules.ProjectNotFoundValidationMessage);
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
            await unitOfWork.DidNotReceive().CompleteAsync();
        }

        [Fact]
        public async Task ProjectExists_Delete_Success_Moq()
        {
            // Arrange
            var project = new Project(
                "Project Title",
                "Project Description",
                Guid.NewGuid(),
                Guid.NewGuid(),
                ValidationRules.ProjectTotalCostMinimumValue);

            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(true) &&
                r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(project));
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Projects == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeDeleteProjectCommand();
            var handler = new DeleteProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.True(project.Deleted);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            project.Deleted.Should().BeTrue();
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Projects == repository);

            var command = FakeDataHelper.CreateFakeDeleteProjectCommand();
            var handler = new DeleteProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsFailure);
            //Assert.False(result.IsSuccess);
            //Assert.Equal(ValidationRules.ProjectNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(ValidationRules.ProjectNotFoundValidationMessage);
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Never);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
        }
    }
}
