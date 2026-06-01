using DevFreela.Core.Entities;
using DevFreela.UnitTests.Common.Helpers;
using System.Collections;

namespace DevFreela.UnitTests.Common.TestDataSources
{
    public class InvalidLoginTestDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { FakeDataHelper.CreateFakeUser(), false }; // Invalid password
            yield return new object[] { null!, FakeDataHelper.Faker.Random.Bool() }; // Invalid user
            yield return new object[] { null!, false }; // Invalid user and password
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
