using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class CampaignEfficiency
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("totalConversationSeconds")]
        public int TotalConversationSeconds { get; set; }

        [JsonProperty("totalWaitingSeconds")]
        public int TotalWaitingSeconds { get; set; }

        [JsonProperty("totalPauseSeconds")]
        public int TotalPauseSeconds { get; set; }

        [JsonIgnore]
        public string CampaignId { get; set; }
    }
}
