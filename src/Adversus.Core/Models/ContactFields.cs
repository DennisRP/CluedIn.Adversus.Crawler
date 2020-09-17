using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class ContactFields
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
