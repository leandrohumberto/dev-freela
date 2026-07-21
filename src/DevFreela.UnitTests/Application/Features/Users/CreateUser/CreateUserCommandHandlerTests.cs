using DevFreela.Application.Features.Users.CreateUser;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Users.CreateUser
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataAreValid_Create_Success_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.ExistsAsync(Arg.Any<string>()).Returns(Task.FromResult(false));
            repository.AddAsync(Arg.Any<User>()).Returns(Task.CompletedTask);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));

            var hasher = Substitute.For<IPasswordHasher>();
            hasher.Hash(Arg.Any<string>()).Returns(FakeDataHelper.Faker.Random.Hash());

            var command = FakeDataHelper.CreateFakeCreateUserCommand();
            var handler = new CreateUserCommandHandler(unitOfWork, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<CreateUserResponse>(result.Value);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.GetType().Should().Be(typeof(CreateUserResponse));
            await repository.Received(1).ExistsAsync(Arg.Any<string>());
            await repository.Received(1).AddAsync(Arg.Any<User>());
            await unitOfWork.Received(1).CompleteAsync();
            hasher.Received(1).Hash(Arg.Any<string>());
        }

        [Fact]
        public async Task InputDataAreValid_Create_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(false) &&
                r.AddAsync(It.IsAny<User>(), CancellationToken.None) == Task.CompletedTask);
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Users == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));

            var hasher = Mock.Of<IPasswordHasher>(h =>
                h.Hash(It.IsAny<string>()) == FakeDataHelper.Faker.Random.Hash(40, false));

            var command = FakeDataHelper.CreateFakeCreateUserCommand();
            var handler = new CreateUserCommandHandler(unitOfWork, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<CreateUserResponse>(result.Value);

            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.GetType().Should().Be(typeof(CreateUserResponse));
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once);
            Mock.Get(hasher).Verify(h => h.Hash(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UserAlreadyExists_Create_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.ExistsAsync(Arg.Any<string>()).Returns(Task.FromResult(true));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);

            var hasher = Substitute.For<IPasswordHasher>();

            var command = FakeDataHelper.CreateFakeCreateUserCommand();
            var handler = new CreateUserCommandHandler(unitOfWork, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.UserAlreadyExistsValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.UserAlreadyExistsValidationMessage);
            await repository.Received(1).ExistsAsync(Arg.Any<string>());
            await repository.DidNotReceive().AddAsync(Arg.Any<User>());
            await unitOfWork.DidNotReceive().CompleteAsync();
            hasher.DidNotReceive().Hash(Arg.Any<string>());
        }

        [Fact]
        public async Task UserAlreadyExists_Create_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(true));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);

            var hasher = Mock.Of<IPasswordHasher>(h =>
                h.Hash(It.IsAny<string>()) == FakeDataHelper.Faker.Random.Hash(40, false));

            var command = FakeDataHelper.CreateFakeCreateUserCommand();
            var handler = new CreateUserCommandHandler(unitOfWork, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.UserAlreadyExistsValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.UserAlreadyExistsValidationMessage);
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<User>(), CancellationToken.None), Times.Never);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
            Mock.Get(hasher).Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
        }
    }
}
