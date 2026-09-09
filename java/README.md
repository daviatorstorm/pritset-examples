# Pritset Java Example

This example uses the official Pritset Java SDK to generate a PDF from JSON
data and save it locally.

The project uses:

- Java 17 or newer
- `com.pritset:pritset-java` version `0.1.5`
- `Data/dummy_data.json` as the sample document data

## Requirements

- JDK 17 or newer
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

`Main.java` reads that file as JSON text for the SDK:

```java
String data = Files.readString(Path.of("Data", "dummy_data.json"));
```

## Build

```bash
mvn --batch-mode --no-transfer-progress verify
```

## Run

```bash
mvn --quiet exec:java
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
mvn --quiet exec:java -Dexec.args=--webhook
```

This submits a webhook-generation job only; it does not run or verify a
webhook receiver.

## Project Structure

```text
java/
  Data/
    dummy_data.json
  Main.java
  WebhookExample.java
  pom.xml
  README.md
```
