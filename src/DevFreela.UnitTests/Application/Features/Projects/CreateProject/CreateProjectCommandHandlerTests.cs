using DevFreela.Application.Features.Projects.CreateProject;
using DevFreela.Application.Features.Projects.CreateProject.Notifications;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using MediatR;
using NSubstitute;
using Moq;
using FluentAssertions;
using DevFreela.UnitTests.Common.Helpers;

namespace DevFreela.UnitTests.Application.Features.Projects.CreateProject
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Success_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.AddAsync(Arg.Any<Project>()).Returns(Task.FromResult(Guid.NewGuid()));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));
            var mediator = Substitute.For<IMediator>();
            mediator.Publish(Arg.Any<ProjectCreatedNotification>()).Returns(Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeCreateProjectCommand();
            var handler = new CreateProjectCommandHandler(unitOfWork, mediator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<Guid>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.GetType().Should().Be(typeof(Guid));
            await repository.Received(1).AddAsync(Arg.Any<Project>());
            await unitOfWork.Received(1).CompleteAsync();
            await mediator.Received(1).Publish(Arg.Any<ProjectCreatedNotification>());
        }

        [Fact]
        public async Task InputDataAreOk_Insert_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r => r.AddAsync(It.IsAny<Project>(), CancellationToken.None) == Task.FromResult(Guid.NewGuid()));
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Projects == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));
            var mediator = Mock.Of<IMediator>(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), CancellationToken.None) == Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeCreateProjectCommand();

            var handler = new CreateProjectCommandHandler(unitOfWork, mediator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<Guid>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.GetType().Should().Be(typeof(Guid));
            Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<Project>(), CancellationToken.None), Times.Once());
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once());
            Mock.Get(mediator).Verify(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), CancellationToken.None), Times.Once());
        }
    }
}
