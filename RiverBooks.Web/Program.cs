using FastEndpoints;
using RiverBooks.Books;
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
builder.Services.AddFastEndpoints();

// Add Module Services
builder.Services.AddBookServices(builder.Configuration, logger);
builder.Services.AddUserModuleServices(builder.Configuration, logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseFastEndpoints();

// //Map Module Endpoints
// app.MapBookEndpoints();

app.Run();

public partial class Program { } // needed for testing 
