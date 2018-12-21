namespace ChessMaster.DataModel
{
    public class Keys
    {

        const string ChessMaster = "chessmaster";
        const string DataPullStatus = "datapullstatus";

        public static string CustomerDataPullStatusDate(string username) 
            => $"{ChessMaster}:{DataPullStatus}:{username}:date";
        public static string CustomerDataPullStatus(string username) 
            => $"{ChessMaster}:{DataPullStatus}:{username}";

        public static string MonthlyNumberOfGames(string username, string yyyymm) 
            => $"{ChessMaster}:{username}:{yyyymm}";
        public static string MonthlyGames(string username, string yyyymm) 
            => $"{ChessMaster}:{username}:{yyyymm}:games";
        public static string Game(string username, string yyyymm, string url) 
            => $"{ChessMaster}:{username}:{yyyymm}:games:{url}";
    }

    
}
