using InstitutoVirtusApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomSwagger();
builder.Services.AddFirebase(builder.Configuration);
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCustomSwagger(app.Environment);
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/ping", () => "pong").RequireAuthorization();

app.Run();
