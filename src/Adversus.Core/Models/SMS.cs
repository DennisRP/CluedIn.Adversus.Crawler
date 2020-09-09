using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class SMS
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("receiver")]
        public string Receiver { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("leadId")]
        public int LeadId { get; set; }

        [JsonProperty("campaignId")]
        public int CampaignId { get; set; }

        [JsonProperty("units")]
        public int Units { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }


}
