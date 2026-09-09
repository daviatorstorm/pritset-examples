from pritset import PritsetApiError, PritsetTransportError

from pritset_client import (
    create_pritset_client,
    get_template_id,
    load_sample_data,
    require_environment_variable,
)


def main() -> None:
    with create_pritset_client() as pritset:
        job = pritset.documents.generate_webhook(
            get_template_id(),
            load_sample_data(),
            require_environment_variable("PRITSET_WEBHOOK_URL"),
        )
    print(f"Webhook generation requested: {job.id}")


if __name__ == "__main__":
    try:
        main()
    except PritsetApiError as error:
        print(f"Pritset API request failed with HTTP {error.status}.")
        raise SystemExit(1) from error
    except PritsetTransportError as error:
        print("The Pritset request did not complete.")
        raise SystemExit(1) from error
    except (RuntimeError, ValueError) as error:
        print(str(error))
        raise SystemExit(1) from error
