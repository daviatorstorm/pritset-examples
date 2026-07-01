# Pritset Python Example

This example shows how to send JSON data to the Pritset direct template process API and save the generated document as a PDF.

The project uses:

- Python 3.10 or newer
- built-in `urllib` for the HTTP request
- `Data/dummy_data.json` as the sample document data
- no external dependencies

## Requirements

- Python 3.10 or newer
- a Pritset template ID
- a Pritset API token and secret from https://app.pritset.com/settings

## Configure

Update the credentials in `pritset_client.py`:

```python
token="your-access-token-here-from-https://app.pritset.com/settings",
secret="your-secret-here-from-https://app.pritset.com/settings",
```

Then update the template ID in `main.py`:

```python
result = create_request(
    "your-template-id"
)
```

## Data

The request payload comes from:

```text
Data/dummy_data.json
```

`pritset_client.py` reads that file as JSON text:

```python
data = Path("Data/dummy_data.json").read_text(encoding="utf-8")
```

## Run

```bash
python main.py
```

If the API call succeeds, the generated PDF is saved as:

```text
generated-document.pdf
```

## Project Structure

```text
python/
  Data/
    dummy_data.json
  main.py
  pritset_client.py
  README.md
```
