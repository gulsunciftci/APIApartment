using AspNetCoreRateLimit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using WebApi.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


        // Add services to the container.

        builder.Services.AddControllers(config => {

            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
            config.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 });
        })
            //.AddXmlDataContractSerializerFormatters()
             //.AddCustomCsvFormatter()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
            .AddNewtonsoftJson(); //PATCH metodu için


     

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureSqlContext(builder.Configuration);
        builder.Services.ConfigureRepositoryManager();
        builder.Services.ConfigureServiceManager();
        builder.Services.ConfigureLoggerService();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.ConfigureActionFilters();
        builder.Services.ConfigureCors();
        builder.Services.ConfigureDataShaper();
        builder.Services.AddCustomMediaTypes();
        builder.Services.AddScoped<IApartmentLinks, ApartmentLinks>();
        builder.Services.ConfigureVersioning();
        builder.Services.ConfigureResponseCaching();
        builder.Services.ConfigureHttpCacheHeaders();
        builder.Services.AddMemoryCache();
        builder.Services.ConfigureRateLimitingOptions();
        builder.Services.AddHttpContextAccessor();
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
        app.UseIpRateLimiting();
        app.UseCors("CorsPolicy");
        app.UseResponseCaching();
        app.UseHttpCacheHeaders();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}