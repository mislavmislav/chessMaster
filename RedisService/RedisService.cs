using System;
using ServiceStack.Redis;

namespace RedisService
{
    public class RedisService : IRedisService
    {
        private RedisManagerPool _redisManagerPool;

        public RedisService(string connectionName)
        {
            _redisManagerPool = new RedisManagerPool(connectionName);
        }

        public bool CheckStatus()
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    return _client.Get<bool>("status");
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }

        public void AddArchive(DateTime dateTime, int count)
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    _client.Set<string>(dateTime.ToLongDateString(), count.ToString());
                }
                catch (System.Exception)
                {
                }
            }
        }

    }
}
