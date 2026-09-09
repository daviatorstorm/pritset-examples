# Pritset .NET Core Example

This example uses the official Pritset .NET SDK to generate a PDF from JSON
data and save it locally.

The project uses:

- .NET 8 console app
- `Pritset` version `0.1.5`
- `Data/dummy_data.json` as the sample document data

## Requirements

- .NET SDK 8 or newer
- a Pritset template ID
- a Pritset API token and secret from https://app.pritset.com/settings

## Configure

Set the required environment variables. PowerShell:

```powershell
$env:PRITSET_ACCESS_TOKEN = "your-access-token"
$env:PRITSET_SECRET = "your-secret"
$env:PRITSET_TEMPLATE_ID = "your-template-id"
```

Bash or Zsh:

```bash
export PRITSET_ACCESS_TOKEN="your-access-token"
export PRITSET_SECRET="your-secret"
export PRITSET_TEMPLATE_ID="your-template-id"
```

Do not paste real credentials into the source files or commit them to Git.

## Data

The request payload comes from:

```text
Data/dummy_data.json
```

`ExampleConfiguration.cs` reads that file as JSON text for the SDK:

```csharp
string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "dummy_data.json");
string data = File.ReadAllText(dataPath);
```

## Run

```bash
dotnet run
```

If the API call succeeds, the generated PDF is saved as:

```text
output/generated-document.pdf
```

The output directory is ignored by Git. The example checks the SDK response
content type and `%PDF-` file signature before writing the PDF.

## Optional webhook generation

Set `PRITSET_WEBHOOK_URL` and run:

```bash
dotnet run -- --webhook
```

This submits a webhook-generation job only; it does not run or verify a
webhook receiver.

## Project Structure

```text
dotnetcore/
  Data/
    dummy_data.json
  Program.cs
  ExampleConfiguration.cs
  WebhookExample.cs
  PritsetDotnetExample.csproj
  README.md
```
