namespace fgasfsasf.Methods;

public partial class queue
{
    public class Geolocation
    {
    }

    public class CustomData
    {
        public string SessionSettings { get; set; }
    }

    public class Rating
    {
        public double rating { get; set; }
        public double RD { get; set; }
        public double volatility { get; set; }
        public double realRating { get; set; }
    }

    public class Regions
    {
        public List<string> good { get; set; }
        public List<string> ok { get; set; }
    }

    public class Skill
    {
        public string country { get; set; }
        public string continent { get; set; }
        public Rating rating { get; set; }
        public int rank { get; set; }
        public int version { get; set; }
        public int x { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Regions regions { get; set; }
    }

    public class Props
    {
        public string EncryptionKey { get; set; }
        public int countA { get; set; }
        public int countB { get; set; }
        public string gameMode { get; set; }
        public string platform { get; set; }
        public string CrossplayOptOut { get; set; }
        public string characterName { get; set; }
        public string PlatformSessionIdForDS { get; set; }
        public bool isDedicated { get; set; }
    }

    public class QueueMatchData
    {
        public string matchId { get; set; }
        public int schema { get; set; }
        public string category { get; set; }
        public int rank { get; set; }
        public Geolocation geolocation { get; set; }
        public long creationDateTime { get; set; }
        public string status { get; set; }
        public string creator { get; set; }
        public CustomData customData { get; set; }
        public int version { get; set; }
        public Skill skill { get; set; }
        public int churn { get; set; }
        public Props props { get; set; }
        public string reason { get; set; }
        public string region { get; set; }
        public List<string> sideA { get; set; }
        public List<string> sideB { get; set; }
    }

    public class QueueMatchedRoot
    {
        public string status { get; set; }
        public QueueMatchData matchData { get; set; }
    }
}