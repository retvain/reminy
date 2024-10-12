namespace Reminy.Core.TestDataInitialization.Models;

public sealed record Note(
    string Title,
    string Content,
    long? Id = null);