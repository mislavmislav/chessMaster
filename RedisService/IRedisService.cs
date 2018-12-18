using System;

namespace RedisService
{
    public interface IRedisService
    {
        bool CheckStatus();
        void AddArchive(DateTime dateTime, int count);
    }
}