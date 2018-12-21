using ChessMaster.DataModel;
using RedisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public DataStatus GenerateDataModel(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException();

            string customerDataPullStatusDateKey = Keys.CustomerDataPullStatusDate(username);
            string customerDataPullStatusKey = Keys.CustomerDataPullStatus(username);

            if (!_redisService.KeyExists(customerDataPullStatusDateKey) && !_redisService.KeyExists(customerDataPullStatusKey))
            {
                _redisService.Add(customerDataPullStatusDateKey, DateTime.Now.ToString("d"));
                _redisService.Add(customerDataPullStatusKey, DataPullStatusValue.Idle.ToString());
            }

            string customerDataPullStatus = _redisService.Get<string>(customerDataPullStatusKey);
            if (customerDataPullStatus == DataPullStatusValue.InProgress.ToString() || customerDataPullStatus == DataPullStatusValue.Ready.ToString())
            {
                return GetStatus(username);
            }

            Task.Run(() =>
            {
                _redisService.Add(customerDataPullStatusKey, DataPullStatusValue.InProgress.ToString());

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

                _redisService.Add(customerDataPullStatusKey, DataPullStatusValue.Ready.ToString());
            });

            return GetStatus(username);
        }


        public DataStatus GetStatus(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException();

            string customerDataPullStatusDateKey = Keys.CustomerDataPullStatusDate(username);
            string customerDataPullStatusKey = Keys.CustomerDataPullStatus(username);

            if (!_redisService.KeyExists(customerDataPullStatusDateKey) && !_redisService.KeyExists(customerDataPullStatusKey))
            {
                return new DataStatus
                {
                    username = username,
                    status = DataPullStatusValue.Idle.ToString()
                };
            }

            string customerDataPullStatus = _redisService.Get<string>(customerDataPullStatusKey);
            DateTime customerDataPullStatusDate = _redisService.Get<DateTime>(customerDataPullStatusDateKey);
            return new DataStatus
            {
                username = username,
                date = customerDataPullStatusDate,
                status = customerDataPullStatus
            };
        }
        #endregion
    }
}
