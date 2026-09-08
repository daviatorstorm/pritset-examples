# Pritset Node.js Example

This example uses the official Pritset Node.js SDK to generate a PDF from JSON
data and save it locally.

The project uses:

- native ECMAScript modules
- `@pritset/sdk` version `0.1.5`
- `data/dummy_data.json` as the sample document data

## Requirements

- Node.js 20 or newer
- npm
- a Pritset template ID
- a Pritset API token and secret from https://app.pritset.com/settings

## Install and verify

```bash
npm ci
npm run check
```

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
data/dummy_data.json
```

`src/pritset.js` reads that file into a JavaScript object:

```js
const source = new URL("../data/dummy_data.json", import.meta.url);
return JSON.parse(await readFile(source, "utf8"));
```

## Run

```bash
npm start
```

If the API call succeeds, the generated PDF is saved as:

```text
output/generated-document.pdf
```

The output directory is ignored by Git. The example verifies the SDK response
has `application/pdf` content type and that its bytes begin with `%PDF-` before
writing the file.

## Optional webhook generation

To request PDF delivery to your own webhook endpoint, set
`PRITSET_WEBHOOK_URL` and run:

```bash
npm run webhook
```

This is intentionally separate from direct PDF generation. It submits a job;
the example does not run or verify a webhook receiver.

## Development Mode

```bash
npm run dev
```

## Project Structure

```text
nodejs/
  data/
    dummy_data.json
  src/
    index.js
    generate-webhook.js
    pritset.js
  package.json
  README.md
```
