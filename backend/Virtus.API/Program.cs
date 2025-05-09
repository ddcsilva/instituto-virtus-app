using Virtus.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarControllers();
builder.Services.ConfigurarCors();
builder.Services.ConfigurarSwagger();
builder.Services.ConfigurarDependencias();

var app = builder.Build();

app.ConfigurarSwaggerUI();
app.ConfigurarMiddlewares();

app.Run();
