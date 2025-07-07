 #nullable disable
 using FluentAssertions;
 using TidyUtility.Core;

 namespace TidyUtility.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void FactoryCallsDefaultConstructor()
        {
            ConstructorCallTest instance = Factory<ConstructorCallTest>.Create();

            instance.ConstructorCalled.Should().BeTrue();
        }
    }

    public class ConstructorCallTest
    {
        public ConstructorCallTest()
        {
            this.ConstructorCalled = true;
        }

        public bool ConstructorCalled { get; }
    }
}
