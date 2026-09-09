import os
import json
from pathlib import Path

from pritset import PritsetClient


def require_environment_variable(name: str) -> str:
    value = os.environ.get(name, "").strip()
    if not value:
        raise RuntimeError(
            f"Set the {name} environment variable before running this example."
        )
    return value


def create_pritset_client() -> PritsetClient:
    return PritsetClient(
        access_token=require_environment_variable("PRITSET_ACCESS_TOKEN"),
        secret=require_environment_variable("PRITSET_SECRET"),
    )


def get_template_id() -> str:
    return require_environment_variable("PRITSET_TEMPLATE_ID")


def load_sample_data() -> object:
    data_path = Path(__file__).resolve().parent / "Data" / "dummy_data.json"
    return json.loads(data_path.read_text(encoding="utf-8"))
