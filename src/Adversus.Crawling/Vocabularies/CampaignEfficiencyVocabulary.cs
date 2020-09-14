using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class CampaignEfficiencyVocabulary : SimpleVocabulary
    {
        public CampaignEfficiencyVocabulary()
        {
            VocabularyName = "Adversus Campaign Efficiency"; // TODO: Set value
            KeyPrefix = "adversus.campaignEfficiency"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Marketing.Campaign; // TODO: Set value

            AddGroup("Adversus Campaign Efficiency Details", group =>
            {
                UserId = group.Add(new VocabularyKey("UserId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                TotalWaitingSeconds = group.Add(new VocabularyKey("TotalWaitingSeconds", VocabularyKeyDataType.Duration, VocabularyKeyVisibility.Visible));
                TotalPauseSeconds = group.Add(new VocabularyKey("TotalPauseSeconds", VocabularyKeyDataType.Duration, VocabularyKeyVisibility.Visible));
                TotalConversationSeconds = group.Add(new VocabularyKey("TotalConversationSeconds", VocabularyKeyDataType.Duration, VocabularyKeyVisibility.Visible));
            });
        }
        public VocabularyKey UserId { get; internal set; }
        public VocabularyKey TotalWaitingSeconds { get; internal set; }
        public VocabularyKey TotalPauseSeconds { get; internal set; }
        public VocabularyKey TotalConversationSeconds { get; internal set; } 
    }
}
