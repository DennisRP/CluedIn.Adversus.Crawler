using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Adversus.Vocabularies
{
    public class ContactVocabulary : SimpleVocabulary
    {
        public ContactVocabulary()
        {
            VocabularyName = "Adversus Contact"; // TODO: Set value
            KeyPrefix = "adversus.contact"; // TODO: Set value
            KeySeparator = ".";
            Grouping = EntityType.Infrastructure.Contact; // TODO: Set value

            AddGroup("Adversus Contact Details", group =>
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
                ExternalId = group.Add(new VocabularyKey("ExternalId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                PoolId = group.Add(new VocabularyKey("PoolId", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                Phone = group.Add(new VocabularyKey("Phone", VocabularyKeyDataType.PhoneNumber, VocabularyKeyVisibility.Visible));
                FirstName = group.Add(new VocabularyKey("FirstName", VocabularyKeyDataType.PersonName, VocabularyKeyVisibility.Visible)); // merge with lastname?
                LastName = group.Add(new VocabularyKey("LastName", VocabularyKeyDataType.PersonName, VocabularyKeyVisibility.Visible));
                Address = group.Add(new VocabularyKey("Address", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                ZipCode = group.Add(new VocabularyKey("ZipCode", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                City = group.Add(new VocabularyKey("City", VocabularyKeyDataType.GeographyCity, VocabularyKeyVisibility.Visible));
                Email = group.Add(new VocabularyKey("Email", VocabularyKeyDataType.Email, VocabularyKeyVisibility.Visible));
                JobTitle = group.Add(new VocabularyKey("JobTitle", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Company = group.Add(new VocabularyKey("Company", VocabularyKeyDataType.OrganizationName, VocabularyKeyVisibility.Visible));
                CVR = group.Add(new VocabularyKey("CVR", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                Source = group.Add(new VocabularyKey("Source", VocabularyKeyDataType.Identifier, VocabularyKeyVisibility.Visible));
                Campaign = group.Add(new VocabularyKey("Campaign", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                UnsuccessfulMessage = group.Add(new VocabularyKey("UnsuccessfulMessage", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });

            AddMapping(Phone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.PhoneNumber);            
            AddMapping(FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.FirstName);
            AddMapping(LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.LastName);
            AddMapping(Address, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.HomeAddress);
            AddMapping(ZipCode, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.HomeAddressZipCode);
            AddMapping(City, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.HomeAddressCity);
            AddMapping(Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email);
            AddMapping(JobTitle, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.JobTitle);
            AddMapping(Company, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName);
            AddMapping(CVR, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCVR);
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
        public VocabularyKey ExternalId { get; internal set; }
        public VocabularyKey PoolId { get; internal set; }
        public VocabularyKey Phone { get; internal set; }
        public VocabularyKey FirstName { get; internal set; }
        public VocabularyKey LastName { get; internal set; }
        public VocabularyKey Address { get; internal set; }
        public VocabularyKey ZipCode { get; internal set; }
        public VocabularyKey City { get; internal set; }
        public VocabularyKey Email { get; internal set; }
        public VocabularyKey JobTitle { get; internal set; }
        public VocabularyKey Company { get; internal set; }
        public VocabularyKey CVR { get; internal set; }
        public VocabularyKey Source { get; internal set; }
        public VocabularyKey Campaign { get; internal set; }
        public VocabularyKey UnsuccessfulMessage { get; internal set; }        
    }
}
