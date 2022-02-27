using BlogApp.Application;
using BlogApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
