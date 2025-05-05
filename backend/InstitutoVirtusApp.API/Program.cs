using InstitutoVirtusApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCustomSwagger();
builder.Services.AddFirebase(builder.Configuration);
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCustomServices();

var app = builder.Build();

app.UseCustomSwagger(app.Environment);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
