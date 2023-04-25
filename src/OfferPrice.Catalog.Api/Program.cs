using MongoDB.Driver;
using OfferPrice.Catalog.Api;
using OfferPrice.Catalog.Domain;
using OfferPrice.Catalog.Infrastucture;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>()!;

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddSingleton(provider =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();

await app.RunAsync();
