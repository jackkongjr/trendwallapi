namespace trendwallapi.Models
{
    public class TrendsDatabaseSettings : ITrendsDatabaseSettings
    {
        public string TrendsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITrendsDatabaseSettings
    {
        string TrendsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}