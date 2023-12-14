using Fhi.Kompetanse.Telemetri.Backend.PersonWebApi;
using Fhi.Kompetanse.Telemetri.Backend.PersonWebApi.Persistence.Context;
using Fhi.Kompetanse.Telemetri.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Refit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
//.Enrich.FromLogContext()
.CreateLogger();
Log.Logger = logger;

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

logger.Information("Start");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<PersonContext>(options
       => options.UseSqlServer(builder.Configuration["ConnectionStrings:PersonDB"], o => o.UseCompatibilityLevel(120)));




builder.Services.AddHttpClient("PersonWebApi", c =>
{
    c.Timeout = new TimeSpan(0, 0, 0, 10);
    c.BaseAddress = new Uri("https://localhost:7247");
})
       .AddTypedClient(c => RestService.For<IAddressWebApi>(c, new RefitSettings
       {
           //   ContentSerializer = new SystemTextJsonContentSerializer(builder.Services.DefaultJsonSerializationOptions())
       }));




const string serviceName = "PersonWebApi";

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
          .AddSqlClientInstrumentation(options => options.SetDbStatementForText = true)
          .AddHttpClientInstrumentation()
          .AddConsoleExporter()
          .AddOtlpExporter()
          .AddSource("PersonWebApi")
          )
      .WithMetrics(metrics => metrics
         // .AddAspNetCoreInstrumentation()
          .AddMeter(Telemetry.greeterMeter.Name)
          .AddConsoleExporter()
          .AddPrometheusExporter()
          );




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


app.MapPrometheusScrapingEndpoint();

logger.Information("Start Migrate");


using (var scope = app.Services.CreateScope())
{
    logger.Information("Migrate PersonContext");
    var db = scope.ServiceProvider.GetRequiredService<PersonContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception exp)
    {
        logger.Error($"Migrate PersonContext exp {exp.Message} ", exp);
    }
}



logger.Information("Start Run");

app.Run();
