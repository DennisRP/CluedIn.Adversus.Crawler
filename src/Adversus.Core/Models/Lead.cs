using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class LeadList
    {
        public List<Lead> Leads { get; set; }
    }
    public class MasterData
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ResultData
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Lead
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("campaignId")]
        public int? CampaignId { get; set; }

        [JsonProperty("contactAttempts")]
        public int? ContactAttempts { get; set; }

        [JsonProperty("lastModifiedTime")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonProperty("nextContactTime")]
        public DateTime? NextContactTime { get; set; }

        [JsonProperty("lastContactedBy")]
        public int? LastContactedBy { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("masterData")]
        public List<MasterData> MasterData { get; set; }

        [JsonProperty("resultData")]
        public List<ResultData> ResultData { get; set; }
    }


}
