using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Events.RabbitMq;

public abstract class RabbitMqConsumer<T>: IConsumer where T: Event
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    
    private readonly ILogger<RabbitMqConsumer<T>> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected RabbitMqConsumer(IQueueResolver queueResolver, RabbitMqSettings settings, ILogger<RabbitMqConsumer<T>> logger)
    {
        _logger = logger;

        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        var queue = queueResolver.Get<T>();
        _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, eventArgs) =>
        {
            var json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            _logger.LogInformation("Start processing event {message}", json);

            var message = JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

            try
            {
                await Execute(message, CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "End processing event {message}", json);
                return;
            }
            
            _logger.LogInformation("End processing event {event_id}", message.Id);
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(queue, false, consumer);
    }

    protected abstract Task Execute(T message, CancellationToken cancellationToken);
    
    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}