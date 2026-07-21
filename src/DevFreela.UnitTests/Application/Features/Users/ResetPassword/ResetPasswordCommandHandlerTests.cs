using DevFreela.Application.Features.Users.ResetPassword;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Users.ResetPassword
{
    public class ResetPasswordCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsValid_ResetPassword_Success_NSubstitute()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(user));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));
            var resetService = Substitute.For<IPasswordResetService>();
            resetService.ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            var hasher = Substitute.For<IPasswordHasher>();
            var hash = FakeDataHelper.Faker.Random.Hash();
            hasher.Hash(Arg.Any<string>()).Returns(hash);

            var command = FakeDataHelper.CreateFakeResetPasswordCommand();
            var handler = new ResetPasswordCommandHandler(unitOfWork, resetService, hasher);

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
            await repository.Received(1).GetByEmailAsync(Arg.Any<string>());
            await unitOfWork.Received(1).CompleteAsync();
            resetService.Received(1).ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>());
            hasher.Received(1).Hash(Arg.Any<string>());
        }

        [Fact]
        public async Task InputDataIsValid_ResetPassword_Success_Moq()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<User?>(user));
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Users == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));
            var resetService = Mock.Of<IPasswordResetService>(r =>
                r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()));
            var hash = FakeDataHelper.Faker.Random.Hash();
            var hasher = Mock.Of<IPasswordHasher>(h => h.Hash(It.IsAny<string>()) == hash);

            var command = FakeDataHelper.CreateFakeResetPasswordCommand();
            var handler = new ResetPasswordCommandHandler(unitOfWork, resetService, hasher);

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
            Mock.Get(repository).Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once);
            Mock.Get(resetService).Verify(r => r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Mock.Get(hasher).Verify(h => h.Hash(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UserNotFound_ResetPassword_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(null!));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);
            var resetService = Substitute.For<IPasswordResetService>();
            var hasher = Substitute.For<IPasswordHasher>();

            var command = FakeDataHelper.CreateFakeResetPasswordCommand();
            var handler = new ResetPasswordCommandHandler(unitOfWork, resetService, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.UserNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.UserNotFoundValidationMessage);
            await repository.Received(1).GetByEmailAsync(Arg.Any<string>());
            await unitOfWork.DidNotReceive().CompleteAsync();
            resetService.DidNotReceive().ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>());
            hasher.DidNotReceive().Hash(Arg.Any<string>());
        }

        [Fact]
        public async Task UserNotFound_ResetPassword_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<User?>(null!));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);
            var resetService = Mock.Of<IPasswordResetService>(r =>
                r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()));
            var hash = FakeDataHelper.Faker.Random.Hash();
            var hasher = Mock.Of<IPasswordHasher>(h => h.Hash(It.IsAny<string>()) == hash);

            var command = FakeDataHelper.CreateFakeResetPasswordCommand();
            var handler = new ResetPasswordCommandHandler(unitOfWork, resetService, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.UserNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.UserNotFoundValidationMessage);
            Mock.Get(repository).Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
            Mock.Get(resetService).Verify(r => r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Mock.Get(hasher).Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ResetCodeIsNotValid_ResetPassword_Failure_NSubstitute()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(user));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);
            var resetService = Substitute.For<IPasswordResetService>();
            resetService.ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>()).Returns(false);
            var hasher = Substitute.For<IPasswordHasher>();

            var command = FakeDataHelper.CreateFakeResetPasswordCommand();
            var handler = new ResetPasswordCommandHandler(unitOfWork, resetService, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.UserNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.InvalidResetPasswordCodeValidationMessage);
            await repository.Received(1).GetByEmailAsync(Arg.Any<string>());
            await unitOfWork.DidNotReceive().CompleteAsync();
            resetService.Received(1).ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>());
            hasher.DidNotReceive().Hash(Arg.Any<string>());
        }

        [Fact]
        public async Task ResetCodeIsNotValid_ResetPassword_Failure_Moq()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<User?>(user));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);
            var resetService = Mock.Of<IPasswordResetService>(r =>
                !r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()));
            var hash = FakeDataHelper.Faker.Random.Hash();
            var hasher = Mock.Of<IPasswordHasher>(h => h.Hash(It.IsAny<string>()) == hash);

            var command = FakeDataHelper.CreateFakeResetPasswordCommand();
            var handler = new ResetPasswordCommandHandler(unitOfWork, resetService, hasher);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.UserNotFoundValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.InvalidResetPasswordCodeValidationMessage);
            Mock.Get(repository).Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
            Mock.Get(resetService).Verify(r => r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Mock.Get(hasher).Verify(h => h.Hash(It.IsAny<string>()), Times.Never);
        }
    }
}
