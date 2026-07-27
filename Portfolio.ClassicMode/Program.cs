using System.ComponentModel.DataAnnotations;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Portfolio.ClassicMode;
using Portfolio.ClassicMode.Configuration;
using Portfolio.ClassicMode.IServices;
using Portfolio.ClassicMode.Services;
using Portfolio.ClassicMode.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder
    .Services.AddOptions<ApiSettings>()
    .Bind(builder.Configuration.GetSection("ApiSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

//API base URL (same domain as Portfolio.Api)
builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;

    return new HttpClient { BaseAddress = new Uri(apiSettings.BaseUrl!) };
});

//Add resume url as singleton service
ResumeConfig resumeCongig = new();
builder.Configuration.GetSection("ResumeConfig").Bind(resumeCongig);
builder.Services.AddSingleton(resumeCongig);

builder.Services.AddScoped<PortfolioService>();

//Register LocamMemoryService
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IMemoryCache, MemoryCacheService>();
builder.Services.AddScoped<LocalStorageCacheService>();
builder.Services.AddScoped<HybridCacheService>();

await builder.Build().RunAsync();
