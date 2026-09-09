# Pritset PHP Example

This independent consumer project uses `pritset/pritset-php` version `0.1.5`
to generate a PDF from `Data/dummy_data.json`.

## Requirements

- PHP 8.3 or newer
- Composer 2
- a Pritset access token, secret, and template ID

## Install

```bash
composer install
composer run check
```

## Configure

Set the shared environment variables:

```bash
export PRITSET_ACCESS_TOKEN="your-access-token"
export PRITSET_SECRET="your-secret"
export PRITSET_TEMPLATE_ID="your-template-id"
```

Do not commit credentials or a local `.env` file.

### TLS certificate errors

PHP normally uses the CA certificate store configured by the operating system
or `php.ini`. If PHP cannot verify Pritset's public TLS certificate, set the
optional `PRITSET_CA_BUNDLE_PATH` variable to a readable, trusted PEM CA bundle:

```bash
export PRITSET_CA_BUNDLE_PATH="/path/to/ca-bundle.crt"
```

On Windows, Git for Windows commonly provides a bundle at:

```text
C:\Program Files\Git\mingw64\etc\ssl\certs\ca-bundle.crt
```

The example passes this path to Guzzle through the SDK's supported
`httpClient` constructor parameter. Do not generate a self-signed testing
certificate and do not disable TLS verification: neither safely establishes
trust in `api.pritset.com`.

## Generate a PDF

```bash
php index.php
```

The example verifies the response content type and `%PDF-` signature before
writing `output/generated-document.pdf`. The output directory is ignored by Git.

## Optional webhook generation

Set `PRITSET_WEBHOOK_URL` and run:

```bash
php webhook.php
```

This submits a webhook-generation job only; it does not run or verify a
webhook receiver.
