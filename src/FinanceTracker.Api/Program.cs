using FinanceTracker.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterDispatchers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
