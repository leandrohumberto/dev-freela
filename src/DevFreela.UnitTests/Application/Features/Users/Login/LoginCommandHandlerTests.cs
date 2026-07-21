using DevFreela.Application.Features.Users.Login;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using DevFreela.UnitTests.Common.TestDataSources;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Users.Login
{
    public class LoginCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsValid_Login_Success_NSubstitute()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(user));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);

            var hasher = Substitute.For<IPasswordHasher>();
            hasher.Verify(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var authentication = Substitute.For<IAuthenticationService>();
            authentication.GenerateJwtToken(user).Returns(FakeDataHelper.Faker.Random.AlphaNumeric(20));

            var command = FakeDataHelper.CreateFakeLoginCommand();
            var handler = new LoginCommandHandler(unitOfWork, hasher, authentication);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<LoginResponse>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.Should().BeOfType<LoginResponse>();
            await repository.Received(1).GetByEmailAsync(Arg.Any<string>());
            hasher.Received(1).Verify(Arg.Any<string>(), Arg.Any<string>());
            authentication.Received(1).GenerateJwtToken(user);

        }

        [Fact]
        public async Task InputDataIsValid_Login_Success_Moq()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();

            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(user));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);

            var hasher = Mock.Of<IPasswordHasher>(h =>
                h.Verify(It.IsAny<string>(), It.IsAny<string>()));

            var authentication = Mock.Of<IAuthenticationService>(a =>
                a.GenerateJwtToken(user) == FakeDataHelper.Faker.Random.AlphaNumeric(20));

            var command = FakeDataHelper.CreateFakeLoginCommand();
            var handler = new LoginCommandHandler(unitOfWork, hasher, authentication);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<LoginResponse>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.Should().BeOfType<LoginResponse>();
            Mock.Get(repository).Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(hasher).Verify(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Mock.Get(authentication).Verify(a => a.GenerateJwtToken(user), Times.Once);
        }

        [Theory]
        [ClassData(typeof(InvalidLoginTestDataSource))]
        public async Task InputDataIsNotValid_Login_Failure_NSubstitute(User? user, bool match)
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult(user));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Users.Returns(repository);

            var hasher = Substitute.For<IPasswordHasher>();
            hasher.Verify(Arg.Any<string>(), Arg.Any<string>()).Returns(match);

            var authentication = Substitute.For<IAuthenticationService>();
            authentication.GenerateJwtToken(user!).Returns(FakeDataHelper.Faker.Random.AlphaNumeric(20));

            var command = FakeDataHelper.CreateFakeLoginCommand();
            var handler = new LoginCommandHandler(unitOfWork, hasher, authentication);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.False(string.IsNullOrWhiteSpace(result.Error));

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            string.IsNullOrWhiteSpace(result.Error).Should().BeFalse();
            await repository.Received(1).GetByEmailAsync(Arg.Any<string>());
            hasher.Received(user is null ? 0 : 1).Verify(Arg.Any<string>(), Arg.Any<string>());
            authentication.DidNotReceive().GenerateJwtToken(user!);
        }

        [Theory]
        [ClassData(typeof(InvalidLoginTestDataSource))]
        public async Task InputDataIsNotValid_Login_Failure_Moq(User? user, bool match)
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(user));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Users == repository);

            var hasher = Mock.Of<IPasswordHasher>(h =>
                h.Verify(It.IsAny<string>(), It.IsAny<string>()) == match);

            var authentication = Mock.Of<IAuthenticationService>(a =>
                a.GenerateJwtToken(user!) == FakeDataHelper.Faker.Random.AlphaNumeric(20));

            var command = FakeDataHelper.CreateFakeLoginCommand();
            var handler = new LoginCommandHandler(unitOfWork, hasher, authentication);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.False(string.IsNullOrWhiteSpace(result.Error));

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            string.IsNullOrWhiteSpace(result.Error).Should().BeFalse();
            Mock.Get(repository).Verify(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(hasher).Verify(
                h => h.Verify(It.IsAny<string>(), It.IsAny<string>()),
                user is null ? Times.Never : Times.Once);
            Mock.Get(authentication).Verify(a => a.GenerateJwtToken(user!), Times.Never);
        }
    }
}
