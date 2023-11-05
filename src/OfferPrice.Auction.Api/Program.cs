using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfferPrice.Auction.Api;
using OfferPrice.Auction.Api.Filters;
using OfferPrice.Auction.Api.Hubs;
using OfferPrice.Auction.Api.Jobs;
using OfferPrice.Auction.Infrastructure;
using OfferPrice.Common.Extensions;
using OfferPrice.Events.RabbitMq;
using System;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(settings!);

builder.Services.AddGatewayAuthentication();

builder.Services.RegisterSwagger().AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

builder.Services.AddVersioning(config);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSignalR();

builder.Services.AddInfrastructure(settings.Database);

builder.Services.AddRabbitMqProducer(settings.RabbitMq);
builder.Services.AddRabbitMqConsumer<ProductCreatedEventConsumer>(settings.RabbitMq);
builder.Services.AddRabbitMqConsumer<UserCreatedEventConsumer>(settings.RabbitMq);

builder.Services.AddSingleton<OperationCanceledFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<OperationCanceledFilter>();
});

builder.Services.AddJobs(settings.Auction);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();
app.MapHub<AuctionHub>("/hubs/auction");

await app.RunAsync();

