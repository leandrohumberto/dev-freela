using DevFreela.Application.Features.Skills.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Skills.GetAllSkills
{
    public class GetAllSkillsQueryHandlerTests
    {
        [Fact]
        public async Task RequestReceived_GetAll_Success_NSubtitute()
        {
            // Arrange
            var repository = Substitute.For<ISkillRepository>();
            repository.GetAllAsync().Returns(Task.FromResult<List<Skill>>([]));
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Skills.Returns(repository);

            var query = FakeDataHelper.CreateFakeGetAllSkillsQuery();
            var handler = new GetAllSkillsQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<List<GetAllSkillsResponse>?>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.Should().BeOfType<List<GetAllSkillsResponse>?>();
            await repository.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task RequestReceived_GetAll_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<ISkillRepository>(r =>
                r.GetAllAsync(It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(new List<Skill>()));
            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.Skills == repository);

            var query = FakeDataHelper.CreateFakeGetAllSkillsQuery();
            var handler = new GetAllSkillsQueryHandler(unitOfWork);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));
            //Assert.IsType<List<GetAllSkillsResponse>?>(result.Value);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            result.Value.Should().BeOfType<List<GetAllSkillsResponse>?>();
            Mock.Get(repository).Verify(r => r.GetAllAsync(It.IsAny<bool>(), CancellationToken.None), Times.Once);
        }
    }
}
