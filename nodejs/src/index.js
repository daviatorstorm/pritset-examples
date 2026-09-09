import { mkdir, writeFile } from "node:fs/promises";

import {
  PritsetApiError,
  PritsetTransportError,
} from "@pritset/sdk";

import {
  createPritsetClient,
  getTemplateId,
  loadSampleData,
} from "./pritset.js";

const outputDirectory = new URL("../output/", import.meta.url);
const outputPath = new URL("generated-document.pdf", outputDirectory);

async function main() {
  const pritset = createPritsetClient();
  const pdf = await pritset.documents.generate(
    getTemplateId(),
    await loadSampleData(),
  );

  if (pdf.contentType?.toLowerCase() !== "application/pdf") {
    throw new Error(
      `Expected application/pdf but received ${pdf.contentType || "no content type"}.`,
    );
  }

  const content = await pdf.toBuffer();
  if (content.subarray(0, 5).toString("ascii") !== "%PDF-") {
    throw new Error("The response did not contain a valid PDF signature.");
  }

  await mkdir(outputDirectory, { recursive: true });
  await writeFile(outputPath, content);
  console.log(`Document generated successfully: ${outputPath.pathname}`);
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
    console.error("Document generation failed.");
  }

  process.exitCode = 1;
}

main().catch(reportError);
