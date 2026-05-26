using System.Collections;

namespace DevFreela.UnitTests.Common.TestDataSources
{
    public class InvalidStringsTestDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null! };
            yield return new object[] { string.Empty };
            yield return new object[] { "   " };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
