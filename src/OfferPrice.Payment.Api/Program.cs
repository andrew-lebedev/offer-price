using MongoDB.Driver;
using OfferPrice.Common.Extensions;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Payment.Domain.Interfaces;
using OfferPrice.Payment.Infrastructure.Repositories;
using OfferPrice.Payment.Api.Settings;
using OfferPrice.Payment.Infrastructure.Events;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(settings!);

builder.Services.AddSingleton(_ => new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

builder.Services.AddGatewayAuthentication();
builder.Services.RegisterSwagger();
builder.Services.AddVersioning(config);

builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ILotRepository, LotRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRabbitMqProducer(settings.RabbitMq);

builder.Services.AddRabbitMqConsumer<UserCreatedEventConsumer>(settings.RabbitMq);

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
