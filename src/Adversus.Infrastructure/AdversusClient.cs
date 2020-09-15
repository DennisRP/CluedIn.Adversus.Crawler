using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Adversus.Core;
using CluedIn.Crawling.Adversus.Core.Models;
using CluedIn.Crawling.Adversus.Core.Models.Webhooks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace CluedIn.Crawling.Adversus.Infrastructure
{
    public class AdversusClient
    {
        private const string BaseUri = "https://api.adversus.dk";

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
            this._adversusCrawlJobData = adversusCrawlJobData;
        }

        public AccountInformation GetAccountInformation()
        {
            var api = "https://api.adversus.dk/organization";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_adversusCrawlJobData.Username + ":" + _adversusCrawlJobData.Password));
                    var auth = string.Format("Basic {0}", credentials);
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
                    var auth = string.Format("Basic {0}", credentials);
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
                    campaigns = results.CampaignList;
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
                    var auth = string.Format("Basic {0}", credentials);
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
                    var auth = string.Format("Basic {0}", credentials);
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

        [CanBeNull]
        public Project GetProjectDetails(int projectId, string username, string password)
        {
            var api = string.Format("https://api.adversus.dk/projects/{0}", projectId);
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic {0}", credentials);
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
                    var results = JsonConvert.DeserializeObject<Project>(responseContent);
                    return results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                    return null;
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
                        var auth = string.Format("Basic {0}", credentials);
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
                        break;
                    }
                    foreach (var item in contacts)
                    {
                        yield return item;
                    }

                    page++;
                }
            }
        }

        [CanBeNull]
        public Contact GetContactDetails(int contactId, string username, string password)
        {
            var api = string.Format("https://api.adversus.dk/contacts/{0}", contactId);
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic {0}", credentials);
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
                    var results = JsonConvert.DeserializeObject<Contact>(responseContent);
                    return results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                    return null;
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
                    var auth = string.Format("Basic {0}", credentials);
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
                    var auth = string.Format("Basic {0}", credentials);
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
                var users = new List<User>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic {0}", credentials);
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
                    users = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in users)
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
                    var auth = string.Format("Basic {0}", credentials);
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
                    var auth = string.Format("Basic {0}", credentials);
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
                        var pagedApi = api + "?page=" + page + "&pageSize=20" + "&filter={startTime : \"" + _adversusCrawlJobData.LastCrawlFinishTime + "\"}";
                        var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                        var auth = string.Format("Basic {0}", credentials);
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
                        break;
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
                    var auth = string.Format("Basic {0}", credentials);
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
                    var auth = string.Format("Basic {0}", credentials);
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
                        var auth = string.Format("Basic {0}", credentials);
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
                        break;
                    }
                    foreach (var item in cdr)
                    {
                        yield return item;
                    }

                    page++;
                }
            }
        }

        public IEnumerable<AdversusWebhook> GetWebhooks(string username, string password)
        {
            var api = "https://api.adversus.dk/webhooks";
            using (HttpClient httpClient = new HttpClient())
            {
                var webhooks = new List<AdversusWebhook>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                    var auth = string.Format("Basic {0}", credentials);
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
                    var results = JsonConvert.DeserializeObject<List<AdversusWebhook>>(responseContent);
                    webhooks = results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }
                foreach (var item in webhooks)
                {
                    yield return item;
                }
            }
        }

        [CanBeNull]
        public WebhookResponse CreateWebhooks(Uri url, string @event)
        {
            var api = "https://api.adversus.dk/webhooks";
            using (HttpClient httpClient = new HttpClient())
            {
                var webhooks = new List<AdversusWebhook>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_adversusCrawlJobData.Username + ":" + _adversusCrawlJobData.Password));
                    var auth = string.Format("Basic {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var json = JsonConvert.SerializeObject(new WebhookPost() { AuthKey = credentials, Event = @event, Url = url.ToString(), Template = new Template() { Event = @event, IntegrationId = this.GetAccountInformation().AccountId } });
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync(api, data).Result;
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
                    var results = JsonConvert.DeserializeObject<WebhookResponse>(responseContent);
                    return results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }

                return null;

            }
        }

        [CanBeNull]
        public WebhookResponse DeleteWebhooks(WebHookSignature webhook)
        {
            var api = "https://api.adversus.dk/webhooks/" + webhook.ExternalId;
            using (HttpClient httpClient = new HttpClient())
            {
                var webhooks = new List<AdversusWebhook>();
                try
                {
                    var credentials = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_adversusCrawlJobData.Username + ":" + _adversusCrawlJobData.Password));
                    var auth = string.Format("Basic {0}", credentials);
                    httpClient.DefaultRequestHeaders.Add("Authorization", auth);
                    var response = httpClient.DeleteAsync(api).Result;
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
                    var results = JsonConvert.DeserializeObject<WebhookResponse>(responseContent);
                    return results;
                }
                catch (Exception exception)
                {
                    log.LogError("Call to Adversus API Failed", exception);
                }

                return null;

            }
        }
    }
}
