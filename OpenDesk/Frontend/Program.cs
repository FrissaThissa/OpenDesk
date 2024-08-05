using Blazored.LocalStorage;
using Frontend;
using Frontend.Handlers;
using Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<ApiHandler>();

builder.Services.AddHttpClient("API", client => client.BaseAddress = new Uri("https://localhost:7240"))
    .AddHttpMessageHandler<ApiHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<WorkspaceService>();
builder.Services.AddScoped<BoardService>();
builder.Services.AddScoped<CardService>();

builder.Services.AddSingleton<StateService>();

await builder.Build().RunAsync();
