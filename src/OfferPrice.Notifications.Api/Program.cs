using OfferPrice.Common.Extensions;
using OfferPrice.Notifications.Api.Extensions;
using OfferPrice.Notifications.Api.Filters;
using OfferPrice.Notifications.Api.Settings;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Infrastructure.Options;
using OfferPrice.Notifications.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddGatewayAuthentication();
builder.Services.RegisterSwagger();

builder.Services.AddVersioning(config);

builder.Services.RegisterDatabase(settings.Database);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRMQ(settings.RabbitMq);

builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssemblies(typeof(OfferPrice.Notifications.Application.Assembly).Assembly));

builder.Services.AddSingleton<IEmailProviderService, EmailProviderService>();
builder.Services.Configure<EmailOptions>(config.GetSection("Email"));

builder.Services.AddProblemDetails();
builder.Services.AddScoped<ExceptionFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();

app.Run();
