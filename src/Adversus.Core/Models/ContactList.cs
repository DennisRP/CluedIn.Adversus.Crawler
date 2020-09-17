using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.Adversus.Core.Models
{

    public class Contact
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("poolId")]
        public int PoolId { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        public Dictionary<string, string> MappedData { get; set; }
    }
}
