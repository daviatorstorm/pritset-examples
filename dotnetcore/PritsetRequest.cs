using System.Text.Json.Nodes;

namespace PritsetDotnetExample;

public sealed class PritsetRequest
{
    public required JsonNode Data { get; init; }

    public required string Token { get; init; }

    public required string Secret { get; init; }

    public required string Api { get; init; }
}
