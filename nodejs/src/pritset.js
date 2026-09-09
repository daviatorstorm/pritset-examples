import { readFile } from "node:fs/promises";

import { PritsetClient } from "@pritset/sdk";

export function requireEnvironmentVariable(name) {
  const value = process.env[name]?.trim();

  if (!value) {
    throw new Error(`Set the ${name} environment variable before running this example.`);
  }

  return value;
}

export function createPritsetClient() {
  return new PritsetClient({
    accessToken: requireEnvironmentVariable("PRITSET_ACCESS_TOKEN"),
    secret: requireEnvironmentVariable("PRITSET_SECRET"),
  });
}

export function getTemplateId() {
  return requireEnvironmentVariable("PRITSET_TEMPLATE_ID");
}

export async function loadSampleData() {
  const source = new URL("../data/dummy_data.json", import.meta.url);
  return JSON.parse(await readFile(source, "utf8"));
}
