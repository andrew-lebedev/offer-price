using MongoDB.Driver;
using OfferPrice.Catalog.Api;
using OfferPrice.Catalog.Api.DataService;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>()!;

builder.Services
    .AddSingleton(provider =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

builder.Services.AddScoped<IDatabaseService,DatabaseService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();

await app.RunAsync();
