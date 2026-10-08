using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
//using Microsoft.Extensions.Configuration.Json;
using RiverBooks.Books;
using RiverBooks.OrderProcessing;
using RiverBooks.Users;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

logger.Information("Starting RiverBook Web Host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, loggerConfig) => loggerConfig.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var jwtSecret = builder.Configuration["Auth:JwtSecret"]
    ?? throw new InvalidOperationException("Auth:JwtSecret is not configured.");

#region Verifying Auth:JwtSecret configured or not in appsettings.json
// var provider = ((IConfigurationRoot)builder.Configuration).Providers
//     .Reverse()
//     .FirstOrDefault(p => p.TryGet("Auth:JwtSecret", out _));

// var source = provider is JsonConfigurationProvider json
//     ? json.Source.Path
//     : provider?.GetType().Name ?? "not found";

// logger.Information("JWT signing key source: {Source}; configured: {Configured}",
//     source, true);
#endregion

builder.Services
.AddAuthenticationJwtBearer(s => s.SigningKey = jwtSecret)
.AddAuthorization()
.SwaggerDocument()
.AddFastEndpoints();

// Add Module Services
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBookModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);

// Set up MediatR
builder.Services.AddMediatR(config =>
                  config.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints()
   .UseSwaggerGen();

// //Map Module Endpoints
// app.MapBookEndpoints();

app.Run();

public partial class Program { } // needed for testing 
