using DevFreela.Application.Features.Users.GetUser;
using DevFreela.Application.Features.Users.RequestPasswordReset;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Features.Users.RequestPasswordReset
{
    public class RequestPasswordResetCommandHandlerTests
    {
        [Fact]
        public async Task UserExists_RequestPasswordRequest_Success_NSubstitute()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();
            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(user));
            var resetService = Substitute.For<IPasswordResetService>();
            resetService.SendPasswordResetCodeAsync(Arg.Any<string>()).Returns(Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeRequestPasswordResetCommand();
            var handler = new RequestPasswordResetCommandHandler(repository, resetService);

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
            await resetService.Received(1).SendPasswordResetCodeAsync(Arg.Any<string>());
        }


        [Fact]
        public async Task UserDoesNotExist_RequestPasswordRequest_Faiure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.GetByEmailAsync(Arg.Any<string>()).Returns(Task.FromResult<User?>(null!));
            var resetService = Substitute.For<IPasswordResetService>();

            var command = FakeDataHelper.CreateFakeRequestPasswordResetCommand();
            var handler = new RequestPasswordResetCommandHandler(repository, resetService);

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
            await resetService.DidNotReceive().SendPasswordResetCodeAsync(Arg.Any<string>());
        }


        [Fact]
        public async Task UserExists_RequestPasswordRequest_Success_Moq()
        {
            // Arrange
            var user = FakeDataHelper.CreateFakeUser();
            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<User?>(user));
            var resetService = Mock.Of<IPasswordResetService>(r =>
                r.SendPasswordResetCodeAsync(It.IsAny<string>(), CancellationToken.None) == Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeRequestPasswordResetCommand();
            var handler = new RequestPasswordResetCommandHandler(repository, resetService);

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
            Mock.Get(resetService).Verify(r => r.SendPasswordResetCodeAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task UserDoesNotExist_RequestPasswordRequest_Faiure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult<User?>(null!));
            var resetService = Mock.Of<IPasswordResetService>(r =>
                r.SendPasswordResetCodeAsync(It.IsAny<string>(), CancellationToken.None) == Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeRequestPasswordResetCommand();
            var handler = new RequestPasswordResetCommandHandler(repository, resetService);

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
            Mock.Get(resetService).Verify(r =>
                r.SendPasswordResetCodeAsync(It.IsAny<string>(), CancellationToken.None), Times.Never);
        }
    }
}
