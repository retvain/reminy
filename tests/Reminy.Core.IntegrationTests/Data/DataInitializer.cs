using Reminy.Core.TestDataInitialization;

namespace Reminy.Core.IntegrationTests.Data;

public sealed class DataInitializer(TestNotesStore testNotesStore)
{
    public void Clean() =>
        testNotesStore.Truncate();
}