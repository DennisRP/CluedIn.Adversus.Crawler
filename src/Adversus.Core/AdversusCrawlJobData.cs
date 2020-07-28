using CluedIn.Core.Crawling;

namespace CluedIn.Crawling.Adversus.Core
{
    public class AdversusCrawlJobData : CrawlJobData
    {
        public string ApiKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
