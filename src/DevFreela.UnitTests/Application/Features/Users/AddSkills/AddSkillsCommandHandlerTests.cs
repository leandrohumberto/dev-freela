using DevFreela.Application.Features.Users.AddSkills;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace DevFreela.UnitTests.Application.Features.Users.AddSkills
{
    public class AddSkillsCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsValid_AddSkill_Success_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(Task.FromResult(true));
            repository.AddSkillsAsync(Arg.Any<List<UserSkill>>()).Returns(Task.CompletedTask);
            repository.SaveChangesAsync().Returns(Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeAddSkillsCommand();
            var handler = new AddSkillsCommandHandler(repository);

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
            await repository.Received(1).AddSkillsAsync(Arg.Any<List<UserSkill>>());
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task InputDataIsNotValid_AddSkill_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();

            var command = FakeDataHelper.CreateFakeAddSkillsCommand(true);
            var handler = new AddSkillsCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.RequiredSkillIdsValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.RequiredSkillIdsValidationMessage);
            await repository.DidNotReceive().ExistsAsync(Arg.Any<Guid>());
            await repository.DidNotReceive().AddSkillsAsync(Arg.Any<List<UserSkill>>());
            await repository.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task UserDoesNotExist_AddSkill_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IUserRepository>();
            repository.ExistsAsync(Arg.Any<Guid>()).Returns(Task.FromResult(false));

            var command = FakeDataHelper.CreateFakeAddSkillsCommand();
            var handler = new AddSkillsCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

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
            await repository.DidNotReceive().AddSkillsAsync(Arg.Any<List<UserSkill>>());
            await repository.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task InputDataIsValid_AddSkill_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(true) &&
                r.AddSkillsAsync(It.IsAny<List<UserSkill>>(), CancellationToken.None) == Task.CompletedTask &&
                r.SaveChangesAsync(CancellationToken.None) == Task.CompletedTask);

            var command = FakeDataHelper.CreateFakeAddSkillsCommand();
            var handler = new AddSkillsCommandHandler(repository);

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
            Mock.Get(repository).Verify(r => r.AddSkillsAsync(It.IsAny<List<UserSkill>>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task InputDataIsNotValid_AddSkill_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>();

            var command = FakeDataHelper.CreateFakeAddSkillsCommand(true);
            var handler = new AddSkillsCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equal(ValidationRules.RequiredSkillIdsValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.RequiredSkillIdsValidationMessage);
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Never);
            Mock.Get(repository).Verify(r => r.AddSkillsAsync(It.IsAny<List<UserSkill>>(), CancellationToken.None), Times.Never);
            Mock.Get(repository).Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task UserDoesNotExist_AddSkill_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<IUserRepository>(r =>
                r.ExistsAsync(It.IsAny<Guid>(), CancellationToken.None) == Task.FromResult(false));

            var command = FakeDataHelper.CreateFakeAddSkillsCommand();
            var handler = new AddSkillsCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

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
            Mock.Get(repository).Verify(r => r.AddSkillsAsync(It.IsAny<List<UserSkill>>(), CancellationToken.None), Times.Never);
            Mock.Get(repository).Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
        }
    }
}
