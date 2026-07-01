# Pritset Node.js Example

This example shows how to send JSON data to the Pritset direct template process API and save the generated document as a PDF.

The project uses:

- native ECMAScript modules
- `axios` for the HTTP request
- `data/dummy_data.json` as the sample document data

## Requirements

- Node.js 20 or newer
- npm
- a Pritset template ID
- a Pritset API token and secret from https://app.pritset.com/settings

## Install

```bash
npm install
```

## Configure

Update the credentials in `src/pritset.js`:

```js
token: "your-access-token-here-from-https://app.pritset.com/settings",
secret: "your-secret-here-from-https://app.pritset.com/settings",
```

Then update the template ID in `src/index.js`:

```js
const result = createPritsetMessage({
  templateId: "your-template-id",
});
```

## Data

The request payload comes from:

```text
data/dummy_data.json
```

`src/pritset.js` reads that file and converts it into a JavaScript object:

```js
const data = JSON.parse(
  readFileSync(new URL("../data/dummy_data.json", import.meta.url), "utf8")
);
```

## Run

```bash
npm start
```

If the API call succeeds, the generated PDF is saved as:

```text
generated-document.pdf
```

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
    pritset.js
  package.json
  README.md
```
