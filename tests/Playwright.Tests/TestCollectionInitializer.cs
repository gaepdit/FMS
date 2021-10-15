using Xunit;

namespace PlaywrightTests
{
    public class TestCollectionInitializer
    {
        public TestCollectionInitializer()
        {
            // Intentionally left blank
        }
    }

    [CollectionDefinition("Test collection")]
    public class TestCollection : ICollectionFixture<TestCollectionInitializer>
    {
        // Intentionally left blank
    }
}
