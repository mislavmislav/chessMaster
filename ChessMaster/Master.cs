using ChessMaster.DataModel;
using RedisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessMaster
{
    public class Master : IMaster
    {
        private IRedisService _redisService;
        private Dictionary<string, MonthGames> _completeArchive;
        public Master(IRedisService redisService)
        {
            _redisService = redisService;
        }

        #region Data pull

        public void CheckDataModel(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException();

            string customerDataPullStatusKey = Keys.CustomerDataPullStatus(username);

            if (!_redisService.KeyExists(customerDataPullStatusKey)) _redisService.Add(customerDataPullStatusKey, DateTime.Now.ToString("d"));

            DateTime customerDataPullStatus = _redisService.Get<DateTime>(customerDataPullStatusKey);
            //if (!(DateTime.Today > customerDataPullStatus.Date)) return;

            var stats = ChessComClient.GetStats(username);
            var months = ChessComClient.GetMonthlyStats(username);

            _completeArchive = new Dictionary<string, MonthGames>();

            foreach (var monthArchive in months.Archives)
            {
                var games = ChessComClient.GetMonthlyGames(monthArchive.AbsoluteUri);
                var month = int.Parse(monthArchive.Segments[6]);
                var year = int.Parse(monthArchive.Segments[5].Split('/').First());
                var yyyyMM = new DateTime(year, month, 1).ToString("yyyyMM");

                _completeArchive.Add(yyyyMM, games);
                _redisService.Add(Keys.MonthlyNumberOfGames(username, yyyyMM), games.Games.Count);

                foreach (var game in games.Games)
                {
                    var gameKey = Keys.Game(username, yyyyMM, game.Url.ToString());
                    _redisService.SortedSetAdd(Keys.MonthlyGames(username, yyyyMM), gameKey, yyyyMM);
                    _redisService.Add(gameKey, game);
                }
            }
        }
        public object GetStatus(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException();
            string customerDataPullStatusKey = Keys.CustomerDataPullStatus(username);
            if (!_redisService.KeyExists(customerDataPullStatusKey))
                return new
                {
                    username = username,
                    status = "Data not present!"
                };

            DateTime customerDataPullStatus = _redisService.Get<DateTime>(customerDataPullStatusKey);
            return new
            {
                username = username,
                date = customerDataPullStatus,
                status = "Data present!"
            };
        }
        #endregion
    }
}
