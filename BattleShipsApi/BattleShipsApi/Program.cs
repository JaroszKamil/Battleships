using BattleShipsApi.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplicationServices(builder.Configuration);

// Add services to the container.

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
