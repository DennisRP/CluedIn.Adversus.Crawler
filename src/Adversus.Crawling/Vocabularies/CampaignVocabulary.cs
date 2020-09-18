using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class CampaignVocabulary : SimpleVocabulary
    {
        public CampaignVocabulary()
        {
            VocabularyName = "Adversus Campaign"; // TODO: Set value
            KeyPrefix = "adversus.campaign"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Marketing.Campaign; // TODO: Set value

            AddGroup("Adversus Campaign Details", group =>
            {
                Id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });
        }
        public VocabularyKey Id { get; internal set; }
        public VocabularyKey Name { get; internal set; }
        public VocabularyKey Visible { get; internal set; }
        public VocabularyKey Active { get; internal set; }
        public VocabularyKey Record { get; internal set; }
        public VocabularyKey ProjectId { get; internal set; }
    }
}
