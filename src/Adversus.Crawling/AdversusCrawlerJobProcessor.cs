using CluedIn.Crawling.Adversus.Core;

namespace CluedIn.Crawling.Adversus
{
    public class AdversusCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<AdversusCrawlJobData>
    {
        public AdversusCrawlerJobProcessor(AdversusCrawlerComponent component) : base(component)
        {
        }
    }
}
