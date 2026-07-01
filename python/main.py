from pathlib import Path
from urllib.error import HTTPError
from urllib.request import Request, urlopen
from uuid import uuid4

from pritset_client import create_request


def create_multipart_body(boundary: str, field_name: str, value: str) -> bytes:
    body = (
        f"--{boundary}\r\n"
        f'Content-Disposition: form-data; name="{field_name}"\r\n'
        "Content-Type: application/json; charset=utf-8\r\n\r\n"
        f"{value}\r\n"
        f"--{boundary}--\r\n"
    )

    return body.encode("utf-8")


def main() -> None:
    result = create_request(
        "your-template-id"  # Replace with your actual template ID
    )

    boundary = f"----PritsetBoundary{uuid4()}"
    body = create_multipart_body(boundary, "data", result.data)

    request = Request(
        result.api,
        data=body,
        headers={
            "Authorization": result.token,
            "X-Secret": result.secret,
            "Content-Type": f"multipart/form-data; boundary={boundary}",
        },
        method="POST",
    )

    try:
        with urlopen(request) as response:
            Path("generated-document.pdf").write_bytes(response.read())
            print("Document generated successfully.")
    except HTTPError as error:
        print(f"Error occurred: {error.code}")
        print(error.read().decode("utf-8"))


if __name__ == "__main__":
    main()
