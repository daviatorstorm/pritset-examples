import java.nio.file.Files;
import java.nio.file.Path;
import com.pritset.sdk.BinaryResponse;
import com.pritset.sdk.PritsetClient;
import com.pritset.sdk.exception.PritsetApiException;
import com.pritset.sdk.exception.PritsetException;
import com.pritset.sdk.exception.PritsetTransportException;

public final class Main {
    private Main() {
    }

    public static void main(String[] args) {
        try {
            if (args.length == 1 && "--webhook".equals(args[0])) {
                WebhookExample.run();
                return;
            }

            generatePdf();
        } catch (PritsetApiException error) {
            System.err.printf("Pritset API request failed with HTTP %d.%n", error.statusCode());
            System.exit(1);
        } catch (PritsetTransportException error) {
            System.err.println("The Pritset request did not complete.");
            System.exit(1);
        } catch (PritsetException error) {
            System.err.println("The Pritset request failed.");
            System.exit(1);
        } catch (IllegalArgumentException | IllegalStateException error) {
            System.err.println(error.getMessage());
            System.exit(1);
        } catch (Exception error) {
            System.err.println("Document generation failed.");
            System.exit(1);
        }
    }

    static void generatePdf() throws Exception {
        try (BinaryResponse pdf = createClient().documents().generate(
                requireEnvironmentVariable("PRITSET_TEMPLATE_ID"),
                loadSampleData())) {
            String contentType = pdf.contentType().orElse("");
            if (!"application/pdf".equalsIgnoreCase(contentType)) {
                throw new IllegalStateException(
                    "Expected application/pdf but received "
                        + (contentType.isBlank() ? "no content type" : contentType) + ".");
            }

            byte[] content = pdf.readAllBytes();
            if (content.length < 5
                    || content[0] != '%'
                    || content[1] != 'P'
                    || content[2] != 'D'
                    || content[3] != 'F'
                    || content[4] != '-') {
                throw new IllegalStateException("The response did not contain a valid PDF signature.");
            }

            Path outputDirectory = Path.of("output");
            Files.createDirectories(outputDirectory);
            Path outputPath = outputDirectory.resolve("generated-document.pdf").toAbsolutePath();
            Files.write(outputPath, content);
            System.out.println("Document generated successfully: " + outputPath);
        }
    }

    static PritsetClient createClient() {
        return PritsetClient.builder(
                requireEnvironmentVariable("PRITSET_ACCESS_TOKEN"),
                requireEnvironmentVariable("PRITSET_SECRET"))
            .build();
    }

    static String loadSampleData() throws Exception {
        return Files.readString(Path.of("Data", "dummy_data.json"));
    }

    static String requireEnvironmentVariable(String name) {
        String value = System.getenv(name);
        if (value == null || value.isBlank()) {
            throw new IllegalStateException("Set the " + name + " environment variable before running this example.");
        }
        return value.trim();
    }
}

