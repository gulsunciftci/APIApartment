using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NLog;
using Repositories.EFCore;
using Services.Contracts;
using WebApi.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


        // Add services to the container.

        builder.Services.AddControllers()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
            .AddNewtonsoftJson(); //PATCH metodu için
                                  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureSqlContext(builder.Configuration);
        builder.Services.ConfigureRepositoryManager();
        builder.Services.ConfigureServiceManager();
        builder.Services.ConfigureLoggerService();

        var app = builder.Build();


        var logger = app.Services.GetRequiredService<ILoggerService>();
        app.ConfigureExceptionHandler(logger);
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsProduction())
        {
            app.UseHsts();
        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}