using System.Collections.Generic;
using CluedIn.Crawling.Adversus.Core;

namespace CluedIn.Crawling.Adversus.Integration.Test
{
  public static class AdversusConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { AdversusConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
