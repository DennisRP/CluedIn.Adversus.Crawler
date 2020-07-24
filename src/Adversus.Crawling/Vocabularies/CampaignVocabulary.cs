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
            Grouping = EntityType.Unknown; // TODO: Set value

            AddGroup("Adversus Campaign Details", group =>
            {
                id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });
        }
        public VocabularyKey id { get; internal set; }
    }
}
