from pathlib import Path

from pritset import PritsetApiError, PritsetTransportError

from pritset_client import create_pritset_client, get_template_id, load_sample_data


def main() -> None:
    output_path = Path(__file__).resolve().parent / "output" / "generated-document.pdf"

    with create_pritset_client() as pritset:
        pdf = pritset.documents.generate(get_template_id(), load_sample_data())
        if pdf.content_type != "application/pdf":
            pdf.close()
            raise RuntimeError(
                f"Expected application/pdf but received {pdf.content_type or 'no content type'}."
            )

        content = pdf.to_bytes()

    if not content.startswith(b"%PDF-"):
        raise RuntimeError("The response did not contain a valid PDF signature.")

    output_path.parent.mkdir(parents=True, exist_ok=True)
    output_path.write_bytes(content)
    print(f"Document generated successfully: {output_path}")


def report_error(error: BaseException) -> None:
    if isinstance(error, PritsetApiError):
        print(f"Pritset API request failed with HTTP {error.status}.")
    elif isinstance(error, PritsetTransportError):
        print("The Pritset request did not complete.")
    else:
        print(str(error))


if __name__ == "__main__":
    try:
        main()
    except (PritsetApiError, PritsetTransportError, RuntimeError, ValueError) as error:
        report_error(error)
        raise SystemExit(1) from error
