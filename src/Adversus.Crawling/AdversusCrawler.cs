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

            foreach (var item in client.GetCampaigns(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;

                foreach (var campaignEfficiency in client.GetCampaignEfficiency(item.Id, adversuscrawlJobData.Username, adversuscrawlJobData.Password))
                {
                    campaignEfficiency.CampaignId = item.Id;
                    yield return campaignEfficiency;
                }
            }

            foreach (var item in client.GetProjects(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return client.GetProjectDetails(item.Id, adversuscrawlJobData.Username, adversuscrawlJobData.Password);
            }

            foreach (var item in client.GetContacts(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return client.GetContactDetails(item.Id, adversuscrawlJobData.Username, adversuscrawlJobData.Password);
            }

            foreach (var item in client.GetLeads(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetSessions(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetUsers(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetPools(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetAppointments(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetCDR(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetSales(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetProducts(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }

            foreach (var item in client.GetSMS(adversuscrawlJobData.Username, adversuscrawlJobData.Password))
            {
                yield return item;
            }
        }       
    }
}
