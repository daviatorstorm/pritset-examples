import {
  PritsetApiError,
  PritsetTransportError,
} from "@pritset/sdk";

import {
  createPritsetClient,
  getTemplateId,
  loadSampleData,
  requireEnvironmentVariable,
} from "./pritset.js";

async function main() {
  const job = await createPritsetClient().documents.generateWebhook(
    getTemplateId(),
    await loadSampleData(),
    requireEnvironmentVariable("PRITSET_WEBHOOK_URL"),
  );

  console.log(`Webhook generation requested: ${job.id}`);
}

function reportError(error) {
  if (error instanceof PritsetApiError) {
    console.error(`Pritset API request failed with HTTP ${error.status}.`);
  } else if (error instanceof PritsetTransportError) {
    console.error(
      `The Pritset request did not complete${error.code ? ` (${error.code})` : ""}.`,
    );
  } else if (error instanceof Error) {
    console.error(error.message);
  } else {
    console.error("Webhook generation failed.");
  }

  process.exitCode = 1;
}

main().catch(reportError);
