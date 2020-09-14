using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models.Webhooks
{
    public class Template
    {

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("integrationId")]
        public string IntegrationId { get; set; }
    }

    public class WebhookPost
    {

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("authKey")]
        public string AuthKey { get; set; }

        [JsonProperty("template")]
        public Template Template { get; set; }
    }


}
