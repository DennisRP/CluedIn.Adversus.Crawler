using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Adversus.Core;
using CluedIn.Crawling.Adversus.Core.Models;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<AdversusClient> log;

        private readonly IRestClient client;

        private AdversusCrawlJobData _adversusCrawlJobData;

        public AdversusClient(ILogger<AdversusClient> log, AdversusCrawlJobData adversusCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
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
            this._adversusCrawlJobData = adversusCrawlJobData;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var request = new RestRequest(url, Method.GET);

            var response = await client.ExecuteTaskAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var diagnosticMessage = $"Request to {client.BaseUrl}{url} failed, response {response.ErrorMessage} ({response.StatusCode})";
                log.LogError(diagnosticMessage);
                throw new InvalidOperationException($"Communication to jsonplaceholder unavailable. {diagnosticMessage}");
            }

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

        public AccountInformation GetAccountInformation()
        {
            var api = "https://api.adversus.dk/organization";
            using (HttpClient httpClient = new HttpClient())
            {
              
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_adversusCrawlJobData.Username + ":" + _adversusCrawlJobData.Password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    
                    var results = JsonConvert.DeserializeObject<Organization>(responseContent);
                    if (results != null)
                        return new AccountInformation(results.Id, results.Name);
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
               
            }
            //TODO - return some unique information about the remote data source
            // that uniquely identifies the account
            return new AccountInformation("", "");
        }

        public IEnumerable<Campaign> GetCampaigns(string username, string password)
        {
            var api = "https://api.adversus.dk/campaigns";
            using (HttpClient httpClient = new HttpClient())
            {
                var campaigns = new List<Campaign>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<Campaigns>(responseContent);
                    campaigns = results.campaigns;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in campaigns)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<CampaignEfficiency> GetCampaignEfficiency(string campaignId, string username, string password)
        {
            var api = string.Format("https://api.adversus.dk/campaigns/{0}/efficiency", campaignId);
            using (HttpClient httpClient = new HttpClient())
            {
                var campaigns = new List<CampaignEfficiency>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<CampaignEfficiency>>(responseContent);
                    campaigns = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in campaigns)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Project> GetProjects(string username, string password)
        {
            var api = "https://api.adversus.dk/projects";
            using (HttpClient httpClient = new HttpClient())
            {
                var projects = new List<Project>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Project>>(responseContent);
                    projects = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in projects)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Project> GetProjectDetails(int projectId, string username, string password)
        {
            var api = string.Format("https://api.adversus.dk/projects/{0}", projectId);
            using (HttpClient httpClient = new HttpClient())
            {
                var projects = new List<Project>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Project>>(responseContent);
                    projects = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in projects)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<ContactList> GetContacts(string username, string password)
        {
            var api = "https://api.adversus.dk/contacts";
            using (HttpClient httpClient = new HttpClient())
            {
               
                int page = 0;
                while (true)
                {
                    var contacts = new List<ContactList>();

                    try
                    {
                        var pagedApi = api + "?page=" + page + "&pageSize=20";
                        var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                        var auth = string.Format("Basic: {0}", credentials);
                        httpClient.DefaultRequestHeaders.Add("Authorization", auth);
      
                        var response = httpClient.GetAsync(pagedApi).Result;
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            log.LogError("401 Unauthorized. Check credentials");
                        }
                        else if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            log.LogError("500 Internal Server Error.");
                        }
                        else if (response.StatusCode != HttpStatusCode.OK)
                        {
                            log.LogError(response.StatusCode.ToString() + " Failed to get data");
                        }
                        var results = JsonConvert.DeserializeObject<List<ContactList>>(responseContent);
                        if (results == null)
                            break;
                        if (!results.Any())
                            break;

                        contacts = results;
                    }
                    catch (Exception exception)
                    {
                        log.LogError("Call to Adversus API Failed", exception);
                    }
                    foreach (var item in contacts)
                    {
                        yield return item;
                    }

                    page++;
                }
            }
        }

        public IEnumerable<Contact> GetContactDetails(int contactId, string username, string password)
        {
            var api = string.Format("https://api.adversus.dk/contacts/{0}", contactId);
            using (HttpClient httpClient = new HttpClient())
            {
                var projects = new List<Contact>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Contact>>(responseContent);
                    projects = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in projects)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Lead> GetLeads(string username, string password)
        {
            var api = "https://api.adversus.dk/leads";
            using (HttpClient httpClient = new HttpClient())
            {
                var projects = new List<Lead>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Lead>>(responseContent);
                    projects = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in projects)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Session> GetSessions(string username, string password)
        {
            var api = "https://api.adversus.dk/sessions";
            using (HttpClient httpClient = new HttpClient())
            {
                var sessions = new List<Session>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Session>>(responseContent);
                    sessions = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in sessions)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<User> GetUsers(string username, string password)
        {
            var api = "https://api.adversus.dk/users";
            using (HttpClient httpClient = new HttpClient())
            {
                var sessions = new List<User>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<User>>(responseContent);
                    sessions = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in sessions)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Pool> GetPools(string username, string password)
        {
            var api = "https://api.adversus.dk/pools";
            using (HttpClient httpClient = new HttpClient())
            {
                var sessions = new List<Pool>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Pool>>(responseContent);
                    sessions = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in sessions)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Appointment> GetAppointments(string username, string password)
        {
            var api = "https://api.adversus.dk/appointments";
            using (HttpClient httpClient = new HttpClient())
            {
                var sessions = new List<Appointment>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Appointment>>(responseContent);
                    sessions = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in sessions)
                {
                    yield return item;
                }
            }
        }


        public IEnumerable<CDR> GetCDR(string username, string password)
        {
            var api = "https://api.adversus.dk/cdr";
            using (HttpClient httpClient = new HttpClient())
            {
     
                int page = 0;
                while (true)
                {
                    var cdr = new List<CDR>();

                    try
                    {
                        var pagedApi = api + "?page=" + page + "&pageSize=20" + "&filter={startTime : \""+ _adversusCrawlJobData.LastCrawlFinishTime + "\"}";
                        var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                        var auth = string.Format("Basic: {0}", credentials);
                        httpClient.DefaultRequestHeaders.Add("Authorization", auth);

                        var response = httpClient.GetAsync(pagedApi).Result;
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            log.LogError("401 Unauthorized. Check credentials");
                        }
                        else if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            log.LogError("500 Internal Server Error.");
                        }
                        else if (response.StatusCode != HttpStatusCode.OK)
                        {
                            log.LogError(response.StatusCode.ToString() + " Failed to get data");
                        }
                        var results = JsonConvert.DeserializeObject<List<CDR>>(responseContent);
                        if (results == null)
                            break;
                        if (!results.Any())
                            break;

                        cdr = results;
                    }
                    catch (Exception exception)
                    {
                        log.LogError("Call to Adversus API Failed", exception);
                    }
                    foreach (var item in cdr)
                    {
                        yield return item;
                    }

                    page++;
                }
            }
        }

        public IEnumerable<Sale> GetSales(string username, string password)
        {
            var api = "https://api.adversus.dk/sales";
            using (HttpClient httpClient = new HttpClient())
            {
                var sessions = new List<Sale>();
                try
                {
                    //Need to figure out syntax for lastModifiedDate and applying multiple filters.
                    var pagedApi = api + "?filter={created: \"" + _adversusCrawlJobData.LastCrawlFinishTime + "\"}";

                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Sale>>(responseContent);
                    sessions = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in sessions)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Product> GetProducts(string username, string password)
        {
            var api = "https://api.adversus.dk/products";
            using (HttpClient httpClient = new HttpClient())
            {
                var sessions = new List<Product>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic: {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.GetAsync(api).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.LogError("401 Unauthorized. Check credentials");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        log.LogError("500 Internal Server Error.");
                    }
                    else if (response.StatusCode != HttpStatusCode.OK)
                    {
                        log.LogError(response.StatusCode.ToString() + " Failed to get data");
                    }
                    var results = JsonConvert.DeserializeObject<List<Product>>(responseContent);
                    sessions = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in sessions)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<SMS> GetSMS(string username, string password)
        {
            var api = "https://api.adversus.dk/sms";
            using (HttpClient httpClient = new HttpClient())
            {
                int page = 0;
                while (true)
                {
                    var cdr = new List<SMS>();

                    try
                    {
                        var pagedApi = api + "?page=" + page + "&pageSize=20" + "&filter={timestamp : \"" + _adversusCrawlJobData.LastCrawlFinishTime + "\"}";
                        var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                        var auth = string.Format("Basic: {0}", credentials);
                        httpClient.DefaultRequestHeaders.Add("Authorization", auth);

                        var response = httpClient.GetAsync(pagedApi).Result;
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            log.LogError("401 Unauthorized. Check credentials");
                        }
                        else if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            log.LogError("500 Internal Server Error.");
                        }
                        else if (response.StatusCode != HttpStatusCode.OK)
                        {
                            log.LogError(response.StatusCode.ToString() + " Failed to get data");
                        }
                        var results = JsonConvert.DeserializeObject<List<SMS>>(responseContent);
                        if (results == null)
                            break;
                        if (!results.Any())
                            break;

                        cdr = results;
                    }
                    catch (Exception exception)
                    {
                        log.LogError("Call to Adversus API Failed", exception);
                    }
                    foreach (var item in cdr)
                    {
                        yield return item;
                    }

                    page++;
                }
            }
        }
    }
}
