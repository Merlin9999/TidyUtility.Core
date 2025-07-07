 #nullable disable
 using FluentAssertions;
 using TidyUtility.Core;

 namespace TidyUtility.Tests;

public class AsyncFactoryTests
{
    [Fact]
    public async Task AsyncFactoryWithDefaultConstructor()
    {
        var obj = await AsyncFactory.ConstructAsync<DefaultAsyncConstructorTest>();
        
        obj.IsInitialized.Should().BeTrue();
    }

    [Fact]
    public async Task AsyncFactoryWithConstructorParam()
    {
        int expectedValue = 5;
        var obj = await AsyncFactory.ConstructAsync(() => new AsyncConstructorWithParamTest(expectedValue));

        obj.InitializedValue.Should().Be(expectedValue);
    }

    public class DefaultAsyncConstructorTest : IAsyncInitializer
    {
        public DefaultAsyncConstructorTest()
        {
            this.AsyncInitialization = this.InitializeAsync();
        }

        public Task AsyncInitialization { get; init; }
        public bool IsInitialized { get; private set; }

        private async Task InitializeAsync()
        {
            await Task.Delay(1);
            this.IsInitialized = true;
        }
    }

    public class AsyncConstructorWithParamTest : IAsyncInitializer
    {
        public AsyncConstructorWithParamTest(int value)
        {
            this.InitializedValue = value - 1;
            this.AsyncInitialization = this.InitializeAsync(value);
        }

        public Task AsyncInitialization { get; init; }
        public int InitializedValue { get; private set; }

        private async Task InitializeAsync(int value)
        {
            await Task.Delay(1);
            this.InitializedValue = value;
        }
    }
}
