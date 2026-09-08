import com.pritset.sdk.model.WebhookJob;
import java.net.URI;

final class WebhookExample {
    private WebhookExample() {
    }

    static void run() throws Exception {
        WebhookJob job = Main.createClient().documents().generateWebhook(
            Main.requireEnvironmentVariable("PRITSET_TEMPLATE_ID"),
            Main.loadSampleData(),
            URI.create(Main.requireEnvironmentVariable("PRITSET_WEBHOOK_URL"))
        );
        System.out.println("Webhook generation requested: " + job.id());
    }
}
