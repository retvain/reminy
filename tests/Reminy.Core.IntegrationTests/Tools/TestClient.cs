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
        => await PostWithResponse<RegisterUserRequestDto, User>("/api/v1/user/register", request);

    public async Task CreateNote(CreateNoteRequestDto request)
        => await Post("/api/v1/note/create", request);

    public async Task UpdateNote(UpdateNoteRequestDto request)
        => await Post("/api/v1/note/update", request);

    public async Task<Note> GetNote(GetNoteRequestDto request)
        => await PostWithResponse<GetNoteRequestDto, Note>("/api/v1/note/get", request);

    public async Task<IReadOnlyCollection<Note>> GetNotes(GetNotesRequestDto request)
        => await PostWithResponse<GetNotesRequestDto, IReadOnlyCollection<Note>>("/api/v1/notes/get", request);

    public async Task DeleteNote(DeleteNoteRequestDto request)
        => await Post("/api/v1/note/delete", request);

    private async Task<TResponse> PostWithResponse<TRequest, TResponse>(string url, TRequest request)
    {
        var response = await Post(url, request);
        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TResponse>(responseJson, _serializeOptionsDefault)
               ?? throw new ApplicationException($"Cannot deserialize response {responseJson}");
    }

    private async Task<HttpResponseMessage> Post<TRequest>(string url, TRequest request)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        requestMessage.Content = JsonContent.Create(request, options: _serializeOptionsDefault);

        var response = await httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public void Dispose()
        => httpClient.Dispose();
}