 #nullable disable
 using FluentAssertions;
 using TidyUtility.Core.Extensions;

 namespace TidyUtility.Tests.Extensions
{
    public class UriExtensionsTests
    {
        [Fact]
        public void CombineNoParams()
        {
            Uri baseUri = new Uri("https://example.com");
            Uri combinedUri = baseUri.Combine();

            combinedUri.Should().BeEquivalentTo(baseUri);
        }

        [Fact]
        public void CombineBaseNoPathWithPath()
        {
            Uri baseUri = new Uri("https://example.com");
            Uri combinedUri = baseUri.Combine("path1");
            Uri expected = new Uri("https://example.com/path1");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseNoPathRootedWithPath()
        {
            Uri baseUri = new Uri("https://example.com/");
            Uri combinedUri = baseUri.Combine("path1");
            Uri expected = new Uri("https://example.com/path1");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseNoPathWithRootedPath()
        {
            Uri baseUri = new Uri("https://example.com");
            Uri combinedUri = baseUri.Combine("/path1");
            Uri expected = new Uri("https://example.com/path1");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseNoPathRootedWithRootedPath()
        {
            Uri baseUri = new Uri("https://example.com/");
            Uri combinedUri = baseUri.Combine("/path1");
            Uri expected = new Uri("https://example.com/path1");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndPathWithPath()
        {
            Uri baseUri = new Uri("https://example.com/path1");
            Uri combinedUri = baseUri.Combine("path2");
            Uri expected = new Uri("https://example.com/path1/path2");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndPathRootedWithPath()
        {
            Uri baseUri = new Uri("https://example.com/path1/");
            Uri combinedUri = baseUri.Combine("path2");
            Uri expected = new Uri("https://example.com/path1/path2");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndPathWithRootedPath()
        {
            Uri baseUri = new Uri("https://example.com/path1");
            Uri combinedUri = baseUri.Combine("/path2");
            Uri expected = new Uri("https://example.com/path1/path2");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndPathRootedWithRootedPath()
        {
            Uri baseUri = new Uri("https://example.com/path1/");
            Uri combinedUri = baseUri.Combine("/path2");
            Uri expected = new Uri("https://example.com/path1/path2");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseNoPathWithSeparator()
        {
            Uri baseUri = new Uri("https://example.com");
            Uri combinedUri = baseUri.Combine("/");
            Uri expected = new Uri("https://example.com/");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseNoPathRootedWithSeparator()
        {
            Uri baseUri = new Uri("https://example.com/");
            Uri combinedUri = baseUri.Combine("/");
            Uri expected = new Uri("https://example.com/");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBasePathWithSeparator()
        {
            Uri baseUri = new Uri("https://example.com/path1");
            Uri combinedUri = baseUri.Combine("/");
            Uri expected = new Uri("https://example.com/path1/");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBasePathRootedWithSeparator()
        {
            Uri baseUri = new Uri("https://example.com/path1/");
            Uri combinedUri = baseUri.Combine("/");
            Uri expected = new Uri("https://example.com/path1/");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndQueryWithPath()
        {
            Uri baseUri = new Uri("https://example.com?name1=value1");
            Uri combinedUri = baseUri.Combine("path");
            Uri expected = new Uri("https://example.com/path?name1=value1");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseWithPathAndQuery()
        {
            Uri baseUri = new Uri("https://example.com");
            Uri combinedUri = baseUri.Combine("path?name2=value2");
            Uri expected = new Uri("https://example.com/path?name2=value2");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndQueryWithPathAndQuery()
        {
            Uri baseUri = new Uri("https://example.com?name1=value1");
            Uri combinedUri = baseUri.Combine("path?name2=value2");
            Uri expected = new Uri("https://example.com/path?name1=value1&name2=value2");

            combinedUri.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CombineBaseAndQueryWithTwoPathsAndQueries()
        {
            Uri baseUri = new Uri("https://example.com?name1=value1");
            Uri combinedUri = baseUri.Combine("path1?name2=value2", "path2?name3=value3");
            Uri expected = new Uri("https://example.com/path1/path2?name1=value1&name2=value2&name3=value3");

            combinedUri.Should().BeEquivalentTo(expected);
        }
    }
}
