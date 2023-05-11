using MongoDB.Driver;
using OfferPrice.Profile.Api;
using OfferPrice.Profile.Api.Filters;
using OfferPrice.Profile.Domain;
using OfferPrice.Profile.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(provider =>
    new MongoClient(settings.Database.ConnectionString).GetDatabase(settings.Database.DatabaseName));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddProblemDetails();

builder.Services.AddScoped<OperationCanceledFilter>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(options =>
{
    options.Filters.Add<OperationCanceledFilter>();
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();