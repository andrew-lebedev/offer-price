using MongoDB.Driver;
using OfferPrice.Common.Extensions;
using OfferPrice.Payment.Api;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Payment.Domain;
using OfferPrice.Payment.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(settings!);

builder.Services.AddSingleton(_ => new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

builder.Services.AddGatewayAuthentication();
builder.Services.RegisterSwagger();
builder.Services.AddVersioning(config);

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRabbitMqProducer(settings.RabbitMq);

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
