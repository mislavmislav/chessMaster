using ServiceStack.Redis;

namespace RClient
{
    public class ClientFactory
    {
        private static Client _client;

        public static Client GetClient(string connectionName)
        {
            if (_client == null)
            {
                _client = new Client(connectionName);
            }
            return _client;
        }
    }
    public class Client
    {
        private RedisManagerPool _redisManagerPool;

        public Client(string connectionName)
        {
            if (_redisManagerPool == null)
            {
                _redisManagerPool = new RedisManagerPool(connectionName);
            }
        }

        public bool CheckStatus()
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    return _client.Get<bool>("status");
                }
                catch (System.Exception e)
                {
                    return false;
                }
            }
        }

        public string Test()
        {
            var manager = new RedisManagerPool("rediscache");
            using (var client = manager.GetClient())
            {
                client.Set("foo", "bar");
                return $"foo={client.Get<string>("foo")}";
            }
        }
    }
}
