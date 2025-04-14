using Microsoft.Net.Http.Headers;
using RMalekarAPI.Services;
using RMalekarEntityModels;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS from Settings.
ConfigureCors(builder);

builder.Services.AddRMalekarDataContext();

// Other service configurations
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keeps property names as defined in the model
        options.JsonSerializerOptions.WriteIndented = true; // Pretty-print JSON (optional)
    });

builder.Services.AddScoped<UpdateDataScheduler>();
builder.Services.AddHostedService<UpdateDataScheduler>();
builder.Services.AddSingleton<GitHubJsonUpdater>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<EmailService>();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-rmalekarapi-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

CheckDbConnection();

ConfigureMiddleware(app);

app.Run();

void ConfigureCors(WebApplicationBuilder webBuilder)
{
    var corsConfig = webBuilder.Configuration.GetSection("Cors");
    var origins = corsConfig.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    var methods = corsConfig.GetSection("AllowedMethods").Get<string[]>() ?? Array.Empty<string>();

    webBuilder.Services.AddCors(options =>
    {
        options.AddPolicy("SecureCors", policy =>
        {
            if (webBuilder.Environment.IsDevelopment())
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
            }
            else
            {
                policy.WithOrigins(origins)
                     .WithMethods(methods);
            }
            policy.WithHeaders(
                    HeaderNames.ContentType,
                    HeaderNames.Authorization,
                    HeaderNames.Accept,
                    HeaderNames.AcceptLanguage,
                    "X-Requested-With", //common ajax header
                    "X-CSRF-Token",  //common anti-forgery token header
                    "If-Modified-Since" //for caching
                 )
                .SetPreflightMaxAge(TimeSpan.FromMinutes(10));

            //Strict transport security for production
            if (!webBuilder.Environment.IsDevelopment())
            {
                policy.WithExposedHeaders(
                    "Content-Disposition", //for file download
                    "X-Pagination" // for pagination metadata
                );
            }
        });
    });
}

void ConfigureMiddleware(WebApplication app)
{
    // Security first
    app.UseHttpsRedirection();
    app.UseMiddleware<DatabaseErrorMiddleware>();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
            context.Response.Headers.Append("Permission-Policy", "geolocation()");
            await next();
        });
    }

    // CORS before auth and endpoints
    app.UseCors("SecureCors");

    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();

    // Health check endpoints (useful for production monitoring)
    app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" })).RequireCors();
}

void CheckDbConnection()
{
   // var provider = service
}
