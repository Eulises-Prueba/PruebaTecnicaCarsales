using WebFront.Core.Interface;
using WebFront.Infrastructure.HttpClient;

using System.IO.Compression;
using System.Reflection;
using Microsoft.AspNetCore.ResponseCompression;
using NLog.Extensions.Logging;
using Polly;
using WebFront.Core;

var builder = WebApplication.CreateBuilder(args);
var urlApi = builder.Configuration["UrlRickAndMortyApi"]!;
bool.TryParse(builder.Configuration["ResponseCompression"]!, out var bCompress);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
});

builder.Services.AddResiliencePipeline("default", x =>
{
    x.AddRetry(new Polly.Retry.RetryStrategyOptions
    {
        ShouldHandle = new PredicateBuilder().Handle<Exception>(),
        Delay = TimeSpan.FromSeconds(2),
        MaxRetryAttempts = 3,
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true
    }).AddTimeout(TimeSpan.FromSeconds(15));
});

builder.WebHost.ConfigureKestrel(serverOptions => { serverOptions.AddServerHeader = false; });
builder.Services.AddHttpClient("RickAndMortyApi", client => { client.BaseAddress = new Uri(urlApi); });
builder.Services.AddSingleton<IRickAndMortyApiClient, RickAndMortyApiClient>();
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
builder.Services.AddSingleton<IConsultasEpisode, ServicioEpisodes>();

if (bCompress)
{
    builder.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    });
    builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
    builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.SmallestSize; });
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policyBuilder => policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .SetIsOriginAllowed(_ => true)
    );
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Consultas y Reportes");
        c.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseExceptionHandler("/error");
}

if (bCompress)
    app.UseResponseCompression();

app.UseCors("AllowAllOrigins");
app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();

app.Run();
