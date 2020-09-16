using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class AppointmentList
    {
        public List<Appointment> Appointments { get; set; }
    }
    public class Appointment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("leadId")]
        public int LeadId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("consultantId")]
        public int ConsultantId { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
