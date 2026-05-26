using DevFreela.Core.Entities;
using DevFreela.UnitTests.Common.Helpers;
using System.Collections;

namespace DevFreela.UnitTests.Common.TestDataSources
{
    public class ValidProjectCommentsTestDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var numberOfComments = FakeDataHelper.Faker.Random.Int(1, 20);
            var comments = new List<ProjectComment>();

            for (var i = 0; i < numberOfComments; i++)
            {
                comments.Add(new ProjectComment(
                    FakeDataHelper.Faker.Lorem.Paragraph(),
                    FakeDataHelper.Faker.Random.Guid(),
                    FakeDataHelper.Faker.Random.Guid()));
            }

            yield return new object[] { comments };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
