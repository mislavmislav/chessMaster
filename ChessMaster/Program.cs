using ChessComClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var username = "mislavmislav";

                var stats = Client.GetStats(username);
                var months = Client.GetMonthlyStats(username);

                Dictionary<DateTime, MonthGames> completeArchive = new Dictionary<DateTime, MonthGames>();

                foreach (var monthArchive in months.Archives)
                {
                    var games = Client.GetMonthlyGames(monthArchive.AbsoluteUri);
                    var month = int.Parse(monthArchive.Segments[6]);
                    var year = int.Parse(monthArchive.Segments[5].Split('/').First());
                    completeArchive.Add(new DateTime(year, month, 1), games);
                }
            }
            catch (System.Exception e)
            {
                throw;
            }
        }

       
    }
}
