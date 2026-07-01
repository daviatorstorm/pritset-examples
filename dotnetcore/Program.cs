using System.Net.Http.Headers;
using System.Text.Json;
using PritsetDotnetExample;

var result = PritsetClient.CreateRequest(
    templateId: "your-template-id" // Replace with your actual template ID
);

using var httpClient = new HttpClient();
using var form = new MultipartFormDataContent();

form.Add(
    new StringContent(result.Data.ToJsonString(new JsonSerializerOptions
    {
        WriteIndented = false,
    })),
    "data"
);

using var request = new HttpRequestMessage(HttpMethod.Post, result.Api)
{
    Content = form,
};

request.Headers.TryAddWithoutValidation("Authorization", result.Token);
request.Headers.Add("X-Secret", result.Secret);

using var response = await httpClient.SendAsync(request);

if (response.IsSuccessStatusCode)
{
    var pdf = await response.Content.ReadAsByteArrayAsync();
    await File.WriteAllBytesAsync("generated-document.pdf", pdf);
    Console.WriteLine("Document generated successfully.");
}
else
{
    Console.WriteLine($"Error occurred: {(int)response.StatusCode} {response.ReasonPhrase}");
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

