# Pritset Python Example

This example uses the official Pritset Python SDK to generate a PDF from JSON
data and save it locally.

The project uses:

- Python 3.10 or newer
- `pritset` version `0.1.5`
- `Data/dummy_data.json` as the sample document data

## Requirements

- Python 3.10 or newer
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

`pritset_client.py` resolves and parses that file relative to the example
source:

```python
data_path = Path(__file__).resolve().parent / "Data" / "dummy_data.json"
return json.loads(data_path.read_text(encoding="utf-8"))
```

## Run

```bash
python -m venv .venv
. .venv/bin/activate
python -m pip install -r requirements.txt
python main.py
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
python webhook.py
```

This submits a webhook-generation job only; it does not run or verify a
webhook receiver.

## Project Structure

```text
python/
  Data/
    dummy_data.json
  main.py
  pritset_client.py
  webhook.py
  requirements.txt
  README.md
```
