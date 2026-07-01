import { readFileSync } from "node:fs";

const data = JSON.parse(
  readFileSync(new URL("../data/dummy_data.json", import.meta.url), "utf8")
);

export function createPritsetMessage(options = {}) {
  return {
    data,
    token: "your-access-token-here-from-https://app.pritset.com/settings",
    secret: "your-secret-here-from-https://app.pritset.com/settings",
    api: `https://api.pritset.com/api/template/process/direct/${options.templateId}`,
  };
}
