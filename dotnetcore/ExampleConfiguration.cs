namespace PritsetDotnetExample;

internal static class ExampleConfiguration
{
    internal static string RequireEnvironmentVariable(string name)
    {
        string? value = Environment.GetEnvironmentVariable(name)?.Trim();
        return !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidOperationException(
                $"Set the {name} environment variable before running this example.");
    }

    internal static string LoadSampleData()
    {
        string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "dummy_data.json");
        return File.ReadAllText(dataPath);
    }
}
