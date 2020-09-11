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
    public class ProjectProducer : BaseClueProducer<Project>
    {
        private readonly IClueFactory _factory;

        public ProjectProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Project input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Project, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Name))
                data.Name = input.Name;        

            var vocab = new ProjectVocabulary();

            data.Properties[vocab.Name] = input.Name.PrintIfAvailable();

            if (input.Campaigns != null)
                foreach (var campaignId in input.Campaigns)
                {
                    if (campaignId != default)
                        _factory.CreateOutgoingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, campaignId.ToString());
                }

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
