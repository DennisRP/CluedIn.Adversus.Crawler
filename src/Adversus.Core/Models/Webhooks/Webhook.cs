using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.Adversus.Core.Models.Webhooks
{

    public class AdversusWebhook
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("template")]
        public JObject Template { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("authKey")]
        public string AuthKey { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }
    }

    public class WebhookResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

    }


}
