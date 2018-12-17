using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ChessMaster
{
    public partial class Stats
    {
        [JsonProperty("chess_daily")]
        public ChessDaily ChessDaily { get; set; }

        [JsonProperty("chess960_daily")]
        public ChessDaily Chess960Daily { get; set; }

        [JsonProperty("chess_rapid")]
        public Chess ChessRapid { get; set; }

        [JsonProperty("chess_bullet")]
        public Chess ChessBullet { get; set; }

        [JsonProperty("chess_blitz")]
        public Chess ChessBlitz { get; set; }
    }

    public partial class ChessDaily
    {
        [JsonProperty("last")]
        public Last Last { get; set; }

        [JsonProperty("best")]
        public Best Best { get; set; }

        [JsonProperty("record")]
        public Chess960DailyRecord Record { get; set; }
    }

    public partial class Best
    {
        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("game")]
        public Uri Game { get; set; }
    }

    public partial class Last
    {
        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("rd")]
        public string Rd { get; set; }
    }

    public partial class Chess960DailyRecord
    {
        [JsonProperty("win")]
        public string Win { get; set; }

        [JsonProperty("loss")]
        public string Loss { get; set; }

        [JsonProperty("draw")]
        public string Draw { get; set; }

        [JsonProperty("time_per_move")]
        public string TimePerMove { get; set; }

        [JsonProperty("timeout_percent")]
        public string TimeoutPercent { get; set; }
    }

    public partial class Chess
    {
        [JsonProperty("last")]
        public Last Last { get; set; }

        [JsonProperty("best")]
        public Best Best { get; set; }

        [JsonProperty("record")]
        public ChessBlitzRecord Record { get; set; }
    }

    public partial class ChessBlitzRecord
    {
        [JsonProperty("win")]
        public string Win { get; set; }

        [JsonProperty("loss")]
        public string Loss { get; set; }

        [JsonProperty("draw")]
        public string Draw { get; set; }
    }

    public partial class Stats
    {
        public static Stats FromJson(string json) => JsonConvert.DeserializeObject<Stats>(json, Converter.Settings);
    }

    public partial class Archive
    {
        [JsonProperty("archives")]
        public List<Uri> Archives { get; set; }
    }

    public partial class Archive
    {
        public static Archive FromJson(string json) => JsonConvert.DeserializeObject<Archive>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public partial class MonthGames
    {
        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }

    public partial class Game
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("pgn")]
        public string Pgn { get; set; }

        [JsonProperty("time_control")]
        public string TimeControl { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("rated")]
        public bool Rated { get; set; }

        [JsonProperty("fen")]
        public string Fen { get; set; }

        [JsonProperty("time_class")]
        public string TimeClass { get; set; }

        [JsonProperty("rules")]
        public string Rules { get; set; }

        [JsonProperty("white")]
        public White White { get; set; }

        [JsonProperty("black")]
        public Black Black { get; set; }
    }

    public partial class Black
    {
        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public partial class White : Black
    {

    }

    public enum Result { Checkmated, Insufficient, Resigned, Timeout, Win, Abandoned, Agreed };

    public enum Rules { Chess };

    public enum TimeClass { Blitz };

    public partial class MonthGames
    {
        public static MonthGames FromJson(string json) => JsonConvert.DeserializeObject<MonthGames>(json, Converter.Settings);
    }
}
