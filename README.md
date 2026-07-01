# Pritset API Examples

This repository contains small language examples for using the [Pritset](https://pritset.com/) direct template process API.

Each example shows the same flow:

1. Load sample JSON data from `dummy_data.json`.
2. Send that data to the Pritset API as multipart form data.
3. Save the generated document response as `generated-document.pdf`.

## Available Examples

| Language | Folder | Notes | Run command |
| --- | --- | --- | --- |
| Node.js | `nodejs/` | Uses ECMAScript modules and `axios`. | `npm start` |
| .NET Core / C# | `dotnetcore/` | Uses a .NET 8 console app and `HttpClient`. | `dotnet run` |
| Java | `java/` | Uses plain Java with built-in `HttpClient`, no external dependencies. | `java Main` |
| Python | `python/` | Uses Python standard library `urllib`, no external dependencies. | `python main.py` |

## Before Running

For each example, update these values in the language-specific helper file:

- Pritset API token
- Pritset API secret
- Pritset template ID

You can get the API token and secret from:

```text
https://app.pritset.com/settings
```

The template ID is configured in each example's main entry file.


## Template Examples Reference

Use the local Pritset docs to browse template examples:

```text
https://pritset.com/docs/category/examples
```
## Repository Structure

```text
examples/
  nodejs/
  dotnetcore/
  java/
  python/
  README.md
```

Each folder has its own `README.md` with exact setup and run instructions for that language.

## Output

When an example succeeds, it writes the generated PDF to that example folder:

```text
generated-document.pdf
```

## License

This repository is licensed under the MIT License.

You may copy, modify, and use these examples in your own commercial or private projects.

The Pritset name, logo, website design, and brand assets are not licensed for reuse unless explicitly stated.