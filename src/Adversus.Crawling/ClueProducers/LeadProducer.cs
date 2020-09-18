using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Adversus.Core.Models;
using CluedIn.Crawling.Adversus.Vocabularies;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;

namespace CluedIn.Crawling.Adversus.ClueProducers
{
    public class LeadProducer : BaseClueProducer<Lead>
    {
        private readonly IClueFactory _factory;

        public LeadProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Lead input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Sales.Lead, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.Name = input.Id.ToString();

            var vocab = new LeadVocabulary();

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.CampaignId] = input.CampaignId.PrintIfAvailable();
            data.Properties[vocab.ContactAttempts] = input.ContactAttempts.PrintIfAvailable();
            data.Properties[vocab.ExternalId] = input.ExternalId.PrintIfAvailable();
            data.Properties[vocab.LastContactedBy] = input.LastContactedBy.PrintIfAvailable();
            data.Properties[vocab.LastModifiedTime] = input.LastModifiedTime.PrintIfAvailable();

            if (input.LastModifiedTime != null)
                data.ModifiedDate = input.LastModifiedTime;

            data.Properties[vocab.NextContactTime] = input.NextContactTime.PrintIfAvailable();
            data.Properties[vocab.ResultData] = input.ResultData.PrintIfAvailable();
            data.Properties[vocab.Status] = input.Status.PrintIfAvailable();

            foreach (var masterData in input.MasterData)
            {
                if (masterData.Label.Equals("Fornavn"))
                    data.Properties[vocab.FirstName] = masterData.Value;
                else if (masterData.Label.Equals("Mobil"))
                    data.Properties[vocab.FirstName] = masterData.Value;
                else if (masterData.Label.Equals("Email"))
                    data.Properties[vocab.Email] = masterData.Value;
                else if (masterData.Label.Equals("Stilling"))
                    data.Properties[vocab.JobTitle] = masterData.Value;
                else if (masterData.Label.Equals("Oprettelsesdato"))
                    data.Properties[vocab.CreatedDate] = masterData.Value;
                else if (masterData.Label.Equals("CPR"))
                    data.Properties[vocab.SocialSecurityNumber] = masterData.Value;
                else if (masterData.Label.Equals("By"))
                    data.Properties[vocab.City] = masterData.Value;
                else if (masterData.Label.Equals("Adresse"))
                    data.Properties[vocab.Address] = masterData.Value;
                else if (masterData.Label.Equals("Note"))
                    data.Properties[vocab.Note] = masterData.Value;
                else if (masterData.Label.Equals("Firma"))
                    data.Properties[vocab.Company] = masterData.Value;
                else if (masterData.Label.Equals("Branche"))
                    data.Properties[vocab.BusinessType] = masterData.Value;
                else
                    data.Properties[$"{masterData.Label}-dynamic"] = masterData.Value;
            }

            if (!string.IsNullOrWhiteSpace(input.Status))
                data.Tags.Add(new Tag(input.Status));

            if (input.CampaignId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, input.CampaignId.ToString());

            if (!string.IsNullOrWhiteSpace(input.ExternalId))
                data.Codes.Add(new EntityCode(EntityType.Infrastructure.Contact, CodeOrigin.CluedIn, input.ExternalId));

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
