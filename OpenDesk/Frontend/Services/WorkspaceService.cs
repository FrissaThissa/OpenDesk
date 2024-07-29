using Shared.Models;
using System.Net.Http.Json;

namespace Frontend.Services;

public class WorkspaceService
{
    private readonly HttpClient _httpClient;

    public WorkspaceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<WorkspaceDto>?> GetWorkspacesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<WorkspaceDto>>("api/Workspace");
    }
}
