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
    public class SessionProducer : BaseClueProducer<Session>
    {
        private readonly IClueFactory _factory;

        public SessionProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Session input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Activity, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Id.ToString()))
                data.Name = input.Id.ToString();        

            var vocab = new SessionVocabulary();

            data.Properties[vocab.CampaignId] = input.CampaignId.PrintIfAvailable();
            data.Properties[vocab.Cdr] = input.Cdr.PrintIfAvailable();
            data.Properties[vocab.EndTime] = input.EndTime.PrintIfAvailable();
            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.LeadId] = input.LeadId.PrintIfAvailable();
            data.Properties[vocab.SessionSeconds] = input.SessionSeconds.PrintIfAvailable();
            data.Properties[vocab.StartTime] = input.StartTime.PrintIfAvailable();
            data.Properties[vocab.Status] = input.Status.PrintIfAvailable();
            data.Properties[vocab.UserId] = input.UserId.PrintIfAvailable();

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
