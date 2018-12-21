using ChessMaster.DataModel;
using RedisService;
using System;

namespace ChessMaster
{
    public class Master
    {
        private IRedisService _redisService;
        public Master(IRedisService redisService)
        {
            _redisService = redisService;
        }

        #region Data pull

        public void CheckDataModel(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException();

            var customerDataPullStatusKey = Keys.CustomerDataPullStatus(username);
            var keyExists = _redisService.KeyExists(customerDataPullStatusKey);
            if (keyExists)
            {
                var value = _redisService.Get<DateTime>(customerDataPullStatusKey);
            }
            else
            {
                _redisService.Add(customerDataPullStatusKey, DateTime.Now.ToString("d"));
            }
        }
        #endregion
    }
}
