using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OfferPrice.Auction.Api;
using OfferPrice.Auction.Api.Filters;
using OfferPrice.Auction.Api.Hubs;
using OfferPrice.Auction.Domain;
using OfferPrice.Auction.Infrastructure;
using OfferPrice.Events.RabbitMq;
using System;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(settings!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSignalR();

ConventionRegistry.Register("IgnoreExtraElementsConvention",
    new ConventionPack { new IgnoreExtraElementsConvention(true) }, _ => true);
builder.Services.AddSingleton(_ => new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.Name));

builder.Services.AddSingleton<ILotRepository, LotRepository>();

builder.Services.AddRabbitMqConsumer<ProductCreatedEventConsumer>(settings.RabbitMq);

builder.Services.AddSingleton<OperationCanceledFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<OperationCanceledFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapHub<AuctionHub>("/auctionHub");

await app.RunAsync();

