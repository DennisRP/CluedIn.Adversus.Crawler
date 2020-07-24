using CluedIn.Core;
using CluedIn.Crawling.Adversus.Core;

using ComponentHost;

namespace CluedIn.Crawling.Adversus
{
    [Component(AdversusConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class AdversusCrawlerComponent : CrawlerComponentBase
    {
        public AdversusCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

