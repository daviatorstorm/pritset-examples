# Pritset SDK examples

Six minimal backend consumers that turn a reusable DOCX template plus JSON data
into a PDF. Every example uses the published Pritset SDK pinned to version
`0.1.5`; none implements the HTTP API itself.

## Quick start

1. In the Pritset portal, upload a DOCX template and copy its template ID.
2. Create an access token and secret in [Settings](https://app.pritset.com/settings).
3. Set the three environment variables below, choose a language, then follow
   its install-and-run command.

For example, in a POSIX shell:

```bash
export PRITSET_ACCESS_TOKEN="your-access-token"
export PRITSET_SECRET="your-secret"
export PRITSET_TEMPLATE_ID="your-template-id"
```

For the shortest path, use Node.js:

```bash
cd nodejs
npm ci
npm start
```

The command reads the process environment and writes
`output/generated-document.pdf` after a successful request.

## Examples

| Language | Published SDK 0.1.5 | First run |
| --- | --- | --- |
| [Node.js](./nodejs/) | `@pritset/sdk` | `npm ci && npm start` |
| [PHP](./php/) | `pritset/pritset-php` | `composer install && php index.php` |
| [Python](./python/) | `pritset` | `python -m pip install -r requirements.txt && python main.py` |
| [Go](./go/) | `github.com/pritset/pritset-go-sdk` | `go run .` |
| [.NET / C#](./dotnetcore/) | `Pritset` | `dotnet run` |
| [Java](./java/) | `com.pritset:pritset-java` | `mvn --quiet exec:java` |

Each language README has its runtime prerequisites and verification command.

## Configuration and inputs

Set these process environment variables; do not put real values in source code,
commit a local `.env`, or log them.

| Variable | Purpose |
| --- | --- |
| `PRITSET_ACCESS_TOKEN` | Identifies the Pritset access token. |
| `PRITSET_SECRET` | Authenticates the access token. |
| `PRITSET_TEMPLATE_ID` | Selects the uploaded DOCX template to process. |
| `PRITSET_WEBHOOK_URL` | Required only for the optional webhook example. |

PHP users whose local trust store cannot verify the API certificate may also
set the optional `PRITSET_CA_BUNDLE_PATH` variable to a trusted PEM CA bundle.
See the [PHP TLS troubleshooting steps](./php/#tls-certificate-errors). This is
transport configuration, not a Pritset credential, and is not needed by the
other examples.

The root [.env.example](./.env.example) is a placeholder reference only; the
examples intentionally read environment variables directly.

The flow is shared across all six projects:

```text
Uploaded DOCX template + template ID
          +
Committed dummy_data.json matching that template
          ↓
Official Pritset SDK
          ↓
Validated PDF → output/generated-document.pdf
```

Replace each committed `dummy_data.json` with data whose keys match your DOCX
placeholders. The source template stays editable in Word or another DOCX editor.

## Direct PDF vs. webhook

The default command performs direct generation: the SDK returns the PDF, the
example verifies its content type and `%PDF-` signature, then saves it locally.
Each project also provides a separately invoked webhook example. It submits a
job and prints its ID; your application must host and verify the receiving
webhook endpoint.

## Learn more

- [Pritset SDK documentation](https://pritset.com/docs/sdks)
- [Template processing API](https://pritset.com/docs/api/template)
- [DOCX to PDF product overview](https://pritset.com/docx-to-pdf)
- [Working DOCX, JSON, and PDF examples](https://pritset.com/docs/category/examples)
- [Representative generated receipt PDF](https://pritset.com/assets/files/receipt.template-8cf974ecbbdf66c7cf4d993adf8c6077.pdf)

## License

The source examples are available under the [MIT License](./LICENSE). The
Pritset name, logo, website design, and brand assets are not licensed for reuse
unless explicitly stated.
