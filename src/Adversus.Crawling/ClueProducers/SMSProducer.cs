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
    public class SMSProducer : BaseClueProducer<SMS>
    {
        private readonly IClueFactory _factory;

        public SMSProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] SMS input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Sms, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Id.ToString()))
                data.Name = input.Id.ToString();        

            var vocab = new SMSVocabulary();
            data.Properties[vocab.CampaignId] = input.CampaignId.PrintIfAvailable();
            data.Properties[vocab.Content] = input.Content.PrintIfAvailable();
            data.Properties[vocab.Receiver] = input.Receiver.PrintIfAvailable();
            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.LeadId] = input.LeadId.PrintIfAvailable();
            data.Properties[vocab.Sender] = input.Sender.PrintIfAvailable();
            data.Properties[vocab.Timestamp] = input.Timestamp.PrintIfAvailable();
            data.Properties[vocab.Status] = input.Status.PrintIfAvailable();
            data.Properties[vocab.UserId] = input.UserId.PrintIfAvailable();
            data.Properties[vocab.Type] = input.Type.PrintIfAvailable();
            data.Properties[vocab.Units] = input.Units.PrintIfAvailable();

            if (input.LeadId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Sales.Lead, EntityEdgeType.PartOf, input, input.LeadId.ToString());

            if (input.CampaignId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, input.CampaignId.ToString());

            if (input.UserId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.PartOf, input, input.UserId.ToString());


            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
