using Reminy.Core.TestDataInitialization;

namespace Reminy.Core.IntegrationTests.Data;

public sealed class DataInitializer(TestNoteStore testNoteStore)
{
    public void Clean() =>
        testNoteStore.Truncate();
}