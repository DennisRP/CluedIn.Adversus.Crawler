using System.IO;
using System.Reflection;
using CluedIn.Crawling.Adversus.Core;
using CrawlerIntegrationTesting.Clues;
using CrawlerIntegrationTesting.Log;
using Xunit.Abstractions;
using DebugCrawlerHost = CrawlerIntegrationTesting.CrawlerHost.DebugCrawlerHost<CluedIn.Crawling.Adversus.Core.AdversusCrawlJobData>;

namespace CluedIn.Crawling.Adversus.Integration.Test
{
    public class AdversusTestFixture
    {
        public ClueStorage ClueStorage { get; }
        private readonly DebugCrawlerHost debugCrawlerHost;

        public TestLogger Log { get; }
        public AdversusTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;
            debugCrawlerHost = new DebugCrawlerHost(executingFolder, AdversusConstants.ProviderName);

            ClueStorage = new ClueStorage();

            Log = debugCrawlerHost.AppContext.Container.Resolve<TestLogger>();

            debugCrawlerHost.ProcessClue += ClueStorage.AddClue;

            debugCrawlerHost.Execute(AdversusConfiguration.Create(), AdversusConstants.ProviderId);
        }

        public void PrintClues(ITestOutputHelper output)
        {
            foreach(var clue in ClueStorage.Clues)
            {
                output.WriteLine(clue.OriginEntityCode.ToString());
            }
        }

        public void PrintLogs(ITestOutputHelper output)
        {
            output.WriteLine(Log.PrintLogs());
        }

        public void Dispose()
        {
        }

    }
}


