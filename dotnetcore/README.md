# Pritset .NET Core Example

This example shows how to send JSON data to the Pritset direct template process API and save the generated document as a PDF.

The project uses:

- .NET 8 console app
- `HttpClient` for the HTTP request
- `Data/dummy_data.json` as the sample document data

## Requirements

- .NET SDK 8 or newer
- a Pritset template ID
- a Pritset API token and secret from https://app.pritset.com/settings

## Configure

Update the credentials in `PritsetClient.cs`:

```csharp
Token = "your-access-token-here-from-https://app.pritset.com/settings",
Secret = "your-secret-here-from-https://app.pritset.com/settings",
```

Then update the template ID in `Program.cs`:

```csharp
var result = PritsetClient.CreateRequest(
    templateId: "your-template-id"
);
```

## Data

The request payload comes from:

```text
Data/dummy_data.json
```

`PritsetClient.cs` reads that file and converts it into a JSON object:

```csharp
var json = File.ReadAllText(Path.Combine("Data", "dummy_data.json"));
var data = JsonNode.Parse(json);
```

## Run

```bash
dotnet run
```

If the API call succeeds, the generated PDF is saved as:

```text
generated-document.pdf
```

## Project Structure

```text
dotnetcore/
  Data/
    dummy_data.json
  Program.cs
  PritsetClient.cs
  PritsetRequest.cs
  PritsetDotnetExample.csproj
  README.md
```
