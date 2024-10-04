namespace Reminy.Core.Host.Composition;

internal static class ConfigurationExtensions
{
    public static T GetRequired<T>(this IConfigurationSection section)
        => section.Get<T>() ?? throw new InvalidOperationException($"Section {section.Path} must be present.");

    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
        => configuration.GetValue<T?>(key) ?? throw new InvalidOperationException($"Key {key} must be present.");
}