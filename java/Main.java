import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.UUID;

public final class Main {
    private Main() {
    }

    public static void main(String[] args) throws Exception {
        PritsetRequest result = PritsetClient.createRequest(
            "your-template-id" // Replace with your actual template ID
        );

        String boundary = "----PritsetBoundary" + UUID.randomUUID();
        byte[] body = createMultipartBody(boundary, "data", result.data());

        HttpRequest request = HttpRequest.newBuilder()
            .uri(URI.create(result.api()))
            .header("Authorization", result.token())
            .header("X-Secret", result.secret())
            .header("Content-Type", "multipart/form-data; boundary=" + boundary)
            .POST(HttpRequest.BodyPublishers.ofByteArray(body))
            .build();

        HttpClient httpClient = HttpClient.newHttpClient();
        HttpResponse<byte[]> response = httpClient.send(
            request,
            HttpResponse.BodyHandlers.ofByteArray()
        );

        if (response.statusCode() >= 200 && response.statusCode() < 300) {
            Files.write(Path.of("generated-document.pdf"), response.body());
            System.out.println("Document generated successfully.");
        } else {
            System.out.printf("Error occurred: %d%n", response.statusCode());
            System.out.println(new String(response.body(), StandardCharsets.UTF_8));
        }
    }

    private static byte[] createMultipartBody(
        String boundary,
        String fieldName,
        String value
    ) throws IOException {
        String body = "--" + boundary + "\r\n"
            + "Content-Disposition: form-data; name=\"" + fieldName + "\"\r\n"
            + "Content-Type: application/json; charset=utf-8\r\n\r\n"
            + value + "\r\n"
            + "--" + boundary + "--\r\n";

        return body.getBytes(StandardCharsets.UTF_8);
    }
}

