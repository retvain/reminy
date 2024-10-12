using System.Net.Http.Json;
using System.Text.Json;
using Reminy.Core.Domain.Entity;
using Reminy.Core.Host.Composition.JsonSerialization;
using Reminy.Core.Host.Dto;

namespace Reminy.Core.IntegrationTests.Tools;

internal sealed class TestClient(HttpClient httpClient) : IDisposable
{
    private readonly JsonSerializerOptions _serializeOptionsDefault = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
        DictionaryKeyPolicy = new SnakeCaseNamingPolicy()
    };

    public async Task<User> RegisterUser(RegisterUserRequestDto request)
        => await PostRequest<RegisterUserRequestDto, User>("/api/v1/user/register", request);

    public async Task<Note> CreateNote(CreateNoteRequestDto request)
        => await PostRequest<CreateNoteRequestDto, Note>("/api/v1/note/create", request);

    public async Task<Note> UpdateNote(UpdateNoteRequestDto request)
        => await PostRequest<UpdateNoteRequestDto, Note>("/api/v1/note/update", request);
    
    public async Task<Note> GetNote(GetNoteRequestDto request)
        => await PostRequest<GetNoteRequestDto, Note>("/api/v1/note/get", request);

    private async Task<TResponse> PostRequest<TRequest, TResponse>(string url, TRequest request)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        requestMessage.Content = JsonContent.Create(request, options: _serializeOptionsDefault);

        var response = await httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TResponse>(responseJson, _serializeOptionsDefault)
               ?? throw new ApplicationException($"Cannot deserialize response {responseJson}");
    }

    public void Dispose()
        => httpClient.Dispose();
}