using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class CDRList
    {
        [JsonProperty("cdr")]
        public List<CDR> CDRs { get; set; }
    }
    public class Links
    {
        [JsonProperty("recording")]
        public string Recording { get; set; }
    }

    public class CDR
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("campaignId")]
        public string CampaignId { get; set; }

        [JsonProperty("leadId")]
        public string LeadId { get; set; }

        [JsonProperty("durationSeconds")]
        public string DurationSeconds { get; set; }

        [JsonProperty("conversationSeconds")]
        public string ConversationSeconds { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }

        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("answerTime")]
        public DateTime? AnswerTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("links")]
        public List<Links> Links { get; set; }
    }
}
