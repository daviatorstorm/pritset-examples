using Pritset;
using Pritset.Exceptions;

namespace PritsetDotnetExample;

internal static class WebhookExample
{
    internal static async Task<int> RunAsync()
    {
        try
        {
            using var pritset = new PritsetClient(
                ExampleConfiguration.RequireEnvironmentVariable("PRITSET_ACCESS_TOKEN"),
                ExampleConfiguration.RequireEnvironmentVariable("PRITSET_SECRET"));
            var job = await pritset.Documents.GenerateWebhookAsync(
                ExampleConfiguration.RequireEnvironmentVariable("PRITSET_TEMPLATE_ID"),
                ExampleConfiguration.LoadSampleData(),
                new Uri(ExampleConfiguration.RequireEnvironmentVariable("PRITSET_WEBHOOK_URL")));
            Console.WriteLine($"Webhook generation requested: {job.Id}");
            return 0;
        }
        catch (PritsetApiException exception)
        {
            Console.Error.WriteLine($"Pritset API request failed with HTTP {exception.StatusCode}.");
        }
        catch (PritsetTransportException)
        {
            Console.Error.WriteLine("The Pritset request did not complete.");
        }
        catch (InvalidOperationException exception)
        {
            Console.Error.WriteLine(exception.Message);
        }
        catch (UriFormatException)
        {
            Console.Error.WriteLine("PRITSET_WEBHOOK_URL must be an absolute HTTP(S) URL.");
        }
        catch (ArgumentException)
        {
            Console.Error.WriteLine("PRITSET_WEBHOOK_URL must be an absolute HTTP(S) URL.");
        }

        return 1;
    }
}
