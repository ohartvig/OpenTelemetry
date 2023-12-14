using Fhi.Kompetanse.Telemetri.Contracts;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Refit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
//.Enrich.FromLogContext()
.CreateLogger();
Log.Logger = logger;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient("PersonWebApi", c =>
        {
            c.Timeout = new TimeSpan(0, 0, 0, 10);
             c.BaseAddress = new Uri("https://localhost:7239");
        })
       .AddTypedClient(c => RestService.For<IPersonWebApi>(c, new RefitSettings
       {
       }));

builder.Services.AddHttpClient("WorkplaceWebApi", c =>
{
    c.Timeout = new TimeSpan(0, 0, 0, 10);
    c.BaseAddress = new Uri("https://localhost:7129");
})
       .AddTypedClient(c => RestService.For<IWorkplaceWebApi>(c, new RefitSettings
       {
       }));



const string serviceName = "kompetanse";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});

builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService(serviceName))
      .WithTracing(tracing => tracing
          .AddAspNetCoreInstrumentation()
          .AddHttpClientInstrumentation()
          .AddConsoleExporter()
          .AddOtlpExporter()
          )
      .WithMetrics(metrics => metrics
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter());




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



logger.Information("Start Run");
app.Run();
