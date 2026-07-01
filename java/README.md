# Pritset Java Example

This example shows how to send JSON data to the Pritset direct template process API and save the generated document as a PDF.

The project uses:

- Java 17 or newer
- built-in `HttpClient` for the HTTP request
- `Data/dummy_data.json` as the sample document data
- no external dependencies

## Requirements

- JDK 17 or newer
- a Pritset template ID
- a Pritset API token and secret from https://app.pritset.com/settings

## Configure

Update the credentials in `PritsetClient.java`:

```java
"your-access-token-here-from-https://app.pritset.com/settings",
"your-secret-here-from-https://app.pritset.com/settings",
```

Then update the template ID in `Main.java`:

```java
PritsetRequest result = PritsetClient.createRequest(
    "your-template-id"
);
```

## Data

The request payload comes from:

```text
Data/dummy_data.json
```

`PritsetClient.java` reads that file as JSON text:

```java
String data = Files.readString(Path.of("Data", "dummy_data.json"));
```

## Build

```bash
javac *.java
```

## Run

```bash
java Main
```

If the API call succeeds, the generated PDF is saved as:

```text
generated-document.pdf
```

## Project Structure

```text
java/
  Data/
    dummy_data.json
  Main.java
  PritsetClient.java
  PritsetRequest.java
  README.md
```
