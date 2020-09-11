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
    public class CampaignEfficiencyProducer : BaseClueProducer<CampaignEfficiency>
    {
        private readonly IClueFactory _factory;

        public CampaignEfficiencyProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] CampaignEfficiency input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create("/CampaignEfficiency", string.Format("{0}|{1}|{2}|{3}", input.UserId, input.TotalConversationSeconds, input.TotalPauseSeconds, input.TotalWaitingSeconds), accountId);

            var data = clue.Data.EntityData;

            data.Name = input.UserId.ToString();

            var vocab = new CampaignEfficiencyVocabulary();
       
            data.Properties[vocab.UserId] = input.UserId.PrintIfAvailable();
            data.Properties[vocab.TotalWaitingSeconds] = input.TotalWaitingSeconds.PrintIfAvailable();
            data.Properties[vocab.TotalPauseSeconds] = input.TotalPauseSeconds.PrintIfAvailable();
            data.Properties[vocab.TotalConversationSeconds] = input.TotalConversationSeconds.PrintIfAvailable();

            if (input.UserId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.PartOf, input, input.UserId.ToString());

            if (!string.IsNullOrWhiteSpace(input.CampaignId))
                _factory.CreateOutgoingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, input.CampaignId);

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
