using Blazored.LocalStorage;
using Frontend.Models.Auth;
using System.Net.Http.Headers;

namespace Frontend.Handlers;

public class ApiHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public ApiHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AuthToken? token = await _localStorage.GetItemAsync<AuthToken>("authToken");

        if(token == null)
            return await base.SendAsync(request, cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}