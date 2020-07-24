using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Adversus.Core;
using CluedIn.Crawling.Adversus.Infrastructure.Factories;

namespace CluedIn.Crawling.Adversus
{
    public class AdversusCrawler : ICrawlerDataGenerator
    {
        private readonly IAdversusClientFactory clientFactory;
        public AdversusCrawler(IAdversusClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is AdversusCrawlJobData adversuscrawlJobData))
            {
                yield break;
            }

            var client = clientFactory.CreateNew(adversuscrawlJobData);

            //retrieve data from provider and yield objects
            foreach (var item in client.Get())
            {
                yield return item;
            }
        }       
    }
}
