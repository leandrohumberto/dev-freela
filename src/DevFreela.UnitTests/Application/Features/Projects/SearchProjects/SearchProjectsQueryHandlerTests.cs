using DevFreela.Application.Features.Projects.SearchProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Common.Helpers;
using FluentAssertions;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsQueryHandlerTests
    {
        [Fact]
        public async Task InputDataIsValid_Search_Success_NSubstitute()
        {
            // Arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.SearchAsync(Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>()).Returns(Task.FromResult<List<Project>>([]));

            var query = FakeDataHelper.CreateFakeSearchProjectsQuery();
            var handler = new SearchProjectsQueryHandler(repository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.NotNull(result.Value);
            //Assert.IsType<List<SearchProjectsResponse>?>(result.Value);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SearchProjectsResponse>?>();
            string.IsNullOrWhiteSpace(result.Error).Should().BeTrue();
            await repository.Received(1).SearchAsync(Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
        }

        [Fact]
        public async Task InputDataIsValid_Search_Success_Moq()
        {
            // Arrange
            var repository = Mock.Of<IProjectRepository>(r =>
                r.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), CancellationToken.None) == Task.FromResult(new List<Project>()));

            var query = FakeDataHelper.CreateFakeSearchProjectsQuery();
            var handler = new SearchProjectsQueryHandler(repository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsSuccess);
            //Assert.False(result.IsFailure);
            //Assert.NotNull(result.Value);
            //Assert.IsType<List<SearchProjectsResponse>?>(result.Value);
            //Assert.True(string.IsNullOrWhiteSpace(result.Error));

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SearchProjectsResponse>?>();
            Mock.Get(repository).Verify(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), CancellationToken.None), Times.Once);
        }
    }
}
