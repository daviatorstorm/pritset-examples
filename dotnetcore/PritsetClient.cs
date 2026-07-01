using System.Text.Json.Nodes;

namespace PritsetDotnetExample;

public static class PritsetClient
{
    public static PritsetRequest CreateRequest(string templateId)
    {
        var json = File.ReadAllText(Path.Combine("Data", "dummy_data.json"));
        var data = JsonNode.Parse(json)
            ?? throw new InvalidOperationException("Unable to parse Data/dummy_data.json.");

        return new PritsetRequest
        {
            Data = data,
            Token = "your-access-token-here-from-https://app.pritset.com/settings",
            Secret = "your-secret-here-from-https://app.pritset.com/settings",
            Api = $"https://api.pritset.com/api/template/process/direct/{templateId}",
        };
    }
}
