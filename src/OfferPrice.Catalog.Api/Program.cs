using MongoDB.Driver;
using OfferPrice.Catalog.Api;
using OfferPrice.Catalog.Api.Filters;
using OfferPrice.Catalog.Domain;
using OfferPrice.Catalog.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>()!;

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddSingleton(provider =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<OperationCanceledFilter>();

builder.Services.AddControllers(options => 
{
    options.Filters.Add<OperationCanceledFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();
