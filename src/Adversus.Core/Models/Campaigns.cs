using System.Collections.Generic;

namespace CluedIn.Crawling.Adversus.Core.Models
{
    public class Campaigns
    {
        public IList<Campaign> campaigns { get; set; }
    }

    public class Campaign
    {
        public string id { get; set; }
        public Settings settings { get; set; }
        public IList<MasterField> masterFields { get; set; }
        public IList<ResultField> resultFields { get; set; }
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
        public IList<string> options { get; set; }
    }

}
