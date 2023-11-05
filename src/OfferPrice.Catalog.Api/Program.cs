using MongoDB.Driver;
using OfferPrice.Catalog.Api;
using OfferPrice.Catalog.Api.Filters;
using OfferPrice.Catalog.Domain;
using OfferPrice.Catalog.Infrastructure;
using OfferPrice.Common.Extensions;
using OfferPrice.Events.RabbitMq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var settings = builder.Configuration.Get<AppSettings>()!;

builder.Services.AddSingleton(settings);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddSingleton(_ =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.Name));

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ILikeRepository, LikeRepository>();

builder.Services.AddGatewayAuthentication();
builder.Services.RegisterSwagger();
builder.Services.AddVersioning(config);

builder.Services.AddRabbitMqProducer(settings.RabbitMq);
builder.Services.AddRabbitMqConsumer<LotStatusUpdatedEventConsumer>(settings.RabbitMq);

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

app.UseAuthentication();

app.MapControllers();

await app.RunAsync();
