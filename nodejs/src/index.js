import { createPritsetMessage } from "./pritset.js";
import fs from "fs";
import axios from "axios";

const result = createPritsetMessage({
  templateId: "your-template-id", // Replace with your actual template ID
});

const form = new FormData();

form.append("data", JSON.stringify(result.data));

const response = await axios.post(result.api, form, {
  headers: {
    Authorization: result.token,
    "X-Secret": result.secret,
  },
  responseType: "arraybuffer",
});

if (response.status === 200) {
  console.log("Document generated successfully.");
  fs.writeFileSync("generated-document.pdf", response.data);
} else {
  console.log("Error occured");
}
