using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Adversus.Core;
using CluedIn.Crawling.Adversus.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CluedIn.Crawling.Adversus.Infrastructure
{
    // TODO - This class should act as a client to retrieve the data to be crawled.
    // It should provide the appropriate methods to get the data
    // according to the type of data source (e.g. for AD, GetUsers, GetRoles, etc.)
    // It can receive a IRestClient as a dependency to talk to a RestAPI endpoint.
    // This class should not contain crawling logic (i.e. in which order things are retrieved)
    public class AdversusClient
    {
        private const string BaseUri = "http://sample.com";

        private readonly ILogger log;

        private readonly IRestClient client;

        public AdversusClient(ILogger log, AdversusCrawlJobData adversusCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
        {
            if (adversusCrawlJobData == null)
            {
                throw new ArgumentNullException(nameof(adversusCrawlJobData));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.log = log ?? throw new ArgumentNullException(nameof(log));
            this.client = client ?? throw new ArgumentNullException(nameof(client));

            // TODO use info from adversusCrawlJobData to instantiate the connection
            client.BaseUrl = new Uri(BaseUri);
            client.AddDefaultParameter("api_key", adversusCrawlJobData.ApiKey, ParameterType.QueryString);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var request = new RestRequest(url, Method.GET);

            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var diagnosticMessage = $"Request to {client.BaseUrl}{url} failed, response {response.ErrorMessage} ({response.StatusCode})";
                log.Error(() => diagnosticMessage);
                throw new InvalidOperationException($"Communication to jsonplaceholder unavailable. {diagnosticMessage}");
            }

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

        public AccountInformation GetAccountInformation()
        {
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", ""); 
        }

        public IEnumerable<Campaign> Get(string username, string password)
        {
            //TODO add actual url
            var api = "https://api.adversus.dk/campaigns";
            using (HttpClient httpClient = new HttpClient())
            {
                var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                var auth = string.Format("Basic: {0}", credentials);
                httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                var response = httpClient.GetAsync(api).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    log.Error("401 Unauthorized. Check credentials");
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    log.Error(response.StatusCode.ToString() + " Failed to get data");
                }
                var results = JsonConvert.DeserializeObject<Campaigns>(responseContent);
                foreach (var item in results.campaigns)
                {
                    yield return item;
                }
            }
        }
    }
}
