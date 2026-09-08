# Pritset Go Example

This independent consumer project uses `github.com/pritset/pritset-go-sdk`
version `v0.1.5` to generate a PDF from `Data/dummy_data.json`.

## Requirements

- Go 1.25 or newer
- a Pritset access token, secret, and template ID

## Install and check

```bash
go mod download
go vet ./...
go test ./...
go build ./...
```

## Configure

Set the shared environment variables:

```bash
export PRITSET_ACCESS_TOKEN="your-access-token"
export PRITSET_SECRET="your-secret"
export PRITSET_TEMPLATE_ID="your-template-id"
```

Do not commit credentials or a local `.env` file.

## Generate a PDF

```bash
go run .
```

The example verifies the response content type and `%PDF-` signature before
writing `output/generated-document.pdf`. The output directory is ignored by Git.

## Optional webhook generation

Set `PRITSET_WEBHOOK_URL` and run:

```bash
go run . --webhook
```

This submits a webhook-generation job only; it does not run or verify a
webhook receiver.
