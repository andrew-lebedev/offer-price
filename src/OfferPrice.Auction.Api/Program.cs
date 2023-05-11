using MongoDB.Driver;
using OfferPrice.Auction.Api;
using OfferPrice.Auction.Api.Filters;
using OfferPrice.Auction.Api.Hubs;
using OfferPrice.Auction.Domain;
using OfferPrice.Auction.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSignalR();

builder.Services.AddSingleton(
    provider => new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName)
    );

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

builder.Services.AddScoped<OperationCanceledFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<OperationCanceledFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.MapHub<AuctionHub>("/auctionHub");

await app.RunAsync();

