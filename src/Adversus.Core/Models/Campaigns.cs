using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class Campaigns
    {
        [JsonProperty("campaigns")]
        public List<Campaign> CampaignList { get; set; }
    }

    public class Campaign
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
        [JsonProperty("masterFields")]
        public List<MasterField> MasterFields { get; set; }
        [JsonProperty("resultFields")]
        public List<ResultField> ResultFields { get; set; }
    }
    public class Settings
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("visible")]
        public string Visible { get; set; }
        [JsonProperty("record")]
        public string Record { get; set; }
        [JsonProperty("active")]
        public string Active { get; set; }
        [JsonProperty("projectId")]
        public string ProjectId { get; set; }
    }

    public class MasterField
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("editable")]
        public string Editable { get; set; }
        [JsonProperty("active")]
        public string Active { get; set; }
    }

    public class ResultField
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("active")]
        public string Active { get; set; }
        [JsonProperty("options")]
        public List<string> Options { get; set; }
    }

}
