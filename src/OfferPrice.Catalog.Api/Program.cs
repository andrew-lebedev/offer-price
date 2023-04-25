using AutoMapper;
using MongoDB.Driver;
using OfferPrice.Catalog.Api;
using OfferPrice.Catalog.Api.DataService;
using OfferPrice.Catalog.Api.Mapper;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>()!;

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services
    .AddSingleton(provider =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IDatabaseService, DatabaseService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();

await app.RunAsync();
