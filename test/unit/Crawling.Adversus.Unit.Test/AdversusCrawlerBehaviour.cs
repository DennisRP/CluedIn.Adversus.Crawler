using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.Adversus;
using CluedIn.Crawling.Adversus.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Adversus.Unit.Test
{
    public class AdversusCrawlerBehaviour
    {
        private readonly ICrawlerDataGenerator _sut;

        public AdversusCrawlerBehaviour()
        {
            var nameClientFactory = new Mock<IAdversusClientFactory>();

            _sut = new AdversusCrawler(nameClientFactory.Object);
        }

        [Fact]
        public void GetDataReturnsData()
        {
            var jobData = new CrawlJobData();

            _sut.GetData(jobData)
                .ShouldNotBeNull();
        }
    }
}
