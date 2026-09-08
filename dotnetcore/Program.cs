using Pritset;
using Pritset.Exceptions;
using PritsetDotnetExample;

if (args.Contains("--webhook", StringComparer.OrdinalIgnoreCase))
{
    Environment.ExitCode = await WebhookExample.RunAsync();
    return;
}

try
{
    using var pritset = new PritsetClient(
        ExampleConfiguration.RequireEnvironmentVariable("PRITSET_ACCESS_TOKEN"),
        ExampleConfiguration.RequireEnvironmentVariable("PRITSET_SECRET"));
    using BinaryResponse pdf = await pritset.Documents.GenerateAsync(
        ExampleConfiguration.RequireEnvironmentVariable("PRITSET_TEMPLATE_ID"),
        ExampleConfiguration.LoadSampleData());

    if (!string.Equals(pdf.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase))
    {
        Console.Error.WriteLine($"Expected application/pdf but received {pdf.ContentType ?? "no content type"}.");
        Environment.ExitCode = 1;
        return;
    }

    using var content = new MemoryStream();
    await pdf.Stream.CopyToAsync(content);
    byte[] document = content.ToArray();
    if (!document.AsSpan().StartsWith("%PDF-"u8))
    {
        Console.Error.WriteLine("The response did not contain a valid PDF signature.");
        Environment.ExitCode = 1;
        return;
    }

    string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "output");
    Directory.CreateDirectory(outputDirectory);
    string outputPath = Path.Combine(outputDirectory, "generated-document.pdf");
    await File.WriteAllBytesAsync(outputPath, document);
    Console.WriteLine($"Document generated successfully: {Path.GetFullPath(outputPath)}");
}
catch (PritsetApiException exception)
{
    Console.Error.WriteLine($"Pritset API request failed with HTTP {exception.StatusCode}.");
    Environment.ExitCode = 1;
}
catch (PritsetTransportException)
{
    Console.Error.WriteLine("The Pritset request did not complete.");
    Environment.ExitCode = 1;
}
catch (InvalidOperationException exception)
{
    Console.Error.WriteLine(exception.Message);
    Environment.ExitCode = 1;
}

