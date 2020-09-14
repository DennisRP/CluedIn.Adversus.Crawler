using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class AppointmentVocabulary : SimpleVocabulary
    {
        public AppointmentVocabulary()
        {
            VocabularyName = "Adversus Appointment"; // TODO: Set value
            KeyPrefix = "adversus.appointment"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Calendar.Event; // TODO: Set value

            AddGroup("Adversus Appointment Details", group =>
            {
                Id = group.Add(new VocabularyKey("Id", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                ConsultantId = group.Add(new VocabularyKey("ConsultantId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                End = group.Add(new VocabularyKey("End", VocabularyKeyDataType.DateTime, VocabularyKeyVisibility.Visible));
                LeadId = group.Add(new VocabularyKey("LeadId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                Start = group.Add(new VocabularyKey("Start", VocabularyKeyDataType.DateTime, VocabularyKeyVisibility.Visible));
                Status = group.Add(new VocabularyKey("Status", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });
        }

        public VocabularyKey ConsultantId { get; internal set; }
        public VocabularyKey End { get; internal set; }
        public VocabularyKey Id { get; internal set; }
        public VocabularyKey LeadId { get; internal set; }
        public VocabularyKey Start { get; internal set; }
        public VocabularyKey Status { get; internal set; }
    }
}
