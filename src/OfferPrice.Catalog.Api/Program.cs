using MongoDB.Driver;
using OfferPrice.Catalog.Api;
using OfferPrice.Catalog.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>()!;

builder.Services
    .AddSingleton(provider =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.MapGet("/init", () => "kyky")
//.WithName("Init")
//.WithOpenApi();

app.UseRouting();

app.MapControllers();

await app.RunAsync();
