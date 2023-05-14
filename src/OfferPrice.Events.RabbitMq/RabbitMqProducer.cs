using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OfferPrice.Events.RabbitMq;

public class RabbitMqProducer : IProducer
{
    private readonly IQueueResolver _queueResolver;
    private readonly RabbitMqSettings _settings;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public RabbitMqProducer(IQueueResolver queueResolver, RabbitMqSettings settings)
    {
        _queueResolver = queueResolver;
        _settings = settings;
    }

    public void SendMessage<T>(T message) where T : Event
    {
        var factory = new ConnectionFactory { HostName = _settings.Host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var queue = _queueResolver.Get<T>();
        
        channel.QueueDeclare(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var json = JsonSerializer.Serialize(message, _jsonSerializerOptions);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: body
        );
    }
}