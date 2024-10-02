using System.Net.Http.Json;
using System.Text.Json;
using Reminy.Core.Host.Composition.JsonSerialization;
using Reminy.Core.Host.Contracts;

namespace Reminy.Core.IntegrationTests.Tools;

internal sealed class TestClient(HttpClient httpClient) : IDisposable
{
    private readonly JsonSerializerOptions _serializeOptionsDefault = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
        DictionaryKeyPolicy = new SnakeCaseNamingPolicy()
    };

    public async Task RegisterUser(RegisterUserRequestDto request)
    {
        const string url = "/api/v1/user/register";

        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        requestMessage.Content = JsonContent.Create(request, options: _serializeOptionsDefault);

        var response = await httpClient.SendAsync(requestMessage);

        response.EnsureSuccessStatusCode();
    }

    public void Dispose()
        => httpClient.Dispose();
}