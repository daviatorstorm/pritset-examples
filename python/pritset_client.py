from dataclasses import dataclass
from pathlib import Path


@dataclass(frozen=True)
class PritsetRequest:
    data: str
    token: str
    secret: str
    api: str


def create_request(template_id: str) -> PritsetRequest:
    data = Path("Data/dummy_data.json").read_text(encoding="utf-8")

    return PritsetRequest(
        data=data,
        token="your-access-token-here-from-https://app.pritset.com/settings",
        secret="your-secret-here-from-https://app.pritset.com/settings",
        api=f"https://api.pritset.com/api/template/process/direct/{template_id}",
    )
