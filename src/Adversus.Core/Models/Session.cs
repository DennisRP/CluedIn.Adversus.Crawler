using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class SessionList
    {
        public List<Session> Sessions { get; set; }
    }
    public class Cdr
    {

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("answerTime")]
        public DateTime? AnswerTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("durationSeconds")]
        public int DurationSeconds { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }
    }

    public class Session
    {

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("leadId")]
        public int? LeadId { get; set; }

        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("campaignId")]
        public int? CampaignId { get; set; }

        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("sessionSeconds")]
        public int? SessionSeconds { get; set; }

        [JsonProperty("cdr")]
        public Cdr Cdr { get; set; }
    }


}
