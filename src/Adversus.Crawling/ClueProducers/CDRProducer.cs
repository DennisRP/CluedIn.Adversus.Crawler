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
    public class CDRProducer : BaseClueProducer<CDR>
    {
        private readonly IClueFactory _factory;

        public CDRProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] CDR input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Calendar.Event, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.Name = input.Id.ToString();

            var vocab = new CDRVocabulary();   

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.AnswerTime] = input.AnswerTime.PrintIfAvailable();
            data.Properties[vocab.CampaignId] = input.CampaignId.PrintIfAvailable();
            data.Properties[vocab.ConversationSeconds] = input.ConversationSeconds.PrintIfAvailable();
            data.Properties[vocab.Destination] = input.Destination.PrintIfAvailable();
            data.Properties[vocab.Disposition] = input.Disposition.PrintIfAvailable();
            data.Properties[vocab.DurationSeconds] = input.DurationSeconds.PrintIfAvailable();
            data.Properties[vocab.EndTime] = input.EndTime.PrintIfAvailable();
            data.Properties[vocab.LeadId] = input.LeadId.PrintIfAvailable();
            data.Properties[vocab.Recording] = input.Links?.Recording.PrintIfAvailable();
            data.Properties[vocab.SessionId] = input.SessionId.PrintIfAvailable();
            data.Properties[vocab.StartTime] = input.StartTime.PrintIfAvailable();
            data.Properties[vocab.UserId] = input.UserId.PrintIfAvailable();

            if (input.LeadId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Sales.Lead, EntityEdgeType.PartOf, input, input.LeadId.ToString());

            if (input.UserId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.PartOf, input, input.UserId.ToString());

            if (!string.IsNullOrWhiteSpace(input.SessionId))
                _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.PartOf, input, input.SessionId);

            if (!string.IsNullOrWhiteSpace(input.CampaignId))
                _factory.CreateOutgoingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, input.CampaignId);

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
