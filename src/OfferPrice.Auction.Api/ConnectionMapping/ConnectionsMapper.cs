namespace OfferPrice.Auction.Api.ConnectionMapping;
public class ConnectionsMapper<T>
{
    private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

    public HashSet<string> GetAuctionConnections(T key)
    {
        _connections.TryGetValue(key, out var connection);
        return connection;
    }

    public IEnumerable<T> GetAllConnections()
    {
        return _connections.Keys;
    }

    public void AddConnection(T key, string connectionId)
    {
        lock (_connections)
        {
            HashSet<string> connection;
            if (!_connections.TryGetValue(key, out connection))
            {
                connection = new HashSet<string>();
                _connections.Add(key, connection);
            }

            lock (connection)
            {
                connection.Add(connectionId);
            }
        }
    }

    public void RemoveConnection(T key, string connectionId)
    {
        lock (_connections)
        {
            HashSet<string> connection;
            if (!_connections.TryGetValue(key, out connection))
            {
                return;
            }

            lock (connection)
            {
                connection.Remove(connectionId);

                if (connection.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }
}

