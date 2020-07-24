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
    public class CampaignProducer : BaseClueProducer<Campaign>
    {
        private readonly IClueFactory _factory;

        public CampaignProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Campaign input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Unknown, input.id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.id))
            {
                data.Name = input.id.ToString();
            }
            
            var vocab = new CampaignVocabulary();

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            data.Properties[vocab.id] = input.id.PrintIfAvailable();

            return clue;
        }
    }
}
