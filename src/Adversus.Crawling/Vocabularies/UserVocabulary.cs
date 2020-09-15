using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class UserVocabulary : SimpleVocabulary
    {
        public UserVocabulary()
        {
            VocabularyName = "Adversus User"; // TODO: Set value
            KeyPrefix = "adversus.user"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.PhoneCall; // TODO: Set value

            AddGroup("Adversus User Details", group =>
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
                Locale = group.Add(new VocabularyKey("Locale", VocabularyKeyDataType.GeographyLocation, VocabularyKeyVisibility.Visible));
                Name = group.Add(new VocabularyKey("Name", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Phone = group.Add(new VocabularyKey("Phone", VocabularyKeyDataType.PhoneNumber, VocabularyKeyVisibility.Visible));
                Email = group.Add(new VocabularyKey("Email", VocabularyKeyDataType.Email, VocabularyKeyVisibility.Visible));
                Admin = group.Add(new VocabularyKey("Admin", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Active = group.Add(new VocabularyKey("Active", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
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
        public VocabularyKey Locale { get; internal set; }
        public VocabularyKey Name { get; internal set; }
        public VocabularyKey Phone { get; internal set; }
        public VocabularyKey Email { get; internal set; }
        public VocabularyKey Admin { get; internal set; }
        public VocabularyKey Active { get; internal set; }
    }
}
