using DevFreela.Application.Features.Skills.CreateSkill;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Skills.CreateSkill
{
    public class CreateSkillCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsValid_Create_Success_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<ISkillRepository>();
            repository.ExistsAsync(Arg.Any<string>()).Returns(Task.FromResult(false));
            repository.AddAsync(Arg.Any<Skill>()).Returns(Task.CompletedTask);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Skills.Returns(repository);
            unitOfWork.CompleteAsync().Returns(Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeCreateSkillCommand();
            var handler = new CreateSkillCommandHandler(unitOfWork);

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
            await repository.Received(1).ExistsAsync(Arg.Any<string>());
            await repository.Received(1).AddAsync(Arg.Any<Skill>());
            await unitOfWork.Received(1).CompleteAsync();
        }

        [Fact]
        public async Task InputDataIsValid_Create_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<ISkillRepository>(r =>
                r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(false) &&
                r.AddAsync(It.IsAny<Skill>(), CancellationToken.None) == Task.CompletedTask);
            var unitOfWork = Mock.Of<IUnitOfWork>(u =>
                u.Skills == repository &&
                u.CompleteAsync(CancellationToken.None) == Task.FromResult(1));

            var command = FakeDataHelper.CreateFakeCreateSkillCommand();
            var handler = new CreateSkillCommandHandler(unitOfWork);

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
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<Skill>(), CancellationToken.None), Times.Once);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SkillAlreadyExists_Create_Failure_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<ISkillRepository>();
            repository.ExistsAsync(Arg.Any<string>()).Returns(Task.FromResult(true));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Skills.Returns(repository);

            var command = FakeDataHelper.CreateFakeCreateSkillCommand();
            var handler = new CreateSkillCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.SkillAlreadyExistsValidationMessage, result.Error);
            //Assert.IsType<Guid>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.SkillAlreadyExistsValidationMessage);
            await repository.Received(1).ExistsAsync(Arg.Any<string>());
            await repository.DidNotReceive().AddAsync(Arg.Any<Skill>());
            await unitOfWork.DidNotReceive().CompleteAsync();
        }

        [Fact]
        public async Task SkillAlreadyExists_Create_Failure_Moq()
        {
            // Arrange
            var repository = Mock.Of<ISkillRepository>(r =>
                r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(true));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Skills == repository);

            var command = FakeDataHelper.CreateFakeCreateSkillCommand();
            var handler = new CreateSkillCommandHandler(unitOfWork);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.SkillAlreadyExistsValidationMessage, result.Error);
            //Assert.IsType<Guid>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.SkillAlreadyExistsValidationMessage);
            Mock.Get(repository).Verify(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
            Mock.Get(repository).Verify(r => r.AddAsync(It.IsAny<Skill>(), CancellationToken.None), Times.Never);
            Mock.Get(unitOfWork).Verify(u => u.CompleteAsync(CancellationToken.None), Times.Never);
        }
    }
}
