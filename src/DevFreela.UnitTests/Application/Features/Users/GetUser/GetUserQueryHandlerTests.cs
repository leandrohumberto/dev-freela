using DevFreela.Application.Features.Users.GetUser;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Users.GetUser
{
    public class GetUserQueryHandlerTests
    {
        [Fact]
        public async Task UserExists_Get_Success_NSubstitute()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();
            var repository = Substitute.For<IUserRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(Task.FromResult(true));
            repository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(user));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);

            var query = FakeDataHelper.CreateFakeGetUserQuery();
            var handler = new GetUserQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<GetUserResponse>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.Should().BeOfType<GetUserResponse>();
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.Received(1).GetByIdAsync(Arg.Any<Guid>());
        }

        [Fact]
        public async Task UserDoesNotExist_Get_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(Task.FromResult(false));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);

            var query = FakeDataHelper.CreateFakeGetUserQuery();
            var handler = new GetUserQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.UserNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.UserNotFoundValidationMessage);
            await repository.Received(1).ExistsAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
        }

        [Fact]
        public async Task UserExists_Get_Success_Moq()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Mock.Of<IUserRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(true) &&
                r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<User?>(user));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);

            var query = FakeDataHelper.CreateFakeGetUserQuery();
            var handler = new GetUserQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<GetUserResponse>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.Should().BeOfType<GetUserResponse>();
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UserDoesNotExist_Get_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);

            var query = FakeDataHelper.CreateFakeGetUserQuery();
            var handler = new GetUserQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.UserNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.UserNotFoundValidationMessage);
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), CancellationToken.None), Times.Never);
        }
    }
}
