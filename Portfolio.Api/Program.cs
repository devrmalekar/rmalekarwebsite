using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Context;
using Portfolio.Api.Helpers;
using Portfolio.Api.Interfaces;
using Portfolio.Api.Middleware;
using Portfolio.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

/*Add required services for blazor server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();*/

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

//AddDbContext
builder.Services.AddDbContext<PortfolioDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Register IService and their implementation
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IGitHubService, GitHubService>();

//builder.Services.AddScoped<IModeCacheService, ModeCacheService>(); -- for future

//add automapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<PortfolioMappingProfile>());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//Add Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(
        "PortfolioLimiter",
        limiterOptions =>
        {
            limiterOptions.PermitLimit = 10; // 20 requests
            limiterOptions.Window = TimeSpan.FromMinutes(1); // per minute
            limiterOptions.QueueLimit = 0;
        }
    );
});

//Compress response
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

//Initialize and seed database

var app = builder.Build();
await DatabaseInitializer.Initialize(app);

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseResponseCompression();
app.UseRateLimiter();
app.UseMiddleware<ResponseCapture>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();
app.MapMethods("/health", ["GET", "HEAD"], () => Results.Ok("Healthy!"));

/*
//Enable razor pages + blazor server
app.MapRazorPages();
app.MapBlazorHub();

app.UseStaticFiles();

// Home route: mode selector or redirect
/*app.MapGet(
    "/",
    async (HttpContext ctx, [FromServices] IThemeCacheService cache) =>
    {
        var ip = ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var theme = await cache.GetTheme(ip);

        if (!string.IsNullOrEmpty(theme))
        {
            return Results.Redirect($"/{theme}");
        }
        var file = Path.Combine(app.Environment.WebRootPath, "index.html");

        return Results.File(file, "text/html");
    }
);

// Route to Blazor WASM classic theme
app.MapFallbackToFile("/classic/{*path}", "classic/index.html");

// Route to Blazor Server developer theme
app.MapFallbackToPage("/developer/{*path}", "/_Host");

// Route to React space theme
app.MapFallbackToFile("/space/{*path}", "space/index.html");*/

app.UseMiddleware<ETagMiddleware>();

app.Run();
