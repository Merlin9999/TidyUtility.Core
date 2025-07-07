 #nullable disable
 using FluentAssertions;
 using TidyUtility.Core.Extensions;

 namespace TidyUtility.Tests.Extensions
{
    public class ComparisonExtensionsTests
    {
        [Fact]
        public void CreateComparerFromComparison()
        {
            Comparison<int> c = (x, y) => x == y ? 0 : (x <= y ? -1 : 1);
            IComparer<int> icc = c.AsComparer();
            TestSort(icc);
        }

        [Fact]
        public void CreateComparerFromFunc()
        {
            Func<int, int, int> f = (x, y) => x == y ? 0 : (x <= y ? -1 : 1);
            IComparer<int> icf = f.AsComparer();
            TestSort(icf);
        }

        private static void TestSort(IComparer<int> comparer)
        {
            int[] unsorted = new[] {3, -1, 0, -5, 1, -3, 5};

            unsorted
                .OrderBy(x => x, comparer)
                .ToArray().Should().BeInAscendingOrder();
        }

    }
}
