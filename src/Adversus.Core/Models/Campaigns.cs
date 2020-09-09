using System.Collections.Generic;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class Campaigns
    {
        public List<Campaign> campaigns { get; set; }
    }

    public class Campaign
    {
        public string id { get; set; }
        public Settings settings { get; set; }
        public List<MasterField> masterFields { get; set; }
        public List<ResultField> resultFields { get; set; }
    }
    public class Settings
    {
        public string name { get; set; }
        public string visible { get; set; }
        public string record { get; set; }
        public string active { get; set; }
        public string projectId { get; set; }
    }

    public class MasterField
    {
        public string id { get; set; }
        public string type { get; set; }
        public string editable { get; set; }
        public string active { get; set; }
    }

    public class ResultField
    {
        public string id { get; set; }
        public string type { get; set; }
        public string active { get; set; }
        public List<string> options { get; set; }
    }

}
