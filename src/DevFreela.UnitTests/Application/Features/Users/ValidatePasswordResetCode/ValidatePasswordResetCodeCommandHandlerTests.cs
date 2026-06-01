using DevFreela.Application.Features.Users.ValidatePasswordResetCode;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Users.ValidatePasswordResetCode
{
    public class ValidatePasswordResetCodeCommandHandlerTests
    {
        [Fact]
        public async Task ResetCodeIsValid_ValidateResetCode_Success_NSUbstitute()
        {
            // Arrange
            var resetService = Substitute.For<IPasswordResetService>();
            resetService.ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var command = FakeDataHelper.CreateFakeValidatePasswordResetCodeCommand();
            var handler = new ValidatePasswordResetCodeCommandHandler(resetService);

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
            resetService.Received(1).ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task ResetCodeIsValid_ValidateResetCode_Success_Moq()
        {
            // Arrange
            var resetService = Mock.Of<IPasswordResetService>(r =>
                r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()));

            var command = FakeDataHelper.CreateFakeValidatePasswordResetCodeCommand();
            var handler = new ValidatePasswordResetCodeCommandHandler(resetService);

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
            Mock.Get(resetService).Verify(r => r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public async Task ResetCodeIsNotValid_ValidateResetCode_Success_NSUbstitute()
        {
            // Arrange
            var resetService = Substitute.For<IPasswordResetService>();
            resetService.ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var command = FakeDataHelper.CreateFakeValidatePasswordResetCodeCommand();
            var handler = new ValidatePasswordResetCodeCommandHandler(resetService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.InvalidResetPasswordCodeValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.InvalidResetPasswordCodeValidationMessage);
            resetService.Received(1).ValidatePasswordResetCode(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task ResetCodeIsNotValid_ValidateResetCode_Success_Moq()
        {
            // Arrange
            var resetService = Mock.Of<IPasswordResetService>(r =>
                !r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()));

            var command = FakeDataHelper.CreateFakeValidatePasswordResetCodeCommand();
            var handler = new ValidatePasswordResetCodeCommandHandler(resetService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.False(result.IsSuccess);
            //Assert.True(result.IsFailure);
            //Assert.Equals(ValidationRules.InvalidResetPasswordCodeValidationMessage, result.Error);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ValidationRules.InvalidResetPasswordCodeValidationMessage);
            Mock.Get(resetService).Verify(r => r.ValidatePasswordResetCode(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
