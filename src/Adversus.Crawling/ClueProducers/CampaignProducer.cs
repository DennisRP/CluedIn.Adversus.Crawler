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

            var clue = _factory.Create(EntityType.Marketing.Campaign, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrEmpty(input?.Settings.Name))
            {
                data.Name = input.Settings.Name;
            }

            var vocab = new CampaignVocabulary();

            if (input.Settings != null)
            {
                data.Properties[vocab.Visible] = input.Settings.Visible;
                data.Properties[vocab.Active] = input.Settings.Active;
                data.Properties[vocab.Record] = input.Settings.Record;
                data.Properties[vocab.ProjectId] = input.Settings.ProjectId;
            }

            if (!string.IsNullOrEmpty(input.Settings?.ProjectId))
                _factory.CreateOutgoingEntityReference(clue, EntityType.Project, EntityEdgeType.PartOf, input, input.Settings.ProjectId);

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();

            return clue;
        }
    }
}
