using CluedIn.Crawling.Adversus.Core;

namespace CluedIn.Crawling.Adversus.Infrastructure.Factories
{
    public interface IAdversusClientFactory
    {
        AdversusClient CreateNew(AdversusCrawlJobData adversusCrawlJobData);
    }
}
