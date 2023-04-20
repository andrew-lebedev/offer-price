using OfferPrice.Catalog.Api;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>()!;

builder.Services.AddSingleton(settings);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/init", () => "kyky")
.WithName("Init")
.WithOpenApi();

await app.RunAsync();
