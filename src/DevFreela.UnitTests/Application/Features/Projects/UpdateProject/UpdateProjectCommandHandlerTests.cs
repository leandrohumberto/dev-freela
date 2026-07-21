using DevFreela.Application.Features.Projects.UpdateProject;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Projects.UpdateProject
{
    public class UpdateProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Update_Success_NSubstitute()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(Task.FromResult(true));
            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<Project?>(project));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeUpdateProjectCommand();
            var handler = new UpdateProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.Equal(command.Title, project.Title);
            //Assert.Equal(command.Description, project.Description);
            //Assert.Equal(command.TotalCost, project.TotalCost);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            project.Title.Should().Be(command.Title);
            project.Description.Should().Be(command.Description);
            project.TotalCost.Should().Be(command.TotalCost);
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.Received(1).GetByIdAsync(Arg.Any<Guid>());
            await unitOfWork.Received(1).CompleteAsync();
        }

        [Fact]
        public async Task ProjectDoesNotExist_Update_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(Task.FromResult(false));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);

            var command = FakeDataHelper.CreateFakeUpdateProjectCommand();
            var handler = new UpdateProjectCommandHandler(unitOfWork);

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
            await unitOfWork.DidNotReceive().CompleteAsync();
        }

        [Fact]
        public async Task ProjectExists_Update_Success_Moq()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(true) &&
                r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<Project?>(project));
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Projects == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeUpdateProjectCommand();
            var handler = new UpdateProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.Equal(command.Title, project.Title);
            //Assert.Equal(command.Description, project.Description);
            //Assert.Equal(command.TotalCost, project.TotalCost);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            project.Title.Should().Be(command.Title);
            project.Description.Should().Be(command.Description);
            project.TotalCost.Should().Be(command.TotalCost);
            Mock.Get(repository).Verify(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Update_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Projects == repository);

            var command = FakeDataHelper.CreateFakeUpdateProjectCommand();
            var handler = new UpdateProjectCommandHandler(unitOfWork);

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
            Mock.Get(repository).Verify(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Never);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
        }
    }
}
