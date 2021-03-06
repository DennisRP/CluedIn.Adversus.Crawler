using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Configuration;
using System.Linq;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.Adversus.Core;
using CluedIn.Crawling.Adversus.Infrastructure.Factories;
using CluedIn.Providers.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CluedIn.Provider.Adversus
{
    public class AdversusProvider : ProviderBase, IExtendedProviderMetadata
    {
        private readonly IAdversusClientFactory _adversusClientFactory;

        public AdversusProvider([NotNull] ApplicationContext appContext, IAdversusClientFactory adversusClientFactory)
            : base(appContext, AdversusConstants.CreateProviderMetadata())
        {
            _adversusClientFactory = adversusClientFactory;
        }

        public override async Task<CrawlJobData> GetCrawlJobData(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var adversusCrawlJobData = new AdversusCrawlJobData();
            if (configuration.ContainsKey(AdversusConstants.KeyName.ApiKey))
            { adversusCrawlJobData.ApiKey = configuration[AdversusConstants.KeyName.ApiKey].ToString(); }
            if (configuration.ContainsKey(AdversusConstants.KeyName.Username))
            { adversusCrawlJobData.Username = configuration[AdversusConstants.KeyName.Username].ToString(); }
            if (configuration.ContainsKey(AdversusConstants.KeyName.Password))
            { adversusCrawlJobData.Password = configuration[AdversusConstants.KeyName.Password].ToString(); }

            return await Task.FromResult(adversusCrawlJobData);
        }

        public override Task<bool> TestAuthentication(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            var adversusCrawlJobData = new AdversusCrawlJobData();
            if (configuration.ContainsKey(AdversusConstants.KeyName.ApiKey))
            { adversusCrawlJobData.ApiKey = configuration[AdversusConstants.KeyName.ApiKey].ToString(); }
            if (configuration.ContainsKey(AdversusConstants.KeyName.Username))
            { adversusCrawlJobData.Username = configuration[AdversusConstants.KeyName.Username].ToString(); }
            if (configuration.ContainsKey(AdversusConstants.KeyName.Password))
            { adversusCrawlJobData.Password = configuration[AdversusConstants.KeyName.Password].ToString(); }


            try
            {
                var client = _adversusClientFactory.CreateNew(adversusCrawlJobData);
                var result = client.GetAccountInformation();
                return Task.FromResult(result != null);
            }
            catch (Exception exception)
            {
                context.Log.LogError("Could not add Adversus provider", exception);
                return Task.FromResult(false);
            }
        }

        public override async Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            return await Task.FromResult(default(ExpectedStatistics));
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            [NotNull] CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            var dictionary = new Dictionary<string, object>();

            if (jobData is AdversusCrawlJobData adversusCrawlJobData)
            {
                //TODO add the transformations from specific CrawlJobData object to dictionary
                // add tests to GetHelperConfigurationBehaviour.cs
                dictionary.Add(AdversusConstants.KeyName.Username, adversusCrawlJobData.Username);


                dictionary.Add("webhooks", new List<object>()
                {
                    new { DisplayName = "New Contact", Name = "contact.creation", Status = "ACTIVE", Description = "When a new contact is created."},
                    new { DisplayName = "Deleted Contact", Name = "contact.deletion", Status = "ACTIVE", Description = "When a contact is deleted."},
                    new { DisplayName = "Contact Update", Name = "contact.propertyChange", Status = "ACTIVE", Description = "When a contact is updated."},
                    new { DisplayName = "New Company", Name = "company.creation", Status = "ACTIVE", Description = "When a company is created."},
                    new { DisplayName = "Deleted Company", Name = "company.deletion", Status = "ACTIVE", Description = "When a company is deleted."},
                    new { DisplayName = "Company Update", Name = "company.propertyChange", Status = "ACTIVE", Description = "When a company is updated."},
                    new { DisplayName = "New Deal", Name = "deal.creation", Status = "ACTIVE", Description = "When a deal is created."},
                    new { DisplayName = "Deleted Deal", Name = "deal.deletion", Status = "ACTIVE", Description = "When a deal is deleted"},
                    new { DisplayName = "Deal Update", Name = "deal.propertyChange", Status = "ACTIVE", Description = "When a deal is updated."}
                });

            }

            return await Task.FromResult(dictionary);
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId,
            string folderId)
        {
            return await GetHelperConfiguration(context, jobData, organizationId, userId, providerDefinitionId);
        }

        public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            if (!(jobData is AdversusCrawlJobData adversusCrawlJobData))
            {
                throw new Exception("Wrong CrawlJobData type");
            }
     
            try
            {
                var client = _adversusClientFactory.CreateNew(adversusCrawlJobData);
                var result = client.GetAccountInformation();
                if (result != null)
                {
                  
                    return new AccountInformation(result.AccountId, result.AccountDisplay);
                }
                return new AccountInformation(string.Empty, string.Empty) { Errors = new Dictionary<string, string>() { { "error", "Please contact CluedIn support in the top menu to help you setup with Adversus." } } };

            }
            catch (Exception e)
            {
                context.Log.LogError("There was an error getting HubSpot account information {message}", e);
                return new AccountInformation(string.Empty, string.Empty) { Errors = new Dictionary<string, string>() { { "error", "Please contact CluedIn support in the top menu to help you setup with Adversus." }, { "exception", e.Message } } };
            }
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            return webHooksEnabled && ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
                : $"{relativeDateTime.Minute} 0/4 * * *";
        }

        public override async Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
        {
            await Task.Run(() =>
            {
                var adversusCrawlJobData = (AdversusCrawlJobData)jobData;
                var webhookSignatures = new List<WebHookSignature>();
                try
                {
                    var client = _adversusClientFactory.CreateNew(adversusCrawlJobData);

                    var data = client.GetWebhooks(adversusCrawlJobData.Username, adversusCrawlJobData.Password);

                    if (data == null)
                        return webhookSignatures;

                    var hookTypes = new[] { "lead_saved", "call_ended", "callAnswered", "leadClosedSuccess", "leadClosedAutomaticRedial", "leadClosedPrivateRedial", "leadClosedNotInterested", "leadClosedInvalid", "leadClosedUnqualified", "leadClosedSystem", "leads_deactivated", "leads_inserted", "mail_activity", "sms_sent", "sms_received", "appointment_added", "appointment_updated" };
                    webhookDefinition.Uri = new Uri(this.appContext.System.Configuration.WebhookReturnUrl.Trim('/') /*+ ConfigurationManagerEx.AppSettings["Providers.HubSpot.WebhookEndpoint"]*/);

                    foreach (var subscription in hookTypes)
                    {
                        if (config.ContainsKey("webhooks"))
                        {
                            //var enabledHooks = (List<WebhookEventType>)config["webhooks"];
                            //var enabled = enabledHooks.Where(s => s.Status == "ACTIVE").Select(s => s.Name);
                            //if (!enabled.Contains(subscription))
                            //{
                            //    continue;
                            //}
                        }

                        try
                        {
                            var result = client.CreateWebhooks(webhookDefinition.Uri, subscription);
                            webhookSignatures.Add(new WebHookSignature { Signature = webhookDefinition.ProviderDefinitionId.ToString(), ExternalVersion = "v1", ExternalId = null, EventTypes = "lead_saved,call_ended,callAnswered,leadClosedSuccess,leadClosedAutomaticRedial,leadClosedPrivateRedial,leadClosedNotInterested,leadClosedInvalid,leadClosedUnqualified,leadClosedSystem,leads_deactivated,leads_inserted,mail_activity,sms_sent,sms_received,appointment_added,appointment_updated" });
                        }
                        catch (Exception exception)
                        {
                            context.Log.LogWarning("Could not create HubSpot Webhook for subscription", exception);
                            return new List<WebHookSignature>();
                        }
                    }


                    webhookDefinition.Verified = true;
                }
                catch (Exception exception)
                {
                    context.Log.LogWarning("Could not create Adversus Webhook", exception);
                    return new List<WebHookSignature>();
                }

                var organizationProviderDataStore = context.Organization.DataStores.GetDataStore<ProviderDefinition>();
                if (organizationProviderDataStore != null)
                {
                    if (webhookDefinition.ProviderDefinitionId != null)
                    {
                        var webhookEnabled = organizationProviderDataStore.GetById(context, webhookDefinition.ProviderDefinitionId.Value);
                        if (webhookEnabled != null)
                        {
                            webhookEnabled.WebHooks = true;
                            organizationProviderDataStore.Update(context, webhookEnabled);
                        }
                    }
                }

                return webhookSignatures;
            });

            return new List<WebHookSignature>();
        }

        public override async Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
        {
            var webhookDefinitionDataStore = context.Organization.DataStores.GetDataStore<WebhookDefinition>();
            return await webhookDefinitionDataStore.SelectAsync(context, s => s.Verified != null && s.Verified.Value);
        }

        public override async Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));

            await Task.Run(() =>
            {
                var webhookDefinitionProviderDataStore = context.Organization.DataStores.GetDataStore<WebhookDefinition>();
                if (webhookDefinitionProviderDataStore != null)
                {
                    var webhook = webhookDefinitionProviderDataStore.GetById(context, webhookDefinition.Id);
                    if (webhook != null)
                    {
                        webhookDefinitionProviderDataStore.Delete(context, webhook);
                    }
                }

                var organizationProviderDataStore = context.Organization.DataStores.GetDataStore<ProviderDefinition>();
                if (organizationProviderDataStore != null)
                {
                    if (webhookDefinition.ProviderDefinitionId != null)
                    {
                        var webhookEnabled = organizationProviderDataStore.GetById(context, webhookDefinition.ProviderDefinitionId.Value);
                        if (webhookEnabled != null)
                        {
                            webhookEnabled.WebHooks = false;
                            organizationProviderDataStore.Update(context, webhookEnabled);
                        }
                    }
                }
            });
        }

        public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
        {
            var endpoints = new List<string> { "https://hooks.cluedin.net/manage/hubspot/hooks" };
            return endpoints;
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));


            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }

        // TODO Please see https://cluedin-io.github.io/CluedIn.Documentation/docs/1-Integration/build-integration.html
        public string Icon => AdversusConstants.IconResourceName;
        public string Domain { get; } = AdversusConstants.Uri;
        public string About { get; } = AdversusConstants.CrawlerDescription;
        public AuthMethods AuthMethods { get; } = AdversusConstants.AuthMethods;
        public IEnumerable<Control> Properties => null;
        public string ServiceType { get; } = JsonConvert.SerializeObject(AdversusConstants.ServiceType);
        public string Aliases { get; } = JsonConvert.SerializeObject(AdversusConstants.Aliases);
        public Guide Guide { get; set; } = new Guide
        {
            Instructions = AdversusConstants.Instructions,
            Value = new List<string> { AdversusConstants.CrawlerDescription },
            Details = AdversusConstants.Details

        };

        public string Details { get; set; } = AdversusConstants.Details;
        public string Category { get; set; } = AdversusConstants.Category;
        public new IntegrationType Type { get; set; } = AdversusConstants.Type;
    }
}
