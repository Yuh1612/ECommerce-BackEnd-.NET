using RabbitMQ.Client;

namespace RabbitMQ.Models
{
    public class RabbitMQConnection
    {
        private ConnectionFactory _factory;

        private IConnection? _conn;

        private IModel? _channel;

        private string _hostName;

        private int _port;

        private string? _usermame;

        private string? _password;

        private string? _virtualHost;

        public RabbitMQConnection(string host, int port = 5672, string? user = null, string? password = null, string? virtualHost = null)
        {
            _hostName = host;
            _port = port;
            _usermame = user;
            _password = password;
            _channel = default;
            _conn = default;
            _factory = new ConnectionFactory() { HostName = host, Port = port };
            _virtualHost = virtualHost;
        }

        public IModel Channel
        {
            get
            {
                if (_channel == null) this.Connect();
                return _channel ?? throw new Exception("Some thing went wrong with RabbitMQ");
            }
        }

        public void Connect()
        {
            _factory = new ConnectionFactory() { HostName = _hostName, Port = _port };
            if (!string.IsNullOrEmpty(_usermame) && !string.IsNullOrEmpty(_password))
            {
                _factory.VirtualHost = _virtualHost;
                _factory.UserName = _usermame;
                _factory.Password = _password;
            }

            //_factory = new ConnectionFactory() { Uri = new Uri(_hostName) };

            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.ExchangeDeclare(RabbitMQServiceBusSettings.Exchange, ExchangeType.Fanout);
        }
    }
}