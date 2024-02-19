using JwtStore.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration(); 

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
