﻿using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class SMSVocabulary : SimpleVocabulary
    {
        public SMSVocabulary()
        {
            VocabularyName = "Adversus SMS"; // TODO: Set value
            KeyPrefix = "adversus.sms"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Sms; // TODO: Set value

            AddGroup("Adversus SMS Details", group =>
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
                Receiver = group.Add(new VocabularyKey("Receiver", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Content = group.Add(new VocabularyKey("Content", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Sender = group.Add(new VocabularyKey("Sender", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Timestamp = group.Add(new VocabularyKey("Timestamp", VocabularyKeyDataType.Time, VocabularyKeyVisibility.Visible));
                Status = group.Add(new VocabularyKey("Status", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Type = group.Add(new VocabularyKey("Type", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Units = group.Add(new VocabularyKey("Units", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
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
        public VocabularyKey Receiver { get; internal set; }
        public VocabularyKey Content { get; internal set; }
        public VocabularyKey Sender { get; internal set; }
        public VocabularyKey Timestamp { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey Type { get; internal set; }
        public VocabularyKey Units { get; internal set; }
    }
}
