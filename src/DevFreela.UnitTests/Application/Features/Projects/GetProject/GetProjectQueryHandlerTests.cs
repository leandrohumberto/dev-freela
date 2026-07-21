using DevFreela.Application.Features.Projects.GetProject;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Projects.GetProject
{
    public class GetProjectQueryHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Get_Success_NSubstitute()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProject();

            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(true);
            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<Project?>(project));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);

            var query = FakeDataHelper.CreateFakeGetProjectQuery();
            var handler = new GetProjectQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.NotNull(result.Value);
            //Assert.IsType<GetProjectResponse>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<GetProjectResponse>();
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.Received(1).GetByIdAsync(Arg.Any<Guid>());
        }

        [Fact]
        public async Task ProjectDoesNotExist_Get_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(false);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Projects.Returns(repository);

            var query = FakeDataHelper.CreateFakeGetProjectQuery();
            var handler = new GetProjectQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.ProjectNotFoundValidationMessage, result.Error);
            //Assert.Null(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.ProjectNotFoundValidationMessage);
            result.Value.Should().BeNull();
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
        }

        [Fact]
        public async Task ProjectExists_Get_Success_Moq()
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
                r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<Project?>(project));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Projects == repository);

            var query = FakeDataHelper.CreateFakeGetProjectQuery();
            var handler = new GetProjectQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.NotNull(result.Value);
            //Assert.IsType<GetProjectResponse>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<GetProjectResponse>();
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ProjectDoesNotExist_Get_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Projects == repository);

            var query = FakeDataHelper.CreateFakeGetProjectQuery();
            var handler = new GetProjectQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.ProjectNotFoundValidationMessage, result.Error);
            //Assert.Null(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.ProjectNotFoundValidationMessage);
            result.Value.Should().BeNull();
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Never);
        }
    }
}
