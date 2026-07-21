using DevFreela.Application.Features.Projects.CommentProject;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Projects.CommentProject
{
    public class CommentProjectCommandHandlerTests
    {
        [Fact]
        public async Task ProjectExists_AddComment_Success_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(true);
            repository.AddCommentAsync(Arg.Any<ProjectComment>()).Returns(Task.CompletedTask);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeCommenteProjectCommand();
            var handler = new CommentProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.Received(1).AddCommentAsync(Arg.Any<ProjectComment>());
            await unitOfWork.Received(1).CompleteAsync();
        }

        [Fact]
        public async Task ProjectDoesNotExist_AddComment_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(false);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);

            var command = FakeDataHelper.CreateFakeCommenteProjectCommand();
            var handler = new CommentProjectCommandHandler(unitOfWork);

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
            await repository.DidNotReceive().AddCommentAsync(Arg.Any<ProjectComment>());
            await unitOfWork.DidNotReceive().CompleteAsync();
        }

        [Fact]
        public async Task ProjectExists_AddComment_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(),CancellationToken.None) == Task.FromResult(true) &&
                r.AddCommentAsync(It.IsAny<ProjectComment>(), CancellationToken.None) == Task.CompletedTask);
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Projects == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeCommenteProjectCommand();
            var handler = new CommentProjectCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.AddCommentAsync(It.IsAny<ProjectComment>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExist_AddComment_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Projects == repository);

            var command = FakeDataHelper.CreateFakeCommenteProjectCommand();
            var handler = new CommentProjectCommandHandler(unitOfWork);

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
            Mock.Get(repository).Verify(r => r.AddCommentAsync(It.IsAny<ProjectComment>(), CancellationToken.None), Times.Never);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
        }
    }
}
