using ChessMaster;
using Microsoft.AspNetCore.Mvc;
using RedisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessController : ControllerBase
    {
        private IRedisService _redisService;

        public ChessController(IRedisService redisService)
        {
            _redisService = redisService;
        }
        // GET api/chess
        //  method will return state of scraping
        //[HttpGet]
        //public ActionResult<bool> Get()
        //{
        //    var client = ClientFactory.GetClient("rediscache");
        //    return client.CheckStatus();
        //}

        // GET api/chess
        //  method will return state of scraping
        [HttpGet]
        public ActionResult<bool> Get()
        {
            return _redisService.CheckStatus();
        }

        // GET api/chess
        //  method will return state of scraping
        [HttpGet("{id}")]
        public ActionResult<bool> Get(string id)
        {
            var username = "mislavmislav";
            var client = ClientFactory.GetClient("rediscache");

            var stats = ChessComClient.GetStats(username);
            var months = ChessComClient.GetMonthlyStats(username);

            Dictionary<DateTime, MonthGames> completeArchive = new Dictionary<DateTime, MonthGames>();

            foreach (var monthArchive in months.Archives)
            {
                var games = ChessComClient.GetMonthlyGames(monthArchive.AbsoluteUri);
                var month = int.Parse(monthArchive.Segments[6]);
                var year = int.Parse(monthArchive.Segments[5].Split('/').First());
                completeArchive.Add(new DateTime(year, month, 1), games);

                client.AddArchive(new DateTime(year, month, 1), games.Games.Count);
            }

            return true;
        }
    }
      
}