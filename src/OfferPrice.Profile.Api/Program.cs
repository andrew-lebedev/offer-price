using MongoDB.Driver;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Profile.Api;
using OfferPrice.Profile.Api.Filters;
using OfferPrice.Profile.Domain;
using OfferPrice.Profile.Infrastructure;
using OfferPrice.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(settings!);

builder.Services.AddGatewayAuthentication();
builder.Services.RegisterSwagger();
builder.Services.AddVersioning(config);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton(_ => new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddRabbitMqProducer(settings.RabbitMq);

builder.Services.AddProblemDetails();
builder.Services.AddScoped<OperationCanceledFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<OperationCanceledFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();

await app.RunAsync();