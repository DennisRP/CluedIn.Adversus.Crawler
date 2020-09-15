using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Xunit;

namespace CluedIn.Provider.Adversus.Unit.Test.AdversusProvider
{
    public static class WebhookManagementEndpoints
    {
        public class FailureCases : AdversusProviderTest
        {
            [Theory(Skip = "No exception")]
            [InlineData(null)]
            public void NullParameterThrowsArgumentNullException(IEnumerable<string> ids)
            {
                Assert.Throws<ArgumentNullException>(() => Sut.WebhookManagementEndpoints(ids));
            }

            [Fact(Skip = "No exception")]
            public void EmptyParameterThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => Sut.WebhookManagementEndpoints(new List<string>()));
            }
        }

        public class PassCases : AdversusProviderTest
        {
            [Theory(Skip = "Method Implemented")]
            [InlineAutoData]
            public void PublicMethodThrowsNotImplementedException(IEnumerable<string> ids)
            {
                Assert.Throws<NotImplementedException>(() => Sut.WebhookManagementEndpoints(ids));
            }
        }
    }
}
