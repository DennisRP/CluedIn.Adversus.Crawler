using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class Line
    {

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public int UnitPrice { get; set; }
    }

    public class Sale
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("campaignid")]
        public int Campaignid { get; set; }

        [JsonProperty("leadid")]
        public int Leadid { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("lines")]
        public List<Line> Lines { get; set; }
    }


}
