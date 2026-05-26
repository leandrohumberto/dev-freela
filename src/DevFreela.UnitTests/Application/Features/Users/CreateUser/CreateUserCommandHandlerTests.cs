using DevFreela.Application.Features.Users.CreateUser;
using DevFreela.Core.Entities;
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
            repository.AddAsync(Arg.Any<User>()).Returns(Task.CompletedTask);
            repository.SaveChangesAsync().Returns(Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeCreateUserCommand();
            var handler = new CreateUserCommandHandler(repository);

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
            await repository.Received(1).AddAsync(Arg.Any<User>());
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task InputDataAreValid_Create_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.AddAsync(It.IsAny<User>(), CancellationToken.None) == Task.CompletedTask &&
                r.SaveChangesAsync(CancellationToken.None) == Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeCreateUserCommand();
            var handler = new CreateUserCommandHandler(repository);

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
            Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
