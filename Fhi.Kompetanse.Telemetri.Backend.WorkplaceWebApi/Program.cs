
using Fhi.Kompetanse.Telemetri.Backend.WorkplaceWebApi.Context;
using Fhi.Kompetanse.Telemetri.Contracts;
using Microsoft.EntityFrameworkCore;
using Refit;

namespace Fhi.Kompetanse.Telemetri.Backend.WorkplaceWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AppContext.SetSwitch("System.Globalization.Invariant", true);

            builder.Services.AddDbContext<WorkContext>(options
                => options.UseSqlServer(builder.Configuration["ConnectionStrings:WorkDB"], o => o.UseCompatibilityLevel(120)));

            builder.Services.AddHttpClient("WorkplaceWebApi", c =>
            {
                c.Timeout = new TimeSpan(0, 0, 0, 10);
                c.BaseAddress = new Uri("https://localhost:7129");
            })
            .AddTypedClient(c => RestService.For<IWorkplaceWebApi>(c, new RefitSettings
            {
                //   ContentSerializer = new SystemTextJsonContentSerializer(builder.Services.DefaultJsonSerializationOptions())
            }));




            const string serviceName = "PersonWebApi";

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

            app.Run();
        }
    }
}
