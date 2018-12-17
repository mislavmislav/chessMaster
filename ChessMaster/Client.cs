using RestSharp;

namespace ChessMaster
{
    public class Client
    {
        public static Stats GetStats(string username)
        {
            RestResponse restresponse = Execute($"https://api.chess.com/pub/player/{username}/stats");
            return Stats.FromJson(restresponse.Content);
        }
        public static Archive GetMonthlyStats(string username)
        {
            RestResponse restresponse = Execute($"https://api.chess.com/pub/player/{username}/games/archives");
            return Archive.FromJson(restresponse.Content);
        }
        public static MonthGames GetMonthlyGames(string uri)
        {
            RestResponse restresponse = Execute(uri);
            return MonthGames.FromJson(restresponse.Content);
        }
        private static RestResponse Execute(string uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            return (RestResponse)client.Execute(request);
        }
    }
}
