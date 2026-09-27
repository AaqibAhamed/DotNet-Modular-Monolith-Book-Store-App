using FastEndpoints;
using RiverBooks.Books;
using RiverBooks.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();

// Add Module Services
builder.Services.AddBookServices(builder.Configuration);
builder.Services.AddUserModuleServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

// //Map Module Endpoints
// app.MapBookEndpoints();

app.Run();

public partial class Program { } // needed for testing 
