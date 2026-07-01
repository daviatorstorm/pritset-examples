import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public final class PritsetClient {
    private PritsetClient() {
    }

    public static PritsetRequest createRequest(String templateId) throws IOException {
        String data = Files.readString(Path.of("Data", "dummy_data.json"));

        return new PritsetRequest(
            data,
            "your-access-token-here-from-https://app.pritset.com/settings",
            "your-secret-here-from-https://app.pritset.com/settings",
            "https://api.pritset.com/api/template/process/direct/" + templateId
        );
    }
}

