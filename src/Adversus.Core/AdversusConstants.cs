using System;
using System.Collections.Generic;
using CluedIn.Core.Net.Mail;
using CluedIn.Core.Providers;

namespace CluedIn.Crawling.Adversus.Core
{
    public class AdversusConstants
    {
        public struct KeyName
        {
            public const string ApiKey = nameof(ApiKey);
            public const string Username = nameof(Username);
            public const string Password = nameof(Password);
        }

        public const string CrawlerDescription = "We help call centers boost KPIs, make better decisions based on insights, and manage contacts more wisely.";
        public const string Instructions = "Provide basic authentication via username and password";
        public const IntegrationType Type = IntegrationType.Cloud;
        public const string Uri = "https://www.adversus.io/"; //Uri of remote tool if applicable

        // To change the icon see embedded resource
        // src\Adversus.Provider\Resources\cluedin.png
        public const string IconResourceName = "Resources.adversus.png";

        public static IList<string> ServiceType = new List<string> { "" };
        public static IList<string> Aliases = new List<string> { "" };
        public const string Category = "CRM";
        public const string Details = "";
        public static AuthMethods AuthMethods = new AuthMethods()
        {
            token = new Control[]
            {
        // You can define controls to show in the GUI in order to authenticate with this integration
        //        new Control()
        //        {
        //            displayName = "API key",
        //            isRequired = false,
        //            name = "api",
        //            type = "text"
        //        }
            }
        };


        public const bool SupportsConfiguration = false;
        public const bool SupportsWebHooks = true;
        public const bool SupportsAutomaticWebhookCreation = true;

        public const bool RequiresAppInstall = false;
        public const string AppInstallUrl = null;
        public const string ReAuthEndpoint = null;

        #region Autogenerated constants
        public const string CodeOrigin = "Adversus";
        public const string ProviderRootCodeValue = "Adversus";
        public const string CrawlerName = "AdversusCrawler";
        public const string CrawlerComponentName = "AdversusCrawler";
        public static readonly Guid ProviderId = Guid.Parse("3987fd4d-f763-4e8a-9985-1565e86862de");
        public const string ProviderName = "Adversus";

        


        public static readonly ComponentEmailDetails ComponentEmailDetails = new ComponentEmailDetails
        {
            Features = new Dictionary<string, string>
            {
                                       { "Calls",        "Improve outbound calls today" },
                                       { "Intelligence",    "Adversus is a web-based dialer and practical CRM solution for telemarketing, fundraising, and appointment scheduling businesses." }
                                   },
            Icon = ProviderIconFactory.CreateUri(ProviderId),
            ProviderName = ProviderName,
            ProviderId = ProviderId,
            Webhooks = SupportsWebHooks
        };

        public static IProviderMetadata CreateProviderMetadata()
        {
            return new ProviderMetadata
            {
                Id = ProviderId,
                ComponentName = CrawlerName,
                Name = ProviderName,
                Type = Type.ToString(),
                SupportsConfiguration = SupportsConfiguration,
                SupportsWebHooks = SupportsWebHooks,
                SupportsAutomaticWebhookCreation = SupportsAutomaticWebhookCreation,
                RequiresAppInstall = RequiresAppInstall,
                AppInstallUrl = AppInstallUrl,
                ReAuthEndpoint = ReAuthEndpoint,
                ComponentEmailDetails = ComponentEmailDetails
            };
        }
        #endregion
    }
}
