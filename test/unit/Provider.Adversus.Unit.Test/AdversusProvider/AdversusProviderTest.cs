using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Adversus.Infrastructure.Factories;
using Moq;

namespace CluedIn.Provider.Adversus.Unit.Test.AdversusProvider
{
    public abstract class AdversusProviderTest
    {
        protected readonly ProviderBase Sut;

        protected Mock<IAdversusClientFactory> NameClientFactory;
        protected Mock<IWindsorContainer> Container;

        protected AdversusProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            NameClientFactory = new Mock<IAdversusClientFactory>();
            var applicationContext = new ApplicationContext(Container.Object);
            Sut = new Adversus.AdversusProvider(applicationContext, NameClientFactory.Object);
        }
    }
}
