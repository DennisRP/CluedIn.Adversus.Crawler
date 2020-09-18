using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class LeadVocabulary : SimpleVocabulary
    {
        public LeadVocabulary()
        {
            VocabularyName = "Adversus Lead"; // TODO: Set value
            KeyPrefix = "adversus.lead"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Sales.Lead; // TODO: Set value

            AddGroup("Adversus Lead Details", group =>
            {
                Id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                AnswerTime = group.Add(new VocabularyKey("AnswerTime", VocabularyKeyDataType.Time, VocabularyKeyVisibility.Visible));
                CampaignId = group.Add(new VocabularyKey("CampaignId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                ConversationSeconds = group.Add(new VocabularyKey("ConversationSeconds", VocabularyKeyDataType.Duration, VocabularyKeyVisibility.Visible));
                Destination = group.Add(new VocabularyKey("Destination", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Disposition = group.Add(new VocabularyKey("Disposition", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                DurationSeconds = group.Add(new VocabularyKey("DurationSeconds", VocabularyKeyDataType.Duration, VocabularyKeyVisibility.Visible));
                EndTime = group.Add(new VocabularyKey("EndTime", VocabularyKeyDataType.Time, VocabularyKeyVisibility.Visible));
                LeadId = group.Add(new VocabularyKey("LeadId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                Recording = group.Add(new VocabularyKey("Recording", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                SessionId = group.Add(new VocabularyKey("SessionId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                StartTime = group.Add(new VocabularyKey("StartTime", VocabularyKeyDataType.Time, VocabularyKeyVisibility.Visible));
                UserId = group.Add(new VocabularyKey("UserId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                ContactAttempts = group.Add(new VocabularyKey("ContactAttempts", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                ExternalId = group.Add(new VocabularyKey("ExternalId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                LastContactedBy = group.Add(new VocabularyKey("LastContactedBy", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                LastModifiedTime = group.Add(new VocabularyKey("LastModifiedTime", VocabularyKeyDataType.Time, VocabularyKeyVisibility.Visible));
                MasterData = group.Add(new VocabularyKey("MasterData", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                NextContactTime = group.Add(new VocabularyKey("NextContactTime", VocabularyKeyDataType.Time, VocabularyKeyVisibility.Visible));
                ResultData = group.Add(new VocabularyKey("ResultData", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Status = group.Add(new VocabularyKey("Status", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });
        }

        public VocabularyKey Id { get; internal set; }
        public VocabularyKey AnswerTime { get; internal set; }
        public VocabularyKey CampaignId { get; internal set; }
        public VocabularyKey ConversationSeconds { get; internal set; }
        public VocabularyKey Destination { get; internal set; }
        public VocabularyKey Disposition { get; internal set; }
        public VocabularyKey DurationSeconds { get; internal set; }
        public VocabularyKey EndTime { get; internal set; }
        public VocabularyKey LeadId { get; internal set; }
        public VocabularyKey Recording { get; internal set; }
        public VocabularyKey SessionId { get; internal set; }
        public VocabularyKey StartTime { get; internal set; }
        public VocabularyKey UserId { get; internal set; }
        public VocabularyKey ContactAttempts { get; internal set; }
        public VocabularyKey ExternalId { get; internal set; }
        public VocabularyKey LastContactedBy { get; internal set; }
        public VocabularyKey LastModifiedTime { get; internal set; }
        public VocabularyKey MasterData { get; internal set; }
        public VocabularyKey NextContactTime { get; internal set; }
        public VocabularyKey ResultData { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey FirstName { get; internal set; }
        public VocabularyKey PhoneNumber { get; internal set; }
        public VocabularyKey Email { get; internal set; }
        public VocabularyKey JobTitle { get; internal set; }
        public VocabularyKey CreatedDate { get; internal set; }
        public VocabularyKey SocialSecurityNumber { get; internal set; }
        public VocabularyKey City { get; internal set; }
        public VocabularyKey Address { get; internal set; }
        public VocabularyKey Note { get; internal set; }
        public VocabularyKey Company { get; internal set; }
        public VocabularyKey BusinessType { get; internal set; }
    }
}
