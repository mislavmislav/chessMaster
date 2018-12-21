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
        public bool KeyExists(string key)
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    var value = _client.Get<string>(key);
                    if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;
                }
                catch (System.Exception)
                {
                    return false;
                }

                return true;
            }
        }
        public void Add<T>(string key, T value)
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    _client.Set(key, value);
                }
                catch (System.Exception)
                {
                }
            }
        }
        public T Get<T>(string key)
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    return _client.Get<T>(key);
                }
                catch (System.Exception)
                {
                    return default(T);
                }
            }
        }
        public void SortedSetAdd(string sortedSetKey, string gameKey, string score)
        {
            using (var _client = _redisManagerPool.GetClient())
            {
                try
                {
                    _client.AddItemToSortedSet(sortedSetKey, gameKey, double.Parse(score));
                }
                catch (System.Exception e)
                {
                   
                }
            }
        }
    }
}
