using System;

namespace RedisService
{
    public interface IRedisService
    {
        bool CheckStatus();
        bool KeyExists(string key);
        void Add<T>(string key, T value);
        T Get<T>(string key);
        void SortedSetAdd(string v, string gameKey, string yyyyMM);
    }
}